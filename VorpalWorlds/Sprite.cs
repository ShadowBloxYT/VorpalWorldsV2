using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorpalWorlds
{
    public class Sprite
    {
        protected Texture2D Image;
        public Vector2 Pos;
        public Color Tint;
        float scale;
        SpriteEffects spriteEffects;

        public Rectangle Hitbox
        {
            get
            {
                return new Rectangle((int)Pos.X, (int)Pos.Y, (int) (Image.Width * scale), (int)(Image.Height * scale));
            }
        }

        public Sprite(Texture2D image, Vector2 pos)
        {
            scale = 0.25f;
            Image = image;
            Pos = pos;
            Tint = Color.White;
            spriteEffects = SpriteEffects.None;
        }

        protected void SetSpriteEffects(SpriteEffects spriteEffects)
        {
            this.spriteEffects = spriteEffects;
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, Pos, null, Tint, 0, Vector2.Zero, scale, spriteEffects, 0);
        }
    }
}