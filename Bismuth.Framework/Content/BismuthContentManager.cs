using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Content;

namespace Bismuth.Framework.Content
{
    /// <summary>
    /// A specialized content manager used to handle the Bismuth asset system.
    /// </summary>
    public class BismuthContentManager : ContentManager, IContentManager
    {
        public BismuthContentManager(IServiceProvider serviceProvider) : base(serviceProvider) { }

        private readonly Dictionary<string, object> _loadedAssets = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Loads an asset that has been processed by the Content Pipeline.
        /// </summary>
        /// <param name="assetName">Asset name, relative to the loader root directory, and not including the .xnb extension.</param>
        public override T Load<T>(string assetName)
        {
            return Load<T>(assetName, false);
        }

        /// <summary>
        /// Loads an asset that has been processed by the Content Pipeline.
        /// </summary>
        /// <param name="assetName">Asset name, relative to the loader root directory, and not including the .xnb extension.</param>
        /// <param name="newInstance">True if an new instance should be created.</param>
        public T Load<T>(string assetName, bool newInstance)
        {
            assetName = GetCleanPath(assetName);

            object obj = null;

            if (newInstance)
            {
                obj = InternalLoad(assetName);
            }
            else if (!_loadedAssets.TryGetValue(assetName, out obj))
            {
                obj = InternalLoad(assetName);
                _loadedAssets.Add(assetName, obj);
            }

            if (!(obj is T)) throw new Exception(string.Format("The expected asset type was '{0}', but the concrete type is '{1}'.", typeof(T), obj.GetType()));

            return (T)obj;
        }

        private object InternalLoad(string assetName)
        {
            int separatorIndex = assetName.IndexOf('#');

            if (separatorIndex > 0)
            {
                string setName = assetName.Substring(0, separatorIndex);
                string itemName = assetName.Substring(separatorIndex + 1);

                IAssetSet assetSet = base.Load<IAssetSet>(setName);

                return assetSet.Load(this, itemName);
            }
            else
            {
                object obj = base.Load<object>(assetName);

                if (obj is IAsset)
                {
                    IAsset asset = (IAsset)obj;
                    obj = asset.Load(this);
                }

                return obj;
            }
        }

        private string GetCleanPath(string path)
        {
            return path.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
        }

        public override void Unload()
        {
            base.Unload();

            _loadedAssets.Clear();
        }
    }
}
