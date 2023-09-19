using Assets.Scripts.Runtime.Commands.Level;
using Assets.Scripts.Runtime.Signals;
using UnityEngine;

namespace Assets.Scripts.Runtime.Managers
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Holder")] [SerializeField] internal GameObject levelHolder;
        [Space] [SerializeField] private byte totalLevelCount;

        private LevelLoaderCommand _levelLoader;
        private LevelDestroyerCommand _levelDestroyer;
        private byte _currentLevel;
        private void Awake()
        {
            Init();
            _currentLevel = GetActiveLevel();
        }

        private void Init()
        {
            _levelLoader = new LevelLoaderCommand(this);
            _levelDestroyer = new LevelDestroyerCommand(this);
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void Start()
        {
            CoreGameSignals.Instance.onLevelInitialize?.Invoke(_currentLevel);
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize += _levelLoader.Execute;
            CoreGameSignals.Instance.onClearActiveLevel += _levelDestroyer.Execute;
            SaveSignals.Instance.onGetLevelID += GetLevelID;
            CoreGameSignals.Instance.onNextLevel += OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel += OnRestartLevel;
        }

        private byte GetLevelID()
        {
            return _currentLevel;
        }
        private byte GetActiveLevel()
        {
            if (!ES3.FileExists()) return 0;
            return (byte)(ES3.KeyExists("Level") ? ES3.Load<int>("Level") % totalLevelCount : 0);
        }

        private void OnNextLevel()
        {
            _currentLevel++;
          
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke(_currentLevel);
            SaveSignals.Instance.onSaveGameData?.Invoke();
        }

        private void OnRestartLevel()
        {
           
            CoreGameSignals.Instance.onClearActiveLevel?.Invoke();
            CoreGameSignals.Instance.onLevelInitialize?.Invoke(_currentLevel);
            SaveSignals.Instance.onSaveGameData?.Invoke();
        }

        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onLevelInitialize -= _levelLoader.Execute;
            CoreGameSignals.Instance.onClearActiveLevel -= _levelDestroyer.Execute;
            SaveSignals.Instance.onGetLevelID -= GetLevelID;
            CoreGameSignals.Instance.onNextLevel -= OnNextLevel;
            CoreGameSignals.Instance.onRestartLevel -= OnRestartLevel;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
    }
}
