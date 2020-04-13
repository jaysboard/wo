## Tasks completed
* Core Task Details
* Bonus Task

## Task not completed
* Super Bonus Task

## Requirements
- .NET Core >= 3.1.x - Download [.NET Core 3.1.x](https://dotnet.microsoft.com/download/dotnet-core/3.1) (install either the Runtime or SDK)
  - **Runtime** (.NET Core Runtime 3.1.0): Minimum requirement for running the project
  - **SDK** (SDK 3.1.100): For running the project and more

## Getting Started
Install [.NET Core 3.1.x](https://dotnet.microsoft.com/download/dotnet-core/3.1)

## Getting the source codes
The easiest way to get the source codes is by cloning the repository with [git](https://git-scm.com/downloads) (if installed), in command line (or terminal) execute this command:
```console
git clone https://github.com/jaysboard/wo.git
```
> You can also [download the source codes as a zip](https://github.com/jaysboard/wo/archive/master.zip).

**Note:** Before running the project, please change the "OMDbAPI" value in *appsettings.json* file (located in the same folder where the *Movies.Web.csproj* project file is, ex: ```C:\src\wo-dotnet-task\Movies.Web```) with your own generated OMDb API.

## Build and run (command line/terminal)
To build and run the project using the command line (or terminal), change the directory to where the **project file** (*Movies.Web.csproj*) is located (ex: ```C:\src\wo-dotnet-task\Movies.Web```) and execute the following command:
```console
dotnet run
```

## Edit, build and run ([MS Visual Studio](https://visualstudio.microsoft.com/vs/))
Using a file explorer (ex: Windows File Explorer), go to the **root folder** where the source codes are located (ex: ```C:\src\wo-dotnet-task\```) and double click or open the **solution file** (*Movies.sln*). Select "Debug>Start Debugging" (F5) from the main menu to build and run the project. 
