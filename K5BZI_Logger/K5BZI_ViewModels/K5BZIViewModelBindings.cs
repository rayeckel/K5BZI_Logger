using K5BZI_ViewModels.Interfaces;
using Ninject.Modules;

namespace K5BZI_ViewModels
{
    public class K5BZIViewModelBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IMainLoggerViewModel>().To<MainLoggerViewModel>().InSingletonScope();
            Bind<IEventViewModel>().To<EventViewModel>().InSingletonScope();
            Bind<IExportViewModel>().To<ExportViewModel>();
            Bind<IOperatorsViewModel>().To<OperatorsViewModel>().InSingletonScope();
        }
    }
}
