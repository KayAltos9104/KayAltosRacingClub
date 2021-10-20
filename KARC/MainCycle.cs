using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using KARC.Controllers;
using KARC.Prefabs;

namespace KARC
{
    public class MainCycle : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private SceneController sceneController;

        public static int windowWidth;
        public static int windowHeight;

        public static int TimeElapsedCycle { get; private set;}


        //Events
        delegate void ButtonHandler(object sender, KeyBoardEventArgs e);
        event ButtonHandler Pushed;



        public MainCycle()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content/Resources";
            IsMouseVisible = true;
            

            
        }

        protected override void Initialize()
        { 
            base.Initialize();

            _graphics.IsFullScreen = false;

            _graphics.PreferredBackBufferWidth = 1600;
            _graphics.PreferredBackBufferHeight = 900;

            _graphics.ApplyChanges();

            windowWidth = Window.ClientBounds.Width;
            windowHeight = Window.ClientBounds.Height;

            sceneController = new SceneController();
            //Здесь надо нагенерировать сцен и добавить в контроллер
            MenuMain mainMenu = new MenuMain();           
            sceneController.AddScene("MainMenu", mainMenu);
            sceneController.Initialize();
            Pushed += sceneController.Update;
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ResourcesStorage.AddImage("SelectButton", Content.Load<Texture2D>("ButtonsImages/btnSelect"));
            ResourcesStorage.AddImage("SelectButton_Light", Content.Load<Texture2D>("ButtonsImages/btnSelect_light"));

        }

        protected override void Update(GameTime gameTime)
        {
            
            TimeElapsedCycle = gameTime.ElapsedGameTime.Milliseconds;

            var pressedKeys = Keyboard.GetState().GetPressedKeys();
            if (pressedKeys.Length > 0)
            {
                Pushed.Invoke(this, new KeyBoardEventArgs(pressedKeys, TimeElapsedCycle));
            }

            sceneController.RunCycle();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);            
            _spriteBatch.Begin(SpriteSortMode.BackToFront);

            sceneController.Render(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }

        
    }
    class KeyBoardEventArgs
    {
        private Keys[] _pushedButtons;
        public int ElapsedTime { get; }
        public KeyBoardEventArgs (Keys [] pushedButtons, int time)
        {
            if (pushedButtons != null)
            {               
                _pushedButtons = (Keys[])pushedButtons.Clone();
                ElapsedTime = time;
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
