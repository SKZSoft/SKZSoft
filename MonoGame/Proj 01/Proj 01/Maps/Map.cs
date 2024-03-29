﻿using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using MonoGame.Extended.Tiled;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;

namespace Proj_01.Maps
{

    public enum TextureType
    {
        Wall01,
        Floor01
    }

    public class Map
    {
        // TODO from configs
        public int Width { get; set; }
        public int Height { get; set; }

        private ContentManager _contentManager;
        private MapTile[,] _tileMap;
        public int TileWidthInPixels { get; set; }
        public int TileHeightInPixels { get; set; }

        Dictionary<TextureType, Texture2D> _textures;
        Dictionary<MapTileType, MapTile> _mapTilePrototypes;

        // Yes, returning the array directly. This is the highest-traffic data of the whole app.
        // So we are going to trust the code to do the right thing with the array, rather than abstract the functionality.
        // Change of mind. Let's have the map return the relevant areas with a quick copy.
        // The rest of the app just just ask for "what is the map at these co-ordinates please"?
        // That way, the map can also be in charge of stuff like collision detection and player speed.

        public Map(int tileWidthInPixels, int tileHeightInPixels, ContentManager content)
        {

            TileWidthInPixels = tileWidthInPixels;
            TileHeightInPixels = tileHeightInPixels;

            _contentManager = content;
        }


        public void Load()
        {
            Width = 20;     // TODO from configs
            Height = 20;
            _tileMap = new MapTile[Height, Width];


            // Tough design decision. Do I keep the textures cached or just create all the tiles and forget the textures?
            // In which case I don't need to cache the textures as they are all in the MapTile properies.
            // The limitation introduced is that new Tile Types (if created later) cannot look up an existing texture.
            // On the other hand, maybe a map should pre-cache any POSSIBLE tile types.
            // They can just be looked up in the tile prototypes.


            // Which brings me to another thing.
            // If we store prototypes and shove them into the array, then they are ALL the same instance.
            // What if we want to change the property of one or more tiles, later, after an event?
            // If we want to slow speed, for example, to show that the tile has been subject to a spillage?
            // If we change the "Speed" property of a tile prototype, then ALL tiles change.
            // It could be done with a new texture right now, but changing the speed on a property of a tile
            // would change it on ALL tiles with that texture.
            // Then we get into dodge design territory like "Oh well this texture is identical to the other one but has a different ID".
            // So when the first texture is updated, the other tiles which used to look identical no longer do.
            // It can be argued that corrupted tiles SHOULD look different, but can equally be argued that sometimes they shouldn't.

            // But.
            // Right now, this is massive design overthink.
            // That question can only be answered when we have a much better grasp of how maps will work.
            // Right now, I want a sprite which can hit a wall.

            // Later, it'll be easy to introduce a .Clone() if needed, or other solution.
            // So, for now: simplicity. Every map tile is a reference to the prototype.

            // In addition to which, if we encapsulate all the behaviour in the Map class, then it won't MATTER to its consumers.
            // We can change the internal behaviour quite safely. Indeed, there is an argument that only clones of MapTiles should be
            // passed back to consumers of the class.

            // Design overthink again, for now.
            // But one to watch out for. If consumers start wanting to change map tiles, then we have a serious design issue.
            // And THAT is when it should be addressed.


            // TODO textures will ultimately be loaded from a config but for now we have only two
            _textures = new Dictionary<TextureType, Texture2D>();
            _mapTilePrototypes = new Dictionary<MapTileType, MapTile>();

            Texture2D texture = _contentManager.Load<Texture2D>("BlockSquare");
            _textures.Add(TextureType.Floor01, texture);
            MapTile tileFloor = new MapTile(MapTileType.Floor, false, texture);
            _mapTilePrototypes.Add(MapTileType.Floor, tileFloor);

            texture = _contentManager.Load<Texture2D>("Floor01");
            _textures.Add(TextureType.Wall01, texture);
            MapTile tileWall = new MapTile(MapTileType.Wall, true, texture);
            _mapTilePrototypes.Add(MapTileType.Wall, tileWall);


            // First and last lines are pure walls
            for (int n = 0; n < Width; n++)
            {
                _tileMap[0, n] = tileWall;
                _tileMap[Height - 1, n] = tileWall;
            }

            // everything else is wall then floor then wall
            for (int row = 1; row < Height - 1; row++)
            {
                _tileMap[row, 0] = tileWall;
                _tileMap[row, Width - 1] = tileWall;
                for (int col = 1; col < Width - 1; col++)
                {
                    _tileMap[row, col] = tileFloor;
                }
            }
        }

        public MapTile GetTileAt(int X, int Y)
        {
            // TODO bounds checking
            return _tileMap[X, Y];
        }

        public void GetMapSection(int arrayX, int arrayY, int width, int height, out MapTile[,] mapSection)
        {
            // create array to copy map to
            mapSection = new MapTile[width, height];
            //System.Diagnostics.Debug.WriteLine(String.Format("Fetching map section of {0},{1}", width, height));
            try
            {

                int sectionX = 0;
                int sectionY = 0;
                int readToX = arrayX + width - 1;
                int readToY = arrayY + height - 1;
                for (int readY = arrayY; readY < readToY; readY++)
                {
                    for (int readX = arrayX; readX < readToX; readX++)
                    {
                        MapTile mapTile;
                        // bounds checks
                        if (readX < 0 || readX >= Width || readY < 0 || readY >= Height)
                        {
                            // null if out of bounds
                            mapTile = null;
                        }
                        else
                        {
                            // fetch map from relevant place in the main array
                            mapTile = _tileMap[readX, readY];
                        }
                        mapSection[sectionX, sectionY] = mapTile;
                        sectionX++;
                    }
                    sectionX = 0;
                    sectionY++;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }

        }

    }
}
