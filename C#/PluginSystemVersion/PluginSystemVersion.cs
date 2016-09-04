/*
  Copyright (C) 2014 Birunthan Mohanathas

  This program is free software; you can redistribute it and/or
  modify it under the terms of the GNU General Public License
  as published by the Free Software Foundation; either version 2
  of the License, or (at your option) any later version.

  This program is distributed in the hope that it will be useful,
  but WITHOUT ANY WARRANTY; without even the implied warranty of
  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
  GNU General Public License for more details.

  You should have received a copy of the GNU General Public License
  along with this program; if not, write to the Free Software
  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

using RainManager;
using System;

// Overview: This example demonstrates the basic concept of Rainmeter C# plugins.

// Sample skin:
/*
    [Rainmeter]
    Update=1000
    BackgroundMode=2
    SolidColor=000000

    [mString]
    Measure=Plugin
    Plugin=RainManager.dll
	PluginAssemblyName=SystemVersion
    PluginMeasureName=SystemVersion
    PluginMeasureType=String

    [mMajor]
    Measure=Plugin
    Plugin=RainManager.dll
	PluginAssemblyName=SystemVersion
    PluginMeasureName=SystemVersion
    PluginMeasureType=Major

    [mMinor]
    Measure=Plugin
    Plugin=RainManager.dll
	PluginAssemblyName=SystemVersion
    PluginMeasureName=SystemVersion
    PluginMeasureType=Minor

    [mNumber]
    Measure=Plugin
    Plugin=RainManager.dll
	PluginAssemblyName=SystemVersion
    PluginMeasureName=SystemVersion
    PluginMeasureType=Number

    [Text1]
    Meter=STRING
    MeasureName=mString
    MeasureName2=mMajor
    MeasureName3=mMinor
    MeasureName4=mNumber
    X=5
    Y=5
    W=300
    H=70
    FontColor=FFFFFF
    Text="String: %1#CRLF#Major: %2#CRLF#Minor: %3#CRLF#Number: %4#CRLF#"

    [Text2]
    Meter=STRING
    MeasureName=mString
    MeasureName2=mMajor
    MeasureName3=mMinor
    MeasureName4=mNumber
    NumOfDecimals=1
    X=5
    Y=5R
    W=300
    H=70
    FontColor=FFFFFF
    Text="String: %1#CRLF#Major: %2#CRLF#Minor: %3#CRLF#Number: %4#CRLF#"
*/

namespace PluginSystemVersion
{
    public class SystemVersionSkin : PluginSkin
    {
        public SystemVersionSkin(RainmeterSkinHandler skinHandler, RainmeterAPI api) : base(skinHandler, api)
        {
        }

        public override void Dispose()
        {
        }
    }

    public enum SystemVersionMeasureEnum
    {
        Major,
        Minor,
        Number,
        String
    }
    public class SystemVersionMeasure : PluginMeasure<SystemVersionSkin, SystemVersionMeasureEnum>
    {
        public SystemVersionMeasure(string pluginType, SystemVersionSkin skin, RainmeterAPI api) : base(pluginType, skin, api)
        {
        }

        public override void Reload(RainmeterAPI rm, ref double maxValue)
        {
        }

        public override double GetNumeric()
        {
            switch (TypeEnum)
            {
                case SystemVersionMeasureEnum.Major:
                    return (double) Environment.OSVersion.Version.Major;

                case SystemVersionMeasureEnum.Minor:
                    return (double) Environment.OSVersion.Version.Minor;

                case SystemVersionMeasureEnum.Number:
                    return (double) Environment.OSVersion.Version.Major + ((double) Environment.OSVersion.Version.Minor / 10.0);
            }

            // MeasureType.MajorMinor is not a number and and therefore will be
            // returned in GetString.

            return 0.0;
        }

        public override string GetString()
        {
            switch (TypeEnum)
            {
                case SystemVersionMeasureEnum.String:
                    return string.Format("{0}.{1} (Build {2})", Environment.OSVersion.Version.Major, Environment.OSVersion.Version.Minor, Environment.OSVersion.Version.Build);
            }

            // MeasureType.Major, MeasureType.Minor, and MeasureType.Number are
            // numbers. Therefore, null is returned here for them. This is to
            // inform Rainmeter that it can treat those types as numbers.

            return null;
        }

        public override void ExecuteBang(string args)
        {
        }

        public override void Dispose()
        {
        }
    }
}
