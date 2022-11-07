using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Fluent.AspNetCore.Swagger.Filters
{
	public delegate bool OpenApiSchemaTryLookupByType(Type type, out OpenApiSchema newSchema);

	public class FluentAnnotationsFilterBase
	{
		/// <summary>
		/// Assignment mappings.
		/// </summary>
		private readonly Dictionary<Type, Type[]> _assignmentMappings = new Dictionary<Type, Type[]>()
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

		public void Apply(OpenApiSchema schema, Type contextType, OpenApiSchemaTryLookupByType tryLookupByType)
		{
			OpenApiSchema newSchema = null;

			if (contextType.IsGenericType && contextType.GetGenericTypeDefinition() == typeof(Optional<>))
			{
				tryLookupByType(contextType.GetGenericArguments()[0], out newSchema);
			}
			else
			{
				MethodInfo schemMethod = GetSchemaMethod(contextType);
				if (schemMethod?.ReturnType == typeof(OpenApiSchema))
					newSchema = (OpenApiSchema)schemMethod.Invoke(null, null);
			}

			if (newSchema is object)
				Patch(schema, newSchema);
		}

		private static MethodInfo GetSchemaMethod(Type type)
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
}