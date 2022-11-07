using AspNetCore.Swagger.Fluent.Annotations.Options;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Fluent.AspNetCore.Swagger;

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