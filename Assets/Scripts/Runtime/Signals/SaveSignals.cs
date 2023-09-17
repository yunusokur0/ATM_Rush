using Assets.Scripts.Runtime.Extentions;
using System;
using UnityEngine.Events;

namespace Assets.Scripts.Runtime.Signals
{
    public class SaveSignals : MonoSingleton<SaveSignals>
    {
        public UnityAction onSaveGameData = delegate { };
        public Func<byte> onGetLevelID = delegate { return 0; };
        public Func<float> onGetMoney = delegate { return 0; };
    }
}