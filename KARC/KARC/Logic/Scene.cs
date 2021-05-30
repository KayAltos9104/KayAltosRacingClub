﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KARC.Logic
{
    class Scene //Класс, который содержит в себе объекты и карту их расстановки
    {
        public List<Object> objectList;//Список всех объектов сцены
        int[,] map;//Клетки карты (одна клетка, по идее, один экран)
        int scale;// Масштаб одного тайла(клетки) карты

        public Song song;

        public Scene (int [,] _map, int _scale, List<Object> _objList)
        {
            map = new int[_map.GetLength(0), _map.GetLength(1)];
            for (int y = 0; y < map.GetLength(1); y++)
                for (int x = 0; x < map.GetLength(0); x++)
                    map[x, y] = _map[x, y];

            scale = _scale;
            objectList = new List<Object>();
            foreach (var obj in _objList)
                objectList.Add(obj);
        }

        public virtual void updateScene(int _time)
        {
            foreach (var obj in objectList)
                obj.Update(_time);

            //if (song !=null)
            //{
            //    MediaPlayer.Play(song);
            //    // повторять после завершения
            //    MediaPlayer.IsRepeating = true;
            //}
           
           
           
        }

        public virtual void updateScene (Keys key, int _time)
        {
            foreach (var obj in objectList)
                obj.Update(key, _time);
            //if (song != null)
            //{
            //    MediaPlayer.Play(song);
            //    // повторять после завершения
            //    MediaPlayer.IsRepeating = true;
            //};
        }



    }
}
