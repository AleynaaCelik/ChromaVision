

# ChromaVision - AI-Powered Color Palette Generator

ChromaVision is a full-stack application that allows users to generate color palettes using AI. Users can create palettes by providing text descriptions or by uploading images. The application uses the power of AI to analyze these inputs and generate harmonious color combinations.

## Features

- **Text-based palette generation**: Create color palettes by describing them in natural language
- **Image-based palette extraction**: Extract dominant colors from uploaded images
- **User accounts**: Save your favorite palettes for future reference
- **Export options**: Export palettes in various formats (CSS variables, Tailwind config, etc.)
- **Clean, modern UI**: Intuitive interface for a seamless user experience

## Architecture

ChromaVision follows a clean architecture design with a layered approach:

- **API Layer**: ASP.NET Core Web API with controllers and middleware
- **Application Layer**: Contains business logic, CQRS features, and DTOs
- **Domain Layer**: Contains entities, value objects, and repository interfaces
- **Infrastructure Layer**: Implements database access, external services integration
- **Core Layer**: Contains cross-cutting concerns like logging, configuration

The frontend is built with React, TypeScript, and uses Axios for API communication.

## Technologies

### Backend
- ASP.NET Core 8.0
- Entity Framework Core for data access
- MediatR for CQRS pattern implementation
- AutoMapper for object mapping
- JWT for authentication

### Frontend
- React with TypeScript
- Axios for API communication
- React Router for navigation
- Modern CSS with responsive design

### Infrastructure
- SQL Server (containerized with Docker)
- OpenAI API integration
- Image processing

## Getting Started

### Prerequisites
- .NET 8.0 SDK
- Node.js and npm
- Docker (for SQL Server)

### Setting Up the Database

```bash
# Pull and run SQL Server Docker image
docker run -e "ACCEPT_EULA=Y" -e "MSSQL_SA_PASSWORD=YourStrong@Passw0rd" -p 1433:1433 --name chromavision-sql -d mcr.microsoft.com/mssql/server:2022-latest
```

### Running the Backend

```bash
# Clone the repository
git clone https://github.com/yourusername/chromavision.git
cd chromavision

# Restore packages
dotnet restore

# Apply migrations
dotnet ef database update --project ChromaVision.Infrastructure --startup-project ChromaVision.API

# Run the API
dotnet run --project ChromaVision.API
```

### Running the Frontend

```bash
# Navigate to the client directory
cd client/chromavision-ui

# Install dependencies
npm install

# Start the development server
npm start
```

## API Endpoints

- `POST /api/v1/ColorPalette/generate-from-text` - Generate palette from text description
- `POST /api/v1/ColorPalette/generate-from-image` - Generate palette from image
- `GET /api/v1/ColorPalette/my-palettes` - Get user's saved palettes
- `POST /api/v1/ColorPalette/save` - Save a palette
- `POST /api/v1/Auth/register` - Register new user
- `POST /api/v1/Auth/login` - User login

## Configuration

The application uses appsettings.json for configuration:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost,1433;Database=ChromaVisionDb;User Id=sa;Password=YourStrong@Passw0rd;TrustServerCertificate=True;"
  },
  "JwtSettings": {
    "Key": "YourSecureJwtKey1234567890",
    "Issuer": "ChromaVision",
    "Audience": "ChromaVisionUsers",
    "ExpiryHours": "24"
  },
  "OpenAI": {
    "ApiKey": "your_openai_key_here"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```

## Project Structure

```
ChromaVision/
├── ChromaVision.API/              # API controllers and configuration
├── ChromaVision.Application/      # Business logic and CQRS
├── ChromaVision.Core/             # Common utilities and interfaces
├── ChromaVision.Domain/           # Entities and repository interfaces
├── ChromaVision.Infrastructure/   # Data access and service implementations
└── client/                        # React frontend
    └── chromavision-ui/           # React application
```

## License

This project is licensed under the MIT License - see the LICENSE file for details.

## Acknowledgments

- OpenAI for providing the AI capabilities
- All contributors to this project

---

Feel free to contribute to this project by submitting pull requests or opening issues for bugs and feature requests!

![image](https://github.com/user-attachments/assets/e05bd459-7dd0-46d1-bfce-335247d02e6b)

![image](https://github.com/user-attachments/assets/43e261dd-b447-422c-a3f6-5260fe33755f)
