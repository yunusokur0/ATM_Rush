using Assets.Scripts.Runtime.Controllers.Collectables;
using Assets.Scripts.Runtime.Data.UnityObject;
using Assets.Scripts.Runtime.Data.ValueObject;
using Assets.Scripts.Runtime.Signals;
using UnityEngine;

namespace Assets.Scripts.Runtime.Managers
{
    public class CollectableManager : MonoBehaviour
    {
        [SerializeField] private CollectableMeshController meshController;
        [SerializeField] private CollectablePhysicsController physicsController;
        private CollectableData _data;
        private byte _currentValue = 0;
        private readonly string _collectableDataPath = "Data/CD_Collectable";


        private void Awake()
        {
            _data = GetCollectableData();
            SendDataToController();
        }

        private CollectableData GetCollectableData() => Resources.Load<CD_Collectable>(_collectableDataPath).Data;

        private void SendDataToController()
        {
            meshController.SetMeshData(_data.MeshData);
        }

        internal void CollectableUpgrade(int value)
        {
            if (_currentValue < 2) _currentValue++;
            meshController.UpgradeCollectableVisual(_currentValue);
            StackSignals.Instance.onUpdateType?.Invoke();
        }

        public byte GetCurrentValue()
        {
            return _currentValue;
        }

        public void InteractionWithCollectable(GameObject collectableGameObject)
        {
            StackSignals.Instance.onInteractionCollectable?.Invoke(collectableGameObject);
        }

        public void InteractionWithAtm(GameObject collectableGameObject)
        {
            StackSignals.Instance.onInteractionATM?.Invoke(collectableGameObject);
        }

        public void InteractionWithObstacle(GameObject collectableGameObject)
        {
            StackSignals.Instance.onInteractionObstacle?.Invoke(collectableGameObject);
        }

        public void InteractionWithConveyor()
        {
            StackSignals.Instance.onInteractionConveyor?.Invoke();
        }
    }
}