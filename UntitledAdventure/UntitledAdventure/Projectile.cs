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
        private float rotation;
        DateTime timer; 

        public Projectile(Texture2D texture, int damage, int distance, int speed, int x, int y, Player.PlayerStates state)
        {
            this.texture = texture;
            this.damage = damage;
            this.distance = distance;
            this.speed = speed;
            this.x = (x + (texture.Width / 2));
            this.y = (y + texture.Height);
            this.state = state;
            startingX = x;
            startingY = y;
            timer = DateTime.Now;

            double pi = Math.PI;
            switch (state)
            {
                case Player.PlayerStates.North:
                    rotation = 0.0f;
                    break;

                case Player.PlayerStates.NorthEast:
                    rotation = (float)(pi * 2.25);
                    break;

                case Player.PlayerStates.East:
                    rotation = (float)(pi * 2.5);
                    break;

                case Player.PlayerStates.SouthEast:
                    rotation = (float)(pi * 2.75);
                    break;

                case Player.PlayerStates.South:
                    rotation = (float)pi;
                    break;

                case Player.PlayerStates.SouthWest:
                    rotation = (float)(pi * 1.25);
                    break;

                case Player.PlayerStates.West:
                    rotation = (float)(pi * 1.5);
                    break;

                case Player.PlayerStates.NorthWest:
                    rotation = (float)(pi * 1.75);
                    break;
            }
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

        // Needs fixing for rotated shapes :S May have to remove diagonal pieces...
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
            spriteBatch.Draw(texture, new Rectangle(x, y, texture.Width, texture.Height), null, Color.GhostWhite, rotation, new Vector2((texture.Width / 2), (texture.Height / 2)), SpriteEffects.None, 0);
        }

    }
}
