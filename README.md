# JsonPatchGenerator
A generator that creates a JSON Patch Document from comparing two objects

Work in progress

*If you faced any problems using this lib please feel free to create an issue. If the library was useful for you - give it a star. This way I'll be able to know that it's useful for someone and try to find time to continue working on it. All contributions are welcome*

## Installation
### to use with AspNetCore (.NET Core)
#### via Package Manager
```powershell
PM> Install-Package JsonPatchGenerator.AspNetCore
```

#### via dotnet CLI
```bash
> dotnet add package JsonPatchGenerator.AspNetCore
```

### to use with Marvin.JsonPatch (.NET Framework)
#### via Package Manager
```powershell
PM> Install-Package JsonPatchGenerator.Marvin.JsonPatch
```

#### via dotnet CLI
```bash
> dotnet add package JsonPatchGenerator.Marvin.JsonPatch
```

## Usage
Everything is simple:
```c#
var patchGenerator = new JsonPatchDocumentGenerator();
var jsonPatchDocument = patchGenerator.Generate(first, second);
```

### Using DI container (only for the AspNetCore version)
Add this line to the `ConfigureServices` method of your `Startup.cs`:
```c#
services.AddJsonPatchGenerator();
```

Add `IJsonPatchGenerator<IJsonPatchDocument>` as a dependency when you need it.
```c#
readonly IJsonPatchGenerator<IJsonPatchDocument> _jsonPatchGenerator;

public HomeController(IJsonPatchGenerator<IJsonPatchDocument> jsonPatchGenerator)
{
    _jsonPatchGenerator = jsonPatchGenerator;
}
```

Now you can use it:
```c#
var jsonPatchDocument = _jsonPatchGenerator.Generate(first, second);
```
