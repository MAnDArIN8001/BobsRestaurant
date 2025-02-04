using UnityEngine;

namespace Setups.Bob
{
    [CreateAssetMenu(fileName = "NewBobSetup", menuName = "Gameplay/Bob Setup", order = 0)]
    public class BobSetup : ScriptableObject
    {
        [Header("Movement characteristics")]
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public float JumpForce { get; private set; }
        
        [Header("Sensitivity characteristics")]
        [field: SerializeField, Space] public float HorizontalSensitivity { get; private set; }
        [field: SerializeField] public float VerticalSensitivity { get; private set; }
    }
}