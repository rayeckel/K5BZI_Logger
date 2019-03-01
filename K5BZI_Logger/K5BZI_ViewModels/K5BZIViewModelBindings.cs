using K5BZI_ViewModels.Interfaces;
using Ninject.Modules;

namespace K5BZI_ViewModels
{
    public class K5BZIViewModelBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IMainLoggerViewModel>().To<MainLoggerViewModel>();
        }
    }
}
