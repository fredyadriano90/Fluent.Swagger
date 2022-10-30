using AspNetCore.Swagger.Fluent.Annotations.Options;
using Fluent.AspNetCore.Swagger;
using Fluent.AspNetCore.Swagger.Filters;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AspNetCore.Swagger.Fluent.Annotations.Extensions;

public static class SwaggerGenOptionsExtensions
{
	public static SwaggerGenOptions AddFluentAnnotations(
		this SwaggerGenOptions options,
		Action<FluentAnnotationsOptions>? annotationsOptions = null)
	{
		if (annotationsOptions is not null)
			SwaggerConfiguration.Instance.AddConfiguration(annotationsOptions);

		options.SchemaFilter<FluentAnnotationsFilter>();

		return options;
	}
}