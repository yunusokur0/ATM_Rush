using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Interfaces;
using Assets.Scripts.Runtime.Managers;
using Assets.Scripts.Runtime.Signals;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets.Scripts.Runtime.Commands.Level
{
    public class LevelLoaderCommand : ICommand
    {
        private readonly LevelManager _levelManager;

        public LevelLoaderCommand(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }

        public void Execute(byte parameter)
        {
            var resourceRequest = Resources.LoadAsync<GameObject>($"LevelPrefabs/level {parameter}");
            resourceRequest.completed += operation =>
            {
                var newLevel = Object.Instantiate(resourceRequest.asset.GameObject(),
                    Vector3.zero, Quaternion.identity);
                if (newLevel != null) newLevel.transform.SetParent(_levelManager.levelHolder.transform);
                CameraSignals.Instance.onSetCinemachineTarget?.Invoke(CameraTargetState.Player);
            };
        }
    }
}