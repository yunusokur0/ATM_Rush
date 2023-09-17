using Assets.Scripts.Runtime.Managers;
using Assets.Scripts.Runtime.Signals;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Commands.Stack
{
    //stack turu gunceler
    public class StackTypeUpdaterCommand
    {
        private List<GameObject> _collectableStack;
        private int _totalListScore;

        //listeyi StackMangerden alir
        public StackTypeUpdaterCommand(ref List<GameObject> collectableStack)
        {
            _collectableStack = collectableStack;
        }

        //listenin içerisinde bulunan öğelerin her birinin değerini toplayar
        public void Execute()
        {
            _totalListScore = 0;
            foreach (var items in _collectableStack)
            {
                _totalListScore += items.GetComponent<CollectableManager>().GetCurrentValue() + 1;
            }

            ScoreSignals.Instance.onSetScore?.Invoke(_totalListScore);
        }
    }
}