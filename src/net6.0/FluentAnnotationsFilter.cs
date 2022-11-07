using Fluent.AspNetCore.Swagger.Filters;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fluent.AspNetCore.Swagger;

public class FluentAnnotationsFilter : FluentAnnotationsFilterBase, ISchemaFilter
{
	public void Apply(OpenApiSchema schema, SchemaFilterContext context)
	{
		Apply(schema, context.Type, context.SchemaRepository.TryLookupByType);
	}
}