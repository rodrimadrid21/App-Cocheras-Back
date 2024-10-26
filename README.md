
Estacionamiento Austral API
Estacionamiento Austral API is a backend developed in ASP.NET Core to manage a parking system. It offers functionalities like JWT authentication, user management, parking spots, parking records, and rates. It uses Entity Framework Core with SQLite as the database.

Features
JWT Authentication: Enables secure user authentication via JWT tokens.
User Management: Registers and manages users, including soft deletion and admin permissions.
Parking Spot Management: Allows adding, disabling, enabling, and deleting parking spots.
Parking Management: Records vehicle check-ins and check-outs, calculates rates, and manages availability.
Rate Management: Configures and updates parking rates.
Project Structure
Controllers: API controllers that expose endpoints for each entity (Users, Parking Spots, Parking Records, Rates).
Data:
ApplicationContext: Entity Framework context that defines the database connection and entities.
Repositories: Classes that handle data access for each entity (UserRepository, CocheraRepository, etc.).
Services: Service classes containing business logic for each entity, delegating data operations to repositories.
Common/Dtos: Data Transfer Objects (DTOs) used for data transfer between the API and the client.
Utils: General utilities, such as JWT authentication handling and common validations.
Requirements
.NET 6 SDK or higher
SQLite for the database
Setup
Clone this repository:

bash
Copiar código
git clone <REPOSITORY_URL>
cd EstacionamientoAustralApi
Restore the required NuGet packages:

bash
Copiar código
dotnet restore
Configure the SQLite database in appsettings.json:

json
Copiar código
"ConnectionStrings": {
  "DBConnectionString": "Data Source=EstacionamientoAustralApi.db"
},
"Authentication": {
  "SecretForKey": "your_secret_key_here",
  "Issuer": "your_issuer",
  "Audience": "your_audience"
}
DBConnectionString: Path to the SQLite database file.
SecretForKey: Secret key to generate JWT tokens.
Issuer and Audience: Values to configure the issuer and audience of the JWT.
Generate the database using migrations:

bash
Copiar código
dotnet ef database update
Running the Application
To run the project in development mode, use the following command:

bash
Copiar código
dotnet run
The server will be available at https://localhost:5001 or http://localhost:5000.

API Documentation
API documentation is available in Swagger. Once the server is running, navigate to https://localhost:5001/swagger to view and test the endpoints.

Main Endpoints
Authentication
POST /api/Authenticate: Authenticates a user and returns a JWT token.
Users
POST /api/User/register: Registers a new user.
GET /api/User/all: Retrieves all non-deleted users.
Parking Spots
GET /api/Cochera/all: Retrieves all non-deleted parking spots.
POST /api/Cochera/add: Adds a new parking spot.
PUT /api/Cochera/disable/{id}: Disables a parking spot.
PUT /api/Cochera/enable/{id}: Enables a parking spot.
Parking Records
POST /api/Estacionamiento/abrir: Opens a parking record for a vehicle.
POST /api/Estacionamiento/cerrar: Closes a parking record and calculates the cost.
Rates
GET /api/Tarifa/all: Retrieves all rates.
PUT /api/Tarifa/update/{id}: Updates the value of an existing rate.
Usage Example
To interact with the API, use an HTTP client like Postman or cURL.

Authenticate a user to obtain a JWT token.
Use the JWT token in the authorization headers (Bearer Token) to access the other endpoints that require authentication.
