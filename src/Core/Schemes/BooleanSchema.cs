using Fluent.AspNetCore.Swagger.Enums;

namespace Fluent.AspNetCore.Swagger.Schemes
{
	/// <summary>
	/// Swagger boolean schema
	/// </summary>
	public class BooleanSchema : BaseSchema
	{
		/// <summary>
		/// Default constructor, initialize the Type as <see cref="DataType.Boolean"/>
		/// </summary>
		public BooleanSchema() : base(DataType.Boolean)
		{
			_openApiSchema.Default = new Microsoft.OpenApi.Any.OpenApiBoolean(false);
		}
	}
}