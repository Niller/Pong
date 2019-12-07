using System;
using UnityEngine;

namespace Assets.Scripts.GUI
{
    [Serializable]
    public struct GuiConfigItem
    {
        public GuiViewType Type;
        public GameObject View;
    }
}