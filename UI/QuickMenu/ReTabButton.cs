﻿using System;
using ReMod.Core.VRChat;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Elements.Controls;

namespace ReMod.Core.UI.QuickMenu
{
    public class ReTabButton : UiElement
    {
        private static GameObject _tabButtonPrefab;
        private static GameObject TabButtonPrefab
        {
            get
            {
                if (_tabButtonPrefab == null)
                {
                    _tabButtonPrefab = QuickMenuEx.Instance.field_Public_Transform_0.Find("Window/Page_Buttons_QM/HorizontalLayoutGroup/Page_Settings").gameObject;
                }
                return _tabButtonPrefab;
            }
        }

        protected ReTabButton(string name, string tooltip, Action action, Sprite sprite) : base(TabButtonPrefab, TabButtonPrefab.transform.parent, $"Page_{name}")
        {
            var button = GameObject.GetComponent<Button>();
            button.onClick = new Button.ButtonClickedEvent();
            button.onClick.AddListener(action);

            var uiTooltip = GameObject.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>();
            uiTooltip.field_Public_String_0 = tooltip;
            uiTooltip.field_Public_String_1 = tooltip;

            var iconImage = RectTransform.Find("Icon").GetComponent<Image>();
            iconImage.sprite = sprite;
            iconImage.overrideSprite = sprite;
        }
        public void AddAction(Action action)
        {
            var button = GameObject.GetComponent<Button>();
            button.onClick.AddListener(action);
        }

        public static ReTabButton Create(string name, string tooltip, string pageName, Sprite sprite)
        {
            ReTabButton tab = new ReTabButton(name, tooltip, () => { }, sprite);

            var menuTab = tab.RectTransform.GetComponent<MenuTab>();
            menuTab.field_Public_String_0 = GetCleanName($"QuickMenuReMod{pageName}");
            menuTab.field_Private_MenuStateController_0 = QuickMenuEx.MenuStateCtrl;
            tab.AddAction(new Action(menuTab.ShowTabContent));
            return tab;
        }
        public static ReTabButton Create(string name, string tooltip, Action action, Sprite sprite)
        {
            return new ReTabButton(name, tooltip, action, sprite);
        }
    }
}
