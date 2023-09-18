using Assets.Scripts.Runtime.Commands.Feature;
using Assets.Scripts.Runtime.Signals;
using UnityEngine;

namespace Assets.Scripts.Runtime.Managers
{
    public class FeatureManager : MonoBehaviour
    {
        private byte _incomeLevel = 1;
        private byte _stackLevel = 1;
        private int _newPriceTag;

        private OnClickIncomeCommand _onClickIncomeCommand;
        private OnClickStackCommand _onClickStackCommand;
        private void Awake()
        {
            _incomeLevel = LoadIncomeData();
            _stackLevel = LoadStackData();
            Init();
        }
        private void Init()
        {
            _onClickIncomeCommand = new OnClickIncomeCommand(this, ref _newPriceTag, ref _incomeLevel);
            _onClickStackCommand = new OnClickStackCommand(this, ref _newPriceTag, ref _stackLevel);
        }
        private void OnEnable()
        {
            Subscription();
        }

        private void Subscription()
        {
            UISignals.Instance.onClickIncome += _onClickIncomeCommand.Execute;
            UISignals.Instance.onClickStack += _onClickStackCommand.Execute;
            CoreGameSignals.Instance.onGetIncomeLevel += OnGetIncomeLevel;
            CoreGameSignals.Instance.onGetStackLevel += OnGetStackLevel;
        }

        private byte OnGetIncomeLevel() => _incomeLevel;
        private byte OnGetStackLevel() => _stackLevel;

        private byte LoadIncomeData()
        {
            if (!ES3.FileExists()) return 1;
            return (byte)(ES3.KeyExists("IncomeLevel") ? ES3.Load<int>("IncomeLevel") : 1);
        }

        private byte LoadStackData()
        {
            if (!ES3.FileExists()) return 1;
            return (byte)(ES3.KeyExists("StackLevel") ? ES3.Load<int>("StackLevel") : 1);
        }

        internal void SaveFeatureData()
        {
            SaveSignals.Instance.onSaveGameData?.Invoke();
        }
        private void UnSubscription()
        {
            UISignals.Instance.onClickIncome -= _onClickIncomeCommand.Execute;
            UISignals.Instance.onClickStack -= _onClickStackCommand.Execute;
            CoreGameSignals.Instance.onGetIncomeLevel -= OnGetIncomeLevel;
            CoreGameSignals.Instance.onGetStackLevel -= OnGetStackLevel;
        }

        private void OnDisable()
        {
            UnSubscription();
        }
    }
}