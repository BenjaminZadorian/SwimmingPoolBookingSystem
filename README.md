# Gym Class Booking System

This is a piece of software allowing for the management of customers, employees and other business services relating to swimming pools.  This software is useful for managing books, lessons, employee payment systems, managing inventory and many more features.

For this demonstration, a customer or employees email is their first and lastname, such as:
```
alice.johnson@email.com -> customer
john.carter@email.com -> employee
```

and all customer and employee passwords are set to:
```
Password123
```
## Prerequisites

Before compiling and running the project, make sure you have the following installed:

- [.NET SDK](https://dotnet.microsoft.com/en-us/download) (version X.X or higher)
- A modern web browser

You can check your .NET version with:

```bash
dotnet --version
```

## Cloning the Repository

```bash
git clone https://ByteFlow@dev.azure.com/ByteFlow/SwimmingPoolBookingSystem/_git/SwimmingPoolBookingSystem
cd SoftwareEngineerProject
```

## Building the Project

Run the following command from the project root:

```bash
dotnet build
```

This will restore dependencies and compile the project.

## Running the Project

Use the following command to run the app:

```bash
dotnet run
```

Once running, open a browser and navigate to:

```
https://localhost:7008/
```

(or the URL that the terminal outputs)

## How to run in Visual Studio

1. Open the `.sln` file in Visual Studio.
2. Set the project as the startup project.
3. Press `F5` to run with debugging or `Ctrl + F5` to run without debugging.

## How to clean the Project

To remove generated files and start fresh:

```bash
dotnet clean
```
