using System;
using Unity.Mathematics;

namespace Assets.Scripts.Runtime.Data.ValueObject
{
    [Serializable]
    public class InputData
    {
        //yatay Giris Hizi, yatay olarak objenin gitme hizi
        public float HorizontalInputSpeed;
        //border
        public float2 ClampSides;
        //nesnenin hızını ne kadar hızlı yavaşlatması gerektiğini kontrol eder.
        public float ClampSpeed;
    }
}