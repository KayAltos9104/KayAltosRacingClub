using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using KARC.Controllers;

namespace KARC
{
    public class MainCycle : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static bool isOff;

        //Events
        delegate void ButtonHandler(object sender, KeyBoardEventArgs e);
        event ButtonHandler Pushed;

        public MainCycle()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            isOff = false;
        }

        protected override void Initialize()
        {
            

            base.Initialize();

            Pushed += SceneController.Update;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (isOff)
                Exit();

            var pressedKeys = Keyboard.GetState().GetPressedKeys();

            if (pressedKeys.Length > 0)
            {
                Pushed.Invoke(this, new KeyBoardEventArgs(pressedKeys));
            }
            
            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        
    }
    class KeyBoardEventArgs
    {
        private Keys[] _pushedButtons;

        public KeyBoardEventArgs (Keys [] pushedButtons)
        {
            if (pushedButtons != null)
            {               
                _pushedButtons = (Keys[])pushedButtons.Clone();
            }
                
            else
                _pushedButtons = null;
        }

        public Keys[] GetPushedButtons()
        {
            if (_pushedButtons != null)
                return (Keys[])_pushedButtons.Clone();
            else
                return null;
        }
    }
}
