using System;

namespace Persistencia.Utils
{
    public class Formateador
    {

        public static string FormatearFecha(DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss.FFFFFFF");
        }
    }
}
