using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.GUI
{
    [CreateAssetMenu]
    public class GuiConfig : ScriptableObject, ISerializationCallbackReceiver
    {
        public GuiConfigItem[] Items;
        public Dictionary<GuiViewType, GuiConfigItem> ItemsDictionary;
        public void OnBeforeSerialize()
        {
        
        }

        public void OnAfterDeserialize()
        {
            if (Items == null)
            {
                return;
            }

            ItemsDictionary = new Dictionary<GuiViewType, GuiConfigItem>();

            foreach (var guiConfigItem in Items)
            {
                ItemsDictionary[guiConfigItem.Type] = guiConfigItem;
            }
        }
    }
}