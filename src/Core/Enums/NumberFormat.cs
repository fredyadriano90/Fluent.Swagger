namespace Fluent.AspNetCore.Swagger.Enums
{
	/// <summary>
	/// Floating-points numbers format.
	/// </summary>
	public enum NumberFormat
	{
		/// <summary>
		/// Any numbers.
		/// </summary>
		Number = 0,

		/// <summary>
		/// Floating-point numbers.
		/// </summary>
		Float = 1,

		/// <summary>
		/// Floating-point numbers with double precision.
		/// </summary>
		Double = 2,

		/// <summary>
		/// Integer numbers.
		/// </summary>
		Integer = 3,

		/// <summary>
		/// Signed 32-bit integers (commonly used integer type).
		/// </summary>
		Int32 = 4,

		/// <summary>
		/// Signed 64-bit integers (long type).
		/// </summary>
		Int64 = 5
	}
}