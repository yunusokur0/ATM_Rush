using Assets.Scripts.Runtime.Commands.Stack;
using Assets.Scripts.Runtime.Data.UnityObject;
using Assets.Scripts.Runtime.Data.ValueObject;
using Assets.Scripts.Runtime.Signals;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Managers
{
    public class StackManager : MonoBehaviour
    {
        //paranin engele vb carpinca dagilmaasi
        public StackJumperCommand StackJumperCommand;
        //bu kaldi yarida
        public StackTypeUpdaterCommand StackTypeUpdaterCommand;
        //para toplama
        public ItemAdderOnStackCommand AdderOnStackCommand;

        public bool LastCheck;

        [SerializeField] private GameObject money;

        private StackData _data;
        private List<GameObject> _collectableStack = new List<GameObject>();

        private StackMoverCommand _stackMoverCommand;
        //paranin atm veya engele carpica setactive edilmesi 
        private ItemRemoverOnStackCommand _itemRemoverOnStackCommand;
        //paralarin buyume animi
        private StackAnimatorCommand _stackAnimatorCommand;
        //sondaki banka geldiginde yapilacaklar
        private StackInteractionWithConveyorCommand _stackInteractionWithConveyorCommand;
        //button ile alinan pralari tutar
        private StackInitializerCommand _stackInitializerCommand;

        private readonly string _stackDataPath = "Data/CD_Stack";

        private void Awake()
        {
            _data = GetStackData();
            Init();
        }

        private void Init()
        {
            _stackMoverCommand = new StackMoverCommand(ref _data);
            AdderOnStackCommand = new ItemAdderOnStackCommand(this, ref _collectableStack, ref _data);
            _itemRemoverOnStackCommand = new ItemRemoverOnStackCommand(this, ref _collectableStack);
            _stackAnimatorCommand = new StackAnimatorCommand(_data, ref _collectableStack);
            StackJumperCommand = new StackJumperCommand(_data, ref _collectableStack);
            _stackInteractionWithConveyorCommand = new StackInteractionWithConveyorCommand(this, ref _collectableStack);
            StackTypeUpdaterCommand = new StackTypeUpdaterCommand(ref _collectableStack);
            _stackInitializerCommand = new StackInitializerCommand(this, ref money);
        }

        private StackData GetStackData()
        {
            return Resources.Load<CD_Stack>(_stackDataPath).Data;
        }

        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            StackSignals.Instance.onInteractionCollectable += OnInteractionWithCollectable;
            StackSignals.Instance.onInteractionObstacle += _itemRemoverOnStackCommand.Execute;
            StackSignals.Instance.onInteractionATM += OnInteractionWithATM;
            StackSignals.Instance.onInteractionConveyor +=
                _stackInteractionWithConveyorCommand.Execute;
            StackSignals.Instance.onStackFollowPlayer += OnStackMove;
            StackSignals.Instance.onUpdateType += StackTypeUpdaterCommand.Execute;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
        }

        //direction player managerden aliyor, playerin xi ve z sini aliyor
        private void OnStackMove(Vector2 direction)
        {
            transform.position = new Vector3(0, gameObject.transform.position.y, direction.y + 2f);
            if (gameObject.transform.childCount > 0)
            {
                _stackMoverCommand.Execute(direction.x, _collectableStack);
            }
        }

        private void OnInteractionWithATM(GameObject collectableGameObject)
        {
            ScoreSignals.Instance.onSetAtmScore?.Invoke((int)collectableGameObject.GetComponent<CollectableManager>()
                .GetCurrentValue() + 1);
            if (LastCheck == false)
            {
                _itemRemoverOnStackCommand.Execute(collectableGameObject);
            }
            else
            {
                collectableGameObject.SetActive(false);
            }
        }

        private void OnInteractionWithCollectable(GameObject collectableGameObject)
        {
            DOTween.Complete(StackJumperCommand);
            AdderOnStackCommand.Execute(collectableGameObject);
            StartCoroutine(_stackAnimatorCommand.Execute());
            StackTypeUpdaterCommand.Execute();
        }

        private void OnPlay()
        {
            _stackInitializerCommand.Execute();
        }

        private void UnSubscribeEvents()
        {
            StackSignals.Instance.onInteractionCollectable -= OnInteractionWithCollectable;
            StackSignals.Instance.onInteractionObstacle -= _itemRemoverOnStackCommand.Execute;
            StackSignals.Instance.onInteractionATM -= OnInteractionWithATM;
            StackSignals.Instance.onInteractionConveyor -=
                _stackInteractionWithConveyorCommand.Execute;
            StackSignals.Instance.onStackFollowPlayer -= OnStackMove;
            StackSignals.Instance.onUpdateType -= StackTypeUpdaterCommand.Execute;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnSubscribeEvents();
        }

        private void OnReset()
        {
            LastCheck = false;
            _collectableStack.Clear();
            _collectableStack.TrimExcess();
        }
    }
}