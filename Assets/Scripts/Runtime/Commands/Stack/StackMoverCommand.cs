﻿using Assets.Scripts.Runtime.Data.ValueObject;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Runtime.Commands.Stack
{
    public class StackMoverCommand
    {
        private StackData _data;

        public StackMoverCommand(ref StackData stackData)
        {
            _data = stackData;
        }
        public void Execute(float directionX, List<GameObject> collectableStack)
        {
            float direct = Mathf.Lerp(collectableStack[0].transform.localPosition.x, directionX,
                _data.LerpSpeed);
            collectableStack[0].transform.localPosition = new Vector3(direct, 0.5f, 0);
            StackItemsLerpMove(collectableStack);
        }

        private void StackItemsLerpMove(List<GameObject> collectableStack)
        {
            for (int i = 1; i < collectableStack.Count; i++)
            {
                Vector3 pos = collectableStack[i].transform.localPosition;
                pos.x = collectableStack[i - 1].transform.localPosition.x;
                float direct = Mathf.Lerp(collectableStack[i].transform.localPosition.x, pos.x, _data.LerpSpeed);
                collectableStack[i].transform.localPosition = new Vector3(direct, pos.y, pos.z);
            }
        }
    }
}
