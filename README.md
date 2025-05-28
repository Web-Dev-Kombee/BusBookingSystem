# 🚎 Bus Booking System - ASP.NET Core Web App

**BusBookingSystem** is a robust and scalable ASP.NET Core 8.0 web application that allows users to search, book, and manage bus tickets online. It follows a clean architecture (CQRS + layered structure) and uses Entity Framework Core with the Code First approach.

## 🧾 Project Structure

```
BusBookingSystem/
│
├── BusBookingSystem.Domain/         # Domain Models and Interfaces
├── BusBookingSystem.Application/    # Application Layer with CQRS handlers
├── BusBookingSystem.Infrastructure/ # EF Core + Repositories + DB Context
├── BusBookingSystem.Api/            # RESTful API Controllers
├── BusBookingSystem.Web/            # Razor Pages or MVC Views
```

## ✨ Features

✅ User Registration and Login  
✅ Search Buses by source, destination, date  
✅ Book and cancel tickets  
✅ Admin panel for managing buses and routes  
✅ Clean UI with Bootstrap 5  
✅ Validations on both client and server side  
✅ Centralized error handling and logging  
✅ REST API for mobile or external access  
✅ Follows Clean Architecture with CQRS pattern

## 🔐 Authentication & Authorization

Uses ASP.NET Core Identity or custom token-based/session authentication for login and route protection.

## 👨‍💻 Technologies Used

- ASP.NET Core 8.0 (MVC/Web API)
- Entity Framework Core (Code First)
- SQL Server (configurable)
- CQRS Pattern
- Bootstrap 5
- JavaScript (form validations)
- AutoMapper, MediatR (optional based on your architecture)

## 🌠 Getting Started

1. **Clone the repo:**
   ```bash
   git clone https://github.com/your-username/BusBookingSystem.git
   ```

2. **Update `appsettings.json` with:**
   - Your SQL Server connection string
   - Email credentials (if notifications enabled)

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=.;Database=BusBookingDB;Trusted_Connection=True;"
   }
   ```

3. **Run database migrations:**
   ```powershell
   Add-Migration InitialCreate
   Update-Database
   ```

4. **Run the app:**
   ```bash
   dotnet run
   ```

## 🔑 Validations

- Client-side JS validations for fields like date, seats, etc.
- Server-side Data Annotations
- Custom exception handling for business rules

## 🎴 UI Pages (Optional Screenshots Section)

- Booking Page:
  ![image](https://github.com/user-attachments/assets/d78a1056-55b6-4bde-bfe8-7c50840586b1)

- Seat Availabilty: 
  ![image](https://github.com/user-attachments/assets/091e48f7-d580-4421-95dc-5de44c5392d7)

-Confirm Bus ticket with QR:
  ![image](https://github.com/user-attachments/assets/e3b0707a-36a4-452b-99fd-67b11522c457)

- QR Scan Output:
   ![image](https://github.com/user-attachments/assets/074af8f0-2059-4fa8-95aa-3f2c835b7b18)

- Admin Dashboard:
  ![image](https://github.com/user-attachments/assets/93bfcd20-77bf-490f-990c-52288fea98a6)


---

## 🤝 **Contributing**

We welcome contributions! Follow these steps to contribute:

1. Fork the repository.
2. Create a new branch for your feature/fix.
3. Commit changes and open a **Pull Request**.

---
   
## 🗝 License

This project is licensed under the [MIT License](LICENSE).

---
## 👨‍💻 **Author**

**Kombee Technologies**

- 🌐 [Portfolio](https://github.com/kombee-technologies)
- 💼 [LinkedIn](https://in.linkedin.com/company/kombee-global)
- 🌍 [Website](https://www.kombee.com/)

---

<p align="center">
  Built with ❤️ using .NET
</p>

---





