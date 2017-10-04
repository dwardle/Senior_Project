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


//Have done comments

namespace Senior_Project
{
    //
    public class Level
    {
        public enum m_DoorPlacement { Up = 0, Down, Left, Right };

        List<Rooms> m_LevelRooms = new List<Rooms>();
        int m_LevelCount;
        int m_RoomCount = 5;
        int m_RemainingRooms;
        int m_CurrentRoomIndex_X;
        int m_CurrentRoomIndex_Y;
        Random Rand = new Random();
        Rooms[,] levelRooms;
        float m_StatMultiplier = 1f;


        public FastShot m_FastShot;
        public HealthUp m_HealthUp;
        int m_LevelItem_Type = 0;
        int m_DeadEnds = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="a_LevelCount"></param>
        public Level(int a_LevelCount)
        {
            m_RoomCount = 3 + (a_LevelCount * 2);
            m_LevelCount = a_LevelCount;
            m_RemainingRooms = m_RoomCount;
            levelRooms = new Rooms[m_RemainingRooms, m_RemainingRooms];
            m_CurrentRoomIndex_X = m_RoomCount / 2;
            m_CurrentRoomIndex_Y = m_RoomCount / 2;
            m_FastShot = new FastShot();
            m_HealthUp = new HealthUp();
            CreateLevel1();
        }


        /// <name>Level::LoadContent()</name>
        /// <summary>
        /// This function is called too load all the content for the level.
        /// </summary>
        /// <param name="a_Content">Content manager passed from TopDownGame. Used to set all textures for all objects that need to be drawn</param>
        /// <param name="temp"></param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void LoadContent(ContentManager a_Content, int temp)
        {
            foreach(Rooms room in levelRooms)
            {
                if(room != null)
                {
                    room.LoadContent(a_Content);
                    if (room.IsItemRoom())
                    {
                        if (m_LevelItem_Type == 1)
                        {
                            m_FastShot.LoadContent(a_Content);
                        }
                        else if (m_LevelItem_Type == 2)
                        {
                            m_HealthUp.LoadContent(a_Content);
                        }
                        else
                        {
                            m_HealthUp.LoadContent(a_Content);
                        }
                    }
                }
                
            }
            

        }


        /// <name>Level::Draw()</name>
        /// <summary>
        /// This function is called to draw all level content to the screen.
        /// </summary>
        /// <param name="a_SpriteBatch">SpriteBatch object passed from TopDownGame. Contains all sprites to be drawn</param>
        /// <param name="temp"></param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void Draw(SpriteBatch a_SpriteBatch, int temp)
        {
            foreach(Rooms room in levelRooms)
            {
                if(room != null)
                {
                    room.Draw(a_SpriteBatch);
                    if(room.IsItemRoom())
                    {
                        if(m_LevelItem_Type == 1)
                        {
                            m_FastShot.Draw(a_SpriteBatch);
                        }
                        else if(m_LevelItem_Type == 2)
                        {
                            m_HealthUp.Draw(a_SpriteBatch);
                        }
                        else
                        {
                            m_HealthUp.Draw(a_SpriteBatch);
                        }
                    }
                }
            }
        }

        /// <name>Level::RoomExists()</name>
        /// <summary>
        /// This function is called to check if a room exisits at the specified array location.
        /// checks if the room at the row and column of the array is a null room.
        /// </summary>
        /// <param name="Row">Row index of the room to be checked</param>
        /// <param name="Col">Column index of the room to be checked</param>
        /// <returns>true if the room at the specified location is not null, false otherwise</returns>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
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



        /// <name>Level::CreateLevel1()</name>
        /// <summary>
        /// This function is called to create a randomly generated level. it will first create all the rooms for the level. after initial room creation
        /// it will check if the level has at least 2 dead ends. 1 dead end for an item and 1 for the boss. if there is not 2 dead ends it will create
        /// new rooms until there is 2 dead ends. after all rooms are created it will then place an item in one room and a boss in another. then generate 
        /// enemies for all rooms except for the item room, boss room, and the first room.
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void CreateLevel1()
        {
            //room size 960 x 832
            Vector2 CurrentRoomIndex;
            int random;
            levelRooms[m_RoomCount / 2, m_RoomCount / 2] = new Rooms(m_RoomCount);
            CurrentRoomIndex = levelRooms[m_RoomCount / 2, m_RoomCount / 2].m_RoomIndex;
            
            while(m_RemainingRooms > 1)
            {
                random = Rand.Next(4);
                CurrentRoomIndex = CreateRoom(CurrentRoomIndex, random);
            }
            
            Console.Write("done");
            
            m_DeadEnds = MarkDeadEnds();
            if(m_DeadEnds < 2)
            {
                CreateDeadEnds();
            }
            PlaceItem(m_LevelCount);
            CreateBossRoom();
            foreach (Rooms room in levelRooms)
            {
                if(room != null)
                {
                    if (room.GetRoomRow() == m_RoomCount / 2)
                    {
                        if (room.GetRoomCol() != m_RoomCount / 2 && room.IsDeadEnd() == false)
                        {
                            room.GenerateEnemies();
                        }
                    }
                    else if(room.IsDeadEnd() == false)
                    {
                        room.GenerateEnemies();
                    }
                }
            }

            foreach (Door d in levelRooms[m_RoomCount / 2, m_RoomCount / 2].GetDoorList())
            {
                d.SetIsOpen(true);
            }

            Console.Write("end");
        }


