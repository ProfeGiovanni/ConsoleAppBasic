# ConsoleAppBasic
This is a short implementation of N-Tier model software architecture. Use it when you have to reset your main project

## ConsoleApp
Here's a basic Windows console app.

## ConsoleApp.BusinessLayer
Namespace dedicated to all related with Business logic for C# applications.

## ConsoleApp.DataLayer
Namespace dedicated to all related with Data conection for C# applications. 


## Instructions to create the .NET Core base app on Visual Code 

The following instructions are to create a new .NetCore layered Solution; it works on Linux bash or Windows cmd. Remember, the .NET Core CLI must be install on your system.
Feel free to change the folder, solution and projects name; the most important thing, follow the N-Tier conventions learned on the course.

1. Create a new folder for the VS solution
```
mkdir ConsoleVsCode
```

2. Change to the new folder
```
cd ConsoleVsCode
```
3. Inside the solution root folder, create a new console app with -n option to put the console application name and then return to root folder.:
```
dotnet new console -n "ConsoleApp"
cd ..
```
4. Create the solution for your current console application
```
dotnet new sln
```
5. Create two new class libraries, named as BusinessLayer and DataLayer, respectively
```
dotnet new classlib -o "ConsoleApp.BusinessLayer" -f "netcoreapp3.1"
dotnet new classlib -o "ConsoleApp.DataLayer" -f "netcoreapp3.1"
```
6. Stablish the references between projects as layered dependencies as folows:
* ConsoleApp depends on BusinessLayer 
* BusinessLayer depends on DataLayer
```
dotnet add ConsoleApp/ConsoleApp.csproj reference ConsoleApp.BusinessLayer/ConsoleApp.BusinessLayer.csproj
dotnet add ConsoleApp.BusinessLayer/ConsoleApp.BusinessLayer.csproj reference ConsoleApp.DataLayer/ConsoleApp.DataLayer.csproj
```
```
