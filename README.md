# ProjecTile

Project and task management web application — my **bachelor's diploma project** (Uzhhorod National University, 2024).

## Features
- User registration and JWT authentication (ASP.NET Core Identity)
- Projects with members, tasks assigned to users, comments on tasks
- Projects, tasks and comments are stored in **Redis**; users in SQLite

## Architecture
Clean Architecture split into three .NET projects plus an Angular client:
| Project | Role |
|---|---|
| `Core` | Entities (`Project`, `ProjectTask`, `Comment`, `UserProjects`, `UserTasks`) and repository interfaces |
| `Infrastructure` | Repository implementations on Redis, EF Core Identity store (SQLite), seed data |
| `API` | ASP.NET Core Web API controllers, authentication, DTOs |
| `client` | Angular front end |

## Running
Requires the .NET 8 SDK, Docker and Node.js.
```
docker compose up -d                 # Redis on :6379, Redis Commander on :8081
dotnet run --project API             # https://localhost:7071
cd client && npm install && npx ng serve
```

> Note: the `client` folder is currently committed as an empty Git link, so the Angular source is not visible on GitHub yet.

## Tech stack
C#, .NET 8, ASP.NET Core Web API, ASP.NET Core Identity + JWT, EF Core (SQLite), Redis (StackExchange.Redis), Docker Compose, Angular, TypeScript.
