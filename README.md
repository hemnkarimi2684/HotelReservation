<div align="center">

# 🏨 Hotel Reservation System

A **layered-architecture hotel reservation system** built in **C# / .NET 9**.

Guests can browse available rooms, reserve a spot, and cancel bookings — while admins manage the room inventory. All data is persisted to **JSON** files using a generic repository pattern.

</div>

---

## ✨ Features

| Area | Details |
|------|---------|
| 🔐 **Authentication** | Register / Login · Session-based auth |
| 🧑‍🤝‍🧑 **Roles** | `Admin` and `User` with role-based access |
| 🔒 **Security** | Strong password policy (8+ chars, upper, lower, digit, symbol) |
| 🛎️ **User Panel** | View available rooms · Reserve · Auto price calculation · Cancel own reservation |
| ⚙️ **Admin Panel** | Add / update / remove rooms · View all rooms, reservations & users |
| 🗄️ **Data Layer** | Generic repository · JSON persistence · Soft delete via `BaseEntity` |
| 🏗️ **Architecture** | Clean layering (Domain / Infrastructure / Business / Presentation) · Rich domain model · Custom exceptions · Global error handling |

---

## 🛠 Tech Stack

`C#` `🔸 .NET 9` `🏗️ Layered Architecture` `🗂️ Generic Repository` `🧬 Rich Domain Model`
`⚠️ Custom Exceptions` `🗑️ Soft Delete` `📄 JSON Persistence` `🔐 Session-Based Authentication`
`🔒 Strong Password Policy` `✅ Domain Validation` `🔧 Extension Methods` `🧑‍🤝‍🧑 Role-Based Access`
`🎨 Colorful Logging` `🌍 Global Exception Handling`

---

## 📁 Project Structure

```
HotelReservation/
├── HotelReservation.Presentation/     # Console UI · Menus · Logging      (startup)
├── HotelReservation.Business/         # Business logic & services
├── HotelReservation.Domain/           # Entities · Enums · Password policy
├── HotelReservation.Infrustracture/   # Generic & specific repositories
├── HotelReservation.PathOfData/       # Data file paths
├── HotelReservation.Exceptions/       # Custom exception classes
├── HotelReservation.Extensions/       # Extension methods
└── HotelReservation.slnx
```

---

## 💻 Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)

### Run

```bash
dotnet run --project HotelReservation.Presentation
```

On first run, the app seeds a default admin account and sample rooms. Data is stored as `.json` files in a `Data/` folder under the output directory.

---

## 🔑 Default Admin Credentials

> **Username:** `hemen2684`
>
> **Password:** `Hemen@2684`

> ⚠️ Change these in `UserService.SeedUsers()` before any real deployment.

---

## 📄 License

This project is for learning purposes.
