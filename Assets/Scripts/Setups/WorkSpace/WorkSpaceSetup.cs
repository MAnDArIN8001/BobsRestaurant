using UnityEngine;

namespace Setups.WorkSpace
{
    [CreateAssetMenu(fileName = "NewCuttingTableSetup", menuName = "Gameplay/WorkSpace/Cutting Table", order = 0)]
    public class WorkSpaceSetup : ScriptableObject
    {
        [field: SerializeField] public float ProcessingTime { get; private set; }
    }
}