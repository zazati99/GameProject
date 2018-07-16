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

            string line;
            while ((line = reader.ReadLine()) != null)
            {
                xPos = (int)position.X;
                for (int i = 0; i < line.Length; i++)
                {
                    if (line[i] != '*')
                    {
                        Ground.GROUND_TYPE groundType = (Ground.GROUND_TYPE)int.Parse(line[i].ToString());
                        Ground ground = Ground.MakeGround(screen, groundType);
                        map.GameObjects.Add(ground);
                        screen.GameObjects.Add(ground);
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

            for (int i = 0; i < 50; i++)
            {
                for (int  j = 0; j < 50; j++)
                {
                    Vector2 Position = new Vector2(32 * j, 32 * i);
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
