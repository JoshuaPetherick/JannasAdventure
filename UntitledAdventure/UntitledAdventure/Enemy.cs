using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            this.x = x;
            this.y = y;
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
