using System.Reflection;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace TradingGlossary.API;

public sealed class ForceAllPropertiesRequiredAndRespectNullabilitySchemaFilter : ISchemaFilter
{
    private static readonly NullabilityInfoContext _nullability = new();

    public void Apply(IOpenApiSchema schemaInterface, SchemaFilterContext context)
    {
        if (schemaInterface is not OpenApiSchema schema)
            return;

        if (schema.Properties == null || schema.Properties.Count == 0)
            return;

        // 1) Alle Properties required -> im TS kein "undefined"
        schema.Required ??= new HashSet<string>();
        foreach (var name in schema.Properties.Keys)
            schema.Required.Add(name);

        // 2) Nullability strikt nach CLR-Typ (nur ? darf null)
        var clrProps = context.Type.GetProperties(BindingFlags.Public | BindingFlags.Instance);

        foreach (var clrProp in clrProps)
        {
            var jsonName = GetJsonName(clrProp);

            if (!schema.Properties.TryGetValue(jsonName, out var propInterface))
                continue;

            if (propInterface is not OpenApiSchema propSchema)
                continue;

            var isNullable = IsNullable(clrProp);

            if (propSchema.Type.HasValue)
            {
                if (isNullable)
                    propSchema.Type |= JsonSchemaType.Null;
                else
                    propSchema.Type &= ~JsonSchemaType.Null;
            }
            else
            {
                // Wenn Type nicht gesetzt ist, nur im nullable-Fall Null hinzufügen
                if (isNullable)
                    propSchema.Type = JsonSchemaType.Null;
            }
        }
    }

    private static bool IsNullable(PropertyInfo prop)
    {
        var t = prop.PropertyType;

        // Nullable<T>
        if (t.IsValueType)
            return Nullable.GetUnderlyingType(t) != null;

        // Reference Types (string vs string?)
        var n = _nullability.Create(prop);
        return n.ReadState == NullabilityState.Nullable;
    }

    private static string GetJsonName(PropertyInfo prop)
    {
        // Attribute Overrides
        var name = prop.GetCustomAttribute<JsonPropertyNameAttribute>()?.Name
                   ?? prop.GetCustomAttribute<DataMemberAttribute>()?.Name;

        if (!string.IsNullOrWhiteSpace(name))
            return name;

        // Default System.Text.Json policy (camelCase)
        return JsonNamingPolicy.CamelCase.ConvertName(prop.Name);
    }
}
