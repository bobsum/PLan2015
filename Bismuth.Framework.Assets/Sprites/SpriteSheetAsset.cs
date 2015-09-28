using System.Collections;
using System.Collections.Generic;
using Bismuth.Framework.Content;
using Bismuth.Framework.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Bismuth.Framework.Assets.Sprites
{
    public class SpriteSheetAsset : IAssetSet
    {
        [ContentSerializer(Optional = true)]
        public string Name { get; set; }
        public string TextureAssetName { get; set; }
        public List<SpriteFrameAsset> Frames { get; set; }

        public object Load(IContentManager contentManager, string assetName)
        {
            SpriteFrame frame = (SpriteFrame)Frames.Find(f => f.Name == assetName).Load(contentManager);
            frame.Texture = contentManager.Load<Texture2D>(TextureAssetName);
            return frame;
        }

        public object Load(IContentManager contentManager)
        {
            Texture2D texture = contentManager.Load<Texture2D>(TextureAssetName);

            SpriteFrame[] frames = new SpriteFrame[Frames.Count];

            for (int i = 0; i < Frames.Count; i++)
            {
                frames[i] = (SpriteFrame)Frames[i].Load(contentManager);
                frames[i].Texture = texture;
            }

            return frames;
        }

        public IEnumerator<IAsset> GetEnumerator()
        {
            return Frames.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Frames.GetEnumerator();
        }
    }

    public class SpriteFrameAsset : IAsset
    {
        [ContentSerializer(Optional = true)]
        public string Name { get; set; }
        public Rectangle Rectangle { get; set; }
        public Vector2 Origin { get; set; }

        public object Load(IContentManager contentManager)
        {
            return new SpriteFrame
            {
                Rectangle = Rectangle,
                Origin = Origin
            };
        }
    }
}
