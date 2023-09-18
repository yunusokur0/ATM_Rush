using Assets.Scripts.Runtime.Managers;
using Assets.Scripts.Runtime.Signals;
using UnityEngine;

namespace Assets.Scripts.Runtime.Commands.Feature
{
    public class OnClickIncomeCommand
    {
        private readonly FeatureManager _featureManager;
        private int _newPriceTag;
        private byte _incomeLevel;

        public OnClickIncomeCommand(FeatureManager featureManager, ref int newPriceTag, ref byte incomeLevel)
        {
            _featureManager = featureManager;
            _newPriceTag = newPriceTag;
            _incomeLevel = incomeLevel;
        }

        internal void Execute()
        {
            _newPriceTag = (int)(CoreGameSignals.Instance.onGetIncomeLevel() -
                                 ((Mathf.Pow(2, Mathf.Clamp(_incomeLevel, 0, 10)) * 100)));
            _incomeLevel += 1;
            ScoreSignals.Instance.onSendMoney?.Invoke((int)_newPriceTag);
            UISignals.Instance.onSetMoneyValue?.Invoke((int)_newPriceTag);
            _featureManager.SaveFeatureData();
        }
    }
}