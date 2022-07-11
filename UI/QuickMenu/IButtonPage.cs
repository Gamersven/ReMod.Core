using System;

using ErrorClient;

using UnityEngine;

namespace ReMod.Core.UI.QuickMenu
{
    public interface IButtonPage
    {
        ReMenuButton AddButton(string text, string tooltip, Action onClick, Sprite sprite = null, bool full = false);
        ReMenuButton AddSpacer(Sprite sprite = null);
        ReMenuToggle AddToggle(string text, string tooltip, Action<bool> onToggle, bool defaultValue = false);
        ReMenuToggle AddToggle(string text, string tooltip, ConfigValue<bool> configValue);
        ReMenuToggle AddToggle(string text, string tooltip, Action<bool> onToggle, bool defaultValue, Sprite iconOn, Sprite iconOff);
        ReMenuToggle AddToggle(string text, string tooltip, ConfigValue<bool> configValue, Sprite iconOn, Sprite iconOff);
        ReMenuToggle AddToggle(string text, string tooltip, ModSettings.Option<bool> configValue, Action<bool> action, Sprite iconOn = null, Sprite iconOff = null);
        ReMenuPage AddMenuPage(string text, string tooltip = "", Sprite sprite = null, bool full = false);
        ReCategoryPage AddCategoryPage(string text, string tooltip = "", Sprite sprite = null, bool full = false);
        ReMenuPage GetMenuPage(string name);
        ReCategoryPage GetCategoryPage(string name);
        void AddCategoryPage(string text, string tooltip, Action<ReCategoryPage> onPageBuilt, Sprite sprite = null);
        void AddMenuPage(string text, string tooltip, Action<ReMenuPage> onPageBuilt, Sprite sprite = null);
    }
}
