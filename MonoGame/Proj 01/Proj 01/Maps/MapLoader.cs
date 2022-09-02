using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj_01.Maps
{

    public enum Tiles
    {
        Wall,
        Floor
    };

    internal class MapLoader
    {
        public const int Width = 500;
        public const int Height = 200;

        public void LoadMap(out Tiles[,] tiles)
        {
            tiles = new Tiles[500, 500];
            
            // First and last lines are pure walls
            for(int n=0; n<Width; n++)
            {
                tiles[0, n] = Tiles.Wall;
                tiles[Height-1, n] = Tiles.Wall;
            }

            // everything eklse is wall then floor then wall
            for(int row = 1; row < Height-2; row++)
            {
                tiles[row, 0] = Tiles.Wall;
                tiles[row, Width - 1] = Tiles.Wall;
                for(int col = 1; col < Width-2; col++)
                {
                    tiles[row, col] = Tiles.Floor;
                }
            }


        }



    }
}
