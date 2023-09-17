using Assets.Scripts.Runtime.Managers;
using Assets.Scripts.Runtime.Signals;
using UnityEngine;

namespace Assets.Scripts.Runtime.Commands.Stack
{
    //button ile alinan pralari tutar
    public class StackInitializerCommand
    {
        private StackManager _stackManager;
        private GameObject _money;

        //Parametreyi StackManagerden alir, money'de StackManagerden alir
        public StackInitializerCommand(StackManager stackManager,
            ref GameObject money)
        {
            _stackManager = stackManager;
            _money = money;
        }

        public void Execute()
        {
            var stackLevel = CoreGameSignals.Instance.onGetStackLevel();
            for (int i = 1; i < stackLevel; i++)
            {
                //money objesini Instanteder ve sonra ItemAdderOnStackCommand scriptine bak
                GameObject obj = Object.Instantiate(_money);
                _stackManager.AdderOnStackCommand.Execute(obj);
            }
            //su kismi sonra anla
            _stackManager.StackTypeUpdaterCommand.Execute();
        }
    }
}