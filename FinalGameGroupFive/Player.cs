using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalGameGroupFive
{
    public enum PlayerState
    {
        Idle,
        WalkingUp,
        WalkingDown,
        WalkingLeft,
        WalkingRight
    }


    class Player : DrawableGameComponent
    {
        #region Variables

        //Constanst
        const double FRAME_DURATION = 0.1;
        const int SPEED = 3;
        public const int WIDTH = 32;
        public const int HEIGHT = 32;

        Vector2 position;

        //Dictionaries
        Dictionary<PlayerState, Texture2D> textures;
        Dictionary<PlayerState, List<Rectangle>> sourceRectangles;

        //Player
        PlayerState state = PlayerState.Idle;
        Color[] playerData;
        public Rectangle PlayerHitbox
        {
            get
            {
                Rectangle rect = textures[PlayerState.Idle].Bounds;
                rect.Location = position.ToPoint();
                return rect;
            }
        }

        //Frames
        int currentFrame;
        int frames;
        double frameTimer;




        Color backgroundColor = Color.CornflowerBlue;
        #endregion

        #region Constructor
        public Player(Game game) : base(game)
        {
            textures = new Dictionary<PlayerState, Texture2D>();
            sourceRectangles = new Dictionary<PlayerState, List<Rectangle>>();
            state = PlayerState.Idle;
            currentFrame = 0;
            frameTimer = 0;
            frames = 3;

            DrawOrder = int.MaxValue - 1;
        }
        #endregion



        protected override void LoadContent()
        {
            textures.Add(PlayerState.Idle, Game.Content.Load<Texture2D>(@"assets\player\Player_Idle"));
            textures.Add(PlayerState.WalkingUp, Game.Content.Load<Texture2D>(@"assets\player\Player_Walk_Up"));
            textures.Add(PlayerState.WalkingDown, Game.Content.Load<Texture2D>(@"assets\player\Player_Walk_Down"));
            textures.Add(PlayerState.WalkingLeft, Game.Content.Load<Texture2D>(@"assets\player\Player_Walk_Left"));
            textures.Add(PlayerState.WalkingRight, Game.Content.Load<Texture2D>(@"assets\player\Player_Walk_Right"));

            playerData = new Color[textures[PlayerState.Idle].Width * textures[PlayerState.Idle].Height];

            sourceRectangles.Add(PlayerState.Idle, new List<Rectangle>());
            sourceRectangles[PlayerState.Idle].Add(new Rectangle(0, 0, WIDTH, HEIGHT));
            sourceRectangles.Add(PlayerState.WalkingUp, new List<Rectangle>());
            for (int j = 0; j < frames; j++)
            {
                Rectangle rect = new Rectangle(j * WIDTH, 0, WIDTH, HEIGHT);
                sourceRectangles[PlayerState.WalkingUp].Add(rect);
            }


            sourceRectangles.Add(PlayerState.WalkingDown, new List<Rectangle>());
            for (int j = 0; j < frames; j++)
            {
                Rectangle rect = new Rectangle(j * WIDTH, 0, WIDTH, HEIGHT);
                sourceRectangles[PlayerState.WalkingDown].Add(rect);
            }


            sourceRectangles.Add(PlayerState.WalkingLeft, new List<Rectangle>());
            for (int j = 0; j < frames; j++)
            {
                Rectangle rect = new Rectangle(j * WIDTH, 0, WIDTH, HEIGHT);
                sourceRectangles[PlayerState.WalkingLeft].Add(rect);
            }


            sourceRectangles.Add(PlayerState.WalkingRight, new List<Rectangle>());
            for (int j = 0; j < frames; j++)
            {
                Rectangle rect = new Rectangle(j * WIDTH, 0, WIDTH, HEIGHT);
                sourceRectangles[PlayerState.WalkingRight].Add(rect);
            }

            position = new Vector2(Game.GraphicsDevice.Viewport.Width / 2 - WIDTH / 2,
                                   Game.GraphicsDevice.Viewport.Height / 2 - HEIGHT / 2);
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(backgroundColor);
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.Draw(textures[state], position, sourceRectangles[state][currentFrame], Color.White,
                    0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            sb.End();


            base.Draw(gameTime);
        }

        static bool PerPixelCollision(Rectangle rect1, Color[] data1, Rectangle rect2, Color[] data2)
        {
            int top = MathHelper.Max(rect1.Top, rect2.Top);
            int bottom = MathHelper.Min(rect1.Bottom, rect2.Bottom);
            int left = MathHelper.Max(rect1.Left, rect2.Left);
            int right = MathHelper.Min(rect1.Right, rect2.Right);
            for (int row = top; row < bottom; row++)
            {
                for (int col = left; col < right; col++)
                {
                    Color color1 = data1[(col - rect1.Left) + (row - rect1.Top) * rect1.Width];
                    Color color2 = data2[(col - rect2.Left) + (row - rect2.Top) * rect2.Width];
                    if (color1.A != 0 && color2.A != 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }


        public override void Update(GameTime gameTime)
        {
            backgroundColor = Color.CornflowerBlue;

            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.W) && ks.IsKeyDown(Keys.A))
            {
                state = PlayerState.WalkingUp;
                position.X -= SPEED;
                position.Y -= SPEED;
            } else if (ks.IsKeyDown(Keys.W) && ks.IsKeyDown(Keys.D))
            {
                state = PlayerState.WalkingUp;
                position.X += SPEED;
                position.Y -= SPEED;
            } else if (ks.IsKeyDown(Keys.S) && ks.IsKeyDown(Keys.A))
            {
                state = PlayerState.WalkingDown;
                position.X -= SPEED;
                position.Y += SPEED;
            } else if (ks.IsKeyDown(Keys.S) && ks.IsKeyDown(Keys.D))
            {
                state = PlayerState.WalkingDown;
                position.X += SPEED;
                position.Y += SPEED;
            } else if (ks.IsKeyDown(Keys.D))
            {
                position.X += SPEED;
                state = PlayerState.WalkingRight;
            } else if (ks.IsKeyDown(Keys.A))
            {
                position.X -= SPEED;
                state = PlayerState.WalkingLeft;
            } else
            if (ks.IsKeyDown(Keys.W))
            {
                state = PlayerState.WalkingUp;
                position.Y -= SPEED;
            } else if (ks.IsKeyDown(Keys.S))
            {
                state = PlayerState.WalkingDown;
                position.Y += SPEED;
            } else
            {
                state = PlayerState.Idle;
            }

            frameTimer += gameTime.ElapsedGameTime.TotalSeconds;
            if (frameTimer >= FRAME_DURATION)
            {
                frameTimer = 0;
                currentFrame++;
            }
            if (currentFrame >= sourceRectangles[state].Count)
            {
                currentFrame = 0;
            }
            if (!PlayerHitbox.Intersects(TestCollision.ItemHitbox))
            {
                Score.hit = false;
                //if (!PerPixelCollision(PlayerHitbox, playerData,
                //    TestCollision.ItemHitbox, TestCollision.testData))
                //{
                
            } else
            {
                Score.hit = true;
                backgroundColor = Color.SlateGray;

            }


            position.X = MathHelper.Clamp(position.X, 0, GraphicsDevice.Viewport.Width - textures[state].Width);
            position.Y = MathHelper.Clamp(position.Y, 0, GraphicsDevice.Viewport.Height - textures[state].Height);

            base.Update(gameTime);
        }

    }
}
