
# .Net Htmx TypeScript Tailwind template with razor views and API

Simple .NET 8 template with everything set up to show basic movie data with CRUD and server side HTML rendering

# Tested on

- Windows 10 

- Linux DB migrations don't work but everything else works.
  
for linux systems try re-creating the migrations or updating the packages to a different sqlite library. Also check all of the filepaths in the code including in package.json.


# Why? Isn't HTMX and Razor redundant?
HTMX is superior in terms of the amount of boilerplate needed compared to razor. No need to write Javascript for AJAX Calls and this produces cleaner code overall.
The goal in using HTMX is to avoid writing JS as much as possible, producing simpler code.

## Prerequisites

Before you begin, ensure you have the following installed:
- .NET 8.0 SDK
- Entity Framework Core CLI
```bash
  dotnet tool install --global dotnet-ef
```

## Getting Started

To get a local copy up and running follow these simple steps:

1. **Install necessary packages**

   Make sure all the required NuGet and NPM packages are restored:

   ```bash
   dotnet restore
   npm install
   ```

2. **Run Migrations**

   Apply the database migrations to set up your database schema:

   ```bash
   dotnet ef database update
   ```
2. **Build css**

   Build tailwind css. (Change this as you see fit into one build script for convenience, add hot reloading for views on build, there should be a way) 
   ```bash
   npm run build:css
   ```

## Usage

Start the API locally by running the code below, or start it in visual studio with IIS

```bash
dotnet run
```
