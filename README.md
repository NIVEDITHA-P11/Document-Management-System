# 📁 Document Management System (Secure File Storage)

A secure document storage backend system built with ASP.NET Core.  
Supports user authentication, file uploads, versioned document access, and private file control.

---

- ChatGPT: Used for architectural guidance, code snippets, README writing, and HTML+JS UI generation.

##Demo Video Url- https://drive.google.com/drive/folders/1omGctAQFs2w0D4SPeqC9QmJwxjFoWQmu?usp=sharing
## ✅ Features

- 🔐 JWT Authentication (Register & Login)
- 📂 File Upload with Versioning
- 🔁 Retrieve specific revisions
- 🔒 Private access — users can only access their own files
- 💾 File storage as BLOBs in the database
- 🧪  Unit tested with xUnit & EF In-Memory
- ☁️ Pushed to GitHub with clean history
- 📃 Swagger API documentation

---
✅ 3 unit test cases succeeded, covering:

✔️ Uploading a file and storing correct metadata

✔️ Retrieving the latest file version

✔️ Blocking unauthorized access to another user's file


## 🚀 Technologies Used

- ASP.NET Core Web API
- Entity Framework Core
- JWT Authentication
- SQL Server
- xUnit for testing


## 🔧 Setup Instructions

1. **Clone the repository**

```bash
git clone https://github.com/NIVEDITHA-P11/Document-Management-System.git
cd Document-Management-System

2.  **Build and Run API**

-dotnet restore
-dotnet build
-dotnet run

3. Access Swagger UI
Open your browser and go to:
http://localhost:<port>/swagger

(Replace <port> with the one shown in terminal, e.g., http://localhost:5000/swagger)

---
