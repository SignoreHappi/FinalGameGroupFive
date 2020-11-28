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
    enum Dirts
    {
        YellowDirt1,
        YellowDirt2,
        YellowDirt3,
        YellowDirt4,
        YellowGrass
    }
    class Ground : DrawableGameComponent
    {

        public const int WIDTH = 32;
        public const int HEIGHT = 32;

        Dictionary<Dirts, Texture2D> textures;
        Dictionary<Dirts, List<Rectangle>> sourceRectangles;
        public Ground(Game game) : base(game)
        {
            textures.Add(Dirts.YellowDirt1, Game.Content.Load<Texture2D>("Ground"));
        }

        protected override void LoadContent()
        {
            //for(int i = 0; i < frames; i++)
            //{
            //    Rectangle rect = new Rectangle(i * WIDTH, 0, WIDTH, HEIGHT);
            //    sourceRectangles[PlayerState.WalkingUp].Add(rect);
            //}
            base.LoadContent();
        }
    }
}
