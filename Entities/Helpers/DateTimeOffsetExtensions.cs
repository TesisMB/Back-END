using System;

namespace Entities.Helpers
{
    public class DateTimeOffsetExtensions
    {
        public static string GetDate(DateTime date)
        {
            var fecha = date.ToString("dd/MM/yyyy");

            return fecha;
        }
        public static string GetDateTime(DateTime date)
        {
            var fecha = date.ToString(@"dd/MM/yyyy hh\:mm\:ss");

            return fecha;
        }

        public static string GetTime(TimeSpan time)
        {
            var hora = time.ToString(@"hh\:mm");

            return hora;
        }
    
    }
}
