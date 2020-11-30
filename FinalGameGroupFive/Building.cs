using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FinalGameGroupFive
{
    enum BuildingColor
    {
        Blue,
        Green,
        Orange,
        Purple
    }


    class Building : DrawableGameComponent
    {
        public static int WIDTH = 32;
        public static int HEIGHT = WIDTH;

        BuildingColor buildingColor;

        Vector2 position;

        static Dictionary<BuildingColor, Texture2D> textures;
        Rectangle sourceRectangle;


        public Rectangle BuildingCollisionBox
        {
            get
            {
                Rectangle rect = textures[BuildingColor.Blue].Bounds;
                rect.Location = position.ToPoint();
                return rect;
            }
        }
        public Building(Game game, Vector2 position, BuildingColor color) : base(game)
        {
            this.position = position;
            this.buildingColor = color;
        }

        protected override void LoadContent()
        {

            if (textures == null)
            {
                textures = new Dictionary<BuildingColor, Texture2D>();
                textures.Add(BuildingColor.Blue, Game.Content.Load<Texture2D>(@"assets\buildings\first_tier\building_blue"));
                textures.Add(BuildingColor.Green, Game.Content.Load<Texture2D>(@"assets\buildings\first_tier\building_green"));
                textures.Add(BuildingColor.Orange, Game.Content.Load<Texture2D>(@"assets\buildings\first_tier\building_orange"));
                textures.Add(BuildingColor.Purple, Game.Content.Load<Texture2D>(@"assets\buildings\first_tier\building_purple"));
            }

            base.LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            SpriteBatch sb = Game.Services.GetService<SpriteBatch>();
            sb.Begin();

            sb.Draw(textures[buildingColor], position, Color.White);

            sb.End();
            base.Draw(gameTime);
        }

        public static bool PlayerCollided(Player player)
        {
            bool playerHasCollided = false;
            //if(player.PlayerCollisionBox.Intersects())


            return playerHasCollided;
        }


    }
}
