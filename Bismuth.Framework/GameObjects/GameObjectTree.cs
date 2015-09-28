using Bismuth.Framework.Composite;

namespace Bismuth.Framework.GameObjects
{
    public static class GameObjectTree
    {
        public static void Initialize(INode node)
        {
            GameObject gameObject = node as GameObject;
            if (gameObject != null)
            {
                gameObject.Initialize();
            }

            for (int i = 0; i < node.Children.Count; i++)
            {
                Initialize(node.Children[i]);
            }
        }
    }
}
