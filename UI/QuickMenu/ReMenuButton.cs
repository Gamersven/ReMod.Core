﻿using System;
using ReMod.Core.VRChat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VRC.UI.Core.Styles;
using Object = UnityEngine.Object;

namespace ReMod.Core.UI.QuickMenu
{
    public class ReMenuButton : UiElement
    {
        private static GameObject _buttonPrefab;

        private static GameObject ButtonPrefab
        {
            get
            {
                if (_buttonPrefab == null)
                {
                    _buttonPrefab = QuickMenuEx.Instance.field_Public_Transform_0
                        .Find("Window/QMParent/Menu_Dashboard/ScrollRect").GetComponent<ScrollRect>().content
                        .Find("Buttons_QuickActions/Button_Respawn").gameObject;
                }
                return _buttonPrefab;
            }
        }

        private readonly TextMeshProUGUI _text;

        public string Text
        {
            get => _text.text;
            set => _text.SetText(value);
        }
        
        private VRC.UI.Elements.Tooltips.UiTooltip _tooltip;

        public string Tooltip {
            get => _tooltip != null ? _tooltip.field_Public_String_0 : "";
            set
            {
                if (_tooltip == null) return;
                _tooltip.field_Public_String_0 = value;
                _tooltip.field_Public_String_1 = value;
            }
        }

        private readonly StyleElement _styleElement;
        private readonly Button _button;
        public bool Interactable
        {
            get => _button.interactable;
            set
            {
                _button.interactable = value;
                _styleElement.Method_Private_Void_Boolean_Boolean_0(value);
            }
        }

        public Image Background { get; }

        public ReMenuButton(string text, string tooltip, Action onClick, Transform parent, Sprite sprite = null, bool full = false) : base(ButtonPrefab, parent,
            $"Button_{text}")
        {
            _text = GameObject.GetComponentInChildren<TextMeshProUGUI>();
            _text.text = text;
            _text.richText = true;

            Background = RectTransform.Find("Background").GetComponent<Image>();
            _styleElement = GameObject.GetComponent<StyleElement>();

            var icon = RectTransform.Find("Icon").gameObject;
            if (sprite == null)
            {
                if (full)
                {
                    _text.fontSize = 35;
                    _text.enableAutoSizing = true;
                    _text.color = new Color(0.4157f, 0.8902f, 0.9765f, 1f);
                    _text.m_fontColor = new Color(0.4157f, 0.8902f, 0.9765f, 1f);
                    _text.m_htmlColor = new Color(0.4157f, 0.8902f, 0.9765f, 1f);
                    _text.transform.localPosition = new Vector3(_text.transform.localPosition.x, -30f);

                    var layoutElement = Background.gameObject.AddComponent<LayoutElement>();
                    layoutElement.ignoreLayout = true;

                    var horizontalLayout = GameObject.AddComponent<HorizontalLayoutGroup>();
                    horizontalLayout.padding.right = 10;
                    horizontalLayout.padding.left = 10;

                    var styleElement = _text.GetComponent<StyleElement>();
                    styleElement.field_Public_String_1 = "H1";
                    icon.SetActive(false);
                }
                else
                {
                    icon.SetActive(false);
                    //var iconImage = RectTransform.Find("Icon").GetComponent<Image>();
                    //iconImage.sprite = null;
                    //iconImage.overrideSprite = null;
                    //iconImage.enabled = false;
                    //_styleElement.field_Public_String_0 = "None";

                }
            }
            else
            {
                if (full)
                {
                    Background.sprite = sprite;
                    Background.overrideSprite = sprite;
                    icon.SetActive(false);
                }
                else
                {
                    var iconImage = icon.GetComponent<Image>();
                    iconImage.sprite = sprite;
                    iconImage.overrideSprite = sprite;
                }
            }



            Object.DestroyImmediate(RectTransform.Find("Icon_Secondary").gameObject);
            Object.DestroyImmediate(RectTransform.Find("Badge_Close").gameObject);
            Object.DestroyImmediate(RectTransform.Find("Badge_MMJump").gameObject);

            var uiTooltips = GameObject.GetComponents<VRC.UI.Elements.Tooltips.UiTooltip>();
            if (uiTooltips.Length > 0)
            {
                //Fuck tooltips, all my friends hate tooltips
                _tooltip = uiTooltips[0];
                
                for(int i = 1; i < uiTooltips.Length; i++)
                    Object.DestroyImmediate(uiTooltips[i]);
            }

            if (_tooltip != null)
            {
                _tooltip.field_Public_String_0 = tooltip;
                _tooltip.field_Public_String_1 = tooltip;
            }

            if (onClick != null)
            {
                _button = GameObject.GetComponent<Button>();
                _button.onClick = new Button.ButtonClickedEvent();
                _button.onClick.AddListener(new Action(onClick));
            }
        }

        public ReMenuButton(Transform transform) : base(transform)
        {
            _text = GameObject.GetComponentInChildren<TextMeshProUGUI>();
            _styleElement = GameObject.GetComponent<StyleElement>();
            _button = GameObject.GetComponent<Button>();
            Background = RectTransform.Find("Background").GetComponent<Image>();
        }

        public static ReMenuButton Create(string text, string tooltip, Action onClick, Transform parent, Sprite sprite = null, bool full = false)
        {
            return new ReMenuButton(text, tooltip, onClick, parent, sprite, full);
        }
    }
}
