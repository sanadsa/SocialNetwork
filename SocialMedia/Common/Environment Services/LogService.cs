using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Environment_Services
{
    public static class LogService
    {
        public static void WriteExceptionsToLogger(System.Exception ex)
        {
            Logger.GetInstance().LogWrite(ex.Source);
            Logger.GetInstance().LogWrite(ex.StackTrace);
        }

        public static void WriteToLogger(string message)
        {
            Logger.GetInstance().LogWrite(message);
        }
    }
}
