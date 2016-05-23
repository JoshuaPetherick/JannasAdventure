using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UntitledAdventure
{
    class Player
    {
        Texture2D texture;

        public Player(Texture2D text, int x, int y)
        {
            // Store player image and databse variable
            texture = text;
            this.x = (x - (text.Width / 2));
            this.y = (y - (text.Height / 2));
        }

        public int x { get; set; }
        public int y { get; set; }

        public void draw(SpriteBatch spriteBatch)
        {
            // Implement boolean if background coming off window space
            spriteBatch.Draw(texture, new Rectangle(x, y, texture.Width, texture.Height), Color.GhostWhite);
        }
    }
}
