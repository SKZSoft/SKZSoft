using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proj_01.Maps
{
    public enum MapTileType
    {
        Empty,
        Wall,
        Floor
    };


    public class MapTile
    {
        public MapTileType Type{ get; set; }
        public bool BlocksPlayer { get; set; }
        public Texture2D Texture { get; set; }

        public MapTile(MapTileType type, bool blocksPlayer, Texture2D texture)
        {
            Type = type;
            BlocksPlayer = blocksPlayer;
            Texture = texture;
        }

    }
}
