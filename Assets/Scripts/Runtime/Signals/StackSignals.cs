﻿using Assets.Scripts.Runtime.Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Runtime.Signals
{
    public class StackSignals : MonoSingleton<StackSignals>
    {
        public UnityAction<GameObject> onInteractionATM = delegate { };
        public UnityAction<GameObject> onInteractionObstacle = delegate { };
        public UnityAction<GameObject> onInteractionCollectable = delegate { };
        public UnityAction<Vector2> onStackFollowPlayer = delegate { };
        public UnityAction onUpdateType = delegate { };
        public UnityAction onInteractionConveyor = delegate { };
    }
}