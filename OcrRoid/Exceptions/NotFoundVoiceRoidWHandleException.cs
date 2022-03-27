using System;
using System.Collections.Generic;
using System.Text;

namespace OcrRoid.Exceptions
{
    internal class NotFoundVoiceRoidWHandleException: Exception
    {
        public NotFoundVoiceRoidWHandleException(string originalErr): base(
            message: $"VoiceRoidのウィンドウハンドルが見つかりませんでした。{Environment.NewLine}" +
                     $"VoiceRoidが起動されているか確認してください。{Environment.NewLine}" +
                     $"エラー元のメッセージ{Environment.NewLine}" +
                     $"{originalErr}")
        {

        }

        public NotFoundVoiceRoidWHandleException() : base(
            message: $"VoiceRoidのウィンドウハンドルが見つかりませんでした。{Environment.NewLine}" +
                     $"VoiceRoidが起動されているか確認してください。{Environment.NewLine}")

        {

        }

    }
}
