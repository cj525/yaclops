﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;


namespace Yaclops.Extensions
{
    internal static class PropertyInfoExtensions
    {
        public static bool IsBool(this PropertyInfo info)
        {
            return info.PropertyType == typeof(bool);
        }


        public static bool IsList(this PropertyInfo info)
        {
            return info.PropertyType.IsGenericType && (info.PropertyType.GetGenericTypeDefinition() == typeof(List<>));
        }


        public static bool IsHashSet(this PropertyInfo info)
        {
            return info.PropertyType.IsGenericType && (info.PropertyType.GetGenericTypeDefinition() == typeof(HashSet<>));
        }


        public static bool IsRequired(this PropertyInfo info)
        {
            return FindAttribute<RequiredAttribute>(info).Any();
        }


        public static IList<T> FindAttribute<T>(this PropertyInfo prop)
        {
            var atts = prop.GetCustomAttributes(typeof(T), true);

            return atts.Cast<T>().ToList();
        }
    }
}
