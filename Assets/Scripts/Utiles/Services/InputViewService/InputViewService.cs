using System;
using Bob.Comunication;
using Setups.UI;

namespace Utiles.Services.InputViewService
{
    public class InputViewService : IDisposable
    {
        private InputBindings _inputBindings;

        public InputViewService(InputBindings bindings)
        {
            _inputBindings = bindings;
        }

        public void Dispose()
        {
            _inputBindings = null;
        }

        public string GetKeyNameForActionOnDevice(InputActionType inputActionType, InputType inputType) =>
            _inputBindings.GetKeyNameForActionOnDevice(inputActionType, inputType);
        
        public InputPare GetInputPare(InputType inputType) => _inputBindings.GetInputPare(inputType);
    }
}