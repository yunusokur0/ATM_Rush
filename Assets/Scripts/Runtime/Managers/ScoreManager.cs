using Assets.Scripts.Runtime.Signals;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Runtime.Managers
{
    public class ScoreManager : MonoBehaviour
    {
        private int _money;
        private int _stackValueMultiplier;
        private int _scoreCache = 0;
        private int _atmScoreValue = 0;

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void Start()
        {
            OnSetValueMultiplier();
            RefreshMoney();
        }
        private void SubscribeEvents()
        {
            ScoreSignals.Instance.onSendMoney += OnSendMoney;
            ScoreSignals.Instance.onGetMoney += () => _money;
            ScoreSignals.Instance.onSetScore += OnSetScore;
            ScoreSignals.Instance.onSetAtmScore += OnSetAtmScore;
            CoreGameSignals.Instance.onMiniGameStart +=
                () => ScoreSignals.Instance.onSendFinalScore?.Invoke(_scoreCache);
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onLevelSuccessful += RefreshMoney;
            CoreGameSignals.Instance.onLevelFailed += RefreshMoney;
            UISignals.Instance.onClickIncome += OnSetValueMultiplier;
        }

        private void OnSendMoney(int value)
        {
            _money = value;
        }

        private void OnSetScore(int setScore)
        {
            _scoreCache = (setScore * _stackValueMultiplier) + _atmScoreValue;
            PlayerSignals.Instance.onSetTotalScore?.Invoke(_scoreCache);
        }

        private void OnSetAtmScore(int atmValues)
        {
            _atmScoreValue += atmValues * _stackValueMultiplier;
            AtmSignals.Instance.onSetAtmScoreText?.Invoke(_atmScoreValue);
        }

        private void OnSetValueMultiplier()
        {
            _stackValueMultiplier = CoreGameSignals.Instance.onGetIncomeLevel();
        }
        private void RefreshMoney()
        {
            _money += (int)(_scoreCache * ScoreSignals.Instance.onGetMultiplier());
            UISignals.Instance.onSetMoneyValue?.Invoke(_money);
        }

        private void OnReset()
        {
            _scoreCache = 0;
            _atmScoreValue = 0;
        }
        private void UnSubscribeEvents()
        {
            ScoreSignals.Instance.onSendMoney -= OnSendMoney;
            ScoreSignals.Instance.onGetMoney -= () => _money;
            ScoreSignals.Instance.onSetScore -= OnSetScore;
            ScoreSignals.Instance.onSetAtmScore -= OnSetAtmScore;
            CoreGameSignals.Instance.onMiniGameStart -=
                () => ScoreSignals.Instance.onSendFinalScore?.Invoke(_scoreCache);
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onLevelSuccessful -= RefreshMoney;
            CoreGameSignals.Instance.onLevelFailed -= RefreshMoney;
            UISignals.Instance.onClickIncome -= OnSetValueMultiplier;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }
    }
}
