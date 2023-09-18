using Assets.Scripts.Runtime.Managers;
using Assets.Scripts.Runtime.Signals;
using UnityEngine;

public class OnClickStackCommand
{
    private FeatureManager _featureManager;
    private byte _stackLevel;
    private int _newPriceTag;

    public OnClickStackCommand(FeatureManager featureManager, ref int newPriceTag, ref byte stackLevel)
    {
        _featureManager = featureManager;
        _newPriceTag = newPriceTag;
        _stackLevel = stackLevel;
    }

    internal void Execute()
    {
        _newPriceTag = (int)(CoreGameSignals.Instance.onGetStackLevel() -
                             ((Mathf.Pow(2, Mathf.Clamp(_stackLevel, 0, 10)) * 100)));
        _stackLevel += 1;
        ScoreSignals.Instance.onSendMoney?.Invoke((int)_newPriceTag);
        UISignals.Instance.onSetMoneyValue?.Invoke((int)_newPriceTag);
        _featureManager.SaveFeatureData();
    }
}

