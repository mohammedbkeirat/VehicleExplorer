# Design Decisions – Vehicle Explorer

## 1. Project Overview

Vehicle Explorer is a web application that allows users to explore vehicle manufacturers, model years, vehicle types, and models.

The application integrates with the NHTSA Vehicle Product Information Catalog (vPIC) API to retrieve vehicle information.

The project was designed to demonstrate modern frontend and backend development practices, API integration, containerization, and cloud deployment.

## 2. Technology Stack

| Component | Technology | Reason |
|---|---|---|
| Backend | ASP.NET Core Web API (.NET 10) | Modern, high-performance backend framework |
| Frontend | Angular 22 | Standalone components, Signals, and modern Angular APIs |
| Language | C# / TypeScript | Strong typing and maintainability |
| External API | NHTSA vPIC API | Provides vehicle manufacturer and model information |
| Containerization | Docker | Consistent application environments and deployment |
| Web Server | Nginx | Serves Angular static files and proxies API requests |
| Cloud Hosting | AWS EC2 | Hosts the containerized application |

## 3. Key Design Decisions

### 3.1 Backend Architecture

- Used ASP.NET Core Web API to expose application endpoints.
- Kept the backend in a single project instead of introducing multiple projects and a full Clean Architecture structure.
- Organized the code into Controllers, Services, Interfaces, and DTOs.
- Used dependency injection to manage service dependencies.
- Used HttpClient to communicate with the external NHTSA API.
- Kept external API response models and application DTOs separate where appropriate.

**Reasoning:**

The application has a relatively small scope and does not require a database or complex business logic. A lightweight structure keeps the solution simple and avoids unnecessary architectural overhead.

### 3.2 Frontend Architecture

- Used Angular 22 with standalone components.
- Organized the frontend using a feature-based structure.
- Used Angular Signals for reactive state management.
- Used Angular HttpResource for HTTP data fetching and resource state management.
- Separated shared components from feature-specific components.

**Reasoning:**

Modern Angular APIs provide a reactive development approach with less boilerplate. Feature-based organization also makes the application easier to maintain and extend.

### 3.3 External API Integration

- Used the NHTSA vPIC API as the primary source of vehicle information.
- Implemented backend endpoints to retrieve and expose the required vehicle data to the frontend.
- Kept external API communication in backend services instead of calling the NHTSA API directly from Angular.

**Reasoning:**

This keeps external API integration centralized in the backend and prevents the frontend from depending directly on the external API contract.

### 3.4 No Database

- Did not introduce a database because the application retrieves vehicle information from the external NHTSA API.
- Avoided storing data that does not need to be persisted by the application.

**Trade-off:**

The application depends on the availability and response time of the external API. It also does not provide local persistence or offline access.

### 3.5 Docker Containerization

- Created separate Docker images for the backend and frontend.
- Published the images to Docker Hub.
- Used a dedicated Docker network (`vehicle-net`) to allow the containers to communicate.
- Used separate containers rather than introducing Docker Compose.

**Reasoning:**

Separate containers allow the frontend and backend to be built, deployed, and updated independently.

### 3.6 Nginx Reverse Proxy

Configured Nginx to:

- Serve Angular production build files.
- Support Angular client-side routing using the SPA fallback.
- Forward requests starting with `/api/` to the backend API container.

The browser communicates with the frontend through a single origin, while Nginx forwards API requests internally through the Docker network.

**Reasoning:**

This avoids exposing the backend container directly to the public internet and eliminates the need for production CORS configuration.

### 3.7 AWS Deployment

- Hosted the application on an AWS EC2 instance using the available Free Tier plan.
- Used Docker containers to run the frontend and backend.
- Exposed HTTP port 80 for public application access.
- Restricted SSH access through the EC2 security group to the developer's IP address.
- Kept the backend API port private and accessible through the Docker network.

**Trade-off:**

The deployment currently uses HTTP without a custom domain or HTTPS certificate. This was an intentional scope and cost decision for the portfolio project, not a recommendation for production applications.

## 4. Assumptions

The following assumptions were made during development:

- The NHTSA vPIC API is available and provides the required vehicle information.
- The application does not require user authentication.
- Vehicle information does not need to be stored in a local database.
- The application is primarily intended for demonstration and portfolio purposes.
- The AWS deployment should remain within the available Free Tier resources.
- The frontend and backend can be deployed independently.

## 5. Trade-offs

| Decision | Benefit | Trade-off |
|---|---|---|
| Single backend project | Simple structure and less overhead | Less separation if the application grows significantly |
| External NHTSA API | No need to maintain vehicle data | Depends on an external service |
| No database | Less infrastructure and maintenance | No persistence or offline access |
| Angular Signals and HttpResource | Modern reactive APIs | Requires familiarity with newer Angular concepts |
| Separate Docker containers | Independent deployment | Requires container networking configuration |
| Nginx reverse proxy | Same-origin API requests and private backend | Adds Nginx configuration and another deployment component |
| HTTP deployment | Avoids additional domain and certificate setup | Traffic is not encrypted |

## 6. AI Usage

AI tools were used as development assistants throughout the project.

Examples of AI assistance included:

- Discussing backend and frontend architecture.
- Generating and reviewing code examples.
- Assisting with UI design, layout, styling, and user experience improvements.
- Troubleshooting Docker and Nginx configuration.
- Assisting with AWS EC2 deployment.
- Reviewing implementation approaches and explaining technical trade-offs.

### Example of an AI Mistake and How It Was Corrected

During deployment, an initial Nginx configuration served Angular files but did not correctly proxy API requests to the backend.

As a result, API requests returned Angular HTML instead of API responses.

The issue was identified by testing the API endpoint and inspecting the Nginx configuration.

The configuration was corrected by adding a reverse proxy rule:

```nginx
location /api/ {
    proxy_pass http://vehicle-explorer-api:8080;
    proxy_set_header Host $host;
    proxy_set_header X-Real-IP $remote_addr;
    proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
    proxy_set_header X-Forwarded-Proto $scheme;
}
```

The frontend image was rebuilt and redeployed, and the API endpoint was tested successfully.

This demonstrated the importance of validating AI-generated suggestions against actual application behavior rather than accepting them without verification.

## 7. Next Steps

Potential improvements for future iterations:

- Add automated unit and integration tests for the backend and frontend.
- Introduce CI/CD using GitHub Actions to automate Docker image builds and deployment.
- Add application logging and monitoring.
- Consider caching frequently requested vehicle data to reduce external API calls.
- Add error handling and resilience strategies for external API failures.
- Configure HTTPS and a custom domain if the application moves beyond its current portfolio deployment.
- Introduce additional architectural separation if the application's complexity increases.

## 8. Conclusion

Vehicle Explorer was designed with simplicity, maintainability, and practical deployment in mind.

The implementation demonstrates a modern Angular frontend, ASP.NET Core Web API backend, external API integration, Docker containerization, Nginx reverse proxy configuration, and AWS hosting.

The architecture intentionally avoids unnecessary complexity while leaving room for future improvements.
