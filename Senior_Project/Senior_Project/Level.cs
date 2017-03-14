using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Senior_Project
{
    public class Level
    {
        List<Rooms> m_LevelRooms = new List<Rooms>();
        int m_LevelCount;
        int m_RoomCount = 5;
        public Level(int a_LevelCount)
        {
            m_LevelCount = a_LevelCount;
            CreateLevel();
        }

        public void LoadContent(ContentManager a_Content)
        {

        }

        public void CreateLevel()
        {
            if (m_LevelCount == 1)
            {
                for (int i = 0; i < m_RoomCount; i++)
                {
                    if(i == 0)
                    {
                        CreateRoom();
                    }
                    
                    //room.GenerateDoors()
                    //m_LevelRooms.Add(room);
                }
            }
            else if(m_LevelCount > 1)
            {
                m_RoomCount = 5 + (m_LevelCount * 2);
                for(int i = 0; i < m_RoomCount; i++)
                {
                    Rooms room = new Rooms();
                    m_LevelRooms.Add(room);
                }
            }
        }

        public void CreateRoom()
        {
            Rooms firstRoom = new Rooms();
            firstRoom.GenerateDoors();
            m_LevelRooms.Add(firstRoom);
        }

        public List<Rooms> GetRoomList()
        {
            return m_LevelRooms;
        }

    }
}
