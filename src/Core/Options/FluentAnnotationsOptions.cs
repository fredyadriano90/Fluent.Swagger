using Fluent.AspNetCore.Swagger.Enums;

namespace AspNetCore.Swagger.Fluent.Annotations.Options
{
	public class FluentAnnotationsOptions
	{
		public NamingConvention NamingConvention { get; set; }

		public virtual void Freeze() => IsFrozen = true;

		public virtual bool IsFrozen { get; private set; }
	}
}