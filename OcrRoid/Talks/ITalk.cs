using System.Threading.Tasks;

namespace OcrRoid.Talks
{
    internal interface ITalk
    {
        Task SpeakAsync(string sentence);
        Task PauseAsync();
        Task RestartAsync();
        Task StopAsync();
    }
}
