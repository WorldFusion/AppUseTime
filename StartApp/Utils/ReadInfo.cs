using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StartApp.Utils
{
    public static class ReadInfo
    {

        public static readonly string infoFileName = "app.ini";
        public static readonly string noApp = "noneapp";
        public static readonly Encoding Enc = Encoding.GetEncoding("shift_jis");

        public static string GetAppString()
        {
            var infoFile = Path.Combine(
                System.AppDomain.CurrentDomain.BaseDirectory,
                infoFileName);

            // ファイル見つからない 初期化する。　　TODO：エラー処理どうしよう
            if (!File.Exists(infoFile))
            {
                var res = InitAppInfo();
                if (!string.IsNullOrEmpty(res)) return res;
            }

            var appPath = File.ReadAllText(infoFile, Enc).Trim('\r', '\n');
            if (! File.Exists(appPath))
                return noApp;

            return appPath;
        }

        public static string InitAppInfo()
        {
            var infoFile = Path.Combine(
                                        System.AppDomain.CurrentDomain.BaseDirectory,
                                        infoFileName);
            try
            {
                var gwb = GetGWBPath();
                if( string.IsNullOrEmpty(gwb))
                    return "Error,  no app.ini initialized.";

                // ログファイルが無い時に初期状態を
                if (!System.IO.File.Exists(infoFile))
                    System.IO.File.WriteAllText(infoFile, gwb + Environment.NewLine);

                if (!System.IO.File.Exists(infoFile))
                    return "Error, not create app.ini .";

            }
            catch (Exception e)
            {
                return e.Message;
            }
            return string.Empty;
        }


        public static string GetGWBPath()
        {
            var programFolder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            var gwbDirs = Directory.GetDirectories(
                                                        Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles),
                                                        "CLC*", 
                                                        SearchOption.TopDirectoryOnly);

            if (!gwbDirs.Any())
                return string.Empty;

            var gwbs = gwbDirs.Select(s => Directory.GetFiles(s, "clcgenomicswb??.exe")).First();

            if (gwbs.Any()) {
                Array.Sort(gwbs);
                return gwbs.Last();

            }
            return string.Empty;
        }

    }
}
