using System;
using Unity.Mathematics;

namespace Assets.Scripts.Runtime.Data.ValueObject
{
    [Serializable]
    public class InputData
    {
        public float HorizontalInputSpeed;
        public float2 ClampSides;
        public float ClampSpeed;
    }
}
