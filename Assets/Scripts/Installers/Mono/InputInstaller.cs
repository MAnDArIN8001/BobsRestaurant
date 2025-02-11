using System;
using Bob.Comunication;
using UnityEngine;
using Zenject;

namespace Installers.Mono
{
    public class InputInstaller : MonoInstaller
    {
        private BaseInput _input;

        [SerializeField] private InputTypeDetector _inputTypeDetector;

        public override void InstallBindings()
        {
            _input = new BaseInput();
            
            Container.Bind<BaseInput>().FromInstance(_input).AsSingle().Lazy();
            Container.Bind<InputTypeDetector>().FromInstance(_inputTypeDetector).AsSingle().NonLazy();

            _input.Enable();
        }

        private void OnDestroy()
        {
            _input.Disable();
        }
    }
}
