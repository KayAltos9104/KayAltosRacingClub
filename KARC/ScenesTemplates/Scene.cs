using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;
using KARC.GameObjsTemplates;
using Microsoft.Xna.Framework.Graphics;

namespace KARC.ScenesTemplates
{
    abstract class Scene:IBehaviour
    {
        public delegate void SceneHandler(string scene);
        public event SceneHandler Change;//Событие переключения сцены

        protected Dictionary<int, GameObject> _objDict; //Список объектов
        private int _id;//Id следующего элемента

        protected int _windowWidth;//Текущая ширина сцены
        protected int _windowHeight;//Текущая высота сцены

        public Scene ()
        {
            _objDict = new Dictionary<int, GameObject>();
            _id = 1;
        }

        public virtual void InitializeScene ()//Сюда писать генерацию объектов для конкретной сцены
        {

        }
        public virtual void Update()
        {
            List<int> deadObjsKeysList = new List<int>();
            foreach (var obj in _objDict)//Апдейтим все объекты и записываем те, которые надо, в список на удаление
            {
                obj.Value.Update();
                if (obj.Value.ToDelete == true)
                    deadObjsKeysList.Add(obj.Key);
            }

            foreach (var key in deadObjsKeysList)//Удаляем "мертвые" объекты
            {
                RemoveObject(key);
            }
        }
        public virtual void UpdateGraphics(int windowWidth, int windowHeight)//Что делать, если поменялись графические настройки
        {
            _windowHeight = windowHeight;
            _windowWidth = windowWidth;
        }

        public virtual void DrawScene(SpriteBatch spriteBatch)//Метод отрисовки сцены
        {
            foreach (var obj in _objDict)
            {
                obj.Value.Draw(spriteBatch);
            }
        }

        public void SceneChangePerform(string scene)//Вызов события переключения сцены
        {
            Change.Invoke(scene);
        }
        protected virtual void AddObject (GameObject newObj)
        {
            _objDict.Add(_id, newObj);
            _id++;
        }

        protected void RemoveObject (int key)
        {
            _objDict.Remove(key);
            _id--;
        }
        
        
    }
}
