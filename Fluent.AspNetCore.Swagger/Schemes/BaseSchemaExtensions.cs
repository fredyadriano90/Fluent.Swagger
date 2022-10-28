using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;

namespace Fluent.AspNetCore.Swagger.Schemes;

/// <summary>
/// Base schema extensions.
/// </summary>
public static class BaseSchemaExtensions
{
	/// <summary>
	/// Set schema title.
	/// </summary>
	/// <typeparam name="T">Schema type.</typeparam>
	/// <param name="schema">Schema instance.</param>
	/// <param name="title">Title.</param>
	public static T Title<T>(this T schema, string title)
		where T : BaseSchema
	{
		schema.SetTitle(title);
		return schema;
	}

	/// <summary>
	/// Set schema title.
	/// </summary>
	/// <typeparam name="T">Schema type.</typeparam>
	/// <param name="schema">Schema instance.</param>
	/// <param name="title">Title.</param>
	public static T SetTitle<T>(this T schema, string title)
		where T : OpenApiSchema
	{
		schema.Title = title;
		return schema;
	}

	/// <summary>
	/// Set schema $ref.
	/// </summary>
	/// <typeparam name="T">Schema type.</typeparam>
	/// <param name="schema">Schema instance.</param>
	/// <param name="reference">Ref.</param>
	public static T Ref<T>(this T schema, OpenApiReference reference)
		where T : BaseSchema
	{
		schema.SetReference(reference);
		return schema;
	}

	/// <summary>
	/// Set schema type.
	/// </summary>
	/// <typeparam name="T">Schema type.</typeparam>
	/// <param name="schema">Schema instance.</param>
	/// <param name="type">Type.</param>
	public static T Type<T>(this T schema, string type)
		where T : BaseSchema
	{
		schema.SetType(type);
		return schema;
	}

	/// <summary>
	/// Set schema description.
	/// </summary>
	/// <typeparam name="T">Schema type.</typeparam>
	/// <param name="schema">Schema instance.</param>
	/// <param name="description">Description.</param>
	public static T Description<T>(this T schema, string description)
		where T : BaseSchema
	{
		schema.SetDescription(description);
		return schema;
	}

	/// <summary>
	/// Set schema readOnly.
	/// </summary>
	/// <typeparam name="T">Schema type.</typeparam>
	/// <param name="schema">Schema instance.</param>
	public static T Readonly<T>(this T schema, bool @readonly = true)
		where T : BaseSchema
	{
		schema.SetReadonly(@readonly);
		return schema;
	}

	/// <summary>
	/// Set schema Required.
	/// </summary>
	/// <typeparam name="T">Schema type.</typeparam>
	/// <param name="schema">Schema instance.</param>
	public static T Required<T>(this T schema, bool required = true)
		where T : BaseSchema
	{
		schema.IsRequired = required;
		return schema;
	}

	/// <summary>
	/// Set schema default value.
	/// </summary>
	/// <typeparam name="T">Schema type.</typeparam>
	/// <param name="schema">Schema instance.</param>
	/// <param name="default">Default value.</param>
	public static T Default<T>(this T schema, IOpenApiAny @default)
		where T : BaseSchema
	{
		schema.SetDefault(@default);
		return schema;
	}
}