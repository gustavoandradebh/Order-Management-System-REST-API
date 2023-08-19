# Order Management System REST API

# Gustavo de Oliveira Andrade Restani

API made for the Senior C# .NET developer position on Sierra Interactive.

The API was built using:
- .NET 7.0 implementing Repository and Unit Of Work patterns
- Entity Framework Core
- ASP.NET Identity with JWT and role-based authorization
- SQL Server
- SQL migrations to create the tables and stored procedure
- XUnit & Moq
- NLog
- Swagger & Postman
- Docker

# Prerequisites

- Docker or Visual Studio 2022 installed.
- SQL Server database
  Before running the application, you will need to update the connection string on appsettings.json in order to refer to your local (or cloud) database.

# How to run

There are two ways to run the application:

1. Using Docker
2. Using Visual Studio 2022

## 1. Using Docker

#### Step 1

Open a PowerShell terminal.

#### Step 2

Navigate to WebApi project folder. Make sure the Dockerfile is available in this folder location.

#### Step 3

Execute the command below to create the Docker image.

> docker build -t webapi -f Dockerfile ..

#### Step 4
Execute the below command to create and run a container.

> docker run -d -p 6379:80 --name webapicontainer webapi 

## 2. Using Visual Studio 2022

#### Step 1
Open the ***Order-Management-System-REST-API.sln*** solution in Visual Studio 2022

#### Step 2
Click on ***Debug -> Start Debugging*** or press ***F5*** key

#### Step 3
Wait until your default browser opens the Swagger page.

# How to test

Please refer to the following Postman collection file, located on the root folder, containing all the endpoints and requests available to test the API.

> WebApi.postman_collection.json

# Database

The database is a SQL Server database hosted locally. 
The scripts used to create the schema and seed the initial data are migrations, and should be executed automatically when the application starts.

# Authenticating

There are some endpoints that require a JWT token. For the first time using the api, you will need to create an user that will be provided with a Bearer token. To do that, please follow the instructions below:

- Call the endpoint `POST /api/User/create` passing the following json on the request body:
```
{
  "username": "AnyUserName",
  "email": "user@example.com",
  "password": "P@ssw0rd",
  "role": "Admin"
}
```
- If the user was created successfully, you can now call the endpoint `POST /api/User/authenticate` passing the following json on the request body:
```
{
  "username": "AnyUserName",
  "password": "P@ssw0rd"
}
```

This will give you a Bearer token for you to use to authenticate on the API.

# Critique

The API was constructed using the Repository and Unit Of Work patterns, which I believe constitutes a robust architectural choice. This approach not only ensured the maintenance of a well-organized codebase but also amplified its testability and practicality. By breaking down data access into repositories and managing units of work, the project is adaptable for potential future database changes.

For managing database interactions, I employed Entity Framework Core. This framework streamlined operations within the SQL Server environment and abstracted away complex data tasks, contributing to a more manageable development process. In terms of security, I implemented ASP.NET Identity in conjunction with JWT and role-based authorization. This combination enabled a strong focus on effective user management and rigorous access control.

The integration of SQL migrations to manage schema alterations and stored procedures aligns seamlessly with industry standards, ensuring the seamless updating of the database. The incorporation of XUnit and Moq for testing provides assurance of reliable code performance. While I'm aware that there's potential for enhancement in terms of testing and validation, a robust foundation and architecture has already been laid.

The inclusion of NLog for comprehensive logging has enhanced the project's stability. Furthermore, the integration of tools like Swagger, Postman, and Docker guarantees the provision of user-friendly documentation, efficient testing, and streamlined deployment practices.

In summary, I hope to have effectively showcased some good architectural decisions, comprehensive security measures, a diligent testing approach, and a state of deployment readiness through this project. 

Thank you for your time and the opportunity. I'm excited to receive your insights and critiques on this project.

