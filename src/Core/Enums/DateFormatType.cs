using System.Collections.Generic;

namespace Fluent.AspNetCore.Swagger.Enums
{
	/// <summary>
	/// String format type.
	/// However, format is an open value, so you can use any formats, even not those defined by the OpenAPI Specification, such as:
	/// email, uuid, uri, hostname, ipv4, ipv6 and others
	/// </summary>
	public enum DateFormatType
	{
		/// <summary>
		/// Full-date notation as defined by RFC 3339, section 5.6, for example, 2017-07-21
		/// </summary>
		Date,

		/// <summary>
		/// The date-time notation as defined by RFC 3339, section 5.6, for example, 2017-07-21T17:32:28Z
		/// </summary>
		DateTime
	}

	/// <summary>
	/// String format type.
	/// However, format is an open value, so you can use any formats, even not those defined by the OpenAPI Specification, such as:
	/// email, uuid, uri, hostname, ipv4, ipv6 and others
	/// </summary>
	public enum StringFormatType
	{
		/// <summary>
		/// Full-date notation as defined by RFC 3339, section 5.6, for example, 2017-07-21
		/// </summary>
		Date,

		/// <summary>
		/// The date-time notation as defined by RFC 3339, section 5.6, for example, 2017-07-21T17:32:28Z
		/// </summary>
		DateTime,

		/// <summary>
		/// A hint to UIs to mask the input
		/// </summary>
		Password,

		/// <summary>
		/// base64-encoded characters, for example, U3dhZ2dlciByb2Nrcw==
		/// </summary>
		Byte,

		/// <summary>
		/// Binary data, used to describe files (see Files below)
		/// </summary>
		Binary
	}

	/// <summary>
	/// Swagger string format.
	/// </summary>
	public class StringFormat
	{
		private readonly string _format;

		private readonly IReadOnlyDictionary<StringFormatType, string> _formatsMap = new Dictionary<StringFormatType, string>()
		{
			{ StringFormatType.DateTime, "date-time" }
		};

		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="format"></param>
		public StringFormat(string format)
		{
			_format = format;
		}

		/// <summary>
		/// Initialize the format from <see cref="StringFormatType"/>
		/// </summary>
		/// <param name="stringFormatType"></param>
		public StringFormat(StringFormatType stringFormatType)
		{
			_format = _formatsMap.ContainsKey(stringFormatType) ? _formatsMap[stringFormatType] : stringFormatType.ToString().ToLowerInvariant();
		}

		/// <inheritdoc/>
		public override string ToString() => _format;

		/// <summary>
		/// Convert to string.
		/// </summary>
		/// <param name="stringFormat"></param>
		public static implicit operator string(StringFormat stringFormat)
		{
			return stringFormat.ToString();
		}

		/// <summary>
		/// Convert to <see cref="StringFormat"/> from <see cref="StringFormatType"/>.
		/// </summary>
		/// <param name="stringFormatType">String format type</param>
		public static implicit operator StringFormat(StringFormatType stringFormatType)
		{
			return new StringFormat(stringFormatType);
		}
	}
}