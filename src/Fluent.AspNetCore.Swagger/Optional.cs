namespace Fluent.AspNetCore.Swagger;

/// <summary>
/// Optional Value Wrapper.
/// </summary>
/// <typeparam name="T">Value type.</typeparam>
public class Optional<T>
{
	private T _value;

	/// <summary>
	/// Parameterless Constructor.
	/// </summary>
	public Optional()
	{ }

	/// <summary>
	/// Constructor.
	/// </summary>
	/// <param name="val"></param>
	public Optional(T val)
	{
		Value = val;
	}

	/// <summary>
	/// Determines if the optional property has a value or not.
	/// </summary>
	public bool HasValue { get; set; }

	/// <summary>
	/// Value.
	/// </summary>
	public T Value
	{
		get => _value;

		set
		{
			HasValue = true;
			_value = value;
		}
	}

	/// <inheritdoc/>
	public override int GetHashCode() => HasValue ? _value.GetHashCode() : 0;

	/// <inheritdoc/>
	public override string ToString() => HasValue ? _value.ToString() : "";
}