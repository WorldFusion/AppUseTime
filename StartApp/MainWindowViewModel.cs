using StartApp.Command;
using StartApp.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;

namespace StartApp
{
    public class MainWindowViewModel : ViewModelsBase
    {
        // constractor
        public MainWindowViewModel()
        {
            // file init
            System.Diagnostics.Debug.WriteLine("initialize main window....");
            this.appPath = Utils.ReadInfo.GetAppString();

            if (string.IsNullOrEmpty(this.appPath)) { 
                    MessageBox.Show("not set app.ini ,  CLC Genomics Worcbench is not found. ");
                System.Windows.Application.Current.Shutdown();
            }

            this.LogCommand = new DelegateCommand<string>(LogWrite);
            this.LogFileOpen = new DelegateCommand<string>(LogFileOpenProcess);

        }

        // 一つのパラメータなら。
        public void LogWrite(string user)
        {
            System.Diagnostics.Debug.WriteLine("Execute.....");

            // 開始時間を登録
            StartAppDateTime = DateTime.Now;
            Utils.WindowsLogWrite.StartEvent(User, StartAppDateTime);


            // 記述のあるAppを起動（非同期）
            var proc = new System.Diagnostics.Process();

            proc.StartInfo.FileName = this.appPath;
            proc.EnableRaisingEvents = true;
            proc.Exited += ProcExited;

            System.Diagnostics.Debug.WriteLine("Start app.....");
            // indow の最小化
            ShowState = WindowState.Minimized;

            proc.Start();

        }

        // App終了時
        public DateTime StartAppDateTime;
        public DateTime EndAppDateTime;
        private void ProcExited(object sender, EventArgs e)
        {
            EndAppDateTime = DateTime.Now;
            var res = Utils.WindowsLogWrite.EndEvent(User, StartAppDateTime, EndAppDateTime, this.appPath);

            CloseIt(); // 5秒後に強制終了
            if (string.IsNullOrEmpty(res))
                MessageBox.Show("log written, normal. ");
            System.Diagnostics.Debug.WriteLine("End app.....");

            Environment.Exit(1);// 自身の終了
        }

        // 
        public ICommand LogCommand { get; private set; }
        public ICommand LogFileOpen { get; private set; }

        private string user = string.Empty;
        public string User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged(nameof(User));
                return;
            }
        }

        private WindowState showState = WindowState.Normal;
                                                        // WindowState.Maximized;
        public WindowState ShowState
        {
            get { return showState; }
            set
            {
                showState = value;
                OnPropertyChanged(nameof(ShowState));
                return;
            }
        }

        private string appPath = string.Empty;
        public string AppName => appPath;

        public string LogFilePath => Utils.WindowsLogWrite.logFile;
        
        public void CloseIt()
        {
            System.Threading.Thread.Sleep(2000);
            Environment.Exit(1);// 自身の終了
        }

        public void LogFileOpenProcess(string filePath)
        {
            if (System.IO.File.Exists(filePath))
                System.Diagnostics.Process.Start(filePath);

            if (System.IO.File.Exists(LogFilePath))
                System.Diagnostics.Process.Start(LogFilePath);
        }
    }
}
