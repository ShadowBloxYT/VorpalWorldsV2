using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorpalWorlds
{
    public class Stamina : Sprite
    {
        Texture2D Image;
        Vector2 Pos;

        int staminaAmount = 7;

        public Stamina(Texture2D image, Vector2 pos, Texture2D FullBar)
            : base(image, pos)
        {
            Image = image;
            Pos = pos;
        }

        public void Update()
        {
            //Will add stuff later
        }

        public void add(int amount)
        {
            if (staminaAmount < 7)
            {
                staminaAmount += amount;
            }

            if (staminaAmount > 7)
            {
                staminaAmount = 7;
            }
        }

        public void remove(int amount)
        {
            if (staminaAmount > 0)
            {
                staminaAmount -= amount;
            }

            if (staminaAmount < 0)
            {
                staminaAmount = 0;
            }
        }

        public int checkStamina()
        {
            return staminaAmount;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Image, Pos, null, Color.White, 0f, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, 0f);
        }
    }
}