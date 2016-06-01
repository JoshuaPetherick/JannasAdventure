using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UntitledAdventure
{
    class Player
    {
        DateTime castTimer;
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
            this.height = texture.Height;
            this.width = texture.Width;

            screenHeight = height;
            screenWidth = width;
            castTimer = DateTime.Now;
        }

        public int x { get; set; }
        public int y { get; set; }
        public int height { get; set; }
        public int width { get; set; }

        public void castAbility1(Texture2D texture)
        {
            if ( castTimer < DateTime.Now)
            {
                // Make the values below variables that get passed based on select skils? Maybe a skill tree class?
                projectiles.Add(new Projectile(texture, 20, 100, 2, this.x, this.y, state));
                castTimer = DateTime.Now.AddSeconds(2);
            }
        }

        public void castAbility2(Texture2D texture)
        {
            if (castTimer < DateTime.Now)
            {
                // Make the values below variables that get passed based on select skils? Maybe a skill tree class?
                projectiles.Add(new Projectile(texture, 5, 300, 7, this.x, this.y, state));
                castTimer = DateTime.Now.AddSeconds(0.75);
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
                    i--;
                }
                else
                {
                    for(int j = 0; j < enemies.Count; j++)
                    {
                        // If projectile collides with enemy unit (really wanna avoid this as lots of enemies and projectiles will make this really long)
                        if (projectiles[i].collision(enemies[j].x, enemies[j].y, enemies[j].height, enemies[j].width) == true)
                        {
                            enemies[j].health -= projectiles[i].damage;
                            if (enemies[j].health <= 0)
                            {
                                enemies.RemoveAt(j);
                            }
                            projectiles.RemoveAt(i);
                            i--;
                            j = enemies.Count; // Escape loop as projectile no longer exists!
                        }
                    }
                }
            }
        }

        public void talkToNpc(Npc npc)
        {

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
