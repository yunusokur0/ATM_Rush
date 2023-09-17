using Assets.Scripts.Runtime.Extentions;
using System;
using UnityEngine.Events;

namespace Assets.Scripts.Runtime.Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        public UnityAction<int> onSetScore = delegate { };
        public UnityAction<int> onSetAtmScore = delegate { };
        public UnityAction<int> onSendFinalScore = delegate { };
        public UnityAction<int> onSendMoney = delegate { };
        public Func<int> onGetMoney = delegate { return 0; };

        public Func<float> onGetMultiplier = delegate { return 0; };
    }
}