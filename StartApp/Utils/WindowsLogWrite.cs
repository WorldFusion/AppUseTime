using System;
using System.Collections.Generic;
using System.Linq;

namespace StartApp.Utils
{
    public static class WindowsLogWrite
    {

        public static readonly string logFile = System.IO.Path.Combine(
                                    System.Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                                    "app-log.csv");

        public static string InitFile()
        {
            try { 

            // ログファイルが無い時に初期状態を
            if( !System.IO.File.Exists(logFile))
                System.IO.File.WriteAllText(logFile, "User, start, end, App" +
                                                                       System.Environment.NewLine);
            } catch (Exception e)
            {
                return e.Message;
            }
            return string.Empty;
        }


        // app起動時間をログファイルに記述する
        public static string StartEvent(string user, DateTime time)
        {
            var isLastExist = false;
            try
            {
                // ファイルが無ければ初期化する
                if (! System.IO.File.Exists(logFile)) InitFile();
                var lastline = System.IO.File.ReadAllLines(logFile).Last().Trim();

                if (!string.IsNullOrEmpty(lastline))  // 最終行に何か入って居る・・・
                    isLastExist = true;


                // app 起動したときに入力されたユーザ名と時間
                var appstart = GetUserStartString(user, time);
                if (isLastExist) appstart = Environment.NewLine + appstart;

                System.IO.File.AppendAllText(logFile, appstart); // file write
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return string.Empty;
        }

        // 全行入れ替え版
        public static string EndEvent(string user, DateTime startTime, DateTime endTime, string pgname)
        {
            var isStartExist = false;
            try
            {
                // ファイルが無ければ初期化する
                if (!System.IO.File.Exists(logFile)) InitFile();
                var appstart = GetUserStartString(user, startTime);

                var newLogLines = new List<string>();
                foreach(var line in System.IO.File.ReadAllLines(logFile))
                {
                    var logLine = line.Trim();
                    if (logLine == appstart)
                    {
                        newLogLines.Add(appstart + " ," + endTime.ToString() + " ," + pgname);
                        isStartExist = true;
                    }
                    else { 
                        newLogLines.Add(line);
                    }
                }

                if(! isStartExist)
                    newLogLines.Add(appstart + " ," + endTime.ToString() + " ," + pgname);

                // ログ書き出し。
                System.IO.File.WriteAllLines(logFile, newLogLines);
            }
            catch (Exception e)
            {

                return e.Message;
            }

            return string.Empty;
        }

        // 当該行を検索する共通文字列
        private static string GetUserStartString(string user, DateTime time )
            => user + ", " + time.ToString() ;
      


    }

}
