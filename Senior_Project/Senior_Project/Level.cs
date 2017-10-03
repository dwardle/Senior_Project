﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


/// <name></name>
/// <author></author>
/// <date></date>

namespace Senior_Project
{
    //
    public class Level
    {
        public enum m_DoorPlacement { Up = 0, Down, Left, Right };

        List<Rooms> m_LevelRooms = new List<Rooms>();
        int m_LevelCount;
        int m_RoomCount = 5;
        //int m_NextRoom; no longer need these two variables
        //int m_CenterRoom;
        int m_RemainingRooms;
        int m_CurrentRoomIndex_X;
        int m_CurrentRoomIndex_Y;
        Random Rand = new Random();
        Rooms[,] levelRooms;
        float m_StatMultiplier = 1f;


        //Testing item//////////////////////////////////////////////////////////////
        public FastShot m_FastShot;
        public HealthUp m_HealthUp;
        ////////////////////////////////////////////////////////////////////////////
        int m_LevelItem_Type = 0;
        


        //new deadends variable
        int m_DeadEnds = 0;

        /// <summary>
        /// 
        /// 
        /// 
        /// 
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


        ////new load content using room array

        /// <name>Level::LoadContent()</name>
        /// <summary>
        /// This function is called too load all the content for the level.
        /// </summary>
        /// <param name="a_Content">Content manager passed from TopDownGame. Used to set all textures for all objects that need to be drawn</param>
        /// <param name="temp"></param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void LoadContent(ContentManager a_Content, int temp)
        {
            foreach(Rooms room in levelRooms)
            {
                if(room != null)
                {
                    room.LoadContent(a_Content);
                    if (room.IsItemRoom())
                    {
                        if (m_LevelItem_Type == 0)
                        {
                            m_FastShot.LoadContent(a_Content);
                        }
                        else if (m_LevelItem_Type == 1)
                        {
                            m_HealthUp.LoadContent(a_Content);
                        }
                    }
                }
                
            }
            

        }

        //new Draw using room array
        //

        /// <name>Level::Draw()</name>
        /// <summary>
        /// This function is called to draw all level content to the screen.
        /// </summary>
        /// <param name="a_SpriteBatch">SpriteBatch object passed from TopDownGame. Contains all sprites to be drawn</param>
        /// <param name="temp"></param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void Draw(SpriteBatch a_SpriteBatch, int temp)
        {
            foreach(Rooms room in levelRooms)
            {
                if(room != null)
                {
                    room.Draw(a_SpriteBatch);
                    if(room.IsItemRoom())
                    {
                        if(m_LevelItem_Type == 0)
                        {
                            m_FastShot.Draw(a_SpriteBatch);
                        }
                        else if(m_LevelItem_Type == 1)
                        {
                            m_HealthUp.Draw(a_SpriteBatch);
                        }
                    }
                }
               // m_FastShot.Draw(a_SpriteBatch);
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
        /// <date></date>
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
        

        /// <name>Level::CreateLevel1()</name>
        /// <summary>
        /// This function is called to create a randomly generated level. it will first create all the rooms for the level. after initial room creation
        /// it will check if the level has at least 2 dead ends. 1 dead end for an item and 1 for the boss. if there is not 2 dead ends it will create
        /// new rooms until there is 2 dead ends. after all rooms are created it will then place an item in one room and a boss in another. then generate 
        /// enemies for all rooms except for the item room, boss room, and the first room.
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date></date>
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
            PlaceItem(1);
            //boss never gets drawn because this is called before generate enemies and the boss gets over written
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
                    //else if(room.IsDeadEnd() == true && room.IsItemRoom() == false)
                    //{
                    //    room.CreateBoss();
                    //}
                }
            }

            foreach (Door d in levelRooms[m_RoomCount / 2, m_RoomCount / 2].GetDoorList())
            {
                d.SetIsOpen(true);
            }

            Console.Write("end");
        }

        //START OF NEW FUNCTIONS USING ROOM ARRAY
        //set all the isDeadEnd value to true for all dead ends not counting the first room.

        

