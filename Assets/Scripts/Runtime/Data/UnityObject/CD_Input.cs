using Assets.Scripts.Runtime.Data.ValueObject;
using UnityEngine;

namespace Assets.Scripts.Runtime.Data.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Input", menuName = "AtmRush/CD_Input", order = 0)]
    public class CD_Input : ScriptableObject
    {
        public InputData Data;
    }
}