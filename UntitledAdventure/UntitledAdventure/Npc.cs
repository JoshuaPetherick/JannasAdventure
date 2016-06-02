using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections;

namespace UntitledAdventure
{
    class Npc
    {
        Texture2D texture;
        SpriteFont font;
        DateTime textTimer;
        Conversation convo;

        private int npcID;
        private int textIND = 0;
        private string text;

        public bool talking { get; set; }

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
            this.talking = false;
        }

        public int x { get; set; }
        public int y { get; set; }
        public string name { get; set; }
        public int height { get; set; }
        public int width { get; set; }
        public int health { get; set; }
        

        public void startConvo()
        {
            textIND = 1;
            textTimer = DateTime.Now.AddSeconds(3);
        }

        public void continueConvo()
        {
            if (textTimer <= DateTime.Now)
            {
                textIND++;
                textTimer = DateTime.Now.AddSeconds(3);
            }
        }

        public void endConvo()
        {
            textIND = 0;
        }

        public bool collision(int x, int y, int height, int width)
        {
            // Add distance to Width & Height - decide distance
            // Then check if Button (Say E) is pressed then do something
            if ((x <= ((this.x - texture.Width) + (texture.Width * 3))) &&
                ((x + width) >= (this.x - texture.Width)) &&
                (y <= ((this.y - texture.Height) + (texture.Height * 3))) &&
                ((y + height) >= (this.y - texture.Height)))
            {
                return true;
            }
            return false;
        }

        public void draw(SpriteBatch spriteBatch)
        {
            // Implement boolean if background coming off window space
            spriteBatch.Draw(texture, new Rectangle(x, y, texture.Width, texture.Height), Color.GhostWhite);
            // Text Box - drawn after Player
            if (textIND != 0)
            {
                spriteBatch.DrawString(font, convo.getText(npcID, textIND), new Vector2(x, (y - (texture.Height / 2))), Color.Purple);
            }
            else
            {
                spriteBatch.DrawString(font, name, new Vector2(x, (y - (texture.Height / 2))), Color.Purple);
            }
        }
    }
}
