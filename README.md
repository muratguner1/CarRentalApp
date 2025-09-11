# 🚗 CarRentalApp

CarRentalApp is a **Car Rental Management System** built with **ASP.NET Core Web API**.  
It follows a **Clean Architecture / Layered Architecture** pattern with four main layers:
- **API** (Presentation)
- **Application**
- **Domain**
- **Infrastructure**

The project implements **JWT-based Authentication & Authorization** to secure endpoints and uses **Role-based & Resource-based Authorization** (`Admin`, `Customer`).

---

## 📌 Features

- **User Management**
  - Register and Login with JWT Authentication
  - Role-based access (`Admin`, `Customer`)
  - Resource-based restrictions (users can only update their own profile)

- **Car Management**
  - Admins can **add, update, delete** cars
  - Customers can **view cars**
  - Filtering cars by brand, model, year, availability, price

- **Rental Management**
  - Customers can **rent cars** if available
  - Cars automatically become unavailable once rented
  - Cars can be **returned**, calculating total price = `days rented × daily price`

---

## ⚙️ Technologies Used

- **.NET 8** (ASP.NET Core Web API)
- **Entity Framework Core** (Code-First, Migrations)
- **AutoMapper** (DTO ↔ Entity mapping)
- **JWT Authentication**
- **Dependency Injection**
- **PostgreSQL** (default database, configurable)
- **Rate Limiting** (e.g. user can send maximum 5 requests in 10 seconds)
- **Async Programming**
- **Postman** (for testing)

---
## Author

- **Murat Güner**


