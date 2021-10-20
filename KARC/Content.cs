using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace KARC
{
    static class Content
    {
        static Dictionary<string, Texture2D> _imagesDict = new Dictionary<string, Texture2D>();

        public static void AddImage (string key, Texture2D image)
        {
            _imagesDict.Add(key, image);
        }

        public static void RemoveImage(string key)
        {
            _imagesDict.Remove(key);
        }
    }
}
