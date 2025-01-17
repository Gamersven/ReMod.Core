﻿using System;
using ReMod.Core.Unity;
using ReMod.Core.VRChat;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace ReMod.Core.UI.QuickMenu
{
    public class ReMenuSlider : UiElement
    {
        private readonly Slider _sliderComponent;
        
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

        public float Value
        {
            get => _sliderComponent.value;
            set => Slide(value);
        }


        public ReMenuSlider(string text, string tooltip, Action<float> onSlide, Transform parent, float defaultValue = 0, float minValue = 0, float maxValue = 10, bool wholenumbers = true, bool percentagebased = false) : base(QuickMenuEx.SliderPrefab, parent, $"Slider_{text}")
        {
            Object.DestroyImmediate(GameObject.GetComponent<UIInvisibleGraphic>()); // Fix for having clickable area overlap main quickmenu ui

            var name = RectTransform.GetChild(0).GetComponentInChildren<TextMeshProUGUI>();
            name.text = text;

            var textValue = RectTransform.GetChild(1).GetComponentInChildren<TextMeshProUGUI>();
            textValue.SetText((defaultValue * (percentagebased ? 100 : 1)).ToString("F"));  //This shit don't work

            _sliderComponent = GameObject.GetComponentInChildren<Slider>();
            _sliderComponent.onValueChanged = new Slider.SliderEvent();
            _sliderComponent.onValueChanged.AddListener(new Action<float>((value) => onSlide(percentagebased ? value / 100 : value)));
            _sliderComponent.onValueChanged.AddListener(new Action<float>(val => textValue.SetText(val.ToString(wholenumbers ? "" : "F"))));
            _sliderComponent.m_OnValueChanged = _sliderComponent.onValueChanged;

            _sliderComponent.minValue = minValue;
            _sliderComponent.maxValue = maxValue;
            _sliderComponent.value = defaultValue;
            _sliderComponent.wholeNumbers = wholenumbers;

            _tooltip = GameObject.GetComponent<VRC.UI.Elements.Tooltips.UiTooltip>();
            _tooltip.field_Public_String_0 = tooltip;
            _tooltip.field_Public_String_1 = tooltip;

            Slide(defaultValue,false);

            EnableDisableErrorListener.RegisterSafe();
            var edl = GameObject.AddComponent<EnableDisableErrorListener>();
        }

        public void Slide(float value, bool callback = true)
        {
            _sliderComponent.Set(value, callback);
        }
    }
}
