using System.Text;
using Fluent.AspNetCore.Swagger.Enums;

namespace Fluent.AspNetCore.Swagger.Schemes;

/// <summary>
/// Swagger object schema
/// </summary>
public class ObjectSchema : BaseSchema
{
	/// <summary>
	/// Default constructor, initialize the type property as <see cref="DataType.Object"/>
	/// </summary>
	public ObjectSchema(string title) : base(DataType.Object)
	{
		_openApiSchema.Title = title;
	}

	/// <summary>
	/// Add properties to the properties collection
	/// </summary>
	/// <param name="properties">Object properties</param>
	/// <exception cref="ArgumentNullException">Missing property name.</exception>
	public ObjectSchema Properties(params (string propertyName, BaseSchema schema)[] properties)
	{
		if (properties == null)
			throw new ArgumentNullException(nameof(properties));

		foreach ((string propertyName, BaseSchema baseSchema) in properties)
		{
			if (string.IsNullOrWhiteSpace(propertyName))
				throw new ArgumentNullException(nameof(propertyName));

			var schema = baseSchema.Build();

			if (string.IsNullOrWhiteSpace(schema.Title))
				schema.Title = propertyName;

			string newPropertyName = GetPropertyName(propertyName);

			_openApiSchema.Properties[newPropertyName] = schema;

			if (baseSchema.IsRequired && !_openApiSchema.Required.Contains(newPropertyName))
				_openApiSchema.Required.Add(newPropertyName);
		}

		return this;
	}

	/// <summary>
	/// Mark a list of properties as required
	/// </summary>
	/// <param name="properties">Required properties</param>
	public ObjectSchema RequiredProperties(params string[] properties)
	{
		if (properties == null)
			throw new ArgumentNullException(nameof(properties));

		foreach (var propertyName in properties)
		{
			if (string.IsNullOrWhiteSpace(propertyName))
				throw new ArgumentException("The properties collection cannot contains empty entries");

			string newPropertyName = GetPropertyName(propertyName);

			if (_openApiSchema.Required.Contains(newPropertyName))
				continue;

			_openApiSchema.Required.Add(newPropertyName);
		}

		return this;
	}

	private static string ReplaceFirstChar(string propertyName, char firstChar)
	{
		var stringBuilder = new StringBuilder(propertyName.Length);

		stringBuilder.Append(firstChar);

		if (propertyName.Length > 1)
			stringBuilder.Append(propertyName, 1, propertyName.Length - 1);

		return stringBuilder.ToString();
	}

	private static string GetPropertyName(string propertyName)
	{
		if (SwaggerConfiguration.Instance.Options.NamingConvention == NamingConvention.CamelCase)
		{
			// propertyName: testIt => testIt do nothing
			char firstCharLower = char.ToLowerInvariant(propertyName[0]);
			if (firstCharLower == propertyName[0])
				return propertyName;

			// propertyName: TestIt => testIt do nothing
			return ReplaceFirstChar(propertyName, firstCharLower);
		}

		if (SwaggerConfiguration.Instance.Options.NamingConvention == NamingConvention.PascalCase)
		{
			// propertyName: TestIt => TestIt do nothing
			char firstCharUpper = char.ToUpperInvariant(propertyName[0]);
			if (firstCharUpper == propertyName[0])
				return propertyName;

			// propertyName: testIt => testIt
			return ReplaceFirstChar(propertyName, firstCharUpper);
		}

		throw new InvalidOperationException("UnHandle  NamingConvention");
	}
}