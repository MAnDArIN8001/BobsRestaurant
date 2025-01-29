using System;
using Zenject;

namespace Installers.Mono
{
    public class InputInstaller : MonoInstaller
    {
        private BaseInput _input;
        
        public override void InstallBindings()
        {
            _input = new BaseInput();
            
            Container.Bind<BaseInput>().FromInstance(_input).AsSingle().Lazy();

            _input.Enable();
        }

        private void OnDestroy()
        {
            _input.Disable();
        }
    }
}
