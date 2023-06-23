# traveltime-internship-task

This is a solution for the test assignment for Scala TravelTime internship by Arnas Bradauskas.

# Dependancies
Instructions for the installation of `.NET SDK 6.0` for Windows / Linux / MacOS can be found here:
https://learn.microsoft.com/en-us/dotnet/core/install/

This project used 2 external libraries:
1. `NetTopologySuite` - used for checking whether polygon covers given point.
2. `Newtonsoft.Json` - used for parsing input data from .json files.

### Libraries installation
To install the packages it is recomended to use the NuGet package manager

How to do it using the `.NET CLI`:
```bash
dotnet add package NetTopologySuite --version 2.5.0
dotnet add package Newtonsoft.Json --version 13.0.3
```

# Usage
To run the application, use the `dotnet run` command with the `--project` option followed by the project name `traveltimeTask`, and provide the filenames as arguments.

```bash
dotnet run --project <projectName> <regionsFile> <locationsFile> <outputFile>
```

#### Example

```bash
dotnet run --project traveltimeTask regions.json locations.json results.json
```

If you are within the root project directory, the `--project` option is unnecessary and the code can be executed like this: 
```bash
dotnet run input/regions.json input/locations.json output/results.json
```
