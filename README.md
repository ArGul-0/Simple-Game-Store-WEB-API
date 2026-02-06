# Simple Game Store WEB-API

**Simple Game Store** is a compact, production-minded **Web API** built with **C# / ASP.NET Core (.NET 10)** and **PostgreSQL**.  
The repository is designed for realistic deployments using **immutable Docker images** and `docker-compose` (dev / prod). This `README` is concise and focused — only the important facts and commands you need.

---

## Table of contents
- [What it is](#what-it-is)  
- [Tech stack & features](#tech-stack--features)  
- [Quick start — local (fast)](#quick-start---local-fast)  
- [Build, tag & test image (local)](#build-tag--test-image-local)  
- [Production (minimal)](#production-minimal)  
- [Migrations (short)](#migrations-short)  
- [Environment variables (`.env.example`)](#environment-variables-envexample)  
- [Health & graceful restart (brief)](#health--graceful-restart-brief)  
- [Troubleshooting (essentials)](#troubleshooting-essentials)  
- [Best practices (short)](#best-practices-short)  
- [License](#license)

---

## What it is
**Simple Game Store** exposes a RESTful backend for managing games and genres. It is intended as a small but realistic example of how to build, containerize and deploy a modern .NET WebAPI.

**Primary goals:** *clarity*, *repeatable deploys*, *reasonable production defaults*.

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

## Quick start — local (fast)

**Requirements:** Docker (Engine + Compose). Optional: .NET 10 SDK for local debugging.

1. Start dev stack (Postgres + API):
```bash
# from repo root (where docker-compose.dev.yaml is located)
docker compose -f docker-compose.dev.yaml up -d
docker compose -f docker-compose.dev.yaml logs -f api
