using UnityEngine;

namespace Setups.Bob
{
    [CreateAssetMenu(fileName = "NewBobSetup", menuName = "Gameplay/Bob Setup", order = 0)]
    public class BobSetup : ScriptableObject
    {
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float JumpForce { get; private set; }
    }
}