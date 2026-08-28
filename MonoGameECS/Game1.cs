using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Engine;

namespace ECSGame
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;

        private SceneManager scenes;
        private AssetManager assets;
        private InputManager inputs;
        private Renderer renderer;

        private const int screenWidth = 1960;
        private const int screenHeight = 1080;

        private const int virtualWidth = 1960;
        private const int virtualHeight = 1080;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            _graphics.PreferredBackBufferWidth = screenWidth;
            _graphics.PreferredBackBufferHeight = screenHeight;
            _graphics.ApplyChanges();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            scenes = new();
            assets = new();
            inputs = new();
            renderer = new Renderer(_graphics, virtualWidth, virtualHeight);
            Prefabs.assets = assets;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            assets.Load(Content);

            scenes.SetScene("Start");
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            inputs.Update();

            scenes.Update(dt);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            renderer.Begin();
            scenes.Draw(dt);
            renderer.End();

            base.Draw(gameTime);
        }
    }
}
