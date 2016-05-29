using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace UntitledAdventure
{
    class Projectile
    {
        Texture2D texture;
        Player.PlayerStates state;
        private int x;
        private int y;
        private int startingX;
        private int startingY;

        public int damage { get; }
        private int distance;
        private int speed;
        DateTime timer; 

        public Projectile(Texture2D texture, int damage, int distance, int speed, int x, int y, Player.PlayerStates state)
        {
            this.texture = texture;
            this.damage = damage;
            this.distance = distance;
            this.speed = speed;
            this.x = x;
            this.y = y;
            this.state = state;
            startingX = x;
            startingY = y;
            timer = DateTime.Now;
        }

        public bool update()
        {
            if (timer < DateTime.Now)
            {
                // Change based on player direction. Future improvement: Based on mouse position
                switch(state)
                {
                    case Player.PlayerStates.North:
                        y = y - speed;
                        if (y <= (startingY - distance))
                        {
                            return true;
                        }
                        timer = DateTime.Now.AddMilliseconds(50);
                        break;

                    case Player.PlayerStates.NorthEast:
                        y = y - speed;
                        x = x + speed;
                        if ((y <= (startingY - distance)) && (x >= (startingX + distance)))
                        {
                            return true;
                        }
                        timer = DateTime.Now.AddMilliseconds(50);
                        break;
                    
                    case Player.PlayerStates.East:
                        x = x + speed;
                        if (x >= (startingX + distance))
                        {
                            return true;
                        }
                        timer = DateTime.Now.AddMilliseconds(50);
                        break;

                    case Player.PlayerStates.SouthEast:
                        y = y + speed;
                        x = x + speed;
                        if ((x >= (startingX + distance)) && (y >= (startingY + distance)))
                        {
                            return true;
                        }
                        timer = DateTime.Now.AddMilliseconds(50);
                        break;

                    case Player.PlayerStates.South:
                        y = y + speed;
                        if (y >= (startingY + distance))
                        {
                            return true;
                        }
                        timer = DateTime.Now.AddMilliseconds(50);
                        break;

                    case Player.PlayerStates.SouthWest:
                        y = y + speed;
                        x = x - speed;
                        if ((y >= (startingY + distance)) && (x <= (startingX - distance)))
                        {
                            return true;
                        }
                        timer = DateTime.Now.AddMilliseconds(50);
                        break;

                    case Player.PlayerStates.West:
                        x = x - speed;
                        if (x <= (startingX - distance))
                        {
                            return true;
                        }
                        timer = DateTime.Now.AddMilliseconds(50);
                        break;

                    case Player.PlayerStates.NorthWest:
                        y = y - speed;
                        x = x - speed;
                        if ((y <= (startingY - distance)) && (x <= (startingX - distance)))
                        {
                            return true;
                        }
                        timer = DateTime.Now.AddMilliseconds(50);
                        break;
                }
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
            // Implement rotation based on direction - so it faces the correct direction, easier then creating different images!
            spriteBatch.Draw(texture, new Rectangle(x, y, texture.Width, texture.Height), Color.GhostWhite);
        }

    }
}
