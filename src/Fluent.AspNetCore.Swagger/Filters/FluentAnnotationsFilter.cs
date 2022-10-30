using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

namespace Fluent.AspNetCore.Swagger.Filters;

public class FluentAnnotationsFilter : ISchemaFilter
{
	/// <summary>
	/// Assignment mappings.
	/// </summary>
	private readonly Dictionary<Type, Type[]> _assignmentMappings = new()
	{
		{typeof(DateTime), new[] {typeof(DateTime?)}},
		{typeof(decimal), new[] {typeof(decimal?)}},
		{typeof(double), new[] {typeof(double?)}},
		{typeof(bool), new[] {typeof(bool?)}},
		{typeof(int), new[] {typeof(int?), typeof(int), typeof(short?), typeof(short)}},
		{typeof(short), new[] {typeof(short?)}},
		{typeof(long), new[] {typeof(long?), typeof(int?), typeof(int), typeof(short?), typeof(short)}},
		{typeof(uint), new[] {typeof(uint?), typeof(uint), typeof(ushort?), typeof(ushort)}},
		{typeof(ushort), new[] {typeof(ushort?)}},
		{typeof(ulong), new[] {typeof(ulong?), typeof(uint?), typeof(uint), typeof(ushort?), typeof(ushort)}}
	};

	public void Apply(OpenApiSchema schema, SchemaFilterContext context)
	{
		OpenApiSchema? newSchema = null;

		if (context.Type.IsGenericType && context.Type.GetGenericTypeDefinition() == typeof(Optional<>))
		{
			context.SchemaRepository.TryLookupByType(context.Type.GetGenericArguments()[0], out newSchema);
		}
		else
		{
			MethodInfo? schemMethod = GetSchemaMethod(context.Type);
			if (schemMethod?.ReturnType == typeof(OpenApiSchema))
				newSchema = (OpenApiSchema)schemMethod.Invoke(null, null)!;
		}

		if (newSchema is not null)
			Patch(schema, newSchema);
	}

	private static MethodInfo? GetSchemaMethod(Type type)
	{
		return type.GetMethod("GetSchema", BindingFlags.Public | BindingFlags.Static);
	}

	private bool CanBeAssigned(Type targetType, Type sourceType)
	{
		return targetType == sourceType
			|| targetType.IsAssignableFrom(sourceType)
			|| (_assignmentMappings.ContainsKey(targetType) && _assignmentMappings[targetType].Any(st => sourceType == st));
	}

	private void Patch<TTarget, TSource>(TTarget target, TSource source)
	{
		PropertyInfo[] sourceFields = typeof(TSource).GetProperties(BindingFlags.Instance | BindingFlags.Public);
		PropertyInfo[] targetFields = typeof(TTarget).GetProperties(BindingFlags.Instance | BindingFlags.Public);

		foreach (PropertyInfo sourceField in sourceFields)
		{
			var sourceFieldValue = sourceField.GetValue(source);
			if (sourceFieldValue is null) continue;

			var targetField = Array.Find(targetFields, x => x.Name == sourceField.Name);
			if (targetField is null) continue;

			if (CanBeAssigned(targetField.PropertyType, sourceField.PropertyType))
			{
				var targetFieldValue = targetField.GetValue(target);

				if (!sourceFieldValue.Equals(targetFieldValue))
					targetField.SetValue(target, sourceFieldValue);
			}
		}
	}
}