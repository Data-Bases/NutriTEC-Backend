using System.Runtime.Serialization;

namespace NutriTEc_Backend.Helpers
{
    public enum DayOfTheWeek
    {
        [EnumMember(Value = "Monday")]
        Monday,
        [EnumMember(Value = "Tuesday")]
        Tuesday,
        [EnumMember(Value = "Wednesday")]
        Wednesday,
        [EnumMember(Value = "Thursday")]
        Thursday,
        [EnumMember(Value = "Friday")]
        Friday,
        [EnumMember(Value = "Saturday")]
        Saturday,
        [EnumMember(Value = "Sunday")]
        Sunday
    }
}
