using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using Bismuth.Framework.Composite;
using Bismuth.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Bismuth.Framework.Assets.Composite
{
    public class NodeAsset : IAsset
    {
        [ContentSerializer(Optional = true)]
        public string Name { get; set; }

        [ContentSerializer(Optional = true)]
        public string Class { get; set; }

        [ContentSerializer(Optional = true)]
        public List<CustomProperty> CustomProperties { get; set; }

        public Vector2 Position { get; set; }
        public float Rotation { get; set; }
        public float ScaleX { get; set; }
        public float ScaleY { get; set; }
        public float SkewX { get; set; }
        public float SkewY { get; set; }
        public bool FlipX { get; set; }
        public bool FlipY { get; set; }
        public float Opacity { get; set; }
        public bool IsVisible { get; set; }
        public int ZIndex { get; set; }

        public List<NodeAsset> Children { get; set; }

        public virtual object Load(IContentManager contentManager)
        {
            INode node;
            if (string.IsNullOrEmpty(Class))
            {
                node = CreateInstance();
            }
            else
            {
                Type type = Type.GetType(Class);
                if (type != null)
                    node = (INode)Activator.CreateInstance(type);
                else
                    node = CreateInstance(); // HACK: For the editor to work!
            }

            LoadProperties(contentManager, node);

            return node;
        }

        protected virtual INode CreateInstance()
        {
            return new Node();
        }

        protected virtual void LoadProperties(IContentManager contentManager, INode node)
        {
            node.Name = Name;

            node.Position = Position;
            node.Rotation = Rotation;
            node.ScaleX = ScaleX;
            node.ScaleY = ScaleY;
            node.SkewX = SkewX;
            node.SkewY = SkewY;
            node.FlipX = FlipX;
            node.FlipY = FlipY;
            node.Opacity = Opacity;
            node.IsVisible = IsVisible;
            node.ZIndex = ZIndex;

            for (int i = 0; i < Children.Count; i++)
            {
                node.Children.Add((INode)Children[i].Load(contentManager));
            }

            LoadCustomProperties(node);
        }

        private void LoadCustomProperties(INode node)
        {
            if (CustomProperties != null)
            {
                Type type = node.GetType();
                for (int i = 0; i < CustomProperties.Count; i++)
                {
                    CustomProperty customProperty = CustomProperties[i];
                    if (string.IsNullOrEmpty(customProperty.Name)) continue;

                    try
                    {
                        PropertyInfo propertyInfo = type.GetProperty(customProperty.Name);
                        if (propertyInfo == null) continue;

                        object value = Convert.ChangeType(customProperty.Value, propertyInfo.PropertyType, CultureInfo.InvariantCulture);
                        propertyInfo.SetValue(node, value, null);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(string.Format("Parsing custom property '{0}' with value '{1}' failed: {2}",
                            customProperty.Name, customProperty.Value, ex.Message));
                    }
                }
            }
        }
    }

    public class CustomProperty
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
