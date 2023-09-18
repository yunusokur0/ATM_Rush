using Assets.Scripts.Runtime.Signals;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Runtime.Managers
{
    public class AtmManager : MonoBehaviour
    {
        [SerializeField] private DOTweenAnimation doTweenAnimation;
        [SerializeField] private TextMeshPro atmText;

        private void Awake()
        {
            GetReferences();
        }

        private void GetReferences()
        {
            doTweenAnimation = GetComponentInChildren<DOTweenAnimation>();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onAtmTouched += OnAtmTouched;
            AtmSignals.Instance.onSetAtmScoreText += OnSetAtmScoreText;
        }

        private void OnAtmTouched(GameObject touchedATMObject)
        {
            if (touchedATMObject.GetInstanceID() == gameObject.GetInstanceID())
            {
                doTweenAnimation.DOPlay();
            }
        }

        private void OnSetAtmScoreText(int value)
        {
            atmText.text = value.ToString();
        }

        private void UnSubscribeEvents()
        {
            CoreGameSignals.Instance.onAtmTouched -= OnAtmTouched;
            AtmSignals.Instance.onSetAtmScoreText -= OnSetAtmScoreText;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}