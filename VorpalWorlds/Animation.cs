using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorpalWorlds
{
    class Animation
    {
        public Texture2D Texture { get; set; }
        public Vector2 Location { get; set; }
        public int Columns { get; set; }
        private int currentFrame;
        private int totalFrames;

        int frameWidth;
        int frameHeight;

        int elapsedTime;
        int delay;

        public Animation(Texture2D tex, Vector2 loc, int col, int updateRate)
        { 
            Texture = tex;
            Location = loc;
            Columns = col;
            totalFrames = col;
            currentFrame = 0;
            delay = updateRate;
            frameWidth = Texture.Width / Columns;
            frameHeight = Texture.Height; 
        }

        public void Update(GameTime gameTime)
        {
            elapsedTime += (int) gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsedTime > delay)
            {
                currentFrame++;
                elapsedTime = 0;
            }

            if (currentFrame == totalFrames)
                currentFrame = 0;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            int currentColumn = currentFrame;

            Rectangle sourceRectangle = new Rectangle(frameWidth * currentColumn, 0, frameWidth, frameHeight);
            Rectangle destinationRectangle = new Rectangle((int)Location.X, (int)Location.Y, frameWidth, frameHeight);

            //spriteBatch.Draw(Texture,Location, Color.White);
            spriteBatch.Draw(Texture, destinationRectangle, sourceRectangle, Color.White);
        }
    }
}
