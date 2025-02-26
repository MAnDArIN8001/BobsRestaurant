using Utiles.EventSystem;
using Zenject;

namespace Installers.Mono.Services
{
    public class EventSystemInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EventBus>().FromNew().AsSingle();
        }
    }
}