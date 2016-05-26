using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace UntitledAdventure
{
    class Projectile
    {
        Texture2D texture;
        private int x;
        private int y;
        private int startingX;
        private int startingY;

        public int damage;
        private int distance;
        DateTime timer; 

        public Projectile(Texture2D texture, int damage, int distance, int x, int y)
        {
            this.texture = texture;
            this.damage = damage;
            this.distance = distance;
            this.x = x;
            this.y = y;

            startingX = x;
            startingY = y;
            timer = DateTime.Now;
        }

        public bool update()
        {
            if (timer < DateTime.Now)
            {
                // Change based on player direction, however just go north for now
                y = y + 3;
                if (y >= (startingY + distance))
                {
                    return true;
                }
                timer = DateTime.Now.AddMilliseconds(50);
            }
            return false;
        }

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
