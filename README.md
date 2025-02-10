# Task Manager API

## Overview

The **Task Manager API** is a robust, secure, and scalable backend application built using **ASP.NET Core 8**, following **Clean Architecture** principles. It provides a comprehensive solution for managing tasks, users, and roles, with support for **JWT-based authentication** and **role-based authorization**. The application is designed to be modular, maintainable, and extensible, making it suitable for both small and large-scale projects.

---

## Features

- **User Management**:
  - Register new users with roles (Admin, Supervisor, Worker).
  - Authenticate users using JWT tokens.
  - Role-based access control (RBAC) for secure operations.

- **Task Management**:
  - Create, update, and delete tasks.
  - Assign tasks to users.
  - Filter tasks by user, status, or deadline.

- **Authentication & Authorization**:
  - Secure JWT-based authentication.
  - Role-based authorization for endpoints.
  - Password hashing using **BCrypt**.

- **CQRS Pattern**:
  - Separation of commands (write operations) and queries (read operations).
  - Improved scalability and maintainability.

- **Validation**:
  - Input validation using **FluentValidation**.
  - Custom validation filters for global error handling.

- **Rate Limiting**:
  - Protect endpoints from abuse using rate limiting.
  - Configurable limits for authentication endpoints.

- **Swagger Documentation**:
  - Interactive API documentation with Swagger UI.
  - JWT authentication support in Swagger.

---

## Technologies Used

- **Backend Framework**: ASP.NET Core 8
- **Database**: SQL Server with Entity Framework Core
- **Authentication**: JWT (JSON Web Tokens)
- **Password Hashing**: BCrypt.Net-Next
- **Validation**: FluentValidation
- **CQRS**: MediatR
- **Object Mapping**: AutoMapper
- **Logging**: Built-in ASP.NET Core logging
- **API Documentation**: Swagger/OpenAPI
- **Rate Limiting**: ASP.NET Core Rate Limiting

---

## API Endpoints

### Authentication
- **POST /api/auth/register**: Register a new user.
- **POST /api/auth/login**: Authenticate and receive a JWT token.

### Users
- **GET /api/users**: Get all users (Admin only).
- **GET /api/users/{id}**: Get a user by ID (Admin only).
- **POST /api/users**: Create a new user (Admin only).
- **PUT /api/users/{id}**: Update a user (Admin only).
- **DELETE /api/users/{id}**: Delete a user (Admin only).

### Tasks
- **GET /api/tasks/all**: Get all tasks
- **GET /api/tasks/user-tasks**: Get all tasks for the logged-in user.
- **GET /api/tasks/{id}**: Get a task by ID.
- **POST /api/tasks**: Create a new task.
- **PUT /api/tasks/{id}**: Update a task.
- **DELETE /api/tasks/{id}**: Delete a task.

---

## Project Structure

The project follows **Clean Architecture** with the following layers:

1. **Domain**:
   - Contains core business logic, entities, and enums.
   - Example: `User`, `Task`, `ApplicationRole`.

2. **Application**:
   - Contains application-specific logic, such as CQRS commands, queries, and validators.
   - Example: `RegisterCommand`, `GetTasksByUserQuery`.

3. **Infrastructure**:
   - Contains implementations of interfaces defined in the Application layer.
   - Example: `JwtTokenService`, `PasswordHasher`.

4. **Persistence**:
   - Contains database context, migrations, and repository implementations.
   - Example: `ApplicationDbContext`, `UserRepository`.

5. **API**:
   - Contains controllers, middleware, and startup configuration.
   - Example: `AuthController`, `TasksController`.

---

## Environment Variables

The following environment variables are required:

- `ASPNETCORE_ENVIRONMENT`: Set to `Development` or `Production`.
- `ConnectionStrings__SqlServer`: SQL Server connection string.
- `JwtSettings__Secret`: Secret key for JWT token generation.
- `JwtSettings__Issuer`: JWT token issuer.
- `JwtSettings__Audience`: JWT token audience.
- `JwtSettings__ExpiryMinutes`: JWT token expiration time in minutes.

---

## Rate Limiting

The API includes rate limiting for authentication endpoints to prevent abuse. The default configuration allows **5 requests per minute** for authentication-related endpoints.

---

## Error Handling

The API uses a global exception handler to manage errors gracefully. Common error responses include:

- **400 Bad Request**: Invalid input data.
- **401 Unauthorized**: Missing or invalid JWT token.
- **403 Forbidden**: Insufficient permissions.
- **404 Not Found**: Resource not found.
- **500 Internal Server Error**: Server-side error.
