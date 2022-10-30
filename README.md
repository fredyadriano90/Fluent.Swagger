# AspNetCore.Swagger.Fluent.Annotations

## Getting Started

1. Install the standard Nuget package into your ASP.NET Core application.

    ```text
    Package Manager : Install-Package AspNetCore.Swagger.Fluent.Annotations --version 1.0.3
    CLI : dotnet add package AspNetCore.Swagger.Fluent.Annotations --version 1.0.3
    ```

2. In the `ConfigureServices` method of `Startup.cs`, in the method to register the Swagger generator, add `AddFluentAnnotations`.

    ```csharp
    using Microsoft.OpenApi.Models;
    namespace AspNetCore.Swagger.Fluent.Annotations.Extensions;
    ```

    ```csharp
    services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
        options.AddFluentAnnotations(annotationsOptions =>
        {
            annotationsOptions.NamingConvention = Fluent.AspNetCore.Swagger.Enums.CamelCase;
        });
    });
    ```

3. Use it in your models

```csharp
    public class LoginResponse
    {
        public bool AllowRememberLogin { get; set; }
        public string? ExternalLoginScheme { get; set; }
        public string ReturnUrl { get; set; }
        public IEnumerable<ExternalProvider> ExternalProviders { get; set; }

        public static OpenApiSchema GetSchema()
        {
            return new ObjectSchema(nameof(LoginResponse))
                .Description("Login response")
                .Properties(
                    (
                        nameof(AllowRememberLogin),
                        new BooleanSchema().Description("Allow remember login enabled").Required().Readonly()),
                    (
                        nameof(ExternalLoginScheme),
                        new StringSchema().Description("External login schema").Readonly()),
                    (
                        nameof(ReturnUrl),
                        new StringSchema().Description("Return url to go after login").Readonly()),
                    (
                        nameof(ExternalProviders),
                        new ArraySchema(ExternalProvider.SchemaRef).Description("External provides available").Required().Readonly())
                );
        }
    }

    public class ExternalProvider
    {
        public string DisplayName { get; set; }
        public string AuthenticationScheme { get; set; }

        public static OpenApiSchema SchemaRef => BaseSchema.GetSchemaRef(nameof(ExternalProvider));

        public static OpenApiSchema GetSchema()
        {
            return new ObjectSchema(nameof(ExternalProvider))
                .Description("External Provider")
                .Properties(
                    (
                        nameof(DisplayName),
                        new StringSchema().Description("Display Name").Required().Readonly()),
                    (
                        nameof(AuthenticationScheme),
                        new StringSchema().Description("Authentication Scheme").Required().Readonly()));
        }
    }
```