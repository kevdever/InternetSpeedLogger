using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternetSpeedLogger.Database
{
    public static class ExceptionHelpers
    {
        /// <summary>
        /// Concats all exceptions into one string, allowing a collapsed view of the outer and inner exceptions.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static string ConcatInnerExceptions(Exception e)
        {
            if (e is null)
                return null;
            var str = $"{e.GetType().ToString()}: Message: {e.Message}.  *** Stack trace: {e.StackTrace}. ***  ";

            if (e.InnerException != null)
                str += ConcatInnerExceptions(e.InnerException);

            return str;
        }
    }
}
