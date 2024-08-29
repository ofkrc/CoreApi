using System.Reflection;

namespace EmaAPI.Helpers
{
    public static class GenericMappingHelper
    {
        public static void Map<TSource, TTarget>(TSource source, TTarget target)
        {
            if (source == null || target == null)
            {
                throw new ArgumentNullException("Source or/and Target objects are null");
            }

            PropertyInfo[] sourceProperties = typeof(TSource).GetProperties();
            PropertyInfo[] targetProperties = typeof(TTarget).GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                if (sourceProperty.Name == "RecordId" || sourceProperty.Name == "Id")
                {
                    continue;
                }

                foreach (var targetProperty in targetProperties)
                {
                    if (targetProperty.Name == sourceProperty.Name &&
                        IsMatchingType(sourceProperty.PropertyType, targetProperty.PropertyType) &&
                        targetProperty.CanWrite)
                    {
                        var sourceValue = sourceProperty.GetValue(source);
                        if (sourceValue != null || IsNullableType(targetProperty.PropertyType))
                        {
                            targetProperty.SetValue(target, sourceValue);
                        }
                        break;
                    }
                }
            }
        }

        private static bool IsMatchingType(Type sourceType, Type targetType)
        {
            if (sourceType == targetType)
                return true;

            if (IsNullableType(sourceType) && Nullable.GetUnderlyingType(sourceType) == targetType)
                return true;

            if (IsNullableType(targetType) && Nullable.GetUnderlyingType(targetType) == sourceType)
                return true;

            return false;
        }

        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
    }
}
