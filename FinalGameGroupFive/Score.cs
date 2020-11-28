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
    class Score : DrawableGameComponent
    {
        SpriteFont font;
        int scoreValue;

        public static bool hit;
        string scoreText => $"Colliding: {hit}";

        public Score(Game game) : base(game)
        {
            hit = false;
            if (Game.Services.GetService<Score>() != null)
            {
                Game.Services.RemoveService(typeof(Score));
            }
            DrawOrder = int.MaxValue - 1;
            Game.Services.AddService(this);
            scoreValue = 0;
        }

        protected override void LoadContent()
        {
            font = Game.Content.Load<SpriteFont>(@"assets\fonts\font");
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();
            sb.DrawString(font, scoreText, Vector2.Zero, Color.Red);
            sb.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// Add a given score to the total amount
        /// Must be a positive value
        /// </summary>
        /// <param name="score"></param>
        public void AddScore(int score)
        {
            if (score > 0)
            {
                scoreValue += score;
            }
        }

    }
}
