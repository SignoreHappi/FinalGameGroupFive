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
    [Flags]
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

        const double FRAME_DURATION = 0.1;
        const int SPEED = 3;
        public const int WIDTH = 32;
        public const int HEIGHT = 32;

        Vector2 position;

        Dictionary<PlayerState, Texture2D> textures;
        Dictionary<PlayerState, List<Rectangle>> sourceRectangles;

        PlayerState state = PlayerState.Idle;

        int currentFrame;
        int frames;
        double frameTimer;
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
            textures.Add(PlayerState.Idle, Game.Content.Load<Texture2D>("Player_Idle"));
            textures.Add(PlayerState.WalkingUp, Game.Content.Load<Texture2D>("Player_Walk_Up"));
            textures.Add(PlayerState.WalkingDown, Game.Content.Load<Texture2D>("Player_Walk_Down"));
            textures.Add(PlayerState.WalkingLeft, Game.Content.Load<Texture2D>("Player_Walk_Left"));
            textures.Add(PlayerState.WalkingRight, Game.Content.Load<Texture2D>("Player_Walk_Right"));



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
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.Draw(textures[state], position, sourceRectangles[state][currentFrame], Color.White,
                    0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
            sb.End();


            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
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
            }
            else if (ks.IsKeyDown(Keys.D))
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
            } 

            
            else
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

            position.X = MathHelper.Clamp(position.X, 0, GraphicsDevice.Viewport.Width - WIDTH);
            position.Y = MathHelper.Clamp(position.Y, 0, GraphicsDevice.Viewport.Height - HEIGHT);

            base.Update(gameTime);
        }

    }
}
