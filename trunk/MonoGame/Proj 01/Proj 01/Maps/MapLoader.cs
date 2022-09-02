using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj_01.Maps
{

    public enum TileType
    {
        Empty,
        Wall,
        Floor
    };

    internal class MapLoader
    {
        public const int Width = 500;
        public const int Height = 200;

        public void LoadMap(out TileType[,] tiles)
        {
            tiles = new TileType[500, 500];
            
            // First and last lines are pure walls
            for(int n=0; n<Width; n++)
            {
                tiles[0, n] = TileType.Wall;
                tiles[Height-1, n] = TileType.Wall;
            }

            // everything else is wall then floor then wall
            for(int row = 1; row < Height-2; row++)
            {
                tiles[row, 0] = TileType.Wall;
                tiles[row, Width - 1] = TileType.Wall;
                for(int col = 1; col < Width-2; col++)
                {
                    tiles[row, col] = TileType.Floor;
                }
            }


        }



    }
}
