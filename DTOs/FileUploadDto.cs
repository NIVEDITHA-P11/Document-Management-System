using System.ComponentModel.DataAnnotations;

namespace DocumentManagementSystem.DTOs
{
    public class FileUploadDto
    {
        [Required]
        public IFormFile File { get; set; }
    }
}
