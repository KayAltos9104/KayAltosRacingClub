using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KARC.Logic;

namespace KARC.Logic
{
    class SceneController//Осуществляет управление сцен
    {        
        Dictionary<string, Scene> _scenesDict;
        Scene _currentScene;
        string _currentSceneKey;

        public SceneController()
        {
            _scenesDict = new Dictionary<string, Scene>();            
        }

        public SceneController (Dictionary<string, Scene> scenesDict, string initialScene)
        {
            _scenesDict = scenesDict;
            _currentSceneKey = initialScene;
            SwitchScene(initialScene);
        }

        public void AddScene (string key, Scene scene)
        {
            _scenesDict.Add(key, scene);
        }

        public void RemoveScene(string key)
        {
            _scenesDict.Remove(key);
        }

        public Scene GetCurrentScene ()
        {
            return _currentScene;
        }

        public string GetCurrentSceneKey()
        {
            return _currentSceneKey;
        }

        public void SwitchScene (string sceneKey)
        {
            try
            {
                _currentScene = _scenesDict[sceneKey];
                _currentSceneKey = sceneKey;
            }
            catch
            {
                
            }
        }

        public void ButtonClick (object sender, ButtonEventArgs e)
        {
            //switch (order)
            //{
            //    case "StartGame":
            //        {
                        _currentSceneKey = "level0";                       
                        SwitchScene(_currentSceneKey);
                        //break;
            //        }
            //}
        }
        
    }
}
