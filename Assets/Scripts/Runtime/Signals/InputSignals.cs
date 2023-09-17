using Assets.Scripts.Runtime.Extentions;
using Assets.Scripts.Runtime.Keys;
using UnityEngine.Events;

namespace Assets.Scripts.Runtime.Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        public UnityAction<bool> onChangeInputState = delegate { };
        public UnityAction onInputTaken = delegate { };
        public UnityAction<HorizontalInputParams> onInputDragged = delegate { };
        public UnityAction onInputReleased = delegate { };
    }
}