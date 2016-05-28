using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UntitledAdventure
{
    class Enemy
    {
        Texture2D texture;

        public Enemy(Texture2D text, int x, int y)
        {
            // Store enemy image and databse variable
            texture = text;

            // Moves x & y from top-left corner to center
            this.x = (x - (text.Width / 2));
            this.y = (y - (text.Height / 2));
            this.height = texture.Height;
            this.width = texture.Width;
        }

        public int x { get; set; }
        public int y { get; set; }
        public int height { get; set; }
        public int width { get; set; }

        public bool collision(int x, int y, int height, int width)
        {
            // Don't ask... just don't ask...
            //  P.s. Axis-Aligned Bounding Box Collision (2D)
            if ((x <= (this.x + texture.Width)) &&
                ((x + width) >= this.x) && 
                (y <= (this.y + texture.Height)) &&
                ((y + height) >= this.y))
            {
                return true;
            }
            return false;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            // Implement boolean if background coming off window space
            spriteBatch.Draw(texture, new Rectangle(x, y, texture.Width, texture.Height), Color.GhostWhite);
        }
    }
}
