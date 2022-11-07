using Fluent.AspNetCore.Swagger.Enums;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Fluent.AspNetCore.Swagger.Schemes
{
	public abstract class BaseSchema
	{
		protected OpenApiSchema _openApiSchema;

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="dataType"></param>
		protected BaseSchema(DataType dataType)
		{
			_openApiSchema = new OpenApiSchema()
			{
				Type = dataType.ToString().ToLowerInvariant()
			};
		}

		public bool IsRequired { get; set; }

		public static OpenApiSchema GetSchemaRef(string referenceName)
		{
			return new OpenApiSchema
			{
				Reference = new OpenApiReference()
				{
					Id = referenceName,
					Type = ReferenceType.Schema,
				}
			};
		}

		/// <summary>
		/// Convert to Schema
		/// </summary>
		/// <param name="baseSchema"></param>
		public static implicit operator OpenApiSchema(BaseSchema baseSchema) => baseSchema.Build();

		/// <summary>
		/// Build full schema
		/// </summary>
		public virtual OpenApiSchema Build()
		{
			_openApiSchema.Nullable = !IsRequired;

			return _openApiSchema;
		}

		public BaseSchema SetDefault(IOpenApiAny @default)
		{
			_openApiSchema.Default = @default;
			return this;
		}

		public BaseSchema SetDescription(string description)
		{
			_openApiSchema.Description = description;
			return this;
		}

		public BaseSchema SetReadonly(bool @readonly)
		{
			_openApiSchema.ReadOnly = @readonly;
			return this;
		}

		public BaseSchema SetReference(OpenApiReference reference)
		{
			_openApiSchema.Reference = reference;
			return this;
		}

		public BaseSchema SetTitle(string title)
		{
			_openApiSchema.Title = title;
			return this;
		}

		public BaseSchema SetType(string type)
		{
			_openApiSchema.Type = type;
			return this;
		}
	}
}