using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Extentions;
using UnityEngine.Events;

namespace Assets.Scripts.Runtime.Signals
{
    public class CameraSignals : MonoSingleton<CameraSignals>
    {
        public UnityAction<CameraStates> onChangeCameraState = delegate { };
        public UnityAction<CameraTargetState> onSetCinemachineTarget = delegate { };
    }
}