using K5BZI_Models;
using K5BZI_ViewModels.Interfaces;

namespace K5BZI_ViewModels
{
    public class MainLoggerViewModel : IMainLoggerViewModel
    {
        public LogEntry LogEntry { get; private set; }

        public MainLoggerViewModel()
        {

        }


    }
}
