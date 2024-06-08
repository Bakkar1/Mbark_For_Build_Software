using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Extension
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            var type = value.GetType();
            var memberInfo = type.GetField(value.ToString());
            var displayAttribute = memberInfo?.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (displayAttribute is not null && displayAttribute?.Length > 0)
            {
                return displayAttribute[0].Name;
            }
            return value.ToString();
        }
    }
}
