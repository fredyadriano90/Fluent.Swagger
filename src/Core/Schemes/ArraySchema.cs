using Fluent.AspNetCore.Swagger.Enums;
using Microsoft.OpenApi.Models;
using System;

namespace Fluent.AspNetCore.Swagger.Schemes
{
	/// <summary>
	/// Swagger array schema
	/// </summary>
	public class ArraySchema : BaseSchema
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public ArraySchema(OpenApiSchema itemsSchema) : base(DataType.Array)
		{
			_openApiSchema.UniqueItems = true;
			_openApiSchema.Items = itemsSchema
				?? throw new ArgumentNullException(nameof(itemsSchema), "The 'ItemsScheme' is required");
		}

		/// <summary>
		/// Set the minimum items
		/// </summary>
		/// <param name="minItems"></param>
		public ArraySchema MinItems(int minItems)
		{
			if (minItems < 0)
				throw new ArgumentException("The minItems must be greater than 0");

			_openApiSchema.MinItems = minItems;
			return this;
		}

		/// <summary>
		/// Set the maximum items
		/// </summary>
		/// <param name="maxItems"></param>
		public ArraySchema MaxItems(int maxItems)
		{
			if (maxItems < 0)
				throw new ArgumentException("The maxItems must be greater than 0");

			_openApiSchema.MaxItems = maxItems;
			return this;
		}

		/// <summary>
		/// Mark the items as unique
		/// </summary>
		/// <param name="unique">Items unique</param>
		public ArraySchema UniqueItems(bool unique)
		{
			_openApiSchema.UniqueItems = unique;
			return this;
		}
	}
}