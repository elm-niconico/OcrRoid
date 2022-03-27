using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Automation;
using OcrRoid.Util;

namespace OcrRoid.Talks
{
   
    internal class VoiceRoid: ITalk
    {
  

        public async Task SpeakAsync(string sentence)
        {
            await VoiceRoidUtil.ClickStopButtonAsync();
            await VoiceRoidUtil.SetTextIntoEditorAsync(sentence);
            await VoiceRoidUtil.ClickSpeakButtonAsync();
        }

        public async Task PauseAsync()
        {
            await VoiceRoidUtil.ClickSpeakButtonAsync();
        }

        public async Task RestartAsync()
        {
            await VoiceRoidUtil.ClickSpeakButtonAsync();
        }

        public async Task StopAsync()
        {
            await VoiceRoidUtil.ClickStopButtonAsync();
            await VoiceRoidUtil.SetTextIntoEditorAsync("");
        }




     
    }
}
