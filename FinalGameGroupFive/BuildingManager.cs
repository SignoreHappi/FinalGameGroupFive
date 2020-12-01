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
    class BuildingManager : DrawableGameComponent
    {

        const int INITIAL_BUILDING_COUNT = 5;
        private Game parent;

        Random random;


        public BuildingManager(Game game) : base(game)
        {
            this.parent = game;
            random = new Random();
        }

        public override void Initialize()
        {
            for (int i = 0; i < INITIAL_BUILDING_COUNT; i++)
            {
                parent.Components.Add(new Building(parent, GetRandomPosition(), 
                    (BuildingColor) random.Next(0, Enum.GetValues(typeof(BuildingColor)).Length)));

            }
            base.Initialize();
        }

        private Vector2 GetRandomPosition()
        {
            return new Vector2(random.Next(0, Game.GraphicsDevice.Viewport.Width - Building.WIDTH),
                               random.Next(0, Game.GraphicsDevice.Viewport.Height - Building.HEIGHT));
        }

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }
    }
}
