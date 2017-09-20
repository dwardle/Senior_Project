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
    
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class TopDownGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager m_Graphics;
        SpriteBatch m_SpriteBatch;
        public List<Rooms> m_RoomList = new List<Rooms>();
        //public Rooms m_Room = new Rooms();
        //public Rooms m_Room2 = new Rooms(0, -832);
        //RoomFloor m_Floor = new RoomFloor();
        //public Door m_Door = new Door();
        Player m_MainPlayer;
        Camera m_Camera;
        //Level m_Level_1 = new Level(1);
        Level m_Level_test = new Level(1);
        const int m_roomWidth = 960;
        const int m_RoomHeight = 832;
        public int LevelCount = 1;

        Texture2D pixel;

        //random number generator for enemy movement delays
        public Random m_MovementRand = new Random();


        public TopDownGame()
        {
            //960 x 832
            //room size
            m_Graphics = new GraphicsDeviceManager(this);
            m_Graphics.IsFullScreen = false;/////////
            m_Graphics.PreferredBackBufferWidth = m_roomWidth;//////
            m_Graphics.PreferredBackBufferHeight = m_RoomHeight;/////
            //m_MainPlayer = new Player(m_Level_1);
            m_MainPlayer = new Player(m_Level_test);
            this.Window.Title = "batdoug";
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;
            //m_RoomList.Add(m_Room);
           // m_RoomList.Add(m_Room2);
           // m_Room2.GenerateDoors(0, 0);
           // m_Room2.GenerateDoors(2, 2);
            //m_Room2.GenerateDoors(2, 3);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            m_Camera = new Camera(GraphicsDevice.Viewport);
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            m_SpriteBatch = new SpriteBatch(GraphicsDevice);

            ///////commented out for testing of new load content and draw
            ///m_Level_1.LoadContent(Content);
            ///////////////
            m_Level_test.LoadContent(Content, 1);


            //m_Room.LoadContent(Content);
            //m_Room2.LoadContent(Content);
            //m_Floor.LoadContent(Content);
            //m_Door.LoadContent(Content);
            m_MainPlayer.LoadContent(Content);


            pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White }); // so that we can draw whatever color we want on top of it
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="a_GameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime a_GameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            //m_MainPlayer.Update(a_GameTime, m_Level_1);  //this line now works with levelrooms array but for sake of testing I will commenet it out and use next line
            m_MainPlayer.Update(a_GameTime, m_Level_test);
            ///new update logic using rooms from level array///
            Rooms CurrentRoom = m_Level_test.GetCurrentRoom();

            //List<Rooms> CurrenRoomList = new List<Rooms>();
            //CurrenRoomList = m_Level_1.GetRoomList();

            List<Enemy> CurrentEnemies = new List<Enemy>();
            CurrentEnemies = CurrentRoom.GetEnemyList();
            int CurrentEnemyType = CurrentRoom.GetEnemyType();
            if (CurrentEnemyType == 1)
            {
                foreach (EnemyNoGun en in CurrentEnemies)
                {
                    if (en.m_IsAlive == false)
                    {
                        CurrentEnemies.Remove(en);
                        break;
                    }
                    else
                    {
                        //the two random values must come from the TopDownGame class because if I try to generate a randome value from
                        //within the Enemy class, every enemy will get the same random values for m_MoveCount and m_MoveDelay
                        //this is because of how the Random Class works. getting the random values from the same Random object 
                        //ensures that all values are different
                        en.Update(a_GameTime, m_MainPlayer, CurrentEnemies, m_MovementRand.Next(0, 300), m_MovementRand.Next(90, 100));//, CurrenRoomList[m_MainPlayer.RoomIndex]);
                    }
                }
            }
            else if (CurrentEnemyType == 2)
            {

            }
            else if (CurrentEnemyType == 2)
            {

            }

            foreach (Enemy en in CurrentEnemies)
            {
                foreach (Bullet b in m_MainPlayer.m_BulletList)
                {
                    if (b.m_BoundingBox.Intersects(en.m_BoundingBox))
                    {
                        en.TakeDamage(m_MainPlayer.m_Damage);
                        b.m_IsVisible = false;
                    }
                }
            }

            //new logic for going threw doors
                //Go threw the top door
            if (CurrentRoom.DoorExists((int)Level.m_DoorPlacement.Up))
            {
                int TopDoor = CurrentRoom.FindDoor((int)Level.m_DoorPlacement.Up);
                if (CurrentRoom.m_RoomDoors[TopDoor].m_BoundingBox.Intersects(m_MainPlayer.m_BoundingBox))
                {
                    if (CurrentRoom.RoomClear() == false)
                    {
                        CurrentRoom.DeactivateEnemies();
                    }
                    //m_MainPlayer.m_PlayerPosition.Y = (int)CurrentRoom.m_RoomPosition.Y + 704;
                    //m_MainPlayer.m_PlayerPosition.X = (int)CurrentRoom.m_RoomPosition.X + 480;
                    m_Level_test.SetCurrentRoom(CurrentRoom.GetRoomRow() - 1, CurrentRoom.GetRoomCol());
                    CurrentRoom = m_Level_test.GetCurrentRoom();
                    //m_MainPlayer.RoomIndex = CurrentRoom.m_RoomDoors[TopDoor].m_nextRoom;
                    m_Camera.Update(a_GameTime, (int)CurrentRoom.m_RoomPosition.X, (int)CurrentRoom.m_RoomPosition.Y);
                    m_MainPlayer.m_PlayerPosition.Y = (int)CurrentRoom.m_RoomPosition.Y + 704;
                    m_MainPlayer.m_PlayerPosition.X = (int)CurrentRoom.m_RoomPosition.X + 480;
                    CurrentRoom.ActivateEnemies();
                }
            }

            //go threw bottom door
            if (CurrentRoom.DoorExists((int)Level.m_DoorPlacement.Down))
            {
                int DownDoor = CurrentRoom.FindDoor((int)Level.m_DoorPlacement.Down);
                if (CurrentRoom.m_RoomDoors[DownDoor].m_BoundingBox.Intersects(m_MainPlayer.m_BoundingBox))
                {
                    if (CurrentRoom.RoomClear() == false)
                    {
                        CurrentRoom.DeactivateEnemies();
                    }
                    //CurrentRoom.DeactivateEnemies();
                    //m_MainPlayer.m_PlayerPosition.Y = (int)CurrentRoom.m_RoomPosition.Y + 128;
                    //m_MainPlayer.m_PlayerPosition.X = (int)CurrentRoom.m_RoomPosition.X + 480;
                    m_Level_test.SetCurrentRoom(CurrentRoom.GetRoomRow() + 1, CurrentRoom.GetRoomCol());
                    CurrentRoom = m_Level_test.GetCurrentRoom();

                    //m_MainPlayer.RoomIndex = CurrentRoom.m_RoomDoors[DownDoor].m_nextRoom;
                    m_Camera.Update(a_GameTime, (int)CurrentRoom.m_RoomPosition.X, (int)CurrentRoom.m_RoomPosition.Y);
                    m_MainPlayer.m_PlayerPosition.Y = (int)CurrentRoom.m_RoomPosition.Y + 128;
                    m_MainPlayer.m_PlayerPosition.X = (int)CurrentRoom.m_RoomPosition.X + 480;
                    CurrentRoom.ActivateEnemies();
                }
            }

            //go threw left door
            if (CurrentRoom.DoorExists((int)Level.m_DoorPlacement.Left))
            {
                int LeftDoor = CurrentRoom.FindDoor((int)Level.m_DoorPlacement.Left);
                if (CurrentRoom.m_RoomDoors[LeftDoor].m_BoundingBox.Intersects(m_MainPlayer.m_BoundingBox))
                {
                    if (CurrentRoom.RoomClear() == false)
                    {
                        CurrentRoom.DeactivateEnemies();
                    }
                    //CurrentRoom.DeactivateEnemies();
                    //m_MainPlayer.m_PlayerPosition.Y = (int)CurrentRoom.m_RoomPosition.Y + 416;
                    //m_MainPlayer.m_PlayerPosition.X = (int)CurrentRoom.m_RoomPosition.X + 832;
                    m_Level_test.SetCurrentRoom(CurrentRoom.GetRoomRow(), CurrentRoom.GetRoomCol() - 1);
                    CurrentRoom = m_Level_test.GetCurrentRoom();

                    //m_MainPlayer.RoomIndex = CurrentRoom.m_RoomDoors[LeftDoor].m_nextRoom;
                    m_Camera.Update(a_GameTime, (int)CurrentRoom.m_RoomPosition.X, (int)CurrentRoom.m_RoomPosition.Y);
                    m_MainPlayer.m_PlayerPosition.Y = (int)CurrentRoom.m_RoomPosition.Y + 416;
                    m_MainPlayer.m_PlayerPosition.X = (int)CurrentRoom.m_RoomPosition.X + 832;
                    CurrentRoom.ActivateEnemies();
                }
            }

            //go threw right door
            if (CurrentRoom.DoorExists((int)Level.m_DoorPlacement.Right))
            {

                int RightDoor = CurrentRoom.FindDoor((int)Level.m_DoorPlacement.Right);
                if (CurrentRoom.m_RoomDoors[RightDoor].m_BoundingBox.Intersects(m_MainPlayer.m_BoundingBox))
                {
                    if (CurrentRoom.RoomClear() == false)
                    {
                        CurrentRoom.DeactivateEnemies();
                    }
                    //CurrentRoom.DeactivateEnemies();
                    //m_MainPlayer.m_PlayerPosition.Y = (int)CurrentRoom.m_RoomPosition.Y + 416;
                    //m_MainPlayer.m_PlayerPosition.X = (int)CurrentRoom.m_RoomPosition.X + 128;
                    m_Level_test.SetCurrentRoom(CurrentRoom.GetRoomRow(), CurrentRoom.GetRoomCol() + 1);
                    CurrentRoom = m_Level_test.GetCurrentRoom();
                    //m_MainPlayer.RoomIndex = CurrentRoom.m_RoomDoors[RightDoor].m_nextRoom;
                    m_Camera.Update(a_GameTime, (int)CurrentRoom.m_RoomPosition.X, (int)CurrentRoom.m_RoomPosition.Y);
                    m_MainPlayer.m_PlayerPosition.Y = (int)CurrentRoom.m_RoomPosition.Y + 416;
                    m_MainPlayer.m_PlayerPosition.X = (int)CurrentRoom.m_RoomPosition.X + 128;
                    CurrentRoom.ActivateEnemies();
                }
            }

            ////////old logic for going threw doors///////////
            //Go threw the top door
            //if (CurrentRoom.DoorExists((int)Level.m_DoorPlacement.Up))
            //{
            //    int TopDoor = CurrentRoom.FindDoor((int)Level.m_DoorPlacement.Up);
            //    if (CurrentRoom.m_RoomDoors[TopDoor].m_BoundingBox.Intersects(m_MainPlayer.m_BoundingBox))
            //    {
            //        if (CurrentRoom.RoomClear() == false)
            //        {
            //            CurrentRoom.DeactivateEnemies();
            //        }
            //        m_MainPlayer.RoomIndex = CurrentRoom.m_RoomDoors[TopDoor].m_nextRoom;
            //        m_Camera.Update(a_GameTime, (int)CurrentRoom.m_RoomPosition.X, (int)CurrentRoom.m_RoomPosition.Y);
            //        m_MainPlayer.m_PlayerPosition.Y = (int)CurrentRoom.m_RoomPosition.Y + 704;
            //        m_MainPlayer.m_PlayerPosition.X = (int)CurrentRoom.m_RoomPosition.X + 480;
            //        CurrentRoom.ActivateEnemies();
            //    }
            //}

            ////go threw bottom door
            //if (CurrentRoom.DoorExists((int)Level.m_DoorPlacement.Down))
            //{
            //    int DownDoor = CurrentRoom.FindDoor((int)Level.m_DoorPlacement.Down);
            //    if (CurrentRoom.m_RoomDoors[DownDoor].m_BoundingBox.Intersects(m_MainPlayer.m_BoundingBox))
            //    {
            //        if (CurrentRoom.RoomClear() == false)
            //        {
            //            CurrentRoom.DeactivateEnemies();
            //        }
            //        //CurrentRoom.DeactivateEnemies();
            //        m_MainPlayer.RoomIndex = CurrentRoom.m_RoomDoors[DownDoor].m_nextRoom;
            //        m_Camera.Update(a_GameTime, (int)CurrentRoom.m_RoomPosition.X, (int)CurrentRoom.m_RoomPosition.Y);
            //        m_MainPlayer.m_PlayerPosition.Y = (int)CurrentRoom.m_RoomPosition.Y + 128;
            //        m_MainPlayer.m_PlayerPosition.X = (int)CurrentRoom.m_RoomPosition.X + 480;
            //        CurrentRoom.ActivateEnemies();
            //    }
            //}

            ////go threw left door
            //if (CurrentRoom.DoorExists((int)Level.m_DoorPlacement.Left))
            //{
            //    int LeftDoor = CurrentRoom.FindDoor((int)Level.m_DoorPlacement.Left);
            //    if (CurrentRoom.m_RoomDoors[LeftDoor].m_BoundingBox.Intersects(m_MainPlayer.m_BoundingBox))
            //    {
            //        if (CurrentRoom.RoomClear() == false)
            //        {
            //            CurrentRoom.DeactivateEnemies();
            //        }
            //        //CurrentRoom.DeactivateEnemies();
            //        m_MainPlayer.RoomIndex = CurrentRoom.m_RoomDoors[LeftDoor].m_nextRoom;
            //        m_Camera.Update(a_GameTime, (int)CurrentRoom.m_RoomPosition.X, (int)CurrentRoom.m_RoomPosition.Y);
            //        m_MainPlayer.m_PlayerPosition.Y = (int)CurrentRoom.m_RoomPosition.Y + 416;
            //        m_MainPlayer.m_PlayerPosition.X = (int)CurrentRoom.m_RoomPosition.X + 832;
            //        CurrentRoom.ActivateEnemies();
            //    }
            //}

            ////go threw right door
            //if (CurrentRoom.DoorExists((int)Level.m_DoorPlacement.Right))
            //{

            //    int RightDoor = CurrentRoom.FindDoor((int)Level.m_DoorPlacement.Right);
            //    if (CurrentRoom.m_RoomDoors[RightDoor].m_BoundingBox.Intersects(m_MainPlayer.m_BoundingBox))
            //    {
            //        if (CurrentRoom.RoomClear() == false)
            //        {
            //            CurrentRoom.DeactivateEnemies();
            //        }
            //        //CurrentRoom.DeactivateEnemies();
            //        m_MainPlayer.RoomIndex = CurrentRoom.m_RoomDoors[RightDoor].m_nextRoom;
            //        m_Camera.Update(a_GameTime, (int)CurrentRoom.m_RoomPosition.X, (int)CurrentRoom.m_RoomPosition.Y);
            //        m_MainPlayer.m_PlayerPosition.Y = (int)CurrentRoom.m_RoomPosition.Y + 416;
            //        m_MainPlayer.m_PlayerPosition.X = (int)CurrentRoom.m_RoomPosition.X + 128;
            //        CurrentRoom.ActivateEnemies();
            //    }
            //}
            ////////////////////End old logic for going threw doors/////////////////////////////////////////////////////////////////////////////////////////////
            base.Update(a_GameTime);
        }


        //////////old update logic. still uses room list instead off array of rooms
        //List<Rooms> CurrenRoomList = new List<Rooms>();
        //    CurrenRoomList = m_Level_1.GetRoomList();
        //    List<Enemy> CurrentEnemies = new List<Enemy>();
        //    CurrentEnemies = CurrenRoomList[m_MainPlayer.RoomIndex].GetEnemyList();
        //    int CurrentEnemyType = CurrenRoomList[m_MainPlayer.RoomIndex].GetEnemyType();
        //    if(CurrentEnemyType == 1)
        //    {
        //        foreach(EnemyNoGun en in CurrentEnemies)
        //        {
        //            if (en.m_IsAlive == false)
        //            {
        //                CurrentEnemies.Remove(en);
        //                break;
        //            }
        //            else
        //            {
        //                //the two random values must come from the TopDownGame class because if I try to generate a randome value from
        //                //within the Enemy class, every enemy will get the same random values for m_MoveCount and m_MoveDelay
        //                //this is because of how the Random Class works. getting the random values from the same Random object 
        //                //ensures that all values are different
        //                en.Update(a_GameTime, m_MainPlayer, CurrentEnemies, m_MovementRand.Next(0, 300), m_MovementRand.Next(90, 100));//, CurrenRoomList[m_MainPlayer.RoomIndex]);
        //            }
        //        }
        //    }
        //    else if(CurrentEnemyType == 2)
        //    {

        //    }
        //    else if (CurrentEnemyType == 2)
        //    {

        //    }

        //    foreach (Enemy en in CurrentEnemies)
        //    {
        //        foreach(Bullet b in m_MainPlayer.m_BulletList)
        //        {
        //            if(b.m_BoundingBox.Intersects(en.m_BoundingBox))
        //            {
        //                en.TakeDamage(m_MainPlayer.m_Damage);
        //                b.m_IsVisible = false;
        //            }
        //        }
        //    }
        //    //Go threw the top door
        //    if(CurrenRoomList[m_MainPlayer.RoomIndex].DoorExists((int)Level.m_DoorPlacement.Up))
        //    {
        //        int TopDoor = CurrenRoomList[m_MainPlayer.RoomIndex].FindDoor((int)Level.m_DoorPlacement.Up);
        //        if(CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomDoors[TopDoor].m_BoundingBox.Intersects(m_MainPlayer.m_BoundingBox))
        //        {
        //            if(CurrenRoomList[m_MainPlayer.RoomIndex].RoomClear() == false)
        //            {
        //                CurrenRoomList[m_MainPlayer.RoomIndex].DeactivateEnemies();
        //            }
        //            m_MainPlayer.RoomIndex = CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomDoors[TopDoor].m_nextRoom;
        //            m_Camera.Update(a_GameTime, (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.X,
        //                (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.Y);
        //            m_MainPlayer.m_PlayerPosition.Y = (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.Y + 704;
        //            m_MainPlayer.m_PlayerPosition.X = (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.X + 480;
        //            CurrenRoomList[m_MainPlayer.RoomIndex].ActivateEnemies();
        //        }
        //    }

        //    //go threw bottom door
        //    if (CurrenRoomList[m_MainPlayer.RoomIndex].DoorExists((int)Level.m_DoorPlacement.Down))
        //    {
        //        int DownDoor = CurrenRoomList[m_MainPlayer.RoomIndex].FindDoor((int)Level.m_DoorPlacement.Down);
        //        if (CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomDoors[DownDoor].m_BoundingBox.Intersects(m_MainPlayer.m_BoundingBox))
        //        {
        //            if (CurrenRoomList[m_MainPlayer.RoomIndex].RoomClear() == false)
        //            {
        //                CurrenRoomList[m_MainPlayer.RoomIndex].DeactivateEnemies();
        //            }
        //            //CurrenRoomList[m_MainPlayer.RoomIndex].DeactivateEnemies();
        //            m_MainPlayer.RoomIndex = CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomDoors[DownDoor].m_nextRoom;
        //            m_Camera.Update(a_GameTime, (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.X,
        //                (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.Y);
        //            m_MainPlayer.m_PlayerPosition.Y = (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.Y + 128;
        //            m_MainPlayer.m_PlayerPosition.X = (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.X + 480;
        //            CurrenRoomList[m_MainPlayer.RoomIndex].ActivateEnemies();
        //        }
        //    }

        //    //go threw left door
        //    if (CurrenRoomList[m_MainPlayer.RoomIndex].DoorExists((int)Level.m_DoorPlacement.Left))
        //    {
        //        int LeftDoor = CurrenRoomList[m_MainPlayer.RoomIndex].FindDoor((int)Level.m_DoorPlacement.Left);
        //        if (CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomDoors[LeftDoor].m_BoundingBox.Intersects(m_MainPlayer.m_BoundingBox))
        //        {
        //            if (CurrenRoomList[m_MainPlayer.RoomIndex].RoomClear() == false)
        //            {
        //                CurrenRoomList[m_MainPlayer.RoomIndex].DeactivateEnemies();
        //            }
        //            //CurrenRoomList[m_MainPlayer.RoomIndex].DeactivateEnemies();
        //            m_MainPlayer.RoomIndex = CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomDoors[LeftDoor].m_nextRoom;
        //            m_Camera.Update(a_GameTime, (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.X,
        //                (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.Y);
        //            m_MainPlayer.m_PlayerPosition.Y = (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.Y + 416;
        //            m_MainPlayer.m_PlayerPosition.X = (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.X + 832;
        //            CurrenRoomList[m_MainPlayer.RoomIndex].ActivateEnemies();
        //        }
        //    }

        //    //go threw right door
        //    if (CurrenRoomList[m_MainPlayer.RoomIndex].DoorExists((int)Level.m_DoorPlacement.Right))
        //    {

        //        int RightDoor = CurrenRoomList[m_MainPlayer.RoomIndex].FindDoor((int)Level.m_DoorPlacement.Right);
        //        if (CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomDoors[RightDoor].m_BoundingBox.Intersects(m_MainPlayer.m_BoundingBox))
        //        {
        //            if (CurrenRoomList[m_MainPlayer.RoomIndex].RoomClear() == false)
        //            {
        //                CurrenRoomList[m_MainPlayer.RoomIndex].DeactivateEnemies();
        //            }
        //            //CurrenRoomList[m_MainPlayer.RoomIndex].DeactivateEnemies();
        //            m_MainPlayer.RoomIndex = CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomDoors[RightDoor].m_nextRoom;
        //            m_Camera.Update(a_GameTime, (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.X,
        //                (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.Y);
        //            m_MainPlayer.m_PlayerPosition.Y = (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.Y + 416;
        //            m_MainPlayer.m_PlayerPosition.X = (int)CurrenRoomList[m_MainPlayer.RoomIndex].m_RoomPosition.X + 128;
        //            CurrenRoomList[m_MainPlayer.RoomIndex].ActivateEnemies();
        //        }
        //    }
            
        //    base.Update(a_GameTime);
        //}
        ////////////////////////////////




        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="a_GameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime a_GameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //if(m_Door.m_IsDoorOpen == true)
            //{
            //    m_Door.LoadContent(Content);
            //}
            m_SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, m_Camera.m_Transform);

            //m_Room.Draw(m_SpriteBatch);
            //m_Room2.Draw(m_SpriteBatch);
            //m_Floor.Draw(m_SpriteBatch);
            //m_Door.Draw(m_SpriteBatch);

            ///////commented out for testing of new level draw and load content
            //m_Level_1.Draw(m_SpriteBatch);
            ////////////////////////

            m_Level_test.Draw(m_SpriteBatch, 1);
            m_MainPlayer.Draw(m_SpriteBatch);

            //temporary function calls to see bounding boxes/////////////////////////////////////////////////
            Rooms temp = m_Level_test.GetCurrentRoom();
            if(temp.m_RoomEnemies.Count != 0)
            {
                foreach(Enemy tempEnemy in temp.m_RoomEnemies)
                {
                    DrawBorder(tempEnemy.m_BoundingBox, 2, Color.Red, m_SpriteBatch);///from internet
                }
                //Enemy tempEnemy = temp.m_RoomEnemies[0];
                //DrawBorder(tempEnemy.m_BoundingBox, 2, Color.Red, m_SpriteBatch);///from internet
            }
            DrawBorder(m_MainPlayer.m_BoundingBox, 2, Color.Red, m_SpriteBatch);
            /////////////////////////////////////////////////////////////////////////////////////////////////

            //Temporary function call to see bullet hit boxes



            m_SpriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(a_GameTime);
        }


        //draw method from the internet to draw my bounding box rectangles
        private void DrawBorder(Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor, SpriteBatch temp)
        {
            // Draw top line
            temp.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);

            // Draw left line
            temp.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);

            // Draw right line
            temp.Draw(pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder),
                                            rectangleToDraw.Y,
                                            thicknessOfBorder,
                                            rectangleToDraw.Height), borderColor);
            // Draw bottom line
            temp.Draw(pixel, new Rectangle(rectangleToDraw.X,
                                            rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
                                            rectangleToDraw.Width,
                                            thicknessOfBorder), borderColor);
        }
    }
}