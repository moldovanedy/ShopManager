using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ShopManager.Controller
{
    public static class Logger
    {
        public static bool IsLoggingAvailable { get; private set; } = true;

        private static bool _isInitialized = false;

        public static void LogDebug(string message)
        {
#if DEBUG
            Initialize();
            WriteToFile($"DEBUG ({DateTime.Now}): {message} STACK {Environment.StackTrace}");
#endif
        }

        public static void LogInfo(string message)
        {
            Initialize();

            string stackTrace = "";
#if DEBUG
            stackTrace = Environment.StackTrace;
#endif

            WriteToFile($"INFO ({DateTime.Now}): {message}{(stackTrace != "" ? (" STACK: " + stackTrace) : "")}");
        }

        public static void LogWarning(string message)
        {
            Initialize();

            string stackTrace = "";
#if DEBUG
            stackTrace = Environment.StackTrace;
#endif

            WriteToFile($"WARNING ({DateTime.Now}): {message}{(stackTrace != "" ? (" STACK: " + stackTrace) : "")}");
        }

        public static void LogError(string message)
        {
            Initialize();

            string stackTrace = "";
#if DEBUG
            stackTrace = Environment.StackTrace;
#endif

            WriteToFile($"ERROR ({DateTime.Now}): {message}{(stackTrace != "" ? (" STACK: " + stackTrace) : "")}");
        }

        public static void Flush()
        {
            Trace.Flush();
        }

        private static void Initialize()
        {
            try
            {
                if (_isInitialized)
                {
                    return;
                }

                _isInitialized = true;

                string logsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
                if (!Directory.Exists(logsPath))
                {
                    Directory.CreateDirectory(logsPath);
                }

                IEnumerable<string> files = Directory.EnumerateFiles(logsPath);
                DateTime oldestTime = DateTime.MaxValue;
                string fileToDelete = "";
                int numberOfFiles = 0;

                //search through all the logs for the oldest file;
                //if there are 5 files or more, delete the oldest one
                foreach (string filePath in files)
                {
                    numberOfFiles++;
                    string fileName = filePath.Substring(
                        Math.Max(filePath.LastIndexOf('\\'), filePath.LastIndexOf('/')) + 1);

                    //it means the file was renamed to something else, just delete it
                    if (fileName.Length != 23)
                    {
                        File.Delete(filePath);
                        continue;
                    }

                    //parse the creation time from the name, if the name is not valid, delete the file
                    DateTime writeDate = DateTime.MaxValue;
                    try
                    {
                        writeDate = new DateTime(
                            int.Parse(fileName.Substring(0, 4)),
                            int.Parse(fileName.Substring(5, 2)),
                            int.Parse(fileName.Substring(8, 2)),
                            int.Parse(fileName.Substring(11, 2)),
                            int.Parse(fileName.Substring(14, 2)),
                            int.Parse(fileName.Substring(17, 2)));
                    }
                    catch
                    {
                        File.Delete(filePath);
                    }

                    if (writeDate < oldestTime)
                    {
                        oldestTime = writeDate;
                        fileToDelete = filePath;
                    }
                }

                if (numberOfFiles >= 5 && fileToDelete != "")
                {
                    File.Delete(fileToDelete);
                }

                string newFilePath = Path.Combine(logsPath, DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss") + ".log");
                FileStream fs = File.Create(newFilePath);
                fs.Close();

                Trace.Listeners.Add(new TextWriterTraceListener(newFilePath));
            }
            catch
            {
#if DEBUG
                Debug.WriteLine("An exception occurred in the logger! Logging unavailable.");
#endif
                IsLoggingAvailable = false;
            }
        }

        private static void WriteToFile(string message)
        {
            Task.Run(() =>
            {
                Trace.WriteLine(message);
                Trace.Flush();
            });
        }
    }
}
