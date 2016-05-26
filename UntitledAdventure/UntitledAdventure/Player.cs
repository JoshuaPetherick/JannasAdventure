using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UntitledAdventure
{
    class Player
    {
        DateTime timer;
        Texture2D texture;
        List<Projectile> projectiles = new List<Projectile>();

        private int screenHeight;
        private int screenWidth;

        public Player(Texture2D texture, int x, int y, int height, int width)
        {
            // Store player image and databse variable
            this.texture = texture;

            // Moves x & y from top-left corner to center
            this.x = (x - (texture.Width / 2));
            this.y = (y - (texture.Height / 2));

            screenHeight = height;
            screenWidth = width;
            timer = DateTime.Now;
        }

        public int x { get; set; }
        public int y { get; set; }

        public void castAbility(Texture2D texture)
        {
            if ( timer < DateTime.Now)
            {
                // Use switch here, possible pass in which key was pressed?
                projectiles.Add(new Projectile(texture, 10, 200, this.x, this.y));
                timer = DateTime.Now.AddSeconds(2);
            }
        }

        public void update()
        {
            for(int i = 0; i < projectiles.Count; i++)
            {
                if (projectiles[i].update() == true)
                {
                    projectiles.RemoveAt(i);
                }
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            // Implement boolean if background coming off window space
            spriteBatch.Draw(texture, new Rectangle(x, y, texture.Width, texture.Height), Color.GhostWhite);
            for (int i = 0; i < projectiles.Count; i++)
            {
                projectiles[i].draw(spriteBatch);
            }
        }
    }
}
