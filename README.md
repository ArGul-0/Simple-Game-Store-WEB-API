# Simple Game Store WEB-API

**Simple Game Store** is a compact, production-minded **Web API** built with **C# / ASP.NET Core (.NET 10)** and **PostgreSQL**.  
The repository is designed for realistic deployments using **immutable Docker images** and `docker-compose` (dev / prod). This `README` is concise and focused — only the important facts and commands you need.

---

## Table of contents
- [What it is](#what-it-is)  
- [Tech stack & features](#tech-stack--features)  
- [Quick start — Production (recommended)](#quick-start--production-recommended)
- [Quick start — Local for development and debugging](#quick-start--local-for-development-and-debugging)  
- [Health & graceful restart (brief)](#health--graceful-restart-brief)  
- [Troubleshooting (essentials)](#troubleshooting--essentials)  
- [License](LICENSE.md)

---

## What it is
**Simple Game Store** exposes a RESTful backend for managing games and genres. It is intended as a small but realistic example of how to build, containerize and deploy a modern .NET Web API.

**Primary goals:** *clarity*, *repeatable deploys*, *reasonable production defaults*.

**What the API exposes (short):**

- RESTful CRUD for Games (Create / Read / Update / Delete).
- Read-only list for Genres (seeded).
- /health — readiness of Web API.

User-friendly Web UI interface for interacting with Web APIs (using Swagger).
(Endpoints follow conventional REST routes: GET/POST/PUT/DELETE /Games, GET /Genres.)

---

## Tech stack & features
- **Framework:** ASP.NET Core (.NET 10)  
- **Database:** PostgreSQL  
- **ORM:** Entity Framework Core (migrations included)  
- **Containerization:** Docker (multi-stage Dockerfile)  
- **Orchestration:** Docker Compose (dev & prod files)  
- **API:** RESTful CRUD for `Games`, read-only `Genres`  
- **Operational:** `/health` endpoint, Docker healthchecks, multi-stage build for small runtime image

---

## Quick start — Production (recommended)
**Requirements:** Docker (Engine + Compose).

1. Download the [**latest version**](https://github.com/ArGul-0/Simple-Game-Store-WEB-API/releases/latest)  
   or browse [**all releases**](https://github.com/ArGul-0/Simple-Game-Store-WEB-API/releases).

2. Place files on server (e.g. /opt/simple-game-store or any other folder / directory on any OS, Linux it's just example):
docker-compose.prod.yaml

3. Pull & Run:
```bash
cd /opt/simple-game-store
docker compose -f docker-compose.prod.yaml pull
docker compose -f docker-compose.prod.yaml up -d
```

Visit ```http://localhost:5000/``` to see Swagger UI and try Web API.
That’s it — this workflow uses an immutable image from your registry. Avoid --build on production.

---

## Quick start — local for development and debugging
**Requirements:** Docker (Engine + Compose), .NET 10 SDK for local debugging and running code (Linux commands it's just example, you can do this on any other OS).

1. Copy repository (or just download .ZIP file):
```bash
git clone https://github.com/ArGul-0/Simple-Game-Store-WEB-API.git
cd Simple-Game-Store-WEB-API/"Simple Game Store WEB-API" # Simple Game Store WEB-API directory it's like /src, source code
```
2. Start dev stack (Postgres):
```bash
# from repo root (where docker-compose.dev.yaml is located)
docker compose -f docker-compose.dev.yaml up -d
docker compose -f docker-compose.dev.yaml logs -f api 
```
3. Run with SDK (for debugging / IDE):
```bash
dotnet restore
dotnet run
# open Swagger / OpenAPI at the URL printed in logs
```

**Useful commands:**
To stop and remove Docker container (dev):
```bash
docker compose -f docker-compose.dev.yaml down -v
```

---

# Health & graceful restart (brief)
App exposes /health  — used by Docker healthchecks.

---

# Troubleshooting — essentials
- **no configuration file provided: not found** → pass -f docker-compose.prod.yaml or rename file to docker-compose.yaml.
- **could not connect to server** → inspect Postgres logs (docker compose logs -f postgres) and run pg_isready. Ensure the API ConnectionStrings__DefaultConnection uses host postgres (the service name).

After pushing a new image tag, run docker compose pull before up -d on server.

**Useful commands:**
```bash
docker compose -f docker-compose.prod.yaml ps
docker compose -f docker-compose.prod.yaml logs -f api
```
