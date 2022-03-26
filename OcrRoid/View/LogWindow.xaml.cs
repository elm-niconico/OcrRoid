using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Windows.Storage;

namespace OcrRoid.View
{
    /// <summary>
    /// LogWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class LogWindow : Window
    {
        public LogWindow(double mainWindowX, double mainWindowWidth)
        {
            InitializeComponent();

            var logs = ExtractLogText();

            this.Loaded += (o, e) =>
            {
                this.Left = mainWindowX + mainWindowWidth + 100;
                if (logs == null)
                {
                    MessageBox.Show("ログファイルが見つかりませんでした");
                }
                else
                {
                    this.LogText.Text = logs;
                }

            };

        }

        public string? ExtractLogText()
        {

            var logFile = $@"{GetCurrentAppDir()}/application.log";
            if (File.Exists(logFile) == false) return null;

            using var stream = new FileStream(logFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var reader = new StreamReader(stream);
            
            string? line;
            var sb = new StringBuilder();
            while ((line = reader.ReadLine()) != null)
            {
                sb.Append($"{line}{Environment.NewLine}");
            }

            return sb.ToString();
        }

        public static string? GetCurrentAppDir()
        {
            return System.IO.Path.GetDirectoryName(
                System.Reflection.Assembly.GetExecutingAssembly().Location);
        }

    }
}
