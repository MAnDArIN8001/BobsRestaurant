using System;
using UnityEngine;

namespace Utiles
{
    [Serializable]
    public class Range
    {
        [field: SerializeField] public float MaxValue { get; private set; }
        [field: SerializeField] public float MinValue { get; private set; }
    }
}
