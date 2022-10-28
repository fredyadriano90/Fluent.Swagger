using Fluent.AspNetCore.Swagger.Enums;
using Microsoft.OpenApi.Models;

namespace Fluent.AspNetCore.Swagger.Schemes;

public class SchemaRef : BaseSchema
{
	public SchemaRef(OpenApiSchema schema, DataType dataType = DataType.Object) : base(dataType)
	{
		if (string.IsNullOrEmpty(schema?.Title))
			throw new ArgumentNullException(nameof(schema), "The Iitle is required");

		if (schema.Reference is null)
			throw new ArgumentNullException(nameof(schema), "The Reference is required");

		_openApiSchema = schema;
	}
}