using Setups.Bob;
using UnityEngine;
using Zenject;

public class BobInstaller : MonoInstaller
{
    [SerializeField] private BobSetup _setup;
    
    public override void InstallBindings()
    {
        Container.Bind<BobSetup>().FromInstance(_setup).AsSingle();
    }
}
