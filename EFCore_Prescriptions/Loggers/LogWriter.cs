using System.Reflection;

namespace EFCore_Prescriptions.Logger
{
    public static class LogWriter
    {
        public static void WriteLog(string text)
        {
            try
            {
                var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                using (StreamWriter w = File.AppendText("C:\\Users\\User\\Desktop\\.NET\\CS_STUDY\\EFCore_Prescriptions\\Loggers\\log.txt"))
                {
                    Log(text, w);
                }
                
            }
            catch (Exception ex)
            {
            }
        }

        public static void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\nLog Entry : ");
                txtWriter.WriteLine("{0}", DateTime.Now);
                txtWriter.WriteLine("  :");
                txtWriter.WriteLine("  :{0}", logMessage);
                txtWriter.WriteLine("-------------------------------");
            }
            catch (Exception ex)
            {
            }
        }
    }
}
