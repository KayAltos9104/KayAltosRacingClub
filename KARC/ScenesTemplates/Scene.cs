﻿using Microsoft.Xna.Framework.Input;
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
        public event SceneHandler Change;

        protected Dictionary<int, GameObject> _objDict;
        private int _id;

        protected int _windowWidth;
        protected int _windowHeight;

        public Scene ()
        {
            _objDict = new Dictionary<int, GameObject>();
            _id = 1;
        }

        public virtual void InitializeScene ()
        {

        }

        public virtual void UpdateGraphics(int windowWidth, int windowHeight)
        {
            _windowHeight = windowHeight;
            _windowWidth = windowWidth;
        }

        public virtual void AddObject (GameObject newObj)
        {
            _objDict.Add(_id, newObj);
            _id++;
        }

        public void RemoveObject (int key)
        {
            _objDict.Remove(key);
            _id--;
        }
        public virtual void Update()
        {
            List<int> deadObjsKeysList = new List<int>();
            foreach (var obj in _objDict)
            {
                obj.Value.Update();
                if (obj.Value.ToDelete == true)
                    deadObjsKeysList.Add(obj.Key);
            }

            foreach (var key in deadObjsKeysList)
            {
                RemoveObject(key);
            }
        }
        public virtual void DrawScene (SpriteBatch spriteBatch)
        {
            foreach (var obj in _objDict)
            {
                obj.Value.Draw(spriteBatch);                
            }
        }

        public void SceneChangePerform(string scene)
        {
            Change.Invoke(scene);
        }
    }
}
