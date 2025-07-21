# SensitiveWordsAPI

This is a RESTful microservice built in ASP.NET Core that receives user messages, sanitizes sensitive SQL keywords, and returns the masked version.

---

## 🚀 Features

- RESTful API built with ASP.NET Core
- Dapper-based MSSQL database integration
- Swagger/OpenAPI documentation
- CRUD endpoints for managing sensitive words
- xUnit unit tests
- GitHub Actions CI for build & test automation

---

## ⚡ Performance Enhancements

To improve response time and scalability:

### ✅ In-Memory Caching
- Cache sensitive word list to avoid repeated DB calls
- Refresh periodically or on change

### ✅ Compiled Regex
- Use compiled or cached regular expressions for repeated use
- Reduces CPU overhead per request

### ✅ Async I/O
- All DB and message processing operations are asynchronous
- Prevents thread blocking

### ✅ Batching/Streaming (Future)
- Efficient bulk inserts or batch message handling
- Useful if sanitizing logs or large documents

---

## 🌟 Additional Enhancements

To make this project production-ready and feature-complete:

### 🔒 Security
- Add rate limiting to prevent abuse
- Input validation and sanitization
- API key or OAuth2-based authentication

### 📊 Monitoring & Observability
- Integrate Serilog or Seq for structured logging
- Add `/health` endpoint using ASP.NET Health Checks

### 🧪 Testing & CI
- Unit tests via xUnit
- GitHub Actions workflow to run tests on push/PR
- Optional integration tests with TestContainers

### 🧰 Developer Tools
- Admin Web UI to manage sensitive words
- Post-deployment data seeding support
- Prebuilt SQL Server Database Project

### 🐳 Docker Support (optional)
- Dockerfile for API
- docker-compose with SQL Server image for local dev

---

## 🔧 Project Structure

```
SensitiveWordsAPI/
├── Controllers/            # API Endpoints
├── Models/                 # Request/Response models
├── Services/               # Business logic and DB operations
├── Data/                   # SQL scripts and connection config
├── Tests/                  # Unit tests with xUnit
├── .github/workflows/      # GitHub Actions CI workflow
├── appsettings.json        # Config file
└── README.md               # You're here
```

---

## 📥 Run Locally

1. Ensure SQL Server or LocalDB is running
2. Run `CreateAndSeedSensitiveWords.sql`
3. Update `appsettings.json` with correct connection string
4. Build and run the API:

```bash
dotnet restore
dotnet build
dotnet run
```

5. Test endpoints via Swagger: `https://localhost:{port}/swagger`

---

## 📬 API Example

**Request**:
```json
{
  "message": "You should CREATE a table"
}
```

**Response**:
```json
{
  "sanitizedMessage": "You should ****** a *****"
}
```

---

## 🤝 Contributions

Feel free to fork, contribute, or raise issues!

---

## 📝 License

MIT License