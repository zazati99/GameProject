using System.IO;
using System.Xml.Serialization;

using GameProject.GameScreens;
using GameProject.GameObjects;
using Microsoft.Xna.Framework;

namespace GameProject.GameUtils
{
    public class GameFileManager
    {
        // Load TileMap
        public static TileMap LoadTileMap(GameScreen screen, string path, Vector2 position)
        {
            TileMap map = new TileMap(screen);
            map.Position = position;

            StreamReader reader = new StreamReader(path);
            int xPos = (int)position.X;
            int yPos = (int)position.Y;

            map.Size.X = int.Parse(reader.ReadLine());
            map.Size.Y = int.Parse(reader.ReadLine());

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                xPos = (int)position.X;
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] != '*')
                    {
                        Ground.GROUND_TYPE groundType = (Ground.GROUND_TYPE)int.Parse(line[i].ToString());
                        Ground ground = Ground.MakeGround(screen, map, groundType);
                        map.GameObjects.Add(ground);
                        ground.Position.X = xPos;
                        ground.Position.Y = yPos;
                    }
                    xPos += (int)MainGame.TILE_SIZE.X;
                }
                yPos += (int)MainGame.TILE_SIZE.Y;
            }

            reader.Close();
            return map;
        }

        // Save a tileMap
        public static void SaveTileMap(TileMap map, string path)
        {
            StreamWriter writer = new StreamWriter(path);

            writer.WriteLine(map.Size.X);
            writer.WriteLine(map.Size.Y);

            for (int i = 0; i < map.Size.X; i++)
            {
                for (int  j = 0; j < map.Size.Y; j++)
                {
                    Vector2 Position = map.Position + new Vector2(MainGame.TILE_SIZE.X * j, MainGame.TILE_SIZE.Y * i);
                    Ground ground = null;
                    for (int k = 0; k < map.GameObjects.Count; k++)
                    {
                        if (map.GameObjects[k] is Ground temp)
                        {
                            if (temp.Position == Position)
                                ground = temp;
                        }
                    }
                    if (ground != null)
                        writer.Write((int)ground.GroundType);
                    else
                        writer.Write("*");
                }
                writer.Write("\n");
            }

            writer.Close();
        }

        // Save an array of tilemaps
        public static void SaveTileMapArray(TileMap[] tileMaps, string path)
        {
            Directory.CreateDirectory(path);
            for (int i = 0; i < tileMaps.Length; i++)
            {
                SaveTileMap(tileMaps[i], path + "/" + i);
            }
        }

        // Load TIleMap array
        public static TileMap[] LoadTileMapArray(string path, GameScreen screen, Point widthHeight, Vector2 startPos)
        {
            TileMap[] maps = new TileMap[widthHeight.X * widthHeight.Y];

            Vector2 position;

            position.Y = startPos.Y;
            for (int i = 0; i < widthHeight.X; i++)
            {
                position.X = startPos.X;
                for (int j = 0; j < widthHeight.Y; j++)
                {
                    maps[i * widthHeight.Y + j] = LoadTileMap(screen, path + "/" + (i * widthHeight.Y + j), position);
                    position.X += maps[i * widthHeight.Y + j].Size.X * MainGame.TILE_SIZE.X;
                }
                position.Y += maps[(i+1) * widthHeight.Y - 1].Size.Y * MainGame.TILE_SIZE.Y;
            }

            return maps;
        } 

        // Load XML Object
        public static T Load<T>(string path)
        {
            T instance;

            FileStream stream = File.OpenRead(path);
            XmlSerializer xmls = new XmlSerializer(typeof(T));

            instance = (T)xmls.Deserialize(stream);

            stream.Close();
            return instance;
        }

        // Save XML Object
        public static void Save(object o, string path)
        {
            FileStream stream = File.Create(path);
            XmlSerializer xmls = new XmlSerializer(o.GetType());

            xmls.Serialize(stream, o);

            stream.Close();
        }
    }
}
