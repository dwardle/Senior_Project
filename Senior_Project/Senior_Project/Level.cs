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
    //
    public class Level
    {
        public enum m_DoorPlacement { Up = 0, Down, Left, Right };

        List<Rooms> m_LevelRooms = new List<Rooms>();
        int m_LevelCount;
        int m_RoomCount = 5;
        int m_NextRoom;
        int m_CenterRoom;
        int m_RemainingRooms;
        int m_CurrentRoomIndex_X;
        int m_CurrentRoomIndex_Y;
        Random Rand = new Random();
        Rooms[,] levelRooms;

        //new deadends variable
        int m_DeadEnds = 0;

        public Level(int a_LevelCount)
        {
            m_LevelCount = a_LevelCount;
            m_RemainingRooms = m_RoomCount;
            levelRooms = new Rooms[m_RemainingRooms, m_RemainingRooms];
            m_NextRoom = 0;
            m_CurrentRoomIndex_X = m_RoomCount / 2;
            m_CurrentRoomIndex_Y = m_RoomCount / 2;
            CreateLevel1();
            //CreateLevel();
        }

        ///////old load content//////
        public void LoadContent(ContentManager a_Content)
        {
            foreach(Rooms r in m_LevelRooms)
            {
                r.LoadContent(a_Content);
            }
        }
        ///end old load content/////
        ///
        ////new load content using room array
        public void LoadContent(ContentManager a_Content, int temp)
        {
            foreach(Rooms room in levelRooms)
            {
                if(room != null)
                {
                    room.LoadContent(a_Content);
                }
            }
        }
        
        ///old draw using room list///
        public void Draw(SpriteBatch a_SpriteBatch)
        {
            foreach (Rooms r in m_LevelRooms)
            {
                r.Draw(a_SpriteBatch);
            }
        }
        ///end old draw///

        ///new Draw using room array
        public void Draw(SpriteBatch a_SpriteBatch, int temp)
        {
            foreach(Rooms room in levelRooms)
            {
                if(room != null)
                {
                    room.Draw(a_SpriteBatch);
                }
                
            }
        }
        


        //check if room already exists
        public bool RoomExists(int Row, int Col)
        {
            if(levelRooms[Row,Col] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
       
        //new create level
        //currently creates a level without overlapping rooms, still need to make sure there are 2 dead ends not counting the first room
        public void CreateLevel1()
        {
            //room size 960 x 832
            Vector2 CurrentRoomIndex;
            int random;
            levelRooms[m_RoomCount / 2, m_RoomCount / 2] = new Rooms(m_RoomCount);
            CurrentRoomIndex = levelRooms[m_RoomCount / 2, m_RoomCount / 2].m_RoomIndex;
            //int[] RoomPlacement = new int[100];
            //for (int i = 0; i < 100; i++)
            //{
            //    random = Rand.Next(4);
            //    RoomPlacement[i] = random;
            //}
            
            while(m_RemainingRooms > 1)
            {
                random = Rand.Next(4);
                CurrentRoomIndex = CreateRoom(CurrentRoomIndex, random);
            }

            //foreach(Rooms room in levelRooms)
            //{
            //    MoveRoom(room);
            //}

            Console.Write("done");

            /*ANYTHING UNDER THIS CAN BE DELETED. NEW FUNCTIONS ARE NOW WORKING
             * 
             * 
            int RoomRow = 2;
            int RoomCol = 2;
            int x = 0;
            int CR_X;
            int CR_Y;
            while (m_RemainingRooms > 0)
            {
                //if (RoomCol - 1 >= 0 && RoomCol + 1 < m_RoomCount)
                //if (RoomRow - 1 >= 0 && RoomRow + 1 < m_RoomCount)
                CR_X = (int)levelRooms[RoomRow, RoomCol].m_RoomPosition.X;
                CR_Y = (int)levelRooms[RoomRow, RoomCol].m_RoomPosition.Y;
                //create room above current room
                if (RoomPlacement[x] == 0)
                {
                    //make sure room index is not outside bounds off array
                    if (RoomRow - 1 >= 0)
                    {
                        //check if the room exists first
                        if (RoomExists(RoomRow - 1, RoomCol) == false)
                        {
                            //if not create a door in the current room leading to the next room
                            levelRooms[RoomRow, RoomCol].CreateDoor(0);
                            //then create a new room with a door leading back to last room
                            RoomRow -= 1;
                            levelRooms[RoomRow, RoomCol] = new Rooms(CR_X, CR_Y - 832, RoomRow, RoomCol);
                            levelRooms[RoomRow, RoomCol].CreateDoor(1);
                            m_RemainingRooms--;
                        }
                        else
                        {
                            if (levelRooms[RoomRow, RoomCol].DoorExists(0) == false)
                            {
                                levelRooms[RoomRow, RoomCol].CreateDoor(0);
                            }
                        }
                    }
                }
               
                else if (RoomPlacement[x] == 1)
                {
                    //make sure room index is not outside bounds off array
                    if (RoomRow + 1 < m_RoomCount)
                    {
                        //check if the room exists first
                        if (RoomExists(RoomRow + 1, RoomCol) == false)
                        {
                            //if not create a door in the current room leading to the next room
                            levelRooms[RoomRow, RoomCol].CreateDoor(1);
                            //then create a new room with a door leading back to last room
                            RoomRow += 1;
                            levelRooms[RoomRow, RoomCol] = new Rooms(CR_X, CR_Y + 832, RoomRow, RoomCol);
                            levelRooms[RoomRow, RoomCol].CreateDoor(0);
                            m_RemainingRooms--;
                        }
                        else
                        {
                            if (levelRooms[RoomRow, RoomCol].DoorExists(1) == false)
                            {
                                levelRooms[RoomRow, RoomCol].CreateDoor(1);
                            }
                        }
                    }
                }
                
                else if (RoomPlacement[x] == 2)
                {
                    //make sure room index is not outside bounds off array
                    if (RoomCol - 1 >= 0)
                    {
                        //check if the room exists first
                        if (RoomExists(RoomRow, RoomCol - 1) == false)
                        {
                            //if not create a door in the current room leading to the next room
                            levelRooms[RoomRow, RoomCol].CreateDoor(2);
                            //then create a new room with a door leading back to last room
                            RoomCol -= 1;
                            levelRooms[RoomRow, RoomCol] = new Rooms(CR_X - 960, CR_Y, RoomRow, RoomCol);
                            levelRooms[RoomRow, RoomCol].CreateDoor(3);
                            m_RemainingRooms--;
                        }
                        else
                        {
                            if (levelRooms[RoomRow, RoomCol].DoorExists(2) == false)
                            {
                                levelRooms[RoomRow, RoomCol].CreateDoor(2);
                            }
                        }
                    }
                }
                
                else if (RoomPlacement[x] == 2)
                {
                    //make sure room index is not outside bounds off array
                    if (RoomCol + 1 < m_RoomCount)
                    {
                        //check if the room exists first
                        if (RoomExists(RoomRow, RoomCol + 1) == false)
                        {
                            //if not create a door in the current room leading to the next room
                            levelRooms[RoomRow, RoomCol].CreateDoor(3);
                            //then create a new room with a door leading back to last room
                            RoomCol += 1;
                            levelRooms[RoomRow, RoomCol] = new Rooms(CR_X + 960, CR_Y, RoomRow, RoomCol);
                            levelRooms[RoomRow, RoomCol].CreateDoor(2);
                            m_RemainingRooms--;
                        }
                        else
                        {
                            if (levelRooms[RoomRow, RoomCol].DoorExists(3) == false)
                            {
                                levelRooms[RoomRow, RoomCol].CreateDoor(3);
                            }
                        }
                    }
                }
                x++;
            }
            */
            m_DeadEnds = MarkDeadEnds();
            if(m_DeadEnds < 2)
            {
                CreateDeadEnds();
            }

            foreach(Rooms room in levelRooms)
            {
                if(room != null)
                {
                    if (room.GetRoomRow() == m_RoomCount / 2)
                    {
                        if (room.GetRoomCol() != m_RoomCount / 2)
                        {
                            room.GenerateEnemies();
                        }
                    }
                    else
                    {
                        room.GenerateEnemies();
                    }
                }
            }



            Console.Write("end");
        }



        public void CreateLevel()
        {
            //NOT SURE IF I NEED THIS STILL, MAY BE CHANGED OR DELETED ALL TOGETHER
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
        //DO NOT NEED ANYMORE
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
        //OLD CREATE ROOM
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
        //OLD CREATE DEAD END
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
        //NO LONGER USING A ROOM LIST SO THIS WILL BE REMOVED EVENTUALLY
        public List<Rooms> GetRoomList()
        {
            return m_LevelRooms;
        }
        //STILL NEED DOOR LIST FROM ROOMS. THIS FUNCTION WILL NEED TO BE REWRITEN
        public List<Door> GetRoomDoorList(int a_CurrentRoom)
        {
            List<Rooms> LevelRooms = GetRoomList();
            return LevelRooms[a_CurrentRoom].m_RoomDoors;
        }
        //NEEDS TO BE REWRITEN
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
        //NEEDS TO BE REWRITTEN
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



        //START OF NEW FUNCTIONS USING ROOM ARRAY
        //set all the isDeadEnd value to true for all dead ends not counting the first room.
        public int MarkDeadEnds()
        {
            int numDeadEnds = m_DeadEnds;
            foreach(Rooms room in levelRooms)
            {
                if (room != null)
                {
                    if ((room.m_RoomDoors.Count == 1 && room != levelRooms[2, 2]) && room.IsDeadEnd() == false)
                    {
                        room.SetDeadEnd(true);
                        m_DeadEnds++;
                    }
                }
            }
            return m_DeadEnds;
        }

        //create dead ends from in new levelRooms array
        public void CreateDeadEnds()
        {
            int random;
            int currentNumDeadEnds;
            Vector2 currentRoomIndex;
            foreach(Rooms room in levelRooms)
            {
                if(room != null)
                {
                    if(room.IsDeadEnd() == false && room.m_RoomDoors.Count < 4)
                    {
                        
                        currentNumDeadEnds = m_DeadEnds;
                        currentRoomIndex = room.GetRoomIndex();
                        random = Rand.Next(4);
                        if (CanCreateRoom(room, random) == true)
                        {
                            while (currentNumDeadEnds == m_DeadEnds)
                            {
                                CreateRoom(currentRoomIndex, random);
                                m_DeadEnds = MarkDeadEnds();
                                if (m_DeadEnds >= 2)
                                {
                                    return;
                                }
                                random = Rand.Next(4);
                            }
                        }
                    }
                }
            }
        }


        //new create room
        public Vector2 CreateRoom(Vector2 currentRoomindex, int NewRoomPlacement)
        {
            //room size 960 x 832
            //levelRooms[m_RoomCount / 2, m_RoomCount / 2] = new Rooms();

            int CR_X = (int)levelRooms[(int)currentRoomindex.X, (int)currentRoomindex.Y].m_RoomPosition.X;
            int CR_Y = (int)levelRooms[(int)currentRoomindex.X, (int)currentRoomindex.Y].m_RoomPosition.Y;
            int CRI_Row = (int)currentRoomindex.X;
            int CRI_Col = (int)currentRoomindex.Y;

            if (NewRoomPlacement == 0)
            {
                //make sure room index is not outside bounds off array
                if (CRI_Row - 1 >= 0)
                {
                    //check if the room exists first
                    if (RoomExists(CRI_Row - 1, CRI_Col) == false)
                    {
                        //if not create a door in the current room leading to the next room
                        levelRooms[CRI_Row, CRI_Col].CreateDoor(0);
                        //then create a new room with a door leading back to last room
                        CRI_Row -= 1;
                        levelRooms[CRI_Row, CRI_Col] = new Rooms(CR_X, CR_Y - 832, CRI_Row, CRI_Col);
                        levelRooms[CRI_Row, CRI_Col].CreateDoor(1);
                        m_RemainingRooms--;
                        currentRoomindex.X = CRI_Row;
                        currentRoomindex.Y = CRI_Col;
                    }
                    else
                    {
                        if (levelRooms[CRI_Row, CRI_Col].DoorExists(0) == false)
                        {
                            levelRooms[CRI_Row, CRI_Col].CreateDoor(0);
                            levelRooms[CRI_Row - 1, CRI_Col].CreateDoor(1);
                        }
                    }
                }
            }

            else if (NewRoomPlacement == 1)
            {
                //make sure room index is not outside bounds off array
                if (CRI_Row + 1 < m_RoomCount)
                {
                    //check if the room exists first
                    if (RoomExists(CRI_Row + 1, CRI_Col) == false)
                    {
                        //if not create a door in the current room leading to the next room
                        levelRooms[CRI_Row, CRI_Col].CreateDoor(1);
                        //then create a new room with a door leading back to last room
                        CRI_Row += 1;
                        levelRooms[CRI_Row, CRI_Col] = new Rooms(CR_X, CR_Y + 832, CRI_Row, CRI_Col);
                        levelRooms[CRI_Row, CRI_Col].CreateDoor(0);
                        m_RemainingRooms--;
                        currentRoomindex.X = CRI_Row;
                        currentRoomindex.Y = CRI_Col;
                    }
                    else
                    {
                        if (levelRooms[CRI_Row, CRI_Col].DoorExists(1) == false)
                        {
                            levelRooms[CRI_Row, CRI_Col].CreateDoor(1);
                            levelRooms[CRI_Row + 1, CRI_Col].CreateDoor(0);
                        }
                    }
                }
            }

            else if (NewRoomPlacement == 2)
            {
                //make sure room index is not outside bounds off array
                if (CRI_Col - 1 >= 0)
                {
                    //check if the room exists first
                    if (RoomExists(CRI_Row, CRI_Col - 1) == false)
                    {
                        //if not create a door in the current room leading to the next room
                        levelRooms[CRI_Row, CRI_Col].CreateDoor(2);
                        //then create a new room with a door leading back to last room
                        CRI_Col -= 1;
                        levelRooms[CRI_Row, CRI_Col] = new Rooms(CR_X - 960, CR_Y, CRI_Row, CRI_Col);
                        levelRooms[CRI_Row, CRI_Col].CreateDoor(3);
                        m_RemainingRooms--;
                        currentRoomindex.X = CRI_Row;
                        currentRoomindex.Y = CRI_Col;
                    }
                    else
                    {
                        if (levelRooms[CRI_Row, CRI_Col].DoorExists(2) == false)
                        {
                            levelRooms[CRI_Row, CRI_Col].CreateDoor(2);
                            levelRooms[CRI_Row, CRI_Col - 1].CreateDoor(3);
                        }
                    }
                }
            }

            else if (NewRoomPlacement == 3)
            {
                //make sure room index is not outside bounds off array
                if (CRI_Col + 1 < m_RoomCount)
                {
                    //check if the room exists first
                    if (RoomExists(CRI_Row, CRI_Col + 1) == false)
                    {
                        //if not create a door in the current room leading to the next room
                        levelRooms[CRI_Row, CRI_Col].CreateDoor(3);
                        //then create a new room with a door leading back to last room
                        CRI_Col += 1;
                        levelRooms[CRI_Row, CRI_Col] = new Rooms(CR_X + 960, CR_Y, CRI_Row, CRI_Col);
                        levelRooms[CRI_Row, CRI_Col].CreateDoor(2);
                        m_RemainingRooms--;
                        currentRoomindex.X = CRI_Row;
                        currentRoomindex.Y = CRI_Col;

                    }
                    else
                    {
                        if (levelRooms[CRI_Row, CRI_Col].DoorExists(3) == false)
                        {
                            levelRooms[CRI_Row, CRI_Col].CreateDoor(3);
                            levelRooms[CRI_Row, CRI_Col + 1].CreateDoor(2);
                        }
                    }
                }
            }
            MoveRoom(levelRooms[CRI_Row, CRI_Col]);
            return currentRoomindex;
        }



        //check if room is outer edge of the array
        //this needs to be fixed. as of right now it is based off the placement of the door but it must determine if a new room is possible from this location not matter what 
        //the door placement is
        bool CanCreateRoom(Rooms a_Room, int a_Placement)
        {
            if(a_Room.m_RoomDoors.Count == 4)
            {
                return false;
            }
            if(a_Room.m_RoomIndex.X == 0) //&& a_Room.m_RoomIndex.Y == 0) //&& (a_Placement == 0 || a_Placement == 2))
            {
                if((a_Room.m_RoomIndex.Y  == 0 && (a_Placement == 0 || a_Placement == 2)) && a_Room.m_RoomDoors.Count < 2)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (a_Room.m_RoomIndex.X == 0) //&& a_Room.m_RoomIndex.Y == 0) //&& (a_Placement == 0 || a_Placement == 2))
            {
                if ((a_Room.m_RoomIndex.Y == m_RoomCount - 1 && (a_Placement == 0 || a_Placement == 3)) && a_Room.m_RoomDoors.Count < 2)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (a_Room.m_RoomIndex.X == m_RoomCount - 1) //&& a_Room.m_RoomIndex.Y == 0) //&& (a_Placement == 0 || a_Placement == 2))
            {
                if ((a_Room.m_RoomIndex.Y == 0 && (a_Placement == 1 || a_Placement == 2)) && a_Room.m_RoomDoors.Count < 2)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else if (a_Room.m_RoomIndex.X == m_RoomCount - 1) //&& a_Room.m_RoomIndex.Y == 0) //&& (a_Placement == 0 || a_Placement == 2))
            {
                if ((a_Room.m_RoomIndex.Y == m_RoomCount - 1 && (a_Placement == 0 || a_Placement == 2)) && a_Room.m_RoomDoors.Count < 2)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
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

        //const int m_roomWidth = 960;
        //const int m_RoomHeight = 832;
        public void MoveRoom(Rooms a_Room)
        {
            if(a_Room != null)
            {
                return;
            }
            int roomCoord_X;
            int roomCoord_Y;
            roomCoord_X = ((int)a_Room.m_RoomIndex.Y - (m_RoomCount / 2)) * 960;
            roomCoord_Y = ((m_RoomCount / 2) - (int)a_Room.m_RoomIndex.X) * 832;
            a_Room.MoveRoom(roomCoord_X, roomCoord_Y);
            Console.Write("done");
            Console.Write("done");
        }


        //Set the current room index for level created from new createlevel
        public void SetCurrentRoom(int CR_X, int CR_Y)
        {
            m_CurrentRoomIndex_X = CR_X;
            m_CurrentRoomIndex_Y = CR_Y;
        }

        //get current room
        public Rooms GetCurrentRoom()
        {
            return levelRooms[m_CurrentRoomIndex_X, m_CurrentRoomIndex_Y];
        }
    }
}
