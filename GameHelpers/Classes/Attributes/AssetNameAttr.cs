using System;

namespace GameHelpers.Classes.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class AssetNameAttr : Attribute
    {
        public string AssetName { get; private set; }

        public AssetNameAttr(string theAssetName)
        {
            AssetName = theAssetName;
        }
    }
}
