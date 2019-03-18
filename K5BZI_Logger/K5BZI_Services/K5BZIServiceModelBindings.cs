using K5BZI_Services.Interfaces;
using Ninject.Modules;

namespace K5BZI_Services
{
    public class K5BZIServiceModelBindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IFileStoreService>().To<FileStoreService>();
        }
    }
}
