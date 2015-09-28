using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bismuth.Framework.Composite;

namespace Bismuth.Framework.Pins
{
    public enum PinBendDirection
    {
        Right,
        Left
    }

    public interface IPin : INode
    {
        string TargetName { get; set; }
        bool IsEnabled { get; set; }
        PinBendDirection BendDirection { get; set; }

        void Bind(INode target);
        void UpdateTarget();
    }
}
