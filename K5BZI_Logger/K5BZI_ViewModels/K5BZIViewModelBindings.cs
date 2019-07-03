using K5BZI_ViewModels.Interfaces;
using Ninject.Modules;

namespace K5BZI_ViewModels
{
    public class K5BZIViewModelBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IMainViewModel>().To<MainViewModel>().InSingletonScope();
            Bind<IEventViewModel>().To<EventViewModel>().InSingletonScope();
            Bind<IExportViewModel>().To<ExportViewModel>();
            Bind<IOperatorsViewModel>().To<OperatorsViewModel>().InSingletonScope();
        }
    }
}
