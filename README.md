# Vehicle Explorer

A web application that allows users to explore vehicle manufacturers, model years, vehicle types, and models.

Vehicle data is retrieved from the [NHTSA Vehicle Product Information Catalog (vPIC) API](https://vpic.nhtsa.dot.gov/api/).

## Tech Stack

### Backend
- ASP.NET Core Web API
- C#
- .NET 10
- HttpClient for external API integration

### Frontend
- Angular 22
- TypeScript
- Angular Signals
- Angular HttpResource

### External API
- [NHTSA Vehicle API](https://vpic.nhtsa.dot.gov/api/)

## Project Structure

```text
VehicleExplorer/
│
├── VehicleExplorer.Api/
│   ├── Controllers/
│   ├── DTOs/
│   ├── Services/
│   ├── Properties/
│   ├── Program.cs
│   ├── appsettings.json
│   └── Dockerfile
│
├── VehicleExplorer.Client/
│   ├── public/
│   ├── src/
│   │   └── app/
│   │       ├── core/
│   │       ├── Dtos/
│   │       ├── features/
│   │       └── shared/
│   ├── angular.json
│   ├── package.json
│   ├── Dockerfile
│   └── nginx.conf
│
└── VehicleExplorer.Api.slnx
```

## Prerequisites

Install the following tools before running the application:

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [Node.js](https://nodejs.org/) (a version compatible with Angular 22)
- npm (included with Node.js)
- [Git](https://git-scm.com/)

Verify the installations:

```bash
dotnet --version
node --version
npm --version
```

## Getting Started

### 1. Clone the repository

```bash
git clone https://github.com/mohammedbkeirat/VehicleExplorer.git
```

Navigate to the repository:

```bash
cd VehicleExplorer
```

### 2. Run the Backend

Open a terminal and navigate to the API project:

```bash
cd VehicleExplorer.Api
```

Restore dependencies:

```bash
dotnet restore
```

Start the API using the HTTP launch profile:

```bash
dotnet run --launch-profile http
```

The API will be available at:

```text
http://localhost:5087
```

Example API endpoint:

```text
http://localhost:5087/api/VehicleExplorer/makes
```

You can also use the HTTPS launch profile:

```bash
dotnet run --launch-profile https
```

The HTTPS endpoint will be:

```text
https://localhost:7223
```

### 3. Run the Frontend

Keep the backend running.

Open another terminal and navigate to the Angular project:

```bash
cd VehicleExplorer/VehicleExplorer.Client
```

Install the npm dependencies:

```bash
npm install
```

Start the Angular development server:

```bash
npm start
```

The application will be available at:

```text
http://127.0.0.1:4200
```

### 4. Configure the API URL

The Angular application uses environment-specific configuration.

For local development, configure the API base URL in the development environment file:

```typescript
apiBaseUrl: 'http://localhost:5087/api'
```

Make sure the configured URL matches the backend launch profile.

### 5. CORS Configuration

The Angular development server and .NET API run on different origins. The backend must allow the Angular development origin.

For the default Angular development URL, the allowed origin is:

```text
http://127.0.0.1:4200
```

If CORS is not already configured in the API, configure an appropriate policy in `Program.cs`:

```csharp
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy
            .WithOrigins("http://127.0.0.1:4200")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
```

Register the middleware before mapping controllers:

```csharp
app.UseCors();

app.MapControllers();
```

Adjust the allowed origin if you change the Angular development host or port.

## Running the Application

To run the complete application:

1. Start the .NET Web API.
2. Verify that the API is accessible.
3. Start the Angular development server in a separate terminal.
4. Open `http://127.0.0.1:4200` in your browser.
5. Explore vehicle makes, model years, types, and models.

Both the backend and frontend must be running simultaneously.

## Docker

Both the backend and frontend are containerized using Docker.

Docker images are published to Docker Hub:

- **Backend:** `mohammedbkeirat/vehicle-explorer-api`
- **Frontend:** `mohammedbkeirat/vehicle-explorer-ui`

The application is also deployed to AWS EC2.

## Live Demo

**Application URL:** [Vehicle Explorer](http://51.21.130.107/vehicle-explorer)

The application is hosted on AWS EC2 using Docker containers.

**Note:** The application currently uses HTTP, not HTTPS. Make sure to access it using `http://` rather than `https://`.

## License

This project is intended for demonstration and portfolio purposes.
