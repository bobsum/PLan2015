using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bismuth.Framework.Assets.Composite;
using Bismuth.Framework.Composite;
using Bismuth.Framework.Content;
using Bismuth.Framework.Pins;

namespace Bismuth.Framework.Assets.Pins
{
    public class PinAsset : NodeAsset
    {
        public string TargetName { get; set; }
        public bool IsEnabled { get; set; }
        public bool IsRotationPinned { get; set; }
        public PinBendDirection BendDirection { get; set; }

        protected override INode CreateInstance()
        {
            return new Pin();
        }

        protected override void LoadProperties(IContentManager contentManager, INode node)
        {
            base.LoadProperties(contentManager, node);

            Pin pin = (Pin)node;
            pin.TargetName = TargetName;
            pin.IsEnabled = IsEnabled;
            pin.IsRotationPinned = IsRotationPinned;
            pin.BendDirection = BendDirection;
        }
    }
}
