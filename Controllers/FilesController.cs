using DocumentManagementSystem.Data;
using DocumentManagementSystem.DTOs;
using DocumentManagementSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace DocumentManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FilesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FilesController(AppDbContext context)
        {
            _context = context;
        }

        private int GetUserId() =>
            int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] FileUploadDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (dto.File == null || dto.File.Length == 0)
                return BadRequest("No file uploaded.");

            var userId = GetUserId();
            var fileName = dto.File.FileName;

            var existing = await _context.Documents
                .Where(d => d.UserId == userId && d.FileName == fileName)
                .OrderByDescending(d => d.Revision)
                .ToListAsync();

            var revision = existing.Any() ? existing.First().Revision + 1 : 0;

            using var memoryStream = new MemoryStream();
            await dto.File.CopyToAsync(memoryStream);

            var doc = new Document
            {
                UserId = userId,
                FileName = fileName,
                Revision = revision,
                Content = memoryStream.ToArray(),
                UploadedAt = DateTime.UtcNow
            };

            _context.Documents.Add(doc);
            await _context.SaveChangesAsync();

            return Ok(new { message = "File uploaded", revision });
        }
        
        [HttpGet("files/{fileName}")]
        public async Task<IActionResult> GetFile(string fileName, [FromQuery] int? revision)
        {
            var userId = GetUserId();
            var baseFileName = Path.GetFileNameWithoutExtension(fileName);

            // Fetch all user's documents with this base name
            var userDocs = await _context.Documents
                .Where(d => d.UserId == userId)
                .ToListAsync();

            // Match on base name (ignoring extension)
            var matchingDocs = userDocs
                .Where(d => Path.GetFileNameWithoutExtension(d.FileName)
                             .Equals(baseFileName, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!matchingDocs.Any())
                return NotFound("File not found.");

            Document doc;

            if (revision.HasValue)
            {
                // Get specific revision
                doc = matchingDocs
                    .FirstOrDefault(d => d.Revision == revision.Value);
            }
            else
            {
                // Get latest revision
                doc = matchingDocs
                    .OrderByDescending(d => d.Revision)
                    .FirstOrDefault();
            }

            if (doc == null)
                return NotFound($"File with revision {revision} not found.");

            return File(doc.Content, "application/octet-stream", doc.FileName);
        }



        [HttpGet("{fileName}/revision/{rev}")]
        public async Task<IActionResult> GetRevision(string fileName, int rev)
        {
            var userId = GetUserId();

            var doc = await _context.Documents
                .FirstOrDefaultAsync(d =>
                    d.UserId == userId &&
                    d.FileName == fileName &&
                    d.Revision == rev);

            if (doc == null)
                return NotFound("Revision not found.");

            return File(doc.Content, "application/octet-stream", fileName);
        }
    }
}
