using System;
using UnityEngine;

namespace Assets.Scripts.Runtime.Data.ValueObject
{
    [Serializable]
    public struct StackData
    {
        public float CollectableOffsetInStack;
        [Range(0f, 5f)] public float LerpSpeed;
        [Range(0f, 5f)] public float StackAnimDuraction;
        [Range(0f, 10f)] public float StackScaleValue;
        [Range(0f, 30f)] public float JumpForce;
        public float JumpItemsClampX;
    }

}