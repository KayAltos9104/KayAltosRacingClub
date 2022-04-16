using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameLogic
{
    public interface IObject
    {
        Vector2 Pos { get; set; }
        void Update();
    }
}
