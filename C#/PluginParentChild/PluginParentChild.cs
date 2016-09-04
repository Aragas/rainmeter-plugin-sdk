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

// Overview: This example demonstrates a basic implementation of a parent/child
// measure structure. In this particular example, we have a "parent" measure
// which contains the values for the options "ValueA", "ValueB", and "ValueC".
// The child measures are used to return a specific value from the parent.

// Use case: You could, for example, have a "main" parent measure that queries
// information some data set. The child measures can then be used to return
// specific information from the data queried by the parent measure.

// Sample skin:
/*
    [Rainmeter]
    Update=1000
    BackgroundMode=2
    SolidColor=000000

    [mParent]
    Measure=Plugin
    Plugin=RainManager.dll
	PluginAssemblyName=ParentChild
    PluginMeasureName=Parent
    PluginMeasureType=A
    ValueA=111
    ValueB=222
    ValueC=333

    [mChild1]
    Measure=Plugin
    Plugin=RainManager.dll
	PluginAssemblyName=ParentChild
    PluginMeasureName=Child
    PluginMeasureType=B
    ParentName=mParent

    [mChild2]
    Measure=Plugin
    Plugin=RainManager.dll
	PluginAssemblyName=ParentChild
    PluginMeasureName=Child
    PluginMeasureType=C
    ParentName=mParent

    [Text]
    Meter=STRING
    MeasureName=mParent
    MeasureName2=mChild1
    MeasureName3=mChild2
    X=5
    Y=5
    W=200
    H=55
    FontColor=FFFFFF
    Text="mParent: %1#CRLF#mChild1: %2#CRLF#mChild2: %3"
*/

namespace PluginParentChild
{
    public class BaseSkin : PluginSkin
    {
        public BaseSkin(RainmeterSkinHandler skinHandler, RainmeterAPI api) : base(skinHandler, api)
        {
        }

        public override void Dispose()
        {
        }
    }
    public class ParentSkin : BaseSkin
    {
        public ParentSkin(RainmeterSkinHandler skinHandler, RainmeterAPI api) : base(skinHandler, api)
        {
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }
    public class ChildSkin : BaseSkin
    {
        public ChildSkin(RainmeterSkinHandler skinHandler, RainmeterAPI api) : base(skinHandler, api)
        {
        }

        public override void Dispose()
        {
            base.Dispose();
        }
    }

    public enum BaseMeasureEnum
    {
        A,
        B,
        C
    }
    public class BaseMeasure<TOverrideSkin> : PluginMeasure<TOverrideSkin, BaseMeasureEnum> where TOverrideSkin : BaseSkin
    {
        public BaseMeasure(string pluginType, TOverrideSkin skin, RainmeterAPI api) : base(pluginType, skin, api)
        {
        }

        public override void Reload(RainmeterAPI api, ref double maxValue)
        {
        }

        public override double GetNumeric()
        {
            return 0.0;
        }

        public override string GetString()
        {
            return null;
        }

        public override void ExecuteBang(string args)
        {
        }

        public override void Dispose()
        {
        }
    }

    public class ParentMeasure : BaseMeasure<ParentSkin>
    {
        public int ValueA;
        public int ValueB;
        public int ValueC;

        public ParentMeasure(string pluginType, ParentSkin skin, RainmeterAPI api) : base(pluginType, skin, api)
        {
        }

        public override void Reload(RainmeterAPI api, ref double maxValue)
        {
            base.Reload(api, ref maxValue);

            ValueA = api.ReadInt("ValueA", 0);
            ValueB = api.ReadInt("ValueB", 0);
            ValueC = api.ReadInt("ValueC", 0);
        }

        public override double GetNumeric()
        {
            return GetValue(TypeEnum);
        }

        public override string GetString()
        {
            return null;
        }

        public override void ExecuteBang(string args)
        {
        }

        public override void Dispose()
        {
        }


        public double GetValue(BaseMeasureEnum type)
        {
            switch (type)
            {
                case BaseMeasureEnum.A:
                    return ValueA;

                case BaseMeasureEnum.B:
                    return ValueB;

                case BaseMeasureEnum.C:
                    return ValueC;
            }

            return 0.0;
        }
    }

    public class ChildMeasure : BaseMeasure<ChildSkin>
    {
        private ParentMeasure ParentMeasure = null;

        public ChildMeasure(string pluginType, ChildSkin skin, RainmeterAPI api) : base(pluginType, skin, api)
        {
        }

        public override void Reload(RainmeterAPI api, ref double maxValue)
        {
            base.Reload(api, ref maxValue);

            string parentName = api.ReadString("ParentName", "");
            IntPtr skinPtr = api.GetSkin();


            ParentMeasure = null;
            foreach (PluginSkin skin in Skin.SkinHandler.PluginSkins)
            {
                if (skin.Ptr == Skin.Ptr)
                {
                    foreach (PluginMeasure measure in skin.PluginMeasures)
                    {
                        ParentMeasure pMeasure = measure as ParentMeasure;
                        if (pMeasure != null && pMeasure.Name == parentName)
                        {
                            ParentMeasure = pMeasure;
                        }
                    }
                }
            }
           
            if (ParentMeasure == null)
            {
                RainmeterAPI.Log(RainmeterAPI.LogType.Error, "ParentChild.dll: ParentName=" + parentName + " not valid");
            }
        }

        public override double GetNumeric()
        {
            if (ParentMeasure != null)
            {
                return ParentMeasure.GetValue(TypeEnum);
            }

            return 0.0;
        }

        public override string GetString()
        {
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
