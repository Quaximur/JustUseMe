using System.Collections.Generic;
using R3;
using R3.Triggers;
using TMPro;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UIElements;

namespace JustUseMe.Shared.Localization
{
    public class UnityLocalizer : ILocalizer
    {
        public void Localize(TextElement textElement, string table, string entry = null)
        {
            entry ??= textElement.text;

            if (!string.IsNullOrEmpty(entry))
                textElement.SetBinding("text", new LocalizedString(table, entry));
        }
        
        public void Localize(Label label, string table, string entry = null)
        {
            entry ??= label.text;

            if (!string.IsNullOrEmpty(entry))
                label.SetBinding("text", new LocalizedString(table, entry));
        }

        public void Localize(Tab tab, string table, string entry = null)
        {
            entry ??= tab.label;

            if (!string.IsNullOrEmpty(entry))
                tab.SetBinding("label", new LocalizedString(table, entry));
        }

        public void Localize(Button button, string table, string entry = null)
        {
            entry ??= button.text;

            if (!string.IsNullOrEmpty(entry))
                button.SetBinding("text", new LocalizedString(table, entry));
        }

        public void Localize(DropdownField dropdown, string table, string entry = null)
        {
            entry ??= dropdown.label;
            if (!string.IsNullOrEmpty(entry))
                dropdown.SetBinding("label", new LocalizedString(table, entry));

            var localizedChoices = new List<string>();
            for (var i = 0; i < dropdown.choices.Count; i++)
            {
                var choice = dropdown.choices[i];
                var localizedString = new LocalizedString(table, choice);

                var i1 = i;
                LocalizedString.ChangeHandler onLocalized = (value) =>
                {
                    if (i1 < dropdown.choices.Count)
                        dropdown.choices[i1] = value;

                    if (dropdown.index == i1)
                        dropdown.value = value;
                };

                localizedString.StringChanged += onLocalized;

                dropdown.RegisterCallbackOnce<DetachFromPanelEvent>(_ => localizedString.StringChanged -= onLocalized);
                localizedChoices.Add(localizedString.GetLocalizedString());
            }
            dropdown.choices = localizedChoices;
        }

        public void Localize(TMP_Text tmpText, string table, string entry = null)
        {
            entry ??= tmpText.text;

            if (string.IsNullOrEmpty(entry)) 
                return;

            if (!tmpText.gameObject.TryGetComponent<LocalizeStringEvent>(out var localizeEvent))
                localizeEvent = tmpText.gameObject.AddComponent<LocalizeStringEvent>();

            localizeEvent.OnUpdateString.AddListener(tmpText.SetText);
            localizeEvent.StringReference = new LocalizedString(table, entry);
        }
    }
}