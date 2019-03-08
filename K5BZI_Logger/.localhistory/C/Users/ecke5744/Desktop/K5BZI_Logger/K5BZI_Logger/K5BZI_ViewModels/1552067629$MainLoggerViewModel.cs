using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using K5BZI_Models;
using K5BZI_ViewModels.Interfaces;

namespace K5BZI_ViewModels
{
    public class MainLoggerViewModel : IMainLoggerViewModel
    {
        LogEntry LogEntry { get; set;}
    }
}
