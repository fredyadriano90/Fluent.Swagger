namespace Fluent.AspNetCore.Swagger.Enums;

/// <summary>
/// Swagger data models (Schemas)
/// </summary>
/// <![CDATA[https://swagger.io/docs/specification/data-models/data-types/]]>
public enum DataType
{
	/// <summary>
	/// Represent an <see cref="string"/> text, this includes <see cref="DateTime"/> and files.
	/// </summary>
	String,

	/// <summary>
	/// Represent a <see cref="float"/>, a <see cref="double"/> or a <see cref="decimal"/>.
	/// </summary>
	Number,

	/// <summary>
	/// Represent a <see cref="int"/>, a <see cref="long"/>.
	/// </summary>
	Integer,

	/// <summary>
	/// Represents a <see cref="bool"/>
	/// Note that truthy and falsy values such as "true", "", 0 or null are not considered boolean values.
	/// </summary>
	Boolean,

	/// <summary>
	/// Represent an array of items
	/// </summary>
	Array,

	/// <summary>
	/// An object is a collection of property/value pairs.
	/// The properties keyword is used to define the object properties
	/// – you need to list the property names and specify a Schema for each property.
	/// </summary>
	Object
}