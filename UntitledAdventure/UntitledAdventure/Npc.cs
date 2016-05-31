using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UntitledAdventure
{
    class Npc
    {
        Texture2D texture;
        SpriteFont font;

        public Npc(Texture2D texture, SpriteFont font, int x, int y, string name)
        {
            // Store enemy image and databse variable
            this.texture = texture;

            // Moves x & y from top-left corner to center
            this.x = (x - (texture.Width / 2));
            this.y = (y - (texture.Height / 2));
            this.height = texture.Height;
            this.width = texture.Width;
            this.font = font;
            this.health = 100; // Should be passed through into constructor (Not sure where from)
            this.name = name;
        }

        public int x { get; set; }
        public int y { get; set; }
        public string name { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public int health { get; set; }

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
            spriteBatch.DrawString(font, name, new Vector2(x, (y - (texture.Height / 2))), Color.Purple);
        }
    }
}
