"# E-Medic - Healthcare Management Platform

[![Live Demo](https://img.shields.io/badge/Live%20Demo-https%3A%2F%2Fe--medic.onrender.com-blue?style=for-the-badge)](https://e-medic.onrender.com/)
[![License](https://img.shields.io/badge/License-MIT-green?style=for-the-badge)](LICENSE)
[![.NET Version](https://img.shields.io/badge/.NET-10.0-purple?style=for-the-badge)](https://dotnet.microsoft.com/en-us/download/dotnet/10.0)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-10.0-blue?style=for-the-badge)](https://www.postgresql.org/)

---

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Technology Stack](#technology-stack)
- [Quick Start](#quick-start)
- [Project Structure](#project-structure)
- [Architecture](#architecture)
- [Database Schema](#database-schema)
- [API Documentation](#api-documentation)
- [Authentication & Authorization](#authentication--authorization)
- [Configuration](#configuration)
- [Deployment](#deployment)
- [Demo Credentials](#demo-credentials)
- [Troubleshooting](#troubleshooting)
- [Contributing](#contributing)

---

## 🏥 Overview

**E-Medic** is a comprehensive, full-stack healthcare management platform built with ASP.NET Core that enables seamless digital interactions between patients and doctors. The platform provides appointment scheduling, medical record management, prescription generation, and administrative oversight capabilities.

### Key Highlights
- 🔐 Secure authentication with role-based access control
- 📅 Intelligent appointment scheduling system
- 📋 Comprehensive medical record management
- 💊 Digital prescription generation with PDF export
- ☁️ Cloud-based image storage via Cloudinary
- 📧 Email notifications system
- 🐳 Production-ready Docker containerization
- 📱 Responsive user interface for all roles

---

## ✨ Features

### 👥 Patient Features
- **Browse Doctors**: Search and filter approved doctors by specialty and availability
- **Book Appointments**: Schedule consultations with problem descriptions
- **Track Status**: Monitor appointment approval and completion
- **Medical History**: Access personal medical records and prescription history
- **Download Records**: Export medical documents as PDF
- **Profile Management**: Update personal information and profile picture
- **Ratings & Reviews**: View doctor ratings and feedback

### 👨‍⚕️ Doctor Features
- **Profile Setup**: Complete professional profiles with qualifications and specialty
- **Availability Management**: Set consultation fees and working hours
- **Patient Queue**: View and manage daily appointment requests
- **Appointment Management**: Approve and manage consultations
- **Medical Records**: Create and maintain patient medical records
- **Prescriptions**: Generate digital prescriptions with recommendations
- **Dashboard Analytics**: Track total earnings, completion rates, and ratings
- **Consultation Notes**: Add detailed notes to each appointment

### 🛡️ Admin Features
- **Doctor Approval**: Review and approve pending doctor registrations
- **System Oversight**: Access comprehensive medical history across all patients
- **User Management**: Manage all platform users and roles
- **Analytics Dashboard**: View system-wide statistics and insights
- **Data Management**: Oversee database integrity and system performance

### 🔒 Security & Compliance
- **Role-Based Access Control**: Three-tier authorization (Patient, Doctor, Admin)
- **Secure Authentication**: ASP.NET Identity with UUID v7
- **Password Policy**: Strong password requirements (8+ chars, uppercase, lowercase, digits)
- **Email Verification**: Account verification workflow
- **Admin Approval Gate**: Doctors require admin approval before access
- **HTTPS Enforcement**: Secure data transmission
- **Data Protection**: ASP.NET Core Data Protection API with file-based key storage

---

## 🛠️ Technology Stack

### Backend Framework
- **Runtime**: .NET 10.0 (LTS)
- **Web Framework**: ASP.NET Core MVC
- **Language**: C#

### Database & ORM
- **Database**: PostgreSQL 10.0
- **ORM**: Entity Framework Core 10.0
- **Migrations**: Code-First approach with automated migrations

### Authentication & Authorization
- **Identity**: ASP.NET Core Identity
- **User ID**: UUID v7 (Sortable GUID)
- **Authorization**: Claims-based and Role-based

### External Services & Libraries
| Service | Library | Version | Purpose |
|---------|---------|---------|---------|
| **Image Storage** | Cloudinary | 1.29.1 | Cloud-based image hosting & optimization |
| **Email Service** | MailKit | 4.17.0 | SMTP-based email delivery |
| **PDF Generation** | DinkToPdf | 1.0.8 | HTML to PDF conversion |
| **Validation** | FluentValidation | 12.1.1 | Data validation rules |
| **AI Integration** | Microsoft Semantic Kernel | 1.77.0 | Future AI/ML capabilities |

### DevOps & Deployment
- **Containerization**: Docker (Multi-stage build)
- **Hosting**: Render.com
- **Data Protection**: File-based key storage

---

## 🚀 Quick Start

### Prerequisites
- .NET 10.0 SDK or Runtime
- PostgreSQL 10.0 or higher
- Docker (optional, for containerized deployment)
- Node.js (optional, for frontend build tools)

### Local Development Setup

#### 1. Clone the Repository
```bash
git clone https://github.com/yourusername/E-Medic.git
cd E-Medic
```

#### 2. Configure Environment
Create or update `appsettings.Development.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=5432;Database=emedic_db;User Id=postgres;Password=your_password;"
  },
  "CloudinarySettings": {
    "CloudName": "your_cloudinary_name",
    "ApiKey": "your_api_key",
    "ApiSecret": "your_api_secret"
  },
  "SmtpSettings": {
    "Server": "smtp.gmail.com",
    "Port": 587,
    "Username": "your_email@gmail.com",
    "Password": "your_app_password",
    "SenderName": "E-Medic",
    "SenderEmail": "noreply@emedic.com"
  }
}
```

#### 3. Initialize Database
```bash
cd E-Medic
dotnet ef database update
```

#### 4. Run the Application
```bash
dotnet run
```

The application will be available at `https://localhost:5001`

### Docker Deployment

#### Build Docker Image
```bash
docker build -t emedic:latest .
```

#### Run Docker Container
```bash
docker run -d \
  -p 8080:8080 \
  -p 8081:8081 \
  -e ConnectionStrings__DefaultConnection="your_connection_string" \
  --name emedic-app \
  emedic:latest
```

---

## 📁 Project Structure

```
E-Medic/
├── Controllers/                 # MVC Controllers (API endpoints)
│   ├── AccountController.cs    # User registration, login, logout
│   ├── DashboardController.cs  # Role-based dashboards
│   ├── DoctorController.cs     # Doctor-specific operations
│   ├── PatientController.cs    # Patient-specific operations
│   ├── AppointmentController.cs # Appointment management
│   ├── PrescriptionController.cs # Prescription handling
│   ├── AdminController.cs      # Administrative functions
│   └── HomeController.cs       # Landing page
│
├── Models/                      # Domain entities
│   ├── User.cs                 # Extended ASP.NET Identity user
│   ├── Doctor.cs               # Doctor profile and metadata
│   ├── Appointment.cs          # Appointment entity
│   ├── MedicalRecord.cs        # Patient medical records
│   └── BaseEntity.cs           # Base entity with common properties
│
├── Services/                    # Business logic layer
│   ├── AccountService.cs       # Authentication & user management
│   ├── DoctorService.cs        # Doctor operations
│   ├── PatientService.cs       # Patient operations
│   ├── AppointmentService.cs   # Appointment logic
│   ├── PrescriptionService.cs  # Prescription & medical records
│   ├── AdminService.cs         # Administrative functions
│   ├── CloudinaryService.cs    # Image upload & storage
│   ├── EmailService.cs         # Email notifications
│   ├── PdfService.cs           # PDF generation
│   ├── DbInitializer.cs        # Database seeding
│   └── Interfaces/             # Service interfaces (DI contracts)
│
├── DTOs/                        # Data Transfer Objects
│   ├── LoginDto.cs
│   ├── RegisterDto.cs
│   ├── AppointmentDto.cs
│   ├── DoctorProfileDto.cs
│   ├── CreatePrescriptionDto.cs
│   └── ...
│
├── Data/                        # Data access layer
│   └── ApplicationDbContext.cs # Entity Framework DbContext
│
├── Migrations/                  # Database migrations
│   └── (Timestamped migration files)
│
├── Validators/                  # FluentValidation validators
│   └── DoctorProfileDtoValidator.cs
│
├── Views/                       # Razor views (UI templates)
│
├── wwwroot/                     # Static files (CSS, JS, images)
│
├── App_Data/                    # Application data storage
│   └── Keys/                   # Data protection keys
│
├── Properties/                  # Project properties
│   └── launchSettings.json     # Debug configuration
│
├── Program.cs                   # Application startup configuration
├── appsettings.json            # Default configuration
├── appsettings.Development.json # Development overrides
├── E-Medic.csproj              # Project file
├── E-Medic.slnx                # Solution file
├── Dockerfile                   # Container configuration
└── README.md                    # This file
```

---

## 🏗️ Architecture

### Architectural Pattern
E-Medic follows a **Model-View-Controller (MVC)** architecture with a clear separation of concerns:

```
┌─────────────────┐
│      View       │ (Razor Templates)
│   (Presentation)│
└────────┬────────┘
         │
┌────────▼────────┐
│   Controller    │ (HTTP Request Handlers)
│   (API Layer)   │
└────────┬────────┘
         │
┌────────▼────────┐
│    Service      │ (Business Logic)
│  (Domain Logic) │
└────────┬────────┘
         │
┌────────▼────────┐
│    DbContext    │ (Entity Framework)
│  (Data Access)  │
└────────┬────────┘
         │
┌────────▼────────┐
│   PostgreSQL    │ (Persistent Data)
│    Database     │
└─────────────────┘
```

### Dependency Injection
All services are registered as **Scoped** in the DI container, ensuring proper lifecycle management and thread safety.

```csharp
// Service Registration (Program.cs)
services.AddScoped<IAccountService, AccountService>();
services.AddScoped<IDoctorService, DoctorService>();
services.AddScoped<IAppointmentService, AppointmentService>();
// ... more services
```

---

## 🗄️ Database Schema

### Entity Relationship Diagram

```
┌──────────────┐
│    Users     │◄──────┬──────────────────────┐
│              │       │                      │
│ Id (PK)      │       │                      │
│ FullName     │       │                      │
│ Email        │       │                      │
│ Role         │       │                      │
│ DateOfBirth  │       │                      │
│ Gender       │       │                      │
│ IsApprovedBy │       │                      │
│ Admin        │       │                      │
└──────────────┘       │                      │
       ▲               │                      │
       │               │                      │
       │ (1:1)         │ (1:Many)             │ (1:Many)
       │               │                      │
┌──────┴─────┐    ┌────▼──────┐         ┌────▼──────┐
│  Doctors   │    │Appointment│         │MedicalRec │
│            │    │            │         │ords       │
│ UserId (FK)│    │PatientId   │         │PatientId  │
│Specialty   │    │ (FK)       │         │ (FK)      │
│Qualif.     │    │DoctorId    │         │DoctorId   │
│Fees        │    │ (FK)       │         │ (FK)      │
└────────────┘    │Status      │         │AppointmentId
                  │Date        │         │ (FK)      │
                  └────────────┘         └───────────┘
```

### Core Tables

| Table | Purpose | Key Fields |
|-------|---------|-----------|
| **AspNetUsers** | User accounts & credentials | Id, Email, PasswordHash, UserName |
| **Doctors** | Doctor profiles & metadata | UserId (FK), Specialty, Qualifications, ConsultationFee |
| **Appointments** | Consultation bookings | PatientId (FK), DoctorId (FK), Date, Status |
| **MedicalRecords** | Patient health records | AppointmentId (FK), PatientId (FK), DiseaseName, Description |
| **AspNetRoles** | Role definitions | Id, Name (Patient, Doctor, Admin) |
| **AspNetUserRoles** | User-to-role mappings | UserId (FK), RoleId (FK) |

---

## 🔌 API Documentation

### Authentication Endpoints
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/Account/Register` | User registration (Patient/Doctor) |
| POST | `/Account/Login` | User login |
| GET | `/Account/Logout` | User logout |

### Dashboard Endpoints
| Method | Endpoint | Role | Description |
|--------|----------|------|-------------|
| GET | `/Dashboard/Index` | Patient/Doctor | User-specific dashboard |
| GET | `/Dashboard/AdminDashboard` | Admin | System-wide statistics |

### Doctor Endpoints
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/Doctor/Profile` | Retrieve doctor profile |
| POST | `/Doctor/UpdateProfile` | Update doctor information |
| GET | `/Doctor/Dashboard` | Doctor performance metrics |
| GET | `/Doctor/DoctorQueue` | View pending appointments |

### Patient Endpoints
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/Patient/Profile` | Retrieve patient profile |
| POST | `/Patient/UpdateProfile` | Update patient information |

### Appointment Endpoints
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/Appointment/FindDoctors` | Search available doctors |
| POST | `/Appointment/Book` | Book a new appointment |

### Prescription Endpoints
| Method | Endpoint | Description |
|--------|----------|-------------|
| POST | `/Prescription/Create` | Create prescription/medical record |
| GET | `/Prescription/MyHistory` | View prescription history |
| GET | `/Prescription/DownloadPdf` | Download prescription as PDF |

### Admin Endpoints
| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/Admin/PendingDoctors` | List unapproved doctors |
| POST | `/Admin/ApproveDoctor` | Approve doctor registration |

---

## 🔐 Authentication & Authorization

### Authentication Flow

```
User Input (Email/Password)
         ↓
   [Validation]
         ↓
   [Hash Check]
         ↓
   [Identity Verified]
         ↓
   [Create Claim]
         ↓
   [Issue Cookie]
         ↓
   [Redirect to Role Dashboard]
```

### Role-Based Access Control

| Role | Permissions |
|------|-------------|
| **Patient** | View/book appointments, manage own medical records, view prescriptions |
| **Doctor** | Manage profile, approve appointments, create prescriptions, view earnings |
| **Admin** | Approve doctors, access all medical records, system management |

### Security Features
- **Password Requirements**: 8+ characters, uppercase, lowercase, digits
- **Cookie Configuration**: HttpOnly flag, 30-day expiration, sliding renewal
- **HTTPS Enforcement**: Required in production
- **Doctor Approval Gate**: Admin approval required before doctor access
- **Claim-Based Authorization**: User ID extracted from `ClaimTypes.NameIdentifier`

---

## ⚙️ Configuration

### Environment Variables & Settings

#### appsettings.json
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Port=5432;Database=emedic_db;User Id=postgres;Password=your_password;"
  }
}
```

#### Cloudinary Configuration
```json
"CloudinarySettings": {
  "CloudName": "your_cloudinary_cloud_name",
  "ApiKey": "your_cloudinary_api_key",
  "ApiSecret": "your_cloudinary_api_secret"
}
```

#### Email/SMTP Configuration
```json
"SmtpSettings": {
  "Server": "smtp.gmail.com",
  "Port": 587,
  "Username": "your_email@gmail.com",
  "Password": "your_app_password",
  "SenderName": "E-Medic",
  "SenderEmail": "noreply@emedic.com"
}
```

### User Secrets (Development)
For local development, use .NET User Secrets to securely store sensitive data:

```bash
# Initialize secrets file
dotnet user-secrets init

# Set secret values
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "your_connection_string"
dotnet user-secrets set "CloudinarySettings:ApiKey" "your_api_key"
```

---

## 🐳 Deployment

### Docker Build & Deployment

#### Build Image
```bash
docker build -t emedic:latest .
```

#### Push to Registry
```bash
docker tag emedic:latest your_registry/emedic:latest
docker push your_registry/emedic:latest
```

#### Run Container
```bash
docker run -d \
  --name emedic-app \
  -p 8080:8080 \
  -p 8081:8081 \
  -e ConnectionStrings__DefaultConnection="your_connection_string" \
  -e CloudinarySettings__CloudName="your_cloud_name" \
  -e CloudinarySettings__ApiKey="your_api_key" \
  -e CloudinarySettings__ApiSecret="your_api_secret" \
  emedic:latest
```

### Docker Compose (Optional)
```yaml
version: '3.9'
services:
  db:
    image: postgres:10
    environment:
      POSTGRES_PASSWORD: postgres
      POSTGRES_DB: emedic_db
    ports:
      - "5432:5432"
    volumes:
      - postgres_data:/var/lib/postgresql/data

  app:
    build: .
    ports:
      - "8080:8080"
      - "8081:8081"
    environment:
      ConnectionStrings__DefaultConnection: "Server=db;Port=5432;Database=emedic_db;User Id=postgres;Password=postgres;"
    depends_on:
      - db

volumes:
  postgres_data:
```

### Render.com Deployment
The application is currently deployed on [Render.com](https://render.com/):
- **Live URL**: [https://e-medic.onrender.com/](https://e-medic.onrender.com/)
- **Environment**: PostgreSQL database with automatic scaling
- **Auto-deploy**: Connected to Git repository for continuous deployment

---

## 👤 Demo Credentials

### Admin Account
```
Email:    admin@emedic.com
Password: Admin@1234
```

**Admin Capabilities:**
- Approve pending doctor registrations
- Access all patient medical histories
- View system analytics and user management

### Test Accounts
You can create your own test accounts during registration as either **Patient** or **Doctor**.

---

## 🔧 Troubleshooting

### Common Issues

#### Database Connection Error
**Problem**: `unable to connect to endpoint (attempting to connect via TCP)`

**Solution**:
1. Verify PostgreSQL is running
2. Check connection string in `appsettings.json`
3. Ensure database exists: `CREATE DATABASE emedic_db;`
4. Run migrations: `dotnet ef database update`

#### Cloudinary Upload Failed
**Problem**: Image upload returns 401 Unauthorized

**Solution**:
1. Verify Cloudinary API credentials in `appsettings.json`
2. Check API key and secret validity at [Cloudinary Dashboard](https://cloudinary.com/console)
3. Ensure folder permissions allow `emedic_profiles` folder creation

#### Email Not Sending
**Problem**: Prescription PDFs or notifications not delivered

**Solution**:
1. Check SMTP settings in `appsettings.json`
2. For Gmail: Enable "Less secure app access" or use App Password
3. Verify firewall allows outbound SMTP connections (port 587)
4. Check email logs in application output

#### Docker Build Fails
**Problem**: `error: unable to resolve image config for mcr.microsoft.com/dotnet/aspnet:10.0`

**Solution**:
1. Ensure Docker daemon is running
2. Check internet connection
3. Pull base image: `docker pull mcr.microsoft.com/dotnet/aspnet:10.0`
4. Try building again: `docker build -t emedic:latest .`

#### Port Already in Use
**Problem**: `Address already in use` when running locally

**Solution**:
1. Change port in `launchSettings.json`
2. Or kill process using port: `netstat -ano | findstr :5001` (Windows)
3. Docker: Use different port mapping: `-p 9090:8080`

---

## 📞 Support & Documentation

### Key Documentation Files
- [Database Migrations](./E-Medic/Migrations/) - Schema evolution history
- [Entity Models](./E-Medic/Models/) - Domain model documentation
- [Service Interfaces](./E-Medic/Services/Interfaces/) - Business logic contracts
- [Configuration Examples](./E-Medic/) - appsettings templates

### External Resources
- [ASP.NET Core Documentation](https://learn.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/)
- [PostgreSQL Documentation](https://www.postgresql.org/docs/)
- [Cloudinary API Reference](https://cloudinary.com/documentation/image_upload_api_reference)

---

## 👨‍💻 Contributing

Contributions are welcome! Please follow these guidelines:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

### Code Standards
- Follow C# naming conventions (PascalCase for classes/methods, camelCase for variables)
- Use meaningful variable and method names
- Add XML comments for public methods
- Write unit tests for new services
- Ensure all tests pass before submitting PR

---

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 🎯 Project Roadmap

### Phase 1 (Current) ✅
- [x] Core appointment scheduling
- [x] Medical record management
- [x] Role-based authentication
- [x] PDF prescription generation
- [x] Cloud image storage

### Phase 2 (Planned) 🚀
- [ ] Video consultation integration
- [ ] AI-powered symptom analysis
- [ ] Mobile app (iOS/Android)
- [ ] Advanced analytics dashboard
- [ ] SMS notifications

### Phase 3 (Future) 💡
- [ ] Telemedicine integration
- [ ] Insurance integration
- [ ] Multi-language support
- [ ] Advanced AI recommendations
- [ ] Blockchain for medical records

---

## 📊 Project Statistics

- **Framework Version**: .NET 10.0 (LTS)
- **Database**: PostgreSQL 10.0
- **Total Controllers**: 8
- **Total Services**: 10+
- **Database Tables**: 10+ (Including identity tables)
- **Deployment**: Docker + Render.com
- **Status**: Production Ready ✅

---

## 📧 Contact & Support

For questions or support, please:
- 📨 Email: support@emedic.com
- 🐛 Report Issues: [GitHub Issues](https://github.com/yourusername/E-Medic/issues)
- 💬 Discussions: [GitHub Discussions](https://github.com/yourusername/E-Medic/discussions)

---

**Made with ❤️ by the E-Medic Development Team**

**Last Updated**: June 6, 2026" 
