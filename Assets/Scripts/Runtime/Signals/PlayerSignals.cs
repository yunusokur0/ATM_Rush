using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Extentions;
using UnityEngine.Events;

namespace Assets.Scripts.Runtime.Signals
{
    public class PlayerSignals : MonoSingleton<PlayerSignals>
    {
        public UnityAction<PlayerAnimationStates> onChangePlayerAnimationState = delegate { };
        public UnityAction<bool> onPlayConditionChanged = delegate { };
        public UnityAction<bool> onMoveConditionChanged = delegate { };
        public UnityAction<int> onSetTotalScore = delegate { };
    }
}