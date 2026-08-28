using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    public class Renderer
    {
        private readonly GraphicsDeviceManager graphicsManager;
        private GraphicsDevice graphicsDevice;

        private SpriteBatch spriteBatch;
        private RenderTarget2D virtualScreen;

        public int VirtualWidth;
        public int VirtualHeight;

        public Renderer(GraphicsDeviceManager graphicsManager, int width, int height)
        {
            this.graphicsManager = graphicsManager;
            VirtualWidth = width;
            VirtualHeight = height;

            graphicsDevice = graphicsManager.GraphicsDevice;
            spriteBatch = new SpriteBatch(graphicsDevice);
            virtualScreen = new RenderTarget2D(
                graphicsDevice,
                VirtualWidth,
                VirtualHeight,
                false,
                graphicsDevice.PresentationParameters.BackBufferFormat,
                DepthFormat.None,
                0,
                RenderTargetUsage.DiscardContents);
        }
        public void Begin()
        {
            graphicsDevice.SetRenderTarget(virtualScreen);
            graphicsDevice.Clear(new Color(0, 0, 0));
            spriteBatch.Begin();

        }
        public void End()
        {
            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);

            spriteBatch.Begin();
            spriteBatch.Draw(virtualScreen, new Vector2(0, 0), Color.White);
            spriteBatch.End();
        }
        public void DrawSpritePerfect(
            Texture2D sprite, 
            Rectangle? source, 
            Vector2 centre)
        {
            spriteBatch.Draw(
                sprite,
                centre - sprite.Bounds.Size.ToVector2(),
                source,
                new Color(255, 255, 255));
        }

        public void DrawSprite(
            Texture2D sprite, 
            Rectangle? source, 
            Vector2 centre, 
            float rotation, 
            Vector2 scale, 
            float depth)
        {
            spriteBatch.Draw(
                sprite, 
                centre, 
                source, 
                new Color(255, 255, 255), 
                rotation, 
                new Vector2(0, 0), 
                scale, 
                SpriteEffects.None, 
                depth);
        }
    }
}
