﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace UntitledAdventure
{
    class Player
    {
        Texture2D texture;
        private int x;
        private int y;

        public Player(Texture2D text, int x, int y)
        {
            // Store player image and databse variable
            texture = text;
            this.x = x;
            this.y = y;
        }

        public void setX(int x)
        {
            this.x = x;
        }

        public void setY(int y)
        {
            this.y = y;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            // Implement boolean if background coming off window space
            spriteBatch.Draw(texture, new Rectangle(400, 300, texture.Width, texture.Height), Color.GhostWhite);
        }
    }
}