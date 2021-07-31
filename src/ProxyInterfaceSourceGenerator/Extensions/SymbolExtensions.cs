﻿using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using ProxyInterfaceSourceGenerator.Enums;

namespace ProxyInterfaceSourceGenerator.Extensions
{
    internal static class SymbolExtensions
    {
        public static bool IsKeywordOrReserved(this ISymbol symbol)
        {
            return SyntaxFacts.GetKeywordKind(symbol.Name) != SyntaxKind.None || SyntaxFacts.GetContextualKeywordKind(symbol.Name) != SyntaxKind.None;
        }

        public static string SanitizedName(this ISymbol symbol)
        {
            return symbol.IsKeywordOrReserved() ? $"@{symbol.Name}" : symbol.Name;
        }        

        public static string GetRefPrefix(this IParameterSymbol ps)
        {
            switch (ps.RefKind)
            {
                case RefKind.In:
                    return "in ";

                case RefKind.Out:
                    return "out ";

                case RefKind.Ref:
                    return "ref ";

                default:
                    return string.Empty;
            }
        }

        public static string GetParamsPrefix(this IParameterSymbol ps)
        {
            return ps.IsParams ? "params " : string.Empty;
        }

        public static TypeEnum GetTypeEnum(this IPropertySymbol p)
        {
            return GetTypeEnum(p.Type);
        }

        public static TypeEnum GetTypeEnum(this IParameterSymbol p)
        {
            return GetTypeEnum(p.Type);
        }

        public static TypeEnum GetTypeEnum(this ITypeSymbol ts)
        {
            if (ts.IsValueType || ts.IsString())
            {
                return TypeEnum.ValueTypeOrString;
            }

            if (ts.TypeKind == TypeKind.Interface)
            {
                return TypeEnum.Interface;
            }

            return TypeEnum.Complex;
        }

        public static bool IsString(this ITypeSymbol ts)
        {
            return ts.ToString() == "string" || ts.ToString() == "string?";
        }

        public static string ToPropertyText(this IPropertySymbol property, string? overrideType = null)
        {
            var get = property.GetMethod != null ? "get; " : string.Empty;
            var set = property.SetMethod != null ? "set; " : string.Empty;

            var type = !string.IsNullOrEmpty(overrideType) ? overrideType : $"{property.Type}";

            return $"{type} {property.SanitizedName()} {{ {get}{set}}}";
        }

        public static string ToPropertyTextForClass(this IPropertySymbol property)
        {
            var get = property.GetMethod != null ? $"get => _Instance.{property.SanitizedName()}; " : string.Empty;
            var set = property.SetMethod != null ? $"set => _Instance.{property.SanitizedName()} = value; " : string.Empty;

            return $"{property.Type} {property.SanitizedName()} {{ {get}{set}}}";
        }

        public static string ToPropertyTextForClass(this IPropertySymbol property, string overrideType)
        {
            var get = property.GetMethod != null ? $"get => _mapper.Map<{overrideType}>(_Instance.{property.SanitizedName()}); " : string.Empty;
            var set = property.SetMethod != null ? $"set => _Instance.{property.SanitizedName()} = _mapper.Map<{property.Type}>(value); " : string.Empty;

            return $"{overrideType} {property.SanitizedName()} {{ {get}{set}}}";
        }
    }
}
