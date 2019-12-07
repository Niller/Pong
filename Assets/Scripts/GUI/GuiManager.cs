using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.GUI
{
    public class GuiManager
    {
        private GuiConfig _config;
        private Transform _root;

        private readonly List<IGuiController> _stack = new List<IGuiController>();
        private readonly HashSet<IGuiController> _guiControllers = new HashSet<IGuiController>();

        public void Initialize(GuiConfig config, Transform guiRoot)
        {
            _config = config;
            _root = guiRoot.transform;
        }

        public void Open(GuiViewType type, bool ignoreStack = false)
        {
            var view = _config.ItemsDictionary[type];

            //TODO Use pool
            var go = Object.Instantiate(view.View, _root);
            var controller = go.GetComponent<IGuiController>();

            if (controller == null)
            {
                Object.Destroy(go);
                throw new Exception($"Cannot create gui view {view.Type} with prefab {view.View.name}");
            }

            _guiControllers.Add(controller);

            if (ignoreStack)
            {
                return;
            }

            if (_stack.Count > 0)
            {
                _stack.Last().Root.SetActive(false);
            }

            _stack.Add(controller);
        }

        public void Open(IGuiController view)
        {
            view.Root.SetActive(true);
            _guiControllers.Add(view);
            _stack.Add(view);
        }

        public void Back()
        {
            if (_stack.Count <= 1)
            {
                return;
            }

            var current = _stack.Last();
            Close(current);

            Open(_stack.Last());
        }

        public void Close(IGuiController view)
        {
            _guiControllers.Remove(view);
            _stack.Remove(view);

            Object.Destroy(view.Root);
        }

        public void CloseAll()
        {
            while (_guiControllers.Count > 0)
            {
                Close(_guiControllers.Last());
            }
            _stack.Clear();
        }
    }
}

