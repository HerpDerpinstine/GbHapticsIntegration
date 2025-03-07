﻿using System.IO;
using MelonLoader.Preferences;
using GbHapticsIntegration.Setup.ConfigModels;

namespace GbHapticsIntegration.Setup.Devices
{
    internal class I_Head<T> : TactFile where T : CM_Toggle, new()
    {
        private MelonPreferences_ReflectiveCategory GeneralCategory;
        internal T General;

        internal I_Head(string basefolder, string className, bool should_register_pattern = true)
        {
            basefolder = Path.Combine(basefolder, "Head");

            General = Config.SetupCategory<T>(ref GeneralCategory, "General", basefolder, className);

            if (should_register_pattern)
                Register(
                    $"{basefolder.Replace("\\", "/").Replace("/", "_")}_{className}", 
                    $"{className}.tact", 
                    basefolder,
                    $"{className}_Head");
        }

        internal bool IsEnabled()
            => Config.HapticDevices.Head 
            && General.Enabled;
    }
}