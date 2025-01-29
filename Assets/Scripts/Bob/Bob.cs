using Bob.Controls;
using Setups.Bob;
using UnityEngine;
using Zenject;

namespace Bob
{
    public class Bob : MonoBehaviour
    {
        [field: SerializeField] public BobMovement BobMovement { get; private set; }

        [SerializeField] private BobSetup _setup;

        private BaseInput _input;

        [Inject]
        private void Initialize(BaseInput input)
        {
            _input = input;
            
            BobMovement?.Initialize(input, _setup);
        }
    }
}