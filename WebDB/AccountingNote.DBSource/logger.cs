using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountingNote.DBSource
{
    public class logger
    {
        public static void WriteLog(Exception ex)
        {
            string msg = $@"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}
                        {ex.ToString()}
                ";
            System.IO.File.AppendAllText("C:\\Logs\\Log.log", ex.ToString());

            throw ex;
        }
    }
}
