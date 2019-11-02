using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VorpalWorlds
{
    public class Ninja : Sprite
    {
        int SpeedX = 5;

        Texture2D staminaImage;

        Texture2D crouchImage;

        List<Texture2D> running;

        List<Texture2D> idle;

        List<Texture2D> ninjaStamina;

        int staminaAmount = 7;

        bool isCrouch = false;

        int runImage = 0;

        int idleNum = 0;

        int walkDistance = 0;

        bool isFalling = false;

        Texture2D idleImage;

        Texture2D jumpImage;

        enum NinjaState
        {
            Idle,
            Jumping,
            Running,
            Crouching,
            Walking,
            MovingJumpingLeft,
            MovingJumpingRight,
        };


        //Timers




        NinjaState ninjaState = NinjaState.Idle;
        TimeSpan timerMain;

        /// <summary>
        /// TIMER MAIN
        /// </summary>

        TimeSpan timerIdleStamina;

        /// <summary>
        /// TIMER IDLE STAMINA
        /// </summary>

        TimeSpan timerWalkStamina;

        /// <summary>
        /// TIMER WALK STAMINA
        /// </summary>

        TimeSpan timerRunStamina;

        /// <summary>
        /// TIMER RUN STAMINA
        /// </summary>

        private Vector2 initialPosition;
        private float UpwardsVelocity = 0;
        private const float gravity = 0.35f;

        public Ninja(Texture2D image, Vector2 position, Texture2D crouch, Texture2D jump, List<Texture2D> runningImages, List<Texture2D> idleImages, Texture2D staminaIm4ge)
            : base(image, position)
        {
            staminaImage = staminaIm4ge;
            crouchImage = crouch;
            idleImage = image;
            jumpImage = jump;
            running = runningImages;
            idle = idleImages;
            initialPosition = position;
        }

        public void Update(KeyboardState keyboardState, KeyboardState lastKeyBoardState, Rectangle screen, GameTime gameTime)
        {
            timerMain += gameTime.ElapsedGameTime;
            timerWalkStamina += gameTime.ElapsedGameTime;
            timerIdleStamina += gameTime.ElapsedGameTime;
            timerRunStamina += gameTime.ElapsedGameTime;

            //if (staminaAmount > 7)
            //{
            //    staminaAmount = 6;
            //}

            //else if (staminaAmount < 0)
            //{
            //    staminaAmount = 0;
            //}

            if (ninjaState == NinjaState.Idle)
            {
                if (idleNum > 9)
                {
                    idleNum = 0;
                }
                if (timerMain.TotalMilliseconds > 45)
                {
                    timerMain = TimeSpan.Zero;
                    idleNum++;

                }


                if (timerIdleStamina.TotalMilliseconds > 2000)
                {
                    timerIdleStamina = TimeSpan.Zero;
                    if (staminaAmount < 7)
                    {
                        staminaAmount++;
                    }
                }
            }



            if (Pos.Y > 380)
            {
                isFalling = false;
                Pos.Y = 380;
                ninjaState = NinjaState.Idle;
            }



            if (isFalling)
            {
                if (Pos.Y - UpwardsVelocity > initialPosition.Y)
                {
                    if (staminaAmount > 0)
                    {
                        staminaAmount--;
                    }

                    UpwardsVelocity = 0;
                    isFalling = false;
                }

                UpwardsVelocity -= gravity;
                Pos.Y -= UpwardsVelocity;
            }

            if (ninjaState == NinjaState.Jumping || ninjaState == NinjaState.MovingJumpingLeft || ninjaState == NinjaState.MovingJumpingRight)
            {
                isFalling = true;

                //if (staminaAmount > 0)
                //{
                //    staminaAmount--;
                //}

                /*
                if (Pos.Y - UpwardsVelocity > initialPosition.Y)
                {
                    UpwardsVelocity = 0;
                    ninjaState = NinjaState.Idle;
                }

                UpwardsVelocity -= gravity;
                Pos.Y -= UpwardsVelocity;
                */
            }
            else if (keyboardState.IsKeyDown(Keys.LeftControl) && isCrouch == false)
            {
                if (staminaAmount > 4)
                {
                    SpeedX = 8;
                    ninjaState = NinjaState.Running;
                    if (timerRunStamina.TotalMilliseconds > 500)
                    {
                        timerRunStamina = TimeSpan.Zero;
                        if (staminaAmount > 0)
                        {
                            staminaAmount--;
                        }
                    }
                }

                else if (staminaAmount <= 4)
                {
                    ninjaState = NinjaState.Walking;
                }

            }

            else if (keyboardState.IsKeyDown(Keys.LeftShift) && ninjaState != NinjaState.Running)
            {
                SpeedX = 2;
                ninjaState = NinjaState.Crouching;
                isCrouch = true;
            }
            else if (keyboardState.IsKeyUp(Keys.LeftShift) || keyboardState.IsKeyUp(Keys.LeftControl))
            {
                SpeedX = 4;
                ninjaState = NinjaState.Idle;
                isCrouch = false;
            }

            if (keyboardState.IsKeyDown(Keys.Space) && ninjaState != NinjaState.Jumping && isCrouch == false && !isFalling)
            {
                if (staminaAmount > 3)
                {
                    ninjaState = NinjaState.Jumping;
                    UpwardsVelocity = 10;
                }
            }

            /*if (keyboardState.IsKeyDown(Keys.A) && keyboardState.IsKeyDown(Keys.Space) && ninjaState == NinjaState.Idle)
            {
                ninjaState = NinjaState.MovingJumpingLeft;
                //UpwardsVelocity = 10;
            }

            else if (keyboardState.IsKeyDown(Keys.D) && keyboardState.IsKeyDown(Keys.Space) && ninjaState == NinjaState.Idle)
            {
                ninjaState = NinjaState.MovingJumpingRight;
                //UpwardsVelocity = 0;
            }
            */

            //if (Pos.Y < 380 && (ninjaState != NinjaState.Jumping)) //ninjatate != NinjaState.MovingJumpingLeft && ninjaState != NinjaState.MovingJumpingRight))
            //{
            //    initialPosition.Y = 380;
            //}

            else if (keyboardState.IsKeyDown(Keys.A))
            {
                if (ninjaState == NinjaState.Running)
                {
                    if (staminaAmount > 4)
                    {
                        ninjaState = NinjaState.Running;
                    }
                    else if (staminaAmount <= 4)
                    {
                        ninjaState = NinjaState.Walking;
                    }
                    if (runImage == 6)
                    {
                        runImage = 0;
                    }
                    if (timerMain.TotalMilliseconds > 20)
                    {
                        timerMain = TimeSpan.Zero;
                        runImage++;
                    }
                    //if (timerRunStamina.TotalMilliseconds > 100)
                    //{
                    //    timerRunStamina = TimeSpan.Zero;
                    //    staminaAmount--;
                    //}
                }

                else
                {
                    ninjaState = NinjaState.Walking;

                    if (timerWalkStamina.TotalMilliseconds > 30)
                    {
                        timerWalkStamina = TimeSpan.Zero;
                        walkDistance++;
                    }

                    //if (walkDistance == 30)
                    //{
                    //    walkDistance = 0;
                    //    if (staminaAmount < 7)
                    //    {
                    //        staminaAmount++;
                    //    }
                    //}

                    if (runImage == 6)
                    {
                        runImage = 0;
                    }

                    if (timerMain.TotalMilliseconds > 50)
                    {
                        timerMain = TimeSpan.Zero;
                        runImage++;
                    }
                }
                Pos.X -= SpeedX;
                SetSpriteEffects(SpriteEffects.FlipHorizontally);
            }

            else if (keyboardState.IsKeyDown(Keys.D))
            {
                if (ninjaState == NinjaState.Running)
                {
                    if (staminaAmount > 4)
                    {
                        ninjaState = NinjaState.Running;
                    }
                    else if (staminaAmount <= 4)
                    {
                        ninjaState = NinjaState.Walking;
                    }



                    if (timerMain.TotalMilliseconds > 20)
                    {
                        timerMain = TimeSpan.Zero;
                        runImage++;
                    }




                    //if (timerRunStamina.TotalMilliseconds > 100)
                    //{
                    //    timerRunStamina = TimeSpan.Zero;
                    //    staminaAmount--;
                    //}
                }

                else
                {
                    ninjaState = NinjaState.Walking;

                    if (timerMain.TotalMilliseconds > 50)
                    {
                        timerMain = TimeSpan.Zero;
                        runImage++;
                    }

                    if (timerWalkStamina.TotalMilliseconds > 30)
                    {
                        timerWalkStamina = TimeSpan.Zero;
                        walkDistance++;
                    }

                    //if (walkDistance == 30)
                    //{
                    //    walkDistance = 0;
                    //    if (staminaAmount < 7)
                    //    {
                    //        staminaAmount++;
                    //    }

                    //}


                }


                Pos.X += SpeedX;
                SetSpriteEffects(SpriteEffects.None);
            }

            if (ninjaState == NinjaState.MovingJumpingLeft)
            {
                UpwardsVelocity = 10;
            }

            else if (ninjaState == NinjaState.MovingJumpingRight)
            {
                UpwardsVelocity = 10;
            }
        }
        int posX1 = 0;
        public override void Draw(SpriteBatch spriteBatch)
        {

            if (isCrouch)
            {
                Image = crouchImage;
                Pos.Y = 405;
                base.Draw(spriteBatch);
            }

            else
            {
                if (ninjaState != NinjaState.Jumping /*|| ninjaState != NinjaState.MovingJumpingLeft || ninjaState != NinjaState.MovingJumpingRight*/)
                {
                    

                    if (ninjaState == NinjaState.Running || ninjaState == NinjaState.Walking)
                    {
                        if (staminaAmount <= 4)
                        {
                            ninjaState = NinjaState.Walking;
                        }
                        Image = running[runImage % running.Count];
                    }

                    else if (ninjaState == NinjaState.Idle && !isFalling)
                    {
                        Pos.Y = 380;
                        Image = idle[idleNum % idle.Count];
                    }


                }

                else
                {
                    Image = jumpImage;
                }





                base.Draw(spriteBatch);
            }

            for (int i = 0; i < staminaAmount; i++)
            {
                spriteBatch.Draw(staminaImage, new Vector2(posX1, 0), null, Color.White, 0f, Vector2.Zero, new Vector2(1, 1), SpriteEffects.None, 0f);
                posX1 += 40;
                //if (posX1 > 280)
                //{
                //    posX1 = 0;
                //}
            }
            posX1 = 0;
        }
    }
}