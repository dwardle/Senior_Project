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
        public enum m_DoorPlacement { Up = 0, Down, Left, Right };

        List<Rooms> m_LevelRooms = new List<Rooms>();
        int m_LevelCount;
        int m_RoomCount = 5;
        int m_NextRoom;
        int m_CenterRoom;
        int m_RemainingRooms;
        Random Rand = new Random();

        public Level(int a_LevelCount)
        {
            m_LevelCount = a_LevelCount;
            m_RemainingRooms = m_RoomCount;
            m_NextRoom = 0;
            CreateLevel();
        }

        public void LoadContent(ContentManager a_Content)
        {
            foreach(Rooms r in m_LevelRooms)
            {
                r.LoadContent(a_Content);
            }
        }

        public void Draw(SpriteBatch a_SpriteBatch)
        {
            foreach (Rooms r in m_LevelRooms)
            {
                r.Draw(a_SpriteBatch);
            }
        }

        public void CreateLevel()
        {
            CreateFirstRoom();
            //m_RemainingRooms--;
            int[] RoomPlacement = new int[100];
            for(int i = 0; i < 100; i++)
            {
                int random = Rand.Next(4);
                RoomPlacement[i] = random;
            }
            int RoomIndex = 0;
            int x = 0;
            while(m_RemainingRooms > 0)
            {
                RoomIndex = CreateRoom(RoomPlacement[x], RoomIndex);
                x++;
            }

            m_LevelRooms[m_LevelRooms.Count - 1].SetDeadEnd(true);
            while(NumDeadEnds() < 2)
            {
                CreateDeadEnd(Rand.Next(0, 4), Rand.Next(0, m_LevelRooms.Count - 1));
            }
            CreateRoom(0, 0);

        }

        public void CreateFirstRoom()
        {
            Rooms firstRoom = new Rooms();
            //firstRoom.GenerateDoors();
            m_LevelRooms.Add(firstRoom);
            m_NextRoom = 0;
            m_RemainingRooms--;
        }

        //public void CreatRoom()
        //{
        //    Rooms NewRoom = new Rooms();
        //    m_LevelRooms.Add(NewRoom);
        //    m_RemainingRooms--;
        //}

        //public void CreateSurroundingRooms(int a_CenterRoom)
        //{
        //    //m_CenterRoom = a_CenterRoom;
        //    if (m_LevelRooms[m_CenterRoom].DoorExists(0))
        //    {
        //        int CenterRoomX = (int)m_LevelRooms[m_CenterRoom].m_RoomPosition.X;
        //        int CenterRoomY = (int)m_LevelRooms[m_CenterRoom].m_RoomPosition.Y;
        //        int doorIndex = m_LevelRooms[m_CenterRoom].FindDoor(0);
        //        m_NextRoom++;
        //        m_LevelRooms[m_CenterRoom].m_RoomDoors[doorIndex].m_nextRoom = m_NextRoom;
        //        m_LevelRooms[m_NextRoom].MoveRoom(CenterRoomX, CenterRoomY - 832);
        //        m_LevelRooms[m_NextRoom].CreateDoor(1);
        //        m_LevelRooms[m_NextRoom].m_RoomDoors[0].m_nextRoom = m_CenterRoom;
        //    }
        //    if (m_LevelRooms[m_CenterRoom].DoorExists(1))
        //    {
        //        int CenterRoomX = (int)m_LevelRooms[m_CenterRoom].m_RoomPosition.X;
        //        int CenterRoomY = (int)m_LevelRooms[m_CenterRoom].m_RoomPosition.Y;
        //        int doorIndex = m_LevelRooms[m_CenterRoom].FindDoor(1);
        //        m_NextRoom++;
        //        m_LevelRooms[m_CenterRoom].m_RoomDoors[doorIndex].m_nextRoom = m_NextRoom;
        //        m_LevelRooms[m_NextRoom].MoveRoom(CenterRoomX, CenterRoomY + 832);
        //        m_LevelRooms[m_NextRoom].CreateDoor(0);
        //        m_LevelRooms[m_NextRoom].m_RoomDoors[0].m_nextRoom = m_CenterRoom;
        //    }
        //    if (m_LevelRooms[m_CenterRoom].DoorExists(2))
        //    {
        //        int CenterRoomX = (int)m_LevelRooms[m_CenterRoom].m_RoomPosition.X;
        //        int CenterRoomY = (int)m_LevelRooms[m_CenterRoom].m_RoomPosition.Y;
        //        int doorIndex = m_LevelRooms[m_CenterRoom].FindDoor(2);
        //        m_NextRoom++;
        //        m_LevelRooms[m_CenterRoom].m_RoomDoors[doorIndex].m_nextRoom = m_NextRoom;
        //        m_LevelRooms[m_NextRoom].MoveRoom(CenterRoomX - 960, CenterRoomY);
        //        m_LevelRooms[m_NextRoom].CreateDoor(3);
        //        m_LevelRooms[m_NextRoom].m_RoomDoors[0].m_nextRoom = m_CenterRoom;
        //    }
        //    if (m_LevelRooms[m_CenterRoom].DoorExists(3))
        //    {
        //        int CenterRoomX = (int)m_LevelRooms[m_CenterRoom].m_RoomPosition.X;
        //        int CenterRoomY = (int)m_LevelRooms[m_CenterRoom].m_RoomPosition.Y;
        //        int doorIndex = m_LevelRooms[m_CenterRoom].FindDoor(3);
        //        m_NextRoom++;
        //        m_LevelRooms[m_CenterRoom].m_RoomDoors[doorIndex].m_nextRoom = m_NextRoom;
        //        m_LevelRooms[m_NextRoom].MoveRoom(CenterRoomX - 960, CenterRoomY);
        //        m_LevelRooms[m_NextRoom].CreateDoor(2);
        //        m_LevelRooms[m_NextRoom].m_RoomDoors[0].m_nextRoom = m_CenterRoom;
        //    }

        //    foreach (Rooms r in m_LevelRooms)
        //    {
        //        if (r.m_RoomPosition.X == 0 && r.m_RoomPosition.Y == 0)
        //        {
        //            continue;
        //        }
        //        else
        //        {
        //            r.GenerateDoors(0, 0);
        //        }
        //    }
        //    if(m_NextRoom < m_RoomCount)
        //    {
        //        //issue, the room that gets set as center room may only have 1 door after door generation
        //        if(m_LevelRooms[m_CenterRoom].DoorExists(0))
        //        {
        //            int doorIndex = m_LevelRooms[m_CenterRoom].FindDoor(0);
        //            m_CenterRoom = m_LevelRooms[m_CenterRoom].m_RoomDoors[doorIndex].m_nextRoom;
        //        }
        //    }
        //}

        //public void CreateRooms(List<Door> a_CurrentRoomDoors, int a_DoorEnteredFrom)//, int Placement)
        //{
        //    //Algorithm to create rooms based off of how many doors there are.
        //    //current problems: If I call this without creating the first room first there will be no door list.
        //    //                  if i call this on any room other then the first room, a room will be created for the previous room
        //    //if(a_DoorEnteredFrom < 0)
        //    //{
        //    //    CreateFirstRoom();
        //    //}
            
        //    int firstRoom = m_NextRoom;
        //    int prevRoomNum = m_NextRoom;
        //    int currentX = (int)m_LevelRooms[m_NextRoom].m_RoomPosition.X;
        //    int currentY = (int)m_LevelRooms[m_NextRoom].m_RoomPosition.Y;
        //    //as of right now this gets a room list where all rooms point back to the center room
        //    foreach(Door d in a_CurrentRoomDoors)
        //    {
        //        if (d.m_Placement == 0) //&& a_DoorEnteredFrom != 0)
        //        {
        //            //prevRoomNum = m_NextRoom;
        //            m_NextRoom++;
        //            d.m_nextRoom = m_NextRoom;
        //            Rooms nRoom = new Rooms(currentX, currentY - 832);
        //            //Trying this out
        //            nRoom.CreateDoor(1);
        //            nRoom.m_RoomDoors[0].m_nextRoom = firstRoom;
        //            //
        //            m_LevelRooms.Add(nRoom);
        //            m_RemainingRooms--;
        //        }
        //        else if(d.m_Placement == 1)// && a_DoorEnteredFrom != 1)
        //        {
        //            //prevRoomNum = m_NextRoom;
        //            m_NextRoom++;
        //            d.m_nextRoom = m_NextRoom;
        //            Rooms nRoom = new Rooms(currentX, currentY + 832);
        //            nRoom.CreateDoor(0);
        //            nRoom.m_RoomDoors[0].m_nextRoom = firstRoom;
        //            m_LevelRooms.Add(nRoom);
        //            m_RemainingRooms--;
        //        }
        //        else if (d.m_Placement == 2)// && a_DoorEnteredFrom != 2)
        //        {
        //            //prevRoomNum = m_NextRoom;
        //            m_NextRoom++;
        //            d.m_nextRoom = m_NextRoom;
        //            Rooms nRoom = new Rooms(currentX - 960, currentY);
        //            nRoom.CreateDoor(3);
        //            nRoom.m_RoomDoors[0].m_nextRoom = firstRoom;
        //            m_LevelRooms.Add(nRoom);
        //            m_RemainingRooms--;
        //        }
        //        else if (d.m_Placement == 3)// && a_DoorEnteredFrom != 3)
        //        {
        //            //prevRoomNum = m_NextRoom;
        //            m_NextRoom++;
        //            d.m_nextRoom = m_NextRoom;
        //            Rooms nRoom = new Rooms(currentX + 960, currentY);
        //            nRoom.CreateDoor(2);
        //            nRoom.m_RoomDoors[0].m_nextRoom = firstRoom;
        //            m_LevelRooms.Add(nRoom);
        //            m_RemainingRooms--;
        //        }  
        //    }
        //    m_NextRoom = firstRoom;

        //    //if (Placement == 0 && m_LevelRooms[m_NextRoom].DoorExists(0))
        //    //{
        //    //    prevRoomNum = m_NextRoom;
        //    //    m_NextRoom++;
        //    //    a_CurrentRoomDoors[0].m_nextRoom = m_NextRoom;
        //    //    Rooms nRoom = new Rooms(currentX, currentY - 832);
        //    //    m_LevelRooms.Add(nRoom);
        //    //    //nRoom.GenerateDoors(m_RemainingRooms, 0);
        //    //    //nRoom.m_RoomDoors[1].m_nextRoom = prevRoomNum;
        //    //}
        //    //else if (Placement == 1 && m_LevelRooms[m_NextRoom].DoorExists(1))
        //    //{
        //    //    prevRoomNum = m_NextRoom;
        //    //    m_NextRoom++;
        //    //    a_CurrentRoomDoors[1].m_nextRoom = m_NextRoom;
        //    //    Rooms nRoom = new Rooms(currentX, currentY + 832);
        //    //    m_LevelRooms.Add(nRoom);
        //    //    //nRoom.GenerateDoors(m_RemainingRooms, 0);
        //    //    //nRoom.m_RoomDoors[1].m_nextRoom = prevRoomNum;
        //    //}
        //    //else if (Placement == 2 && m_LevelRooms[m_NextRoom].DoorExists(2))
        //    //{
        //    //    prevRoomNum = m_NextRoom;
        //    //    m_NextRoom++;
        //    //    a_CurrentRoomDoors[2].m_nextRoom = m_NextRoom;
        //    //    Rooms nRoom = new Rooms(currentX - 960, currentY);
        //    //    m_LevelRooms.Add(nRoom);
        //    //    //nRoom.GenerateDoors(m_RemainingRooms, 0);
        //    //    //nRoom.m_RoomDoors[1].m_nextRoom = prevRoomNum;
        //    //}
        //    //else if (Placement == 3 && m_LevelRooms[m_NextRoom].DoorExists(3))
        //    //{
        //    //    prevRoomNum = m_NextRoom;
        //    //    m_NextRoom++;
        //    //    a_CurrentRoomDoors[3].m_nextRoom = m_NextRoom;
        //    //    Rooms nRoom = new Rooms(currentX + 960, currentY);
        //    //    m_LevelRooms.Add(nRoom);
        //    //    //nRoom.GenerateDoors(m_RemainingRooms, 0);
        //    //    //nRoom.m_RoomDoors[1].m_nextRoom = prevRoomNum;
        //    //}
        //}

        public int CreateRoom(int a_DoorPlacement, int a_CurrentRoomIndex)
        {
            int NewRoomIndex = 0;
            if(a_DoorPlacement == (int)m_DoorPlacement.Up && m_LevelRooms[a_CurrentRoomIndex].DoorExists(a_DoorPlacement) == false)
            {
                int CR_X = (int)m_LevelRooms[a_CurrentRoomIndex].m_RoomPosition.X;
                int CR_Y = (int)m_LevelRooms[a_CurrentRoomIndex].m_RoomPosition.Y;
                m_LevelRooms[a_CurrentRoomIndex].CreateDoor(a_DoorPlacement);
                m_LevelRooms[a_CurrentRoomIndex].SetDeadEnd(false);
                Rooms NewRoom = new Rooms();
                m_RemainingRooms--;
                NewRoom.SetDeadEnd(true);
                NewRoom.MoveRoom(CR_X, CR_Y - 832);
                NewRoom.CreateDoor((int)m_DoorPlacement.Down, a_CurrentRoomIndex);
                NewRoom.GenerateEnemies();
                m_LevelRooms.Add(NewRoom);
                NewRoomIndex = m_LevelRooms.Count - 1;
                int DoorIndex = m_LevelRooms[a_CurrentRoomIndex].FindDoor(a_DoorPlacement);
                m_LevelRooms[a_CurrentRoomIndex].m_RoomDoors[DoorIndex].m_nextRoom = NewRoomIndex;
            }
            else if(a_DoorPlacement == (int)m_DoorPlacement.Down && m_LevelRooms[a_CurrentRoomIndex].DoorExists(a_DoorPlacement) == false)
            {
                int CR_X = (int)m_LevelRooms[a_CurrentRoomIndex].m_RoomPosition.X;
                int CR_Y = (int)m_LevelRooms[a_CurrentRoomIndex].m_RoomPosition.Y;
                m_LevelRooms[a_CurrentRoomIndex].SetDeadEnd(false);
                m_LevelRooms[a_CurrentRoomIndex].CreateDoor(a_DoorPlacement);
                Rooms NewRoom = new Rooms();
                m_RemainingRooms--;
                NewRoom.SetDeadEnd(true);
                NewRoom.MoveRoom(CR_X, CR_Y + 832);
                NewRoom.CreateDoor((int)m_DoorPlacement.Up, a_CurrentRoomIndex);
                NewRoom.GenerateEnemies();
                m_LevelRooms.Add(NewRoom);
                NewRoomIndex = m_LevelRooms.Count - 1;
                int DoorIndex = m_LevelRooms[a_CurrentRoomIndex].FindDoor(a_DoorPlacement);
                m_LevelRooms[a_CurrentRoomIndex].m_RoomDoors[DoorIndex].m_nextRoom = NewRoomIndex;
            }
            else if (a_DoorPlacement == (int)m_DoorPlacement.Left && m_LevelRooms[a_CurrentRoomIndex].DoorExists(a_DoorPlacement) == false)
            {
                int CR_X = (int)m_LevelRooms[a_CurrentRoomIndex].m_RoomPosition.X;
                int CR_Y = (int)m_LevelRooms[a_CurrentRoomIndex].m_RoomPosition.Y;
                m_LevelRooms[a_CurrentRoomIndex].SetDeadEnd(false);
                m_LevelRooms[a_CurrentRoomIndex].CreateDoor(a_DoorPlacement);
                Rooms NewRoom = new Rooms();
                m_RemainingRooms--;
                NewRoom.SetDeadEnd(true);
                NewRoom.MoveRoom(CR_X - 960, CR_Y);
                NewRoom.CreateDoor((int)m_DoorPlacement.Right, a_CurrentRoomIndex);
                NewRoom.GenerateEnemies();
                m_LevelRooms.Add(NewRoom);
                NewRoomIndex = m_LevelRooms.Count - 1;
                int DoorIndex = m_LevelRooms[a_CurrentRoomIndex].FindDoor(a_DoorPlacement);
                m_LevelRooms[a_CurrentRoomIndex].m_RoomDoors[DoorIndex].m_nextRoom = NewRoomIndex;
            }
            else if (a_DoorPlacement == (int)m_DoorPlacement.Right && m_LevelRooms[a_CurrentRoomIndex].DoorExists(a_DoorPlacement) == false)
            {
                int CR_X = (int)m_LevelRooms[a_CurrentRoomIndex].m_RoomPosition.X;
                int CR_Y = (int)m_LevelRooms[a_CurrentRoomIndex].m_RoomPosition.Y;
                m_LevelRooms[a_CurrentRoomIndex].SetDeadEnd(false);
                m_LevelRooms[a_CurrentRoomIndex].CreateDoor(a_DoorPlacement);
                Rooms NewRoom = new Rooms();
                m_RemainingRooms--;
                NewRoom.SetDeadEnd(true);
                NewRoom.MoveRoom(CR_X + 960, CR_Y);
                NewRoom.CreateDoor((int)m_DoorPlacement.Left, a_CurrentRoomIndex);
                NewRoom.GenerateEnemies();
                m_LevelRooms.Add(NewRoom);
                NewRoomIndex = m_LevelRooms.Count - 1;
                int DoorIndex = m_LevelRooms[a_CurrentRoomIndex].FindDoor(a_DoorPlacement);
                m_LevelRooms[a_CurrentRoomIndex].m_RoomDoors[DoorIndex].m_nextRoom = NewRoomIndex;
            }
            else
            {
                int temp = m_LevelRooms[a_CurrentRoomIndex].FindDoor(a_DoorPlacement);
                if(a_CurrentRoomIndex != 0)
                {
                    m_LevelRooms[a_CurrentRoomIndex].SetDeadEnd(true);
                }
                return m_LevelRooms[a_CurrentRoomIndex].m_RoomDoors[temp].m_nextRoom;
            }
            
            return NewRoomIndex;

        }

        public int CreateDeadEnd(int a_DoorPlacement, int a_CurrentRoomIndex)
        {
            int NewRoomIndex = 0;
            if (a_DoorPlacement == (int)m_DoorPlacement.Up && m_LevelRooms[a_CurrentRoomIndex].DoorExists(a_DoorPlacement) == false)
            {
                int CR_X = (int)m_LevelRooms[a_CurrentRoomIndex].m_RoomPosition.X;
                int CR_Y = (int)m_LevelRooms[a_CurrentRoomIndex].m_RoomPosition.Y;
                m_LevelRooms[a_CurrentRoomIndex].CreateDoor(a_DoorPlacement);
                m_LevelRooms[a_CurrentRoomIndex].SetDeadEnd(false);
                Rooms NewRoom = new Rooms();
                m_RemainingRooms--;
                NewRoom.SetDeadEnd(true);
                NewRoom.MoveRoom(CR_X, CR_Y - 832);
                NewRoom.CreateDoor((int)m_DoorPlacement.Down, a_CurrentRoomIndex);
                m_LevelRooms.Add(NewRoom);
                NewRoomIndex = m_LevelRooms.Count - 1;
                int DoorIndex = m_LevelRooms[a_CurrentRoomIndex].FindDoor(a_DoorPlacement);
                m_LevelRooms[a_CurrentRoomIndex].m_RoomDoors[DoorIndex].m_nextRoom = NewRoomIndex;
            }
            else if (a_DoorPlacement == (int)m_DoorPlacement.Down && m_LevelRooms[a_CurrentRoomIndex].DoorExists(a_DoorPlacement) == false)
            {
                int CR_X = (int)m_LevelRooms[a_CurrentRoomIndex].m_RoomPosition.X;
                int CR_Y = (int)m_LevelRooms[a_CurrentRoomIndex].m_RoomPosition.Y;
                m_LevelRooms[a_CurrentRoomIndex].SetDeadEnd(false);
                m_LevelRooms[a_CurrentRoomIndex].CreateDoor(a_DoorPlacement);
                Rooms NewRoom = new Rooms();
                m_RemainingRooms--;
                NewRoom.SetDeadEnd(true);
                NewRoom.MoveRoom(CR_X, CR_Y + 832);
                NewRoom.CreateDoor((int)m_DoorPlacement.Up, a_CurrentRoomIndex);
                m_LevelRooms.Add(NewRoom);
                NewRoomIndex = m_LevelRooms.Count - 1;
                int DoorIndex = m_LevelRooms[a_CurrentRoomIndex].FindDoor(a_DoorPlacement);
                m_LevelRooms[a_CurrentRoomIndex].m_RoomDoors[DoorIndex].m_nextRoom = NewRoomIndex;
            }
            else if (a_DoorPlacement == (int)m_DoorPlacement.Left && m_LevelRooms[a_CurrentRoomIndex].DoorExists(a_DoorPlacement) == false)
            {
                int CR_X = (int)m_LevelRooms[a_CurrentRoomIndex].m_RoomPosition.X;
                int CR_Y = (int)m_LevelRooms[a_CurrentRoomIndex].m_RoomPosition.Y;
                m_LevelRooms[a_CurrentRoomIndex].SetDeadEnd(false);
                m_LevelRooms[a_CurrentRoomIndex].CreateDoor(a_DoorPlacement);
                Rooms NewRoom = new Rooms();
                m_RemainingRooms--;
                NewRoom.SetDeadEnd(true);
                NewRoom.MoveRoom(CR_X - 960, CR_Y);
                NewRoom.CreateDoor((int)m_DoorPlacement.Right, a_CurrentRoomIndex);
                m_LevelRooms.Add(NewRoom);
                NewRoomIndex = m_LevelRooms.Count - 1;
                int DoorIndex = m_LevelRooms[a_CurrentRoomIndex].FindDoor(a_DoorPlacement);
                m_LevelRooms[a_CurrentRoomIndex].m_RoomDoors[DoorIndex].m_nextRoom = NewRoomIndex;
            }
            else if (a_DoorPlacement == (int)m_DoorPlacement.Right && m_LevelRooms[a_CurrentRoomIndex].DoorExists(a_DoorPlacement) == false)
            {
                int CR_X = (int)m_LevelRooms[a_CurrentRoomIndex].m_RoomPosition.X;
                int CR_Y = (int)m_LevelRooms[a_CurrentRoomIndex].m_RoomPosition.Y;
                m_LevelRooms[a_CurrentRoomIndex].SetDeadEnd(false);
                m_LevelRooms[a_CurrentRoomIndex].CreateDoor(a_DoorPlacement);
                Rooms NewRoom = new Rooms();
                m_RemainingRooms--;
                NewRoom.SetDeadEnd(true);
                NewRoom.MoveRoom(CR_X + 960, CR_Y);
                NewRoom.CreateDoor((int)m_DoorPlacement.Left, a_CurrentRoomIndex);
                m_LevelRooms.Add(NewRoom);
                NewRoomIndex = m_LevelRooms.Count - 1;
                int DoorIndex = m_LevelRooms[a_CurrentRoomIndex].FindDoor(a_DoorPlacement);
                m_LevelRooms[a_CurrentRoomIndex].m_RoomDoors[DoorIndex].m_nextRoom = NewRoomIndex;
            }
            else
            {
                return -1;
            }
            return 0;

        }

        public List<Rooms> GetRoomList()
        {
            return m_LevelRooms;
        }

        public List<Door> GetRoomDoorList(int a_CurrentRoom)
        {
            List<Rooms> LevelRooms = GetRoomList();
            return LevelRooms[a_CurrentRoom].m_RoomDoors;
        }

        public int FindDeadEnd()
        {
            for(int i = 0; i < m_LevelRooms.Count; i++)
            {
                if(m_LevelRooms[i].IsDeadEnd() == true)
                {
                    return i;
                }
            }
            return -1;
        }

        public int FindNextDeadEnd(int RoomIndex)
        {
            RoomIndex++;
            for (; RoomIndex < m_LevelRooms.Count; RoomIndex++)
            {
                if (m_LevelRooms[RoomIndex].IsDeadEnd() == true)
                {
                    return RoomIndex;
                }
            }
            return -1;
        }

        public int NumDeadEnds()
        {
            int DeadEndCount = 0;
            for (int i = 0; i < m_LevelRooms.Count; i++)
            {
                if (m_LevelRooms[i].IsDeadEnd() == true)
                {
                    DeadEndCount++;
                }
            }
            return DeadEndCount;
        }

    }
}
