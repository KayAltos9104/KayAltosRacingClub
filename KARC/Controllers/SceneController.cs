using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using KARC.ScenesTemplates;

namespace KARC.Controllers
{

    class SceneController
    {
        private Dictionary<string, Scene> _scenesDict =  new Dictionary<string, Scene>();
        private (string key, Scene scene) _currentScene;
      

        public SceneController()
        {
            
        }

        public void Initialize()
        {           
            SwitchScene("MainMenu");
        }

        public void AddScene(string key, Scene scene)
        {
            _scenesDict.Add(key, scene);
        }

        public void RemoveScene(string key)
        {
            _scenesDict.Remove(key);
        }

        public (string key, Scene scene) GetCurrentScene()
        {
            return _currentScene;
        }
       
        private void SwitchScene(string sceneKey)
        {
            try
            {
                _currentScene = (key:sceneKey, scene: _scenesDict[sceneKey]);
            }
            catch
            {

            }
        }

        public void Update(object sender, KeyBoardEventArgs e)
        {
            if (e.GetPushedButtons()[0] == Keys.Escape)
            {
                var game = (MainCycle)sender;
                game.Exit();
            }
        }

    }
}
