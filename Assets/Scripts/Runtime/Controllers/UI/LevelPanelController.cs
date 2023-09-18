using Assets.Scripts.Runtime.Signals;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Runtime.Controllers.UI
{
    public class LevelPanelController : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelTexts;

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            UISignals.Instance.onSetNewLevelValue += OnSetLevelValue;
        } 
        private void UnSubscribeEvents()
        {
            UISignals.Instance.onSetNewLevelValue -= OnSetLevelValue;
        }

        private void OnSetLevelValue(byte levelValue)
        {
            var additionalValue = ++levelValue;
            levelTexts.text = "LEVEL " + additionalValue.ToString();
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}