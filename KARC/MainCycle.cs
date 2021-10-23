using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using KARC.Controllers;
using KARC.Prefabs;
using KARC.Prefabs.Scenes;

namespace KARC
{
    public class MainCycle : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;


        private SceneController sceneController;

        //Текущее разрешение экрана
        public static int windowWidth;
        public static int windowHeight;

        public static bool isLoopOff;//Флаг на продолжение игрового цикла
        public static int TimeElapsedCycle { get; private set;}//Сколько времени прошло между текущим и предыдущим шагом цикла

        //Events
        delegate void ButtonHandler(object sender, KeyBoardEventArgs e);
        event ButtonHandler Pushed;
        delegate void GraphicsHandler(object sender,GraphicsEventArgs e);
        event GraphicsHandler GraphicsChanged;

        public MainCycle()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content/Resources";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        { 
            base.Initialize();

            isLoopOff = false;

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

            //Контроллер отслеживает нажатие клавиши и изменение графических настроек
            Pushed += sceneController.Update;
            GraphicsChanged += sceneController.Update;

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            ResourcesStorage.AddImage("MenuMainBackGround", Content.Load<Texture2D>("BackGrounds/MenuBackGround"));

            ResourcesStorage.AddImage("SelectButton", Content.Load<Texture2D>("ButtonsImages/btnSelect"));
            ResourcesStorage.AddImage("SelectButton_Light", Content.Load<Texture2D>("ButtonsImages/btnSelect_light"));

            ResourcesStorage.AddImage("StartButton", Content.Load<Texture2D>("ButtonsImages/btnStart"));
            ResourcesStorage.AddImage("StartButton_Light", Content.Load<Texture2D>("ButtonsImages/btnStart_light"));

            ResourcesStorage.AddImage("OptionsButton", Content.Load<Texture2D>("ButtonsImages/btnOptions"));
            ResourcesStorage.AddImage("OptionsButton_Light", Content.Load<Texture2D>("ButtonsImages/btnOptions_light"));

            ResourcesStorage.AddImage("ExitButton", Content.Load<Texture2D>("ButtonsImages/btnExit"));
            ResourcesStorage.AddImage("ExitButton_Light", Content.Load<Texture2D>("ButtonsImages/btnExit_light"));

        }

        protected override void Update(GameTime gameTime)
        {
            if (isLoopOff)
                Exit();

            TimeElapsedCycle = gameTime.ElapsedGameTime.Milliseconds;

            var pressedKeys = Keyboard.GetState().GetPressedKeys();
            if (pressedKeys.Length > 0)
            {
                Pushed.Invoke(this, new KeyBoardEventArgs(pressedKeys, TimeElapsedCycle));
            }

            //Отладка
            //if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            //{
            //    _graphics.PreferredBackBufferWidth = 1024;
            //    _graphics.PreferredBackBufferHeight = 768;
            //    _graphics.ApplyChanges();
            //    windowWidth = Window.ClientBounds.Width;
            //    windowHeight = Window.ClientBounds.Height;
            //    GraphicsChanged.Invoke(this, new GraphicsEventArgs(windowWidth, windowHeight, false));
            //}

            sceneController.Update();//Апдейт, если ничего не произошло

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
    class GraphicsEventArgs
    {
        public int WindowHeight { get; }
        public int WindowWidth { get; }
        public bool IsFullScreen { get; }

        public GraphicsEventArgs(int width, int height, bool fScreen)
        {
            WindowHeight = height;
            WindowWidth = width;
            IsFullScreen = fScreen;
        }
    }

}
