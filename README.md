# JsonPatchGenerator
A generator that creates a JSON Patch Document from comparing two objects

Work in progress

## Installation
### via Package Manager
```powershell
PM> Install-Package JsonPatchGenerator.AspNetCore
```

### via dotnet CLI
```bash
> dotnet add package JsonPatchGenerator.AspNetCore
```

## Usage
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
