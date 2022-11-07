using Fluent.AspNetCore.Swagger.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fluent.AspNetCore.Swagger.Schemes
{
	/// <summary>
	/// Swagger string schema
	/// </summary>
	public class StringSchema : BaseSchema
	{
		private IList<object> _enum;

		/// <summary>
		/// Default constructor
		/// </summary>
		public StringSchema() : base(DataType.String)
		{
		}

		/// <summary>
		/// Enum.
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		public StringSchema Enum<T>()
			where T : struct
		{
			if (!typeof(T).IsEnum)
				throw new ArgumentException($"Invalid type: {typeof(T)}, the type must be an Enum");

			if (_enum == null)
				_enum = new List<object>();

			var names = System.Enum
				.GetNames(typeof(T))
				.Where(it => !it.Equals("IgnoredBySwagger", StringComparison.OrdinalIgnoreCase));

			foreach (object name in names)
				_enum.Add(name);

			return this;
		}

		/// <summary>
		/// Enum.
		/// </summary>
		public StringSchema Enum(string[] enums)
		{
			if (enums == null)
				_enum = new List<object>();

			foreach (string name in enums)
				_enum.Add(name);

			return this;
		}

		/// <summary>
		/// Set the format
		/// </summary>
		/// <param name="stringFormat"></param>
		public StringSchema Format(StringFormat stringFormat)
		{
			_openApiSchema.Format = stringFormat;
			return this;
		}

		/// <summary>
		/// Set the exact length.
		/// </summary>
		/// <param name="length">Exact length.</param>
		public StringSchema Length(int length)
		{
			_openApiSchema.MinLength = length;
			_openApiSchema.MaxLength = length;
			return this;
		}

		/// <summary>
		/// Set the minimum and maximum length.
		/// </summary>
		/// <param name="minLength">Min length.</param>
		/// <param name="maxLength">Max length.</param>
		public StringSchema Length(int minLength, int maxLength)
		{
			_openApiSchema.MinLength = minLength;
			_openApiSchema.MaxLength = maxLength;
			return this;
		}

		/// <summary>
		/// Set the maximum length
		/// </summary>
		/// <param name="maxLength"></param>
		public StringSchema MaxLength(int maxLength)
		{
			_openApiSchema.MaxLength = maxLength;
			return this;
		}

		/// <summary>
		/// Set the minimum length
		/// </summary>
		/// <param name="minLength"></param>
		public StringSchema MinLength(int minLength)
		{
			_openApiSchema.MinLength = minLength;
			return this;
		}

		/// <summary>
		/// The pattern keyword lets you define a regular expression template for the string value.
		/// Only the values that match this template will be accepted.
		/// The regular expression syntax used is from JavaScript (more specifically, ECMA 262).
		/// Regular expressions are case-sensitive, that is, [a-z] and [A-Z] are different expressions.
		/// For example, the following pattern matches a Social Security Number (SSN) in the 123-45-6789 format: ^\d{3}-\d{2}-\d{4}$
		/// </summary>
		/// <param name="pattern"></param>
		public StringSchema Pattern(string pattern)
		{
			_openApiSchema.Pattern = pattern;
			return this;
		}
	}
}