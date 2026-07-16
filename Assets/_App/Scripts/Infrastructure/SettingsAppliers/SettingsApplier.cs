using System;
using SparksOfTheAbyss.Data.Settings;

namespace SparksOfTheAbyss.Infrastructure.SettingsAppliers
{
    public abstract class SettingsApplier
    {
        public abstract IDisposable BindSettings(SettingsModel settingsModel);
    }
}