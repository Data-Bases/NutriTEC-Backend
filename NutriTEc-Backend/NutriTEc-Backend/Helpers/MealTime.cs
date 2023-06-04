using System.Runtime.Serialization;

namespace NutriTEc_Backend.Helpers
{
    public enum Mealtime
    {
        [EnumMember(Value = "Breakfast")]
        Breakfast,
        [EnumMember(Value = "Lunch")]
        Lunch,
        [EnumMember(Value = "Dinner")]
        Dinner,
        [EnumMember(Value = "Snack")]
        Snack
    }
}
