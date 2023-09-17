using Assets.Scripts.Runtime.Data.ValueObject;
using UnityEngine;

namespace Assets.Scripts.Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Collectable", menuName = "AtmRush/CD_Collectable", order = 0)]
    public class CD_Collectable : ScriptableObject
    {
        public CollectableData Data;
    }
}