# Publish package

.\nuget.exe pack .\AspNetCore.Swagger.Fluent.Annotations.nuspec -Symbols -SymbolPackageFormat snupkg

.\nuget.exe push AspNetCore.Swagger.Fluent.Annotations.1.0.4.nupkg -Source https://api.nuget.org/v3/index.json -ApiKey ---

