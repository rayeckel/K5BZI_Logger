using K5BZI_Services.Interfaces;
using K5BZI_ViewModels.Interfaces;
using Ninject;
using Ninject.Modules;

namespace K5BZI_ViewModels
{
    public class K5BZIViewModelBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IMainLoggerViewModel>().To<MainLoggerViewModel>();
            Bind<ISelectEventViewModel>().To<SelectEventViewModel>()
                .WithConstructorArgument("fileStoreService", c => c.Kernel.Get<IFileStoreService>());
        }
    }
}