        /// <name>Level::MarkDeadEnds()</name>
        /// <summary>
        /// This function is called to search through the levelRooms array and set the IsDeadEnd value to true for any room
        /// that is a dead end. a dead end is defined as a room that only contains 1 door.
        /// </summary>
        /// <returns>The number of dead ends in the levelRooms array</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
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

        //create dead ends from in new levelRooms array


        /// <name>Level::CreateDeadEnds()</name>
        /// <summary>
        /// This function is called when dead ends need to be created in the level. if a level does not have at least 2 dead ends
        /// this function will create new rooms until the number of dead ends is at least 2
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date></date>
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
        /// <date></date>
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

        /// <name>Level::CanCreateRoom()</name>
        /// <summary>
        /// Function checks to see if a room can be created in the specified location relative to a_Room. 
        /// </summary>
        /// <param name="a_Room">Room that a new room is to be connected to if a new room can be created</param>
        /// <param name="a_Placement">location the new room will be placed in relation to a_Room</param>
        /// <returns>true if a room can be created, false otherwise</returns>
        /// <author>Douglas Wardle</author>
        /// <date></date>
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


        /// <name></name>
        /// <author></author>
        /// <date></date>

       //not using anymore
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

        /// <name></name>
        /// <author></author>
        /// <date></date>

        /// <name>Level::MoveRoom()</name>
        /// <summary>
        /// Function is called to move a specified room to the proper game window coordiantes. function first calculates the correct coordiantes
        /// then passes them to the Rooms::MoveRoom() for a_Room.
        /// </summary>
        /// <param name="a_Room"></param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void MoveRoom(Rooms a_Room)
        {
            //if (a_Room != null)
            //{
            //    return;
            //}
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


        //new function to place item in a room
        public void PlaceItem()
        {
            Rooms ItemRoom = FindDeadEnd();
            Vector2 ItemPostition = ItemRoom.GetRoomPosition();
            ItemPostition.X = ItemPostition.X + (960 / 2);
            ItemPostition.Y = ItemPostition.Y + (832 / 2);
            m_FastShot.SetPosition(ItemPostition);
            ItemRoom.SetAsItemRoom();
            //m_FastShot.SetUsed(true);
        }

        public void PlaceItem(int a_ItemType)
        {
            Rooms ItemRoom = FindDeadEnd();
            Vector2 ItemPostition = ItemRoom.GetRoomPosition();
            ItemPostition.X = ItemPostition.X + (960 / 2);
            ItemPostition.Y = ItemPostition.Y + (832 / 2);

            if(a_ItemType == 0)
            {
                m_FastShot.SetPosition(ItemPostition);
            }
            else if(a_ItemType == 1)
            {
                m_HealthUp.SetPosition(ItemPostition);
                m_LevelItem_Type = 1;
            }

            
            ItemRoom.SetAsItemRoom();
            //m_FastShot.SetUsed(true);
        }

        ////new find dead end function
        //public Rooms FindDeadEnd(int a_Row, int a_Col)
        //{
        //    foreach(Rooms room in levelRooms)
        //    {
        //        if(room != null && room.IsDeadEnd() == true  && room.GetRoomRow() >= a_Row && room.GetRoomCol() >= a_Col)
        //        {
        //            return room;
        //        }
        //    }
        //    return null;
        //}

        public Rooms FindDeadEnd(Rooms a_StartingPoint)
        {
            foreach (Rooms room in levelRooms)
            {
                if (room != null && room.IsDeadEnd() == true)// && room.GetRoomRow() != a_StartingPoint.GetRoomRow() && room.GetRoomCol() != a_StartingPoint.GetRoomCol())
                {
                    if(room.GetRoomRow() != a_StartingPoint.GetRoomRow() || room.GetRoomCol() != a_StartingPoint.GetRoomCol())
                    {
                        return room;
                    }

                    //return room;
                }
            }
            return null;
        }

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

        public void DefeatBoss(Boss a_LevelBoss)
        {
            a_LevelBoss = null;
        }

        public int GetLevelCount()
        {
            return m_LevelCount;
        }

        public int GetItemType()
        {
            return m_LevelItem_Type;
        }
    }
}
