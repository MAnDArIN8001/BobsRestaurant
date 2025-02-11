using System;
using Bob.Comunication;
using Bob.Controls;
using Setups.Bob;
using UnityEngine;
using Zenject;

namespace Bob
{
    public class Bob : MonoBehaviour
    {
        [field: SerializeField] public BobMovement BobMovement { get; private set; }
        [field: SerializeField] public BobJump BobJump { get; private set; }
        [field: SerializeField] public BobRotator BobRotator { get; private set; }
        [field: SerializeField] public BobCommunicationController BobCommunicationController { get; private set; }

        [Space, SerializeField] private BobSetup _setup;

        [Inject]
        private void Initialize(BaseInput input)
        {
            BobMovement.Initialize(input, _setup);
            BobJump.Initialize(input, _setup);
            BobRotator.Initialize(input, _setup);

            BobCommunicationController.Initialize(input);
        }

        private void OnEnable()
        {
            if (BobCommunicationController is not null)
            {
                BobCommunicationController.OnCommunicationStart += HandleCommunicationStart;
                BobCommunicationController.OnCommunicationEnd += HandleCommunicationEnd;
            }
        }

        private void OnDisable()
        {
            if (BobCommunicationController is not null)
            {
                BobCommunicationController.OnCommunicationStart -= HandleCommunicationStart;
                BobCommunicationController.OnCommunicationEnd -= HandleCommunicationEnd;
            }
        }

        private void HandleCommunicationStart()
        {
            BobJump.enabled = false;
            BobMovement.enabled = false;
            BobRotator.enabled = false;
        }

        private void HandleCommunicationEnd()
        {
            BobJump.enabled = true;
            BobMovement.enabled = true;
            BobRotator.enabled = true;
        }
    }
}