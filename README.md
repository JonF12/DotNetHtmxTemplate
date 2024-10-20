
# .Net Htmx TypeScript Tailwind template with razor views and API

Simple .NET 8 template with everything set up to show basic movie data with CRUD and server side HTML rendering

# Tested on

-- Windows 10 

-- Linux DB migrations don't work but everything else works. 
I suggest re-creating the migrations or updating the packages to a different sqlite library. Also check all of the filepaths in the code including in package.json.


# Why?
To write little or no javascript

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

Start the API locally by running, or start it in visual studio with IIS

```bash
dotnet run
```

## Recommendations
1.  **Move the database logic/services out onto a seperate project**
2.  **Find some additional .js plugins or a framework for state management if needed**
3.  **Move some of the View into components**
4.  **Combine tailwind classes to compress HTML more**
5.  **Improve the logic in the HtmxRequest attribute, it renders either a partial, or a full viewpage with layout depending on if it's the first time rendering or not.**
6.  **If you want pure html with as little JS as possible use some of the built in razor directives e.g @Html.Action, which will help with rendering**
7.  **Replace the custom htmx.AddExtension("submitjson") with a library if it exists, unfortunately htmx doesn't make it easy to send form data as JSON, I don't know why.**
8.  **When defining and returning a partial view make sure to not add the @page{} directive otherwise your model will be null with no clear error indicating why. **
