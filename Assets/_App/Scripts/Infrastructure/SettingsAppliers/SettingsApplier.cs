using System;
using JustUseMe.Data.Settings;

namespace JustUseMe.Infrastructure.SettingsAppliers
{
    public abstract class SettingsApplier
    {
        public abstract IDisposable BindSettings(SettingsModel settingsModel);
    }
}