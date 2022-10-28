using Fluent.AspNetCore.Swagger.Enums;

namespace Fluent.AspNetCore.Swagger.Schemes;

/// <summary>
/// Swagger number schema
/// </summary>
public class NumberSchema : BaseSchema
{
	/// <summary>
	/// Default constructor, initialize the Type as <see cref="DataType.Number"/>
	/// </summary>
	public NumberSchema(NumberFormat format)
		: base((int)format <= 2 ? DataType.Number : DataType.Integer)
	{
		if (format != NumberFormat.Number)
			_openApiSchema.Format = format.ToString().ToLowerInvariant();
	}

	/// <summary>
	/// Exclude the maximum value from the comparison
	/// </summary>
	/// <param name="exclude"></param>
	/// <example>
	/// exclusiveMaximum: false or not included value ≤ maximum
	/// exclusiveMaximum: true 	maximum > value
	/// </example>
	public NumberSchema ExclusiveMaximum(bool exclude)
	{
		_openApiSchema.ExclusiveMaximum = exclude;
		return this;
	}

	/// <summary>
	/// Exclude the minimum value from the comparison
	/// </summary>
	/// <param name="exclude"></param>
	/// <example>
	/// exclusiveMinimum: false or not included value ≥ minimum
	/// exclusiveMinimum: true 	value > minimum
	/// </example>
	public NumberSchema ExclusiveMinimum(bool exclude)
	{
		_openApiSchema.ExclusiveMinimum = exclude;
		return this;
	}

	/// <summary>
	/// Set maximum allow value
	/// </summary>
	/// <param name="maximum"></param>
	public NumberSchema Maximum(int maximum)
	{
		_openApiSchema.Maximum = maximum;
		return this;
	}

	/// <summary>
	/// Set minimum allow value
	/// </summary>
	/// <param name="minimum"></param>
	public NumberSchema Minimum(int minimum)
	{
		_openApiSchema.Minimum = minimum;
		return this;
	}

	/// <summary>
	/// Specify that a number must be the multiple of another number
	/// </summary>
	/// <param name="multipleOf"></param>
	public NumberSchema MultipleOf(decimal multipleOf)
	{
		if (multipleOf < 0)
			throw new ArgumentException("The multipleOf value cannot be lower than 0");

		_openApiSchema.MultipleOf = multipleOf;
		return this;
	}
}