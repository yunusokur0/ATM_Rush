using Assets.Scripts.Runtime.Extentions;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Runtime.Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    {
        public UnityAction<GameStates> onChangeGameStates = delegate { };
        public UnityAction<byte> onLevelInitialize = delegate { };
        public UnityAction onClearActiveLevel = delegate { };
        public UnityAction onLevelSuccessful = delegate { };
        public UnityAction onLevelFailed = delegate { };
        public UnityAction onNextLevel = delegate { };
        public UnityAction onRestartLevel = delegate { };
        public UnityAction onPlay = delegate { };
        public UnityAction onReset = delegate { };
       
        public UnityAction onMiniGameEntered = delegate { };
        public UnityAction<GameObject> onAtmTouched = delegate { };
        public UnityAction onMiniGameStart = delegate { };



        public Func<byte> onGetIncomeLevel = delegate { return 0; };
        public Func<byte> onGetStackLevel = delegate { return 0; };
    }
}
