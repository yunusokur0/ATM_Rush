using Assets.Scripts.Runtime.Extentions;
using UnityEngine.Events;

namespace Assets.Scripts.Runtime.Signals
{
    public class AtmSignals : MonoSingleton<AtmSignals>
    {
        public UnityAction<int> onSetAtmScoreText = delegate { };
    }
}