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
    public class TestCollision : DrawableGameComponent
    {
        static Texture2D texture;
        static Vector2 position;
        SpriteBatch sb;
        public static Color[] testData;
        public TestCollision(Game game) : base(game)
        {
            DrawOrder = int.MaxValue - 1;
        }


        public static Rectangle ItemHitbox
        {
            get
            {
                Rectangle rect = texture.Bounds;
                rect.Location = position.ToPoint();
                return rect;
            }
        }

        protected override void LoadContent()
        {
            texture = Game.Content.Load<Texture2D>(@"assets\buildings\collision_test");
            position = new Vector2(Game.GraphicsDevice.Viewport.Width / 3 * 2 - texture.Width / 2,
                Game.GraphicsDevice.Viewport.Height / 3 * 2 - texture.Height / 2);
            testData = new Color[texture.Width * texture.Height];
            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {

            sb = Game.Services.GetService<SpriteBatch>();

            sb.Begin();
            sb.Draw(texture, position, Color.White);




            sb.End();
            base.Draw(gameTime);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }


    }
}