        /// <name>Level::MarkDeadEnds()</name>
        /// <summary>
        /// This function is called to search through the levelRooms array and set the IsDeadEnd value to true for any room
        /// that is a dead end. a dead end is defined as a room that only contains 1 door.
        /// </summary>
        /// <returns>The number of dead ends in the levelRooms array</returns>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public int MarkDeadEnds()
        {
            int numDeadEnds = m_DeadEnds;
            foreach(Rooms room in levelRooms)
            {
                if (room != null)
                {
                    if ((room.m_RoomDoors.Count == 1 && room != levelRooms[m_RoomCount/2, m_RoomCount/2]) && room.IsDeadEnd() == false)
                    {
                        room.SetDeadEnd(true);
                        m_DeadEnds++;
                    }
                }
            }
            return m_DeadEnds;
        }

        /// <name>Level::CreateDeadEnds()</name>
        /// <summary>
        /// This function is called when dead ends need to be created in the level. if a level does not have at least 2 dead ends
        /// this function will create new rooms until the number of dead ends is at least 2
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
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

        /// <name>Level::CreateRoom()</name>
        /// <summary>
        /// This function is called to create a new room at a specified location in relation to the current room the levelRooms array. it will first check if
        /// the room trying to be created already exisits. if the room already exists it will not create a new room, it will instead create a door to the
        /// already exisiting room. if it does not exisist, a room will be created in relation to the current room based off of the placement
        /// </summary>
        /// <param name="currentRoomindex">Current room that a new room will be connected to</param>
        /// <param name="NewRoomPlacement">Placement of the new room in relation to the current room</param>
        /// <returns>Returns a Vector2 with the index of the room connected to the current room</returns>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public Vector2 CreateRoom(Vector2 currentRoomindex, int NewRoomPlacement)
        {
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
                        //then create a new room with a door leading back to last room
                        levelRooms[CRI_Row, CRI_Col].CreateDoor(0);
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
                if (CRI_Row + 1 < m_RoomCount)
                {
                    if (RoomExists(CRI_Row + 1, CRI_Col) == false)
                    {
                        levelRooms[CRI_Row, CRI_Col].CreateDoor(1);
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
                if (CRI_Col - 1 >= 0)
                {
                    if (RoomExists(CRI_Row, CRI_Col - 1) == false)
                    {
                        levelRooms[CRI_Row, CRI_Col].CreateDoor(2);
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
                if (CRI_Col + 1 < m_RoomCount)
                {
                    if (RoomExists(CRI_Row, CRI_Col + 1) == false)
                    {
                        levelRooms[CRI_Row, CRI_Col].CreateDoor(3);
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

        /// <name>Level::CanCreateRoom()</name>
        /// <summary>
        /// Function checks to see if a room can be created in the specified location relative to a_Room. 
        /// </summary>
        /// <param name="a_Room">Room that a new room is to be connected to if a new room can be created</param>
        /// <param name="a_Placement">location the new room will be placed in relation to a_Room</param>
        /// <returns>true if a room can be created, false otherwise</returns>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        bool CanCreateRoom(Rooms a_Room, int a_Placement)
        {
            if(a_Room.m_RoomDoors.Count == 4)
            {
                return false;
            }
            //Check to make sure that that a room is not placed outside th bounds of the level rooms array
            if(a_Room.m_RoomIndex.X == 0) 
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
            else if (a_Room.m_RoomIndex.X == 0) 
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
            else if (a_Room.m_RoomIndex.X == m_RoomCount - 1) 
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
            else if (a_Room.m_RoomIndex.X == m_RoomCount - 1) 
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

        /// <name>Level::MoveRoom()</name>
        /// <summary>
        /// Function is called to move a specified room to the proper game window coordiantes. function first calculates the correct coordiantes
        /// then passes them to the Rooms::MoveRoom() for a_Room.
        /// </summary>
        /// <param name="a_Room"></param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void MoveRoom(Rooms a_Room)
        {
            if (a_Room != null)
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



        /// <name>Level::SetCurrentRoom()</name>
        /// <summary>
        /// Function takes an x and y value that represent the room index of the current room
        /// </summary>
        /// <param name="CR_X">Row index of current room</param>
        /// <param name="CR_Y">Column index of current room</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void SetCurrentRoom(int CR_X, int CR_Y)
        {
            m_CurrentRoomIndex_X = CR_X;
            m_CurrentRoomIndex_Y = CR_Y;
        }

        /// <summary>
        /// Accesses the current room that the player is in
        /// </summary>
        /// <returns>Returns room object for the current room</returns>
        public Rooms GetCurrentRoom()
        {
            return levelRooms[m_CurrentRoomIndex_X, m_CurrentRoomIndex_Y];
        }

        
        /// <name>Level::PlaceItem()</name>
        /// <summary>
        /// Function places an item in a room and then sets that room as an item room
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void PlaceItem()
        {
            Rooms ItemRoom = FindDeadEnd();
            Vector2 ItemPostition = ItemRoom.GetRoomPosition();
            ItemPostition.X = ItemPostition.X + (960 / 2);
            ItemPostition.Y = ItemPostition.Y + (832 / 2);
            m_FastShot.SetPosition(ItemPostition);
            ItemRoom.SetAsItemRoom();
        }


        /// <name>Level::PlaceItem</name>
        /// <summary>
        /// Function places a specific item in a room and then sets the room as an item room
        /// </summary>
        /// <param name="a_ItemType">Int to represent a specific item</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void PlaceItem(int a_ItemType)
        {
            Rooms ItemRoom = FindDeadEnd();
            Vector2 ItemPostition = ItemRoom.GetRoomPosition();
            ItemPostition.X = ItemPostition.X + (960 / 2);
            ItemPostition.Y = ItemPostition.Y + (832 / 2);

            if(a_ItemType == 1)
            {
                m_FastShot.SetPosition(ItemPostition);
                m_LevelItem_Type = 1;
            }
            else if(a_ItemType == 2)
            {
                m_HealthUp.SetPosition(ItemPostition);
                m_LevelItem_Type = 2;
            }
            else
            {
                m_HealthUp.SetPosition(ItemPostition);
                m_LevelItem_Type = 2;
            }
            ItemRoom.SetAsItemRoom();
        }

        /// <name>Level::FindDeadEnd()</name>
        /// <summary>
        /// Function takes a rooms object as a starting point and searches the levelRooms array for the next dead end after that room.
        /// </summary>
        /// <param name="a_StartingPoint"></param>
        /// <returns>If the function finds a dead end room it returns that room, if not return null</returns>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public Rooms FindDeadEnd(Rooms a_StartingPoint)
        {
            foreach (Rooms room in levelRooms)
            {
                if (room != null && room.IsDeadEnd() == true)
                {
                    if (room.GetRoomRow() != a_StartingPoint.GetRoomRow() || room.GetRoomCol() != a_StartingPoint.GetRoomCol())
                    {
                        return room;
                    }
                }
            }
            return null;
        }

        
        /// <name>Level::FindDeadEnd()</name>
        /// <summary>
        /// Function finds the first instance of a room that is a dead end in the levelRooms array
        /// </summary>
        /// <returns>Returns the first dead end room in the levelRooms array if one is found. if no dead ends are found, returns null</returns>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public Rooms FindDeadEnd()
        {
            foreach (Rooms room in levelRooms)
            {
                if (room != null && room.IsDeadEnd() == true)
                {
                    return room;
                }
            }
            return null;
        }

        /// <name>Level::CreateBossRoom()</name>
        /// <summary>
        /// Function will attempt to find a dead end room that is not an item room and create a boss room. if a dead end is found
        /// the function will create a Boss object and place it in the room. 
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void CreateBossRoom()
        {
            Rooms deadEnd = FindDeadEnd();
            if(deadEnd.IsItemRoom())
            {
                deadEnd = FindDeadEnd(deadEnd);
                deadEnd.SetAsBossRoom();
                List<Door> bossRoomDoors = deadEnd.GetDoorList();
                deadEnd.CreateBoss1(m_LevelCount);
            }
            else
            {
                deadEnd.IsBossRoom();
                List<Door> bossRoomDoors = deadEnd.GetDoorList();
            }
            Console.Write("Done");
        }

        /// <name>Level::DefeatBoss()</name>
        /// <summary>
        /// Function is called when the player defeats the level boss. it will accept a Boss object and set it equal to null
        /// </summary>
        /// <param name="a_LevelBoss">A boss object to pass the current level boss too</param>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public void DefeatBoss(Boss a_LevelBoss)
        {
            a_LevelBoss = null;
        } 

        /// <name>Level::GetLevelCount()</name>
        /// <summary>
        /// Accessor function for the m_LevelCount variable
        /// </summary>
        /// <returns>returns level objects m_LevelCount variable</returns>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public int GetLevelCount()
        {
            return m_LevelCount;
        }

        /// <name>Level::GetItemType()</name>
        /// <summary>
        /// Function accesses Level objects m_LevelItem_Type variable
        /// </summary>
        /// <returns>Level Objects m_LevelItem_Type integer</returns>
        /// <author>Douglas Wardle</author>
        /// <date>10/4/2017</date>
        public int GetItemType()
        {
            return m_LevelItem_Type;
        }
    }
}
