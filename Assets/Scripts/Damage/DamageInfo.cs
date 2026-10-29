using UnityEngine;

namespace Components
{
    public struct DamageInfo
    {
        public float Amount { get; }
        public GameObject Source { get; }
        public DamageType Type { get; }

        public DamageInfo(float amount, GameObject source, DamageType type)
        {
            Amount = amount;
            Source = source;
            Type = type;
        }
    }
}