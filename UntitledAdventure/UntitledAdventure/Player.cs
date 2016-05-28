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
        public PlayerStates state { get; set; }
        public enum PlayerStates
        {
            North,
            NorthEast,
            East,
            SouthEast,
            South,
            SouthWest,
            West,
            NorthWest
        };

        public Player(Texture2D texture, int height, int width)
        {
            // Store player image and databse variable
            this.texture = texture;

            // Moves x & y from top-left corner to center
            this.x = ((height / 2) - (texture.Width / 2));
            this.y = ((width / 2) - (texture.Height / 2));

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
                projectiles.Add(new Projectile(texture, 10, 200, this.x, this.y, state));
                timer = DateTime.Now.AddSeconds(2);
            }
        }

        // List passed across also updates list passed from (Pointer(y))
        public void update(List<Enemy> enemies)
        {
            for(int i = 0; i < projectiles.Count; i++)
            {
                // If projectile reached max distance
                if (projectiles[i].update() == true)
                {
                    projectiles.RemoveAt(i);
                }
                else
                {
                    for(int j = 0; j < enemies.Count; j++)
                    {
                        // If projectile collided with enemy unit
                        if (projectiles[i].collision(enemies[j].x, enemies[j].y, enemies[j].height, enemies[j].width) == true)
                        {
                            enemies.RemoveAt(j);
                            projectiles.RemoveAt(i);
                        }
                    }
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
