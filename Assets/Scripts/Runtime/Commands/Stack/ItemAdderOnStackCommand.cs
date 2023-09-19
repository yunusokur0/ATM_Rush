using Assets.Scripts.Runtime.Data.ValueObject;
using Assets.Scripts.Runtime.Managers;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Commands.Stack
{
    public class ItemAdderOnStackCommand
    {
        private StackManager _stackManager;
        private List<GameObject> _collectableStack;
        private StackData _data;

        public ItemAdderOnStackCommand(StackManager stackManager, ref List<GameObject> collectableStack,
            ref StackData stackData)
        {
            _stackManager = stackManager;
            _collectableStack = collectableStack;
            _data = stackData;
        }

        public void Execute(GameObject collectableGameObject)
        {
            if (_collectableStack.Count <= 0)
            {
                _collectableStack.Add(collectableGameObject);
                collectableGameObject.transform.SetParent(_stackManager.transform);
                collectableGameObject.transform.localPosition = new Vector3(0, 0.5f, 0);
            }
            else
            {
                collectableGameObject.transform.SetParent(_stackManager.transform);
                Vector3 newPos = _collectableStack[^1].transform.localPosition;
                newPos.z += _data.CollectableOffsetInStack;
                collectableGameObject.transform.localPosition = newPos;
                _collectableStack.Add(collectableGameObject);
            }
        }
    }
}
