using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OcrRoid.Util
{
    internal class WindowsApi
    {
        private WindowsApi(){}

        /// <summary>
        /// 指定したクラス名、タイトルを持つ要素のハンドラを取得
        /// </summary>
        /// <param name="lpClassName">指定するクラス名（Winspectorで表示されている）</param>
        /// <param name="lpWindowName">指定するタイトル（Winspectorで表示されている）</param>
        /// <returns>指定した要素のハンドル。指定したものが無ければ0が戻る</returns>
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string? lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint message, uint wParam, uint lParam);
    }
}
