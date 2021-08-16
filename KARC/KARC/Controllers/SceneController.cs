using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KARC.Logic;

namespace KARC.Logic
{
    class SceneController//Осуществляет управление в сценах
    {
        Dictionary<string, Scene> _scenesDict;
        Scene _currentScene;

        public SceneController()
        {
            _scenesDict = new Dictionary<string, Scene>();            
        }

        public SceneController (Dictionary<string, Scene> scenesDict, string initialScene)
        {
            _scenesDict = scenesDict;
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

        public void SwitchScene (string sceneKey) //Потом надо переделать, чтобы ключ получали прямо внутри контроллера
        {
            try
            {
                _currentScene = _scenesDict[sceneKey];
            }
            catch
            {
                
            }
        }
    }
}
