using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using KARC.ScenesTemplates;
using Microsoft.Xna.Framework.Graphics;

namespace KARC.Controllers
{

    class SceneController
    {
        private Dictionary<string, Scene> _scenesDict =  new Dictionary<string, Scene>();
        private (string key, Scene scene) _currentScene;
        
        const int pushInterfaceCoolDown = 100;
        int pushElapsedTime;

        public SceneController()
        {
            pushElapsedTime = 0;
        }

        public void Initialize()
        {           
            SwitchScene("MainMenu");
            _currentScene.scene.UpdateGraphics(MainCycle.windowWidth, MainCycle.windowHeight);//TODO:Избавиться от статики
            _currentScene.scene.InitializeScene();

            foreach (var scene in _scenesDict)
                scene.Value.Change += SwitchScene;
        }

        public void Update()
        {
            _currentScene.scene.Update();
        }
        public void Update(object sender, KeyBoardEventArgs e)
        {
            pushElapsedTime += e.ElapsedTime;
            if (e.GetPushedButtons()[0] == Keys.Escape)
            {
                var game = (MainCycle)sender;
                game.Exit();
            }

            switch (_currentScene.key)
            {
                case "MainMenu":
                    {
                        if (IsPushedInterface())
                            break;
                        var menu = (Menu)_currentScene.scene;
                        switch (e.GetPushedButtons()[0])
                        {
                            case Keys.Up:
                                {
                                    menu.ChoosePerform(this, new CursorEventArgs(Menu.CursorDirection.up));
                                    break;
                                }
                            case Keys.Down:
                                {
                                    menu.ChoosePerform(this, new CursorEventArgs(Menu.CursorDirection.down));
                                    break;
                                }
                            case Keys.Space:
                                {
                                    menu.AcceptPerform(this, null);
                                    break;
                                }
                        }

                        break;
                    }
            }
        }

        public void Update(object sender, GraphicsEventArgs e)
        {
            foreach (var scene in _scenesDict)
                scene.Value.UpdateGraphics(e.WindowWidth, e.WindowHeight);
        }

        

        public void Render(SpriteBatch spriteBatch)
        {
            _currentScene.scene.DrawScene(spriteBatch);
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
            if (sceneKey=="Exit")
            {
                MainCycle.isLoopOff = true;
                return;
            }
            if (_scenesDict.ContainsKey(sceneKey))
                _currentScene = (key: sceneKey, scene: _scenesDict[sceneKey]);
            else
                System.Windows.Forms.MessageBox.Show("Такой сцены еще нет!");
        }
      
        public bool IsPushedInterface ()
        {
            if (pushElapsedTime > pushInterfaceCoolDown)
            {
                pushElapsedTime = 0;
                return false;
            }
            else
            {
                return true;
            }
        }

        

    }
}
