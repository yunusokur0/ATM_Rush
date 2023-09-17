using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Managers;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Runtime.Handler
{
    public class UIEventSubscriber : MonoBehaviour
    {
        [SerializeField] private UIEventSubscriptionTypes type;
        [SerializeField] private Button button;

        private UIManager _manager;

        private void Awake()
        {
            FindReferences();
        }

        private void FindReferences()
        {
            _manager = FindObjectOfType<UIManager>();
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            switch (type)
            {
                case UIEventSubscriptionTypes.OnPlay:
                        button.onClick.AddListener(_manager.OnPlay);
                        break;

                case UIEventSubscriptionTypes.OnNextLevel:
                        button.onClick.AddListener(_manager.OnNextLevel);
                        break;

                case UIEventSubscriptionTypes.OnRestartLevel:
                        button.onClick.AddListener(_manager.OnRestartLevel);
                        break;

                case UIEventSubscriptionTypes.OnIncreaseIncome:
                    button.onClick.AddListener(_manager.OnIncomeUpdate);
                    break;

                case UIEventSubscriptionTypes.OnIncreaseStack:
                    button.onClick.AddListener(_manager.OnStackUpdate);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UnSubscribeEvents()
        {
            switch (type)
            {
                case UIEventSubscriptionTypes.OnPlay:
                    button.onClick.AddListener(_manager.OnPlay);
                    break;

                case UIEventSubscriptionTypes.OnNextLevel:
                    button.onClick.AddListener(_manager.OnNextLevel);
                    break;

                case UIEventSubscriptionTypes.OnRestartLevel:
                    button.onClick.AddListener(_manager.OnRestartLevel);
                    break;

                case UIEventSubscriptionTypes.OnIncreaseIncome:
                    button.onClick.AddListener(_manager.OnIncomeUpdate);
                    break;

                case UIEventSubscriptionTypes.OnIncreaseStack:
                    button.onClick.AddListener(_manager.OnStackUpdate);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}