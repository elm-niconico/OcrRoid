using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Documents;
using OcrRoid.Exceptions;

namespace OcrRoid.Util
{
    internal class VoiceRoidUtil
    {
        private VoiceRoidUtil(){}

        private const string WindowTitle = "VOICEROID2";
        // VoiceRoid側で文字を入れるなどの変更があった場合このタイトルに変わる
        private const string WindowTitleOther = "VOICEROID2*";

        public static async Task  SetTextIntoEditorAsync(string sentence)
        {
            await Task.Run(async () =>
            {
                await ClickStopButtonAsync();
                var editor = GetInputEditor().GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
                editor?.SetValue(sentence);
            });
        }

        public static async Task ClickSpeakButtonAsync()
        {
            await Task.Run(() =>
            {
                var speakButton =  GetSpeakButton();
                if(speakButton == null)
                    return; 
                var button = speakButton.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
                button?.Invoke();
            });
        }

        public static async Task ClickStopButtonAsync()
        {
            await Task.Run(() =>
            {
                var speakButton = GetStopButton();
                if (speakButton == null)
                    return;
                var button = speakButton.GetCurrentPattern(InvokePattern.Pattern) as InvokePattern;
                button?.Invoke();
            });
        }


        private static AutomationElement GetInputEditor()
            => GetTextEditor().FindFirst(
                TreeScope.Descendants, 
                new PropertyCondition(AutomationElement.AutomationIdProperty, "TextBox"));

        private static AutomationElement? GetStopButton()
        {
            var buttons = GetSpeakButtons();

            return buttons?
                .Cast<AutomationElement>()
                .FirstOrDefault(button =>
                    button.Current.HelpText.StartsWith("テキストの読み上げを停止します。"));
        }



        private static AutomationElement? GetSpeakButton()
        {
            var buttons = GetSpeakButtons();
            return buttons?
                .Cast<AutomationElement>()
                .FirstOrDefault(button => 
                    button.Current.HelpText.StartsWith("入力テキストのカーソルの位置からテキストを読み上げます。"));
        }

        private static AutomationElementCollection? GetSpeakButtons()
        {
            return GetTextEditor().FindAll(
                TreeScope.Descendants | TreeScope.Children,
                new PropertyCondition(AutomationElement.ClassNameProperty, "Button")
            );
        }

        private static AutomationElement GetTextEditor()
            => GetVoiceRoidElement().FindFirst(
                TreeScope.Descendants | TreeScope.Element,
                new PropertyCondition(AutomationElement.AutomationIdProperty, "c"));

        private static AutomationElement GetVoiceRoidElement()
            => AutomationElement.FromHandle(GetVoiceRoidHWnd());

        private static IntPtr GetVoiceRoidHWnd()
        {
            try
            {
                var handle = WindowsApi.FindWindow(null, WindowTitle);
                return handle != IntPtr.Zero ?
                    handle :
                    WindowsApi.FindWindow(null, WindowTitleOther);
            }
            catch (ArgumentException ex)
            {
                throw new NotFoundVoiceRoidWHandleException(ex.Message);
            }
        }
    }
}
