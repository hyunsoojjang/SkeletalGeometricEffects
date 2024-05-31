
using System;
using System.Collections.Generic;

using System.Text;
using System.IO;
using System.Diagnostics;
//using GN;


    public class TRACE
    {
        public delegate void LogHandler(object msg);
        public static LogHandler logHandle;
        protected static StreamWriter sw = null;
        protected static DateTime SAVE_TIME = DateTime.Now;
        public static string CURRENT_HTML_FILE = "";
        public static void Log(object msg)
        {

            string date = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
             
            try
            {
                string value = "[" + date + "][" + CURRENT_HTML_FILE + "]" + msg;
            // if ( Config.DEBUG == 1 )

            UnityEngine.Debug.LogWarning(msg.ToString());

            if (logHandle != null)
                    logHandle(value);

                var dt = DateTime.Now;
                if (sw == null || SAVE_TIME.Hour != dt.Hour)
                {
                    if (sw != null)
                    {
                        sw.Close();
                    }

                    if (Directory.Exists("DebugLog") == false)
                        Directory.CreateDirectory("DebugLog");
                    if (Directory.Exists("trace") == false)
                        Directory.CreateDirectory("trace");

                    var folder = @"C:\DebugLog\trace\" + DateTime.Now.ToString("yyyy-MM-dd");
                    if (!Directory.Exists(folder))
                        Directory.CreateDirectory(folder);

                    string fileDate = "Log_" + DateTime.Now.Hour.ToString();

                    sw = new StreamWriter(folder + @"\" + fileDate + ".txt", true, Encoding.Unicode);
                }
                if (sw != null)
                {
                    sw.WriteLine(value);
                    sw.Flush();
                }
            }
            catch (Exception e)
            {
                if (logHandle != null)
                    logHandle(e.ToString());
            }
        }
        public int hour = 8;
        public static void FileLog(object msg)
        {
            string date = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");

            try
            {
                if (sw == null)
                {
                    if (Directory.Exists("trace") == false)
                        Directory.CreateDirectory("trace");
                    string fileDate = DateTime.Now.ToString("yyyy_MM_dd_hh_mm_ss");
                    sw = new StreamWriter("trace/" + fileDate + ".txt", true, Encoding.Unicode);
                }
                if (sw != null)
                {
                    sw.WriteLine("[" + date + "]" + msg.ToString());
                    sw.Flush();
                }
            }
            catch (Exception e)
            {
            }
        }
        public static void Close()
        {
            if (sw != null)
            {
                sw.Flush();
                sw.Close();
                sw = null;
            }
        }
    }
