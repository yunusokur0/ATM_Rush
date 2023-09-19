﻿using Cinemachine;
using UnityEngine;
namespace Assets.Scripts.Runtime.Extentions
{
    [ExecuteInEditMode]
    [SaveDuringPlay
    [AddComponentMenu("")]
    public class LookCinemachineAxis : CinemachineExtension
    {
        [Tooltip("Lock the camera's X position to this value")]
        public byte m_XPosition=0;

        protected override void PostPipelineStageCallback(
            CinemachineVirtualCameraBase vcam,
            CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (stage == CinemachineCore.Stage.Body)
            {
                var pos = state.RawPosition;
                pos.x = m_XPosition;
                state.RawPosition = pos;
            }
        }
    }
}
