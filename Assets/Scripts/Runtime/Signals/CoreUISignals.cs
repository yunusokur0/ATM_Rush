using Assets.Scripts.Runtime.Enums;
using Assets.Scripts.Runtime.Extentions;
using UnityEngine.Events;

namespace Assets.Scripts.Runtime.Signals
{
    public class CoreUISignals : MonoSingleton<CoreUISignals>
    {
        public UnityAction<UIPanelTypes, int> onOpenPanel = delegate { };
        public UnityAction<int> onClosePanel = delegate { };
        public UnityAction onCloseAllPanels = delegate { };
    }
}