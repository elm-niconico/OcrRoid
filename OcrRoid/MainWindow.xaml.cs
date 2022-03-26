using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using OcrRoid.Ocr;
using OcrRoid.Talks;
using OcrRoid.Util;
using OcrRoid.View;

namespace OcrRoid
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async  void OnClickClippingBtn(object sender, RoutedEventArgs e)
        {

            var clippingWindow = new ClippingWindow();
            this.Visibility = Visibility.Hidden;
            clippingWindow.ShowDialog();

            this.Visibility = Visibility.Visible;
        }

        private async void OnClickPauseBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                await TalkFactory
                    .FactTalkFromSettings()
                    .PauseAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void OnClickStopBtn(object sender, RoutedEventArgs e)
        {
            try
            {
                await TalkFactory
                    .FactTalkFromSettings()
                    .StopAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void OnClickRestartBtn(object sender, RoutedEventArgs e)
        {

            try
            {
                
                await TalkFactory
                    .FactTalkFromSettings()
                    .RestartAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OnClickChangeBouyomi(object sender, RoutedEventArgs e)
        {
            Settings.Default.talkType = 0;
            Settings.Default.Save();
        }

        private void OnClickChangeVoiceRoid(object sender, RoutedEventArgs e)
        {
            Settings.Default.talkType = 1;
            Settings.Default.Save();
        }

    }
}
