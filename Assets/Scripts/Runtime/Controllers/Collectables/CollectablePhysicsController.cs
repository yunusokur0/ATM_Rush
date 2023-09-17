using Assets.Scripts.Runtime.Managers;
using UnityEngine;

namespace Assets.Scripts.Runtime.Controllers.Collectables
{
    public class CollectablePhysicsController : MonoBehaviour
    {
        [SerializeField] private CollectableManager manager;
        private readonly string _collectable = "Collectable";
        private readonly string _collected = "Collected";
        private readonly string _gate = "Gate";
        private readonly string _atm = "ATM";
        private readonly string _obstacle = "Obstacle";
        private readonly string _conveyor = "Conveyor";

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(_collectable) && CompareTag(_collected))
            {
                other.tag = _collected;
                manager.InteractionWithCollectable(other.transform.parent.gameObject);
            }

            if (other.CompareTag(_gate) && CompareTag(_collected))
            {
                manager.CollectableUpgrade(manager.GetCurrentValue());
            }

            if (other.CompareTag(_atm) && CompareTag(_collected))
            {
                manager.InteractionWithAtm(transform.parent.gameObject);
            }
            //carpan ,carpılan
            if (other.CompareTag(_obstacle) && CompareTag(_collected))
            {
                manager.InteractionWithObstacle(transform.parent.gameObject);
            }

            if (other.CompareTag(_conveyor) && CompareTag(_collected))
            {
                manager.InteractionWithConveyor();
            }
        }
    }
}