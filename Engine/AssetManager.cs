using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Engine
{
    public class AssetManager
    {
        private readonly Dictionary<string, Texture2D> textures = new();

        private readonly List<string> textureNames = new()
        {
        };

        public void Load(ContentManager content)
        {
            foreach (string name in textureNames)
            {
                textures[name] = content.Load<Texture2D>(name);
            }
        }

        public Texture2D GetTexture(string name)
        {
            return textures[name];
        }
    }
}