using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Signals;
using UnityEngine;

namespace Assets.Scripts.Runtime.Controllers.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            PlayerSignals.Instance.onChangePlayerAnimationState += OnChangeAnimationState;
        }

        private void OnChangeAnimationState(PlayerAnimationStates animationState)
        {
            animator.SetTrigger(animationState.ToString());
        }

        private void UnSubscribeEvents()
        {
            PlayerSignals.Instance.onChangePlayerAnimationState -= OnChangeAnimationState;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        internal void OnReset()
        {
            PlayerSignals.Instance.onChangePlayerAnimationState?.Invoke(PlayerAnimationStates.Idle);
        }
    }
}