using Fluent.AspNetCore.Swagger.Enums;

namespace Fluent.AspNetCore.Swagger;

public class SwaggerConfigurationOptions
{
	public NamingConvention NamingConvention { get; set; }

	public virtual void Freeze() => IsFrozen = true;

	public virtual bool IsFrozen { get; private set; }
}

public sealed class SwaggerConfiguration
{
	private SwaggerConfiguration()
	{
		Options = new SwaggerConfigurationOptions()
		{
			NamingConvention = NamingConvention.CamelCase
		};
	}

	public static SwaggerConfiguration Instance { get; } = new SwaggerConfiguration();

	public SwaggerConfigurationOptions Options { get; }

	public void AddConfiguration(Action<SwaggerConfigurationOptions> onConfiguring)
	{
		if (Options.IsFrozen) return;

		onConfiguring(Options);

		Options.Freeze();
	}

	public static string DefaultSchemaIdSelector(Type modelType)
	{
		// By default the type CollectionResonpone<AccountDto> is named CollectionResonpone[AccountDto]
		// but that name is nor allowed by swagger, so with will create a new name, CollectionResonponeAccountDto.
		if (!modelType.IsConstructedGenericType)
			return modelType.Name;

		var prefix = modelType.GetGenericArguments()
			.Select(genericArg => DefaultSchemaIdSelector(genericArg))
			.Aggregate((previous, current) => previous + current);

		string suffix = modelType.IsGenericType && modelType.GetGenericTypeDefinition() == typeof(Optional<>)
			? "Optional_"
			: modelType.Name.Split('`')[0];

		return suffix + prefix;
	}
}