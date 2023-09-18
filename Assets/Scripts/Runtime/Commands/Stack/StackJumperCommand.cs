using Assets.Scripts.Runtime.Data.ValueObject;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Commands.Stack
{
    public class StackJumperCommand
    {
        private StackData _data;
        private List<GameObject> _collectableStack;
        private Transform _levelHolder;

        public StackJumperCommand(StackData stackData, ref List<GameObject> collectableStack)
        {
            _data = stackData;
            _collectableStack = collectableStack;
            _levelHolder = GameObject.Find("LevelHolder").transform;
        }

        public void Execute(int last, int index)
        {
            // last ve index ayni rakam olursa for dongusu calismaz direk zaten setactive olur ItemRemoverOnStackCommand de
            for (int i = last; i > index; i--)
            {

                _collectableStack[i].transform.GetChild(1).tag = "Collectable";
                _collectableStack[i].transform.SetParent(_levelHolder.transform.GetChild(0));
                _collectableStack[i].transform.DOJump(
                    new Vector3(
                        //yatayda rastgele bir yönde zıplatır
                        Random.Range(-_data.JumpItemsClampX, _data.JumpItemsClampX + 1),
                        0.5f,
                        _collectableStack[i].transform.position.z + Random.Range(10, 15)),
                    // nesnenin zıplama kuvvetini belirler
                    _data.JumpForce,
                    //zıplama sırasındaki dönüş sayısını rastgele olarak belirler, Zıplama animasyonunun süresini belirler.
                    Random.Range(1, 3), 0.5f
                );
                _collectableStack[i].transform.DOScale(Vector3.one, 0);
                _collectableStack.RemoveAt(i);
                // listenin belegini temizler
                _collectableStack.TrimExcess();
            }
        }
    }
}