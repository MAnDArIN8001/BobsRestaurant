using Setups.UI;
using UnityEngine;
using Utiles.Services.InputViewService;
using Zenject;

namespace Installers.Mono.Services
{
    public class InputBindingsInstaller : MonoInstaller
    {
        [SerializeField] private InputBindings _inputBindings;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputViewService>().AsSingle().WithArguments(_inputBindings).Lazy();
        }
    }
}