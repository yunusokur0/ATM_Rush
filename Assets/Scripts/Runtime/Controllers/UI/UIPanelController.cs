using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Signals;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Controllers.UI
{
    public class UIPanelController : MonoBehaviour
    {
        [SerializeField] private List<Transform> layers = new List<Transform>();

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreUISignals.Instance.onClosePanel += OnClosePanel;
            CoreUISignals.Instance.onOpenPanel += OnOpenPanel;
            CoreUISignals.Instance.onCloseAllPanels += OnCloseAllPanels;
        }
        private void OnCloseAllPanels()
        {
            for (int i = 0; i < layers.Count; i++)
            {
                if (i == 0)
                {
                    continue;
                }

                Transform layer = layers[i];

                if (layer.childCount > 0)
                {
                    DestroyImmediate(layer.GetChild(0).gameObject);
                }
            }
        }
        private void OnOpenPanel(UIPanelTypes panelType, int value)
        {
            OnClosePanel(value);
            Instantiate(Resources.Load<GameObject>($"Screens/{panelType}Panel"), layers[value]);
        }

        private void OnClosePanel(int value)
        {
            if (layers[value].childCount <= 0) return;
            DestroyImmediate(layers[value].GetChild(0).gameObject);
        }

        private void UnSubscribeEvents()
        {
            CoreUISignals.Instance.onClosePanel -= OnClosePanel;
            CoreUISignals.Instance.onOpenPanel -= OnOpenPanel;
            CoreUISignals.Instance.onCloseAllPanels -= OnCloseAllPanels;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}