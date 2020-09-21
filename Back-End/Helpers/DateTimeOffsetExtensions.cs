
using System;

namespace Back_End.Helpers
{
    public static class DateTimeOffsetExtensions
    {

        //Metodo para calcular la Edad en base a la fecha de nacimiento
        public static int GetCurrentAge(this DateTimeOffset dateTimeOffset)
        {
            var currentDate = DateTime.UtcNow;
            int age = currentDate.Year - dateTimeOffset.Year;

            if (currentDate < dateTimeOffset.AddYears(age))
            {
                age--;
            }

            return age;
        }
    }
}
