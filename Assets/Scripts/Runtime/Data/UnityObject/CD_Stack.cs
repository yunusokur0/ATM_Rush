using Assets.Scripts.Runtime.Data.ValueObject;
using UnityEngine;

namespace Assets.Scripts.Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Stack", menuName = "AtmRush/CD_Stack", order = 0)]
    public class CD_Stack : ScriptableObject
    {
        public StackData Data;
    }
}