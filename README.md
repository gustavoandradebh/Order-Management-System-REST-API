# Order Management System REST API

# Gustavo de Oliveira Andrade Restani

API made for the Senior C# .NET developer position on Sierra Interactive, based on the following assessments:  

- https://torc.coderbyte.com/sl-candidate?inviteKey=58BgO9xdDf

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

For the development of this project, I implemented an API based on the Repository and Unit Of Work patterns, not only because the assigment asked to, but also due to its layer implementation, testability and practicality. An application like this relies heavily on database read and write operations, and if we decide to change the database or adding another one, this architecture makes it easy to achieve that.

I hope I had covered all the requirements, however, if I had more time, I would improve the POST validations, adding more edge cases, implementing FluentValidation library and adding more unit tests and integration tests to cover those cases. 

