using System;
using System.Collections.Generic;
using System.Linq;
using Bob.Comunication;
using UnityEngine;
using Utiles;

namespace Setups.UI
{
    [CreateAssetMenu(fileName = "NewInputBindings", menuName = "Gameplay/Input Bindings", order = 0)]
    public class InputBindings : ScriptableObject
    {
        [SerializeField] private List<DeviceInputPare> _deviceInputPares;

        public InputPare GetInputPare(InputType inputType) =>
            _deviceInputPares.FirstOrDefault((item) => item.InputType == inputType)?.InputPare;

        public string GetKeyNameForActionOnDevice(InputActionType inputActionType, InputType inputType) => 
            _deviceInputPares.FirstOrDefault(
                (item) => item.InputType == inputType 
                      && item.InputPare.InputActionType == inputActionType)?.InputPare.KeyName;
    }

    [Serializable]
    public class DeviceInputPare
    {
        [field: SerializeField] public InputType InputType { get; private set; }
        
        [field: SerializeField, Space] public InputPare InputPare { get; private set; }
    }

    [Serializable]
    public class InputPare
    {
        [field: SerializeField] public InputActionType InputActionType { get; private set; }

        [field: SerializeField] public string KeyName { get; private set; }
    }
}