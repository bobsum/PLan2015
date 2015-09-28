using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Bismuth.Framework.Scenes
{
    public class SceneManager
    {
        public BismuthGame Game { get; private set; }
        public Scene CurrentScene { get; private set; }
        public ISceneTransition Transition { get; set; }

        private readonly Dictionary<string, Scene> _scenes = new Dictionary<string, Scene>();

        public SceneManager(BismuthGame game)
        {
            Game = game;
        }

        public void AddScene(string name, Scene scene)
        {
            _scenes.Add(name, scene);
        }

        public void RemoveScene(string name)
        {
            _scenes.Remove(name);
        }

        public void ChangeScene(string name)
        {
            CurrentScene = _scenes[name];
        }

        public void ChangeScene(string name, ISceneTransition transition)
        {
            Scene nextScene = _scenes[name];

            Transition = transition;
            Transition.Begin(CurrentScene, nextScene);

            CurrentScene = nextScene;
        }

        public void Update(GameTime gameTime)
        {
            if (Transition != null)
            {
                Transition.Update(gameTime);
                if (Transition.IsFinished)
                    Transition = null;
            }
            else
            {
                CurrentScene.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            if (Transition != null)
            {
                Transition.Draw(gameTime);
            }
            else
            {
                CurrentScene.Draw(gameTime);
            }
        }
    }
}
