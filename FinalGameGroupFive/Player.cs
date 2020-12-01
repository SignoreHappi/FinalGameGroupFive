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
        Idle_Up,
        Idle_Left,
        Idle_Right,
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
        PlayerState previousState = PlayerState.Idle;

        int currentFrame;
        int frames;
        double frameTimer;

        public Rectangle PlayerCollisionBox
        {
            get
            {
                Rectangle rect = textures[state].Bounds;
                rect.Location = position.ToPoint();
                return rect;
            }
        }




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
            textures.Add(PlayerState.Idle, Game.Content.Load<Texture2D>(@"assets\player\player_idle"));
            textures.Add(PlayerState.Idle_Up, Game.Content.Load<Texture2D>(@"assets\player\player_idle_up"));
            textures.Add(PlayerState.Idle_Left, Game.Content.Load<Texture2D>(@"assets\player\player_idle_left"));
            textures.Add(PlayerState.Idle_Right, Game.Content.Load<Texture2D>(@"assets\player\player_idle_right"));

            textures.Add(PlayerState.WalkingUp, Game.Content.Load<Texture2D>(@"assets\player\player_walking_up"));
            textures.Add(PlayerState.WalkingDown, Game.Content.Load<Texture2D>(@"assets\player\player_walking_down"));
            textures.Add(PlayerState.WalkingLeft, Game.Content.Load<Texture2D>(@"assets\player\player_walking_left"));
            textures.Add(PlayerState.WalkingRight, Game.Content.Load<Texture2D>(@"assets\player\player_walking_right"));


            //Creates just one frame per animation, to make sure that it won't be blinking
            sourceRectangles.Add(PlayerState.Idle, new List<Rectangle>());
            sourceRectangles[PlayerState.Idle].Add(new Rectangle(0, 0, WIDTH, HEIGHT));
            sourceRectangles.Add(PlayerState.Idle_Up, new List<Rectangle>());
            sourceRectangles[PlayerState.Idle_Up].Add(new Rectangle(0, 0, WIDTH, HEIGHT)); 
            sourceRectangles.Add(PlayerState.Idle_Left, new List<Rectangle>());
            sourceRectangles[PlayerState.Idle_Left].Add(new Rectangle(0, 0, WIDTH, HEIGHT));
            sourceRectangles.Add(PlayerState.Idle_Right, new List<Rectangle>());
            sourceRectangles[PlayerState.Idle_Right].Add(new Rectangle(0, 0, WIDTH, HEIGHT));

            //Creates 3 frames per sprite, to give it the animation
            for (int i = 4; i < (int)Enum.GetValues(typeof(PlayerState)).Length; i++)
            {
                string state = Enum.GetName(typeof(PlayerState), i);
                PlayerState playerState = (PlayerState)Enum.Parse(typeof(PlayerState), state);
                sourceRectangles.Add(playerState, new List<Rectangle>());

                for (int j = 0; j < frames; j++)
                {
                    Rectangle rect = new Rectangle(j * WIDTH, 0, WIDTH, HEIGHT);
                    sourceRectangles[playerState].Add(rect);
                }
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
            PlayerKeyboardController();

            UpdatePlayerFrame(gameTime);

            position.X = MathHelper.Clamp(position.X, 0, GraphicsDevice.Viewport.Width - WIDTH);
            position.Y = MathHelper.Clamp(position.Y, 0, GraphicsDevice.Viewport.Height - HEIGHT);

            base.Update(gameTime);
        }

        private void PlayerKeyboardController()
        {
            previousState = state;

            if (previousState == PlayerState.WalkingDown)
            {
                state = PlayerState.Idle;
            } else if (previousState == PlayerState.WalkingUp)
            {
                state = PlayerState.Idle_Up;
            } else if (previousState == PlayerState.WalkingLeft)
            {
                state = PlayerState.Idle_Left;
            } else if (previousState == PlayerState.WalkingRight)
            {
                state = PlayerState.Idle_Right;
            }

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

            }


        }



        private void UpdatePlayerFrame(GameTime gameTime)
        {
            if (previousState != PlayerState.Idle ||
                previousState != PlayerState.Idle_Left ||
                previousState != PlayerState.Idle_Right ||
                previousState != PlayerState.Idle_Up)
            {

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
            }
        }
    }
}
