﻿using GbHapticsIntegration.Setup.Devices;
using GbHapticsIntegration.Setup.ConfigModels;
using MelonLoader;
using Tomlet.Attributes;
using UnityEngine;

namespace GbHapticsIntegration.Setup.Effects
{
    internal class E_Blunt3 : I_EffectBase
    {
        [TomlDoNotInlineObject]
        internal class CM_VelocityNew : CM_Velocity
        {
            public CM_VelocityNew()
                => Multiplier = 0.0075f;
        }
        internal I_Hand<CM_Intensity, CM_VelocityNew> HandL;
        internal I_Hand<CM_Intensity, CM_VelocityNew> HandR;

        [TomlDoNotInlineObject]
        internal class CM_VelocityNew2 : CM_Velocity
        {
            public CM_VelocityNew2()
                => Multiplier = 0.00575f;
        }
        internal I_Arm<CM_Intensity, CM_VelocityNew2> ArmL;
        internal I_Arm<CM_Intensity, CM_VelocityNew2> ArmR;

        internal E_Blunt3(I_WeaponBase weaponBase, string basefolder) : base(weaponBase)
        {
            string className = "Blunt";

            HandL = new I_Hand<CM_Intensity, CM_VelocityNew>(true, basefolder, className);
            HandR = new I_Hand<CM_Intensity, CM_VelocityNew>(false, basefolder, className);
            ArmL = new I_Arm<CM_Intensity, CM_VelocityNew2>(true, basefolder, className);
            ArmR = new I_Arm<CM_Intensity, CM_VelocityNew2>(false, basefolder, className);
        }

        internal void Play(Vector3 velocity, bool is_left)
        {
            if (!Config.HapticEffects.Blunt)
                return;

            if (is_left)
            {
                if (HandL.IsEnabled()
                    && !WeaponBase.IsPlaying_Cut(bHaptics.PositionType.HandL)
                    && !WeaponBase.IsPlaying_Shoot(bHaptics.PositionType.HandL)
                    && !WeaponBase.IsPlaying_ShootString(bHaptics.PositionType.HandL)
                    && !WeaponBase.IsPlaying_Stab(bHaptics.PositionType.HandL))
                {
                    bHaptics.ScaleOption scaleOption = GetScaleOption(velocity.magnitude, HandL.VelocityScaling);
                    scaleOption.Intensity *= HandL.General.IntensityScale;
                    HandL.Play(scaleOption);
                }

                if (ArmL.IsEnabled()
                    && !WeaponBase.IsPlaying_Cut(bHaptics.PositionType.ForearmL)
                    && !WeaponBase.IsPlaying_Shoot(bHaptics.PositionType.ForearmL)
                    && !WeaponBase.IsPlaying_ShootString(bHaptics.PositionType.ForearmL)
                    && !WeaponBase.IsPlaying_Stab(bHaptics.PositionType.ForearmL))
                {
                    bHaptics.ScaleOption scaleOption = GetScaleOption(velocity.magnitude, ArmL.VelocityScaling);
                    scaleOption.Intensity *= ArmL.General.IntensityScale;
                    ArmL.Play(scaleOption);
                }

                return;
            }

            if (HandR.IsEnabled()
                && !WeaponBase.IsPlaying_Cut(bHaptics.PositionType.HandR)
                && !WeaponBase.IsPlaying_Shoot(bHaptics.PositionType.HandR)
                && !WeaponBase.IsPlaying_ShootString(bHaptics.PositionType.HandR)
                && !WeaponBase.IsPlaying_Stab(bHaptics.PositionType.HandR))
            {
                bHaptics.ScaleOption scaleOption = GetScaleOption(velocity.magnitude, HandR.VelocityScaling);
                scaleOption.Intensity *= HandR.General.IntensityScale;
                HandR.Play(scaleOption);
            }

            if (ArmR.IsEnabled()
                    && !WeaponBase.IsPlaying_Cut(bHaptics.PositionType.ForearmR)
                    && !WeaponBase.IsPlaying_Shoot(bHaptics.PositionType.ForearmR)
                    && !WeaponBase.IsPlaying_ShootString(bHaptics.PositionType.ForearmR)
                    && !WeaponBase.IsPlaying_Stab(bHaptics.PositionType.ForearmR))
            {
                bHaptics.ScaleOption scaleOption = GetScaleOption(velocity.magnitude, ArmR.VelocityScaling);
                scaleOption.Intensity *= ArmR.General.IntensityScale;
                ArmR.Play(scaleOption);
            }
        }

        internal override bool IsPlaying(bHaptics.PositionType positionType)
        {
            switch (positionType)
            {
                case bHaptics.PositionType.HandL:
                    return HandL.IsPlaying();
                case bHaptics.PositionType.ForearmL:
                    return ArmL.IsPlaying();
                case bHaptics.PositionType.HandR:
                    return HandR.IsPlaying();
                case bHaptics.PositionType.ForearmR:
                    return ArmR.IsPlaying();
                default:
                    return false;
            }
        }
    }
}