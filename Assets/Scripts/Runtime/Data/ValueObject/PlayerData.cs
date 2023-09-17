using System;

namespace Assets.Scripts.Runtime.Data.ValueObject
{
    [Serializable]
    public struct PlayerData
    {
        public PlayerMovementData MovementData;
    }

    [Serializable]
    public struct PlayerMovementData
    {
        //ileri hiz
        public float ForwardSpeed;
        //sag sol gitme hizi
        public float SidewaysSpeed;
    }
}