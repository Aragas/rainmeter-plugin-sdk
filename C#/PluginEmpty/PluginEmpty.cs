using RainManager;

namespace PluginEmpty
{
    public class EmptySkin : PluginSkin
    {
        public EmptySkin(RainmeterSkinHandler skinHandler, RainmeterAPI api) : base(skinHandler, api)
        {
        }

        public override void Dispose()
        {
        }
    }

    public enum EmptyMeasureEnum { }
    public class EmptyMeasure : PluginMeasure<EmptySkin, EmptyMeasureEnum>
    {
        public EmptyMeasure(string pluginType, EmptySkin skin, RainmeterAPI api) : base(pluginType, skin, api)
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
            return "";
        }

        public override void ExecuteBang(string args)
        {
        }

        public override void Dispose()
        {
        }
    }
}
