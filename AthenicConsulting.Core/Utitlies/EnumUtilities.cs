using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace AthenicConsulting.Core.Utitlies
{
    public static class EnumUtilities
    {
        public static IEnumerable<T> GetEnums<T>()
        {
            var enumValues = Enum.GetValues(typeof(T)).Cast<T>();
            return enumValues;
        }
        public static string GetDisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()
                            .GetName();
        }
    }
}
