using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Signals;
using Cinemachine;
using System;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Runtime.Managers
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;
        [SerializeField] private Animator animator;

        private float3 _initialPosition;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _initialPosition = transform.position;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onReset += OnReset;
            //hedef belirleme
            CameraSignals.Instance.onSetCinemachineTarget += OnSetCinemachineTarget;
            //kamera belirleme
            CameraSignals.Instance.onChangeCameraState += OnChangeCameraState;
        }

        private void OnSetCinemachineTarget(CameraTargetState state)
        {
            switch (state)
            {
                case CameraTargetState.Player:
                    {
                        var playerManager = FindObjectOfType<PlayerManager>().transform;
                        stateDrivenCamera.Follow = playerManager;
                    }
                    break;
                case CameraTargetState.FakePlayer:
                    {
                        stateDrivenCamera.Follow = null;
                        //var fakePlayer = FindObjectOfType<WallCheckController>().transform.parent.transform;
                        //stateDrivenCamera.Follow = fakePlayer;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }

        private void OnChangeCameraState(CameraStates state)
        {
            animator.SetTrigger(state.ToString());
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            CameraSignals.Instance.onSetCinemachineTarget -= OnSetCinemachineTarget;
            CameraSignals.Instance.onChangeCameraState -= OnChangeCameraState;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }


        private void OnReset()
        {
            CameraSignals.Instance.onChangeCameraState?.Invoke(CameraStates.Initial);
            stateDrivenCamera.Follow = null;
            stateDrivenCamera.LookAt = null;
            transform.position = _initialPosition;
        }
    }
}