using AspNetCore.Swagger.Fluent.Annotations.Options;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

namespace Fluent.AspNetCore.Swagger
{
	public static class SwaggerGenOptionsExtensions
	{
		public static SwaggerGenOptions AddFluentAnnotations(
			this SwaggerGenOptions options,
			Action<FluentAnnotationsOptions> annotationsOptions = null)
		{
			if (annotationsOptions is object)
				SwaggerConfiguration.Instance.AddConfiguration(annotationsOptions);

			options.SchemaFilter<FluentAnnotationsFilter>();

			return options;
		}
	}
}