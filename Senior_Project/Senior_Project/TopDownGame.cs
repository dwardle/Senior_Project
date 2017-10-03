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



/// <summary>
/// spmLongShort::ProcessNewOpens() spmLongShort::ProcessNewOpens()
/// 
///
///NAME
///
///        spmLongShort::ProcessNewOpens - processes new opens for this model.
///
///SYNOPSIS
///
///        bool spmLongShort::ProcessNewOpens(spmTrObj &a_obj, double a_capital
///                                                                       , Jar::Date a_date );
///            a_obj            --> the trading object to be opened.
///            a_capital        --> the amount of capital to apply.
///            a_date           --> the date we are processing in the simulation.
///
////DESCRIPTION
///
///        This function will attempt to open the trading object a_obj with the
///        specified amount of capital. Before attempting the open, it will
///        apply portfolio constraints. If any of the portfolio constraints are
///        not met, this object will be opened as a phantom.  The constraint
///        may also reduce the amount of capital to be applied.
///
////        The status flags and phantom flag for the object will be set
////        appropriately.
///
////RETURNS
///
////        Returns true if the open was successful and false if it was opened
////        as a phantom.One of these two cases will always occur.
///
////AUTHOR
///
////        Victor Miller
///
///DATE
///
///        6:27pm 9 / 1 / 2001
/// </summary>
/// 

/// <summary>
/// NAME    <Name></Name>
/// SYNOPSIS 
/// DESCRIPTION <Description></Description>
/// RETURNS
/// AUTHOR  <Author></Author>
/// DATE    <Date></Date>
/// </summary>
/// 

/// <name></name>
/// <author></author>
/// <date></date>
namespace Senior_Project
{
    //October 2nd
    //Work done so far today.
    //Made menu for winning the game and menu items for new game. made a door appear after boss is defeated to allow player to continue to the next level. the next level keeps the same player
    //character and creates a new bigger level. doors now lock until enemies are cleared in the room. 
    //THINGS i NEED TO DO
    //make 2 more items work and randomly decide which one the player can get at each level
    //remove the item from the screen when the player picks it up
    //Make a Game over menu.

    //After those things are done, start to comment all my code and create my manual. finish manual tomorrow then if there is time add a different boss type.



    //September 25th
    //Add code to make sart menu

    //Work done so far.
    //Added start menu and start of pause menu
    //things that need fixing
    //pause menu and start menu code needs a bit of cleaning. pause menu needs to add a continue option. posibly want to change pause menu to be transparent. 
    //pause menu needs some spacing fixes. 

    //////////////////////////////////////////////
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 

    
    public class TopDownGame : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager m_Graphics;
        SpriteBatch m_SpriteBatch;
        public List<Rooms> m_RoomList = new List<Rooms>();
        Player m_MainPlayer;
        Camera m_Camera;
        Level m_Level;

        const int m_roomWidth = 960;
        const int m_RoomHeight = 832;
        public int m_LevelCount = 1;

        Texture2D pixel;

        //random number generator for enemy movement delays

        public Random m_MovementRand = new Random();
        


        //Game state variables.
        int m_GameState;
        Menu m_StartMenu;
        Menu m_PauseMenu;
        Menu m_WinMenu;
        Menu m_GameOverMenu;

        const int m_Start = 0;
        const int m_GamePlay = 1;
        const int m_GamePaused = 2;
        const int m_GameWin = 3;
        const int m_GameOver = 4;
        const int m_NextLevel = 5;


        /// <summary>
        /// NAME
        /// 
        ///     TopDownGame()::TopDownGame()
        ///     
        /// SYNOPSIS
        ///     
        ///     m_Graphics          --> Graphics Deveice manager to manage the games graphics
        ///     m_SpriteBatch       --> SpriteBatch to hold all sprites/images to be drawn to the screen
        ///     m_RoomList          --> List of all rooms
        ///     m_MainPlayer        --> Player object to control the users character
        ///     m_Camera            --> Object to control the viewport for the game
        ///     m_Level_Test        --> Level object to hold the current level
        ///     m_LevelCount          --> Number of the current level i.e. level 1 = 1
        ///     m_MovementRand      --> Random number generator for enenemy movement
        ///     m_GameState         --> Current state of the game
        ///     m_StartMenu         --> Menu object for the games start menu
        ///     m_PauseMenu         --> Menu object for the games pause menu
        ///     m_SizeMenu          --> Menu object for the games size menu
        ///     m_WinMenu           --> Menu object for when the player beats the game
        ///     m_GameOverMenu      --> Menu object for when the player dies
        ///     m_Start             --> Constant for start menu game state
        ///     m_Gameplay          --> Constant for when the game is in play state
        ///     m_GamePaused        --> Constant for when the game is in pause state
        ///     m_SizeSelect        --> Constant for when the game is in size select menu state
        ///     m_GameWin           --> Constant for when the game is in the win state
        ///     m_GameOver          --> Constant for when the game is in the game over state
        ///     m_NextLevel         --> Constant for when the game is in the next level state
        ///      
        /// DESCRIPTION
        ///     
        ///     Constructor for the TopDownGames Object. Function will initialize the game
        ///     
        /// RETURNS
        ///     
        ///     Nothing
        ///     
        /// AUTHOR
        ///     
        ///     Douglas Wardle
        ///     
        /// DATE
        ///     
        ///     
        /// </summary>
        public TopDownGame()
        {
            //960 x 832
            //room size
            m_Graphics = new GraphicsDeviceManager(this);
            m_Graphics.IsFullScreen = false;/////////
            m_Graphics.PreferredBackBufferWidth = m_roomWidth;//////
            m_Graphics.PreferredBackBufferHeight = m_RoomHeight;/////
            this.Window.Title = "batdoug";
            Content.RootDirectory = "Content";
            this.IsMouseVisible = true;

            //initialize Start menu
            m_StartMenu = new Menu();
            m_StartMenu.AddMenuItem(Content, 0);
            m_StartMenu.AddMenuItem(Content, 1);
            m_StartMenu.SetOptionPositions();
        
            //initialize pause menu
            m_PauseMenu = new Menu();
            m_PauseMenu.SetMenuType(1);
            m_PauseMenu.AddMenuItem(Content, 1);

            //initialize win menu
            m_WinMenu = new Menu();
            m_WinMenu.SetMenuType(3);
            m_WinMenu.AddMenuItem(Content, 5);
            m_WinMenu.AddMenuItem(Content, 1);

            //initialize game over menu
            m_GameOverMenu = new Menu();
            m_GameOverMenu.SetMenuType(4);
            m_GameOverMenu.AddMenuItem(Content, 5);
            m_GameOverMenu.AddMenuItem(Content, 1);
            
            m_GameState = m_Start;

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


        /// <name>
        ///      TopDownGame()::LoadContent()
        /// </name>
        /// <summary>
        ///         Calls LoadContent for all content needed for the game
        /// </summary>
        /// <author>
        ///         Douglas Wardle
        /// </author>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.

            m_SpriteBatch = new SpriteBatch(GraphicsDevice);
            //Switch to load content based off of the current gamestate
            switch (m_GameState)
            {
                //Load content based off of which game state the game is in
                case m_Start:
                    m_StartMenu.LoadContent(Content);
                    pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                    pixel.SetData(new[] { Color.White });
                    break;
                case m_GamePaused:
                    Rooms testRoom = m_Level.GetCurrentRoom();
                    m_PauseMenu.m_Position = testRoom.m_RoomPosition;
                    m_PauseMenu.SetOptionPositions();
                    m_PauseMenu.LoadContent(Content);
                    pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                    pixel.SetData(new[] { Color.White });
                    break;
                case m_GamePlay:
                    m_Level.LoadContent(Content, 1);
                    m_MainPlayer.LoadContent(Content);

                    //code to work with drawboarder function found online/////////////////////////////////////////////
                    pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
                    pixel.SetData(new[] { Color.White }); // so that we can draw whatever color we want on top of it
                                                          //////////////////////////////////////////////////////////////////////////////////////////////////
                                                          // TODO: use this.Content to load your game content here
                    break;

                case m_GameWin:
                    System.Threading.Thread.Sleep(500); //Wait 5 seconds before showing the you win menu
                    Rooms testRoom1 = m_Level.GetCurrentRoom();
                    m_WinMenu.m_Position = testRoom1.m_RoomPosition;
                    m_WinMenu.SetOptionPositions();
                    m_WinMenu.LoadContent(Content);
                    break;
                case m_GameOver:
                    testRoom = m_Level.GetCurrentRoom();
                    m_GameOverMenu.m_Position = testRoom.m_RoomPosition;
                    m_GameOverMenu.SetOptionPositions(350, 450);
                    m_GameOverMenu.LoadContent(Content);
                    break;
            }
            
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

            MouseState ms = Mouse.GetState();
            Rectangle t_MousePosition = new Rectangle(ms.X, ms.Y, 10, 10);
            switch (m_GameState) //Switch to load the correct images for the game state
            {
                case m_Start:
                    foreach (MenuItem mi in m_StartMenu.m_MenuOptions) //Allows the user to select a menu item from the start menu
                    {
                        if (t_MousePosition.Intersects(mi.m_Hitbox) && ms.LeftButton == ButtonState.Pressed && mi.m_Option == 0)
                        {
                            m_GameState = m_GamePlay;
                            m_Level = new Level(m_LevelCount);
                            m_MainPlayer = new Player(m_Level);
                            LoadContent();
                        }
                        else if (t_MousePosition.Intersects(mi.m_Hitbox) && ms.LeftButton == ButtonState.Pressed && mi.m_Option == 1)
                        {
                            this.Exit();
                        }
                    }
                    break;

                case m_GamePaused:
                    foreach (MenuItem mi in m_PauseMenu.m_MenuOptions) //Allows the user to select a menu item from the pause menu
                    {
                        ms = Mouse.GetState();
                        t_MousePosition = new Rectangle(ms.X + (int)m_PauseMenu.m_Position.X, ms.Y + (int)m_PauseMenu.m_Position.Y, 10, 10);
                        if (t_MousePosition.Intersects(mi.m_Hitbox) && ms.LeftButton == ButtonState.Pressed && mi.m_Option == 0)
                        {
                            m_GameState = m_GamePlay;
                            LoadContent();
                        }
                        else if (t_MousePosition.Intersects(mi.m_Hitbox) && ms.LeftButton == ButtonState.Pressed && mi.m_Option == 1)
                        {
                            m_GameState = m_GamePlay;
                            LoadContent();
                        }
                    }
                    break;
                case m_GamePlay:
                    if (Keyboard.GetState().IsKeyDown(Keys.P))
                    {
                        m_GameState = m_GamePaused;
                        LoadContent();
                    }
                    m_MainPlayer.Update(a_GameTime, m_Level);
                    Rooms CurrentRoom = m_Level.GetCurrentRoom();

                    List<Enemy> CurrentEnemies = new List<Enemy>();
                    CurrentEnemies = CurrentRoom.GetEnemyList();
                    int CurrentEnemyType = CurrentRoom.GetEnemyType();

                    //Set the doors to open if there are no more enemies in the room
                    if (CurrentEnemies.Count == 0)
                    {
                        foreach (Door d in CurrentRoom.GetDoorList())
                        {
                            d.SetIsOpen(true);
                            d.LoadContent(Content);
                        }
                    }
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
                        //Right now after the boss is defeated and the player enters the door in the room, a new level is loaded
                        Boss levelBoss = CurrentRoom.GetBoss();
                        if (levelBoss.BossDefeated() && m_Level.GetLevelCount() == 3)
                        {
                            m_GameState = m_GameWin;
                            LoadContent();
                        }
                        float PlayerLastX = m_MainPlayer.m_PlayerPosition.X;
                        float PlayerLastY = m_MainPlayer.m_PlayerPosition.Y;
                        List<Door> BossRoomDoors = CurrentRoom.GetDoorList();
                        if (BossRoomDoors.Count != 0 && levelBoss.BossDefeated() == false)
                        {
                            while (BossRoomDoors.Count != 0)
                            {
                                BossRoomDoors.RemoveAt(0);
                            }
                        }
                        else if (BossRoomDoors.Count == 0 && levelBoss.BossDefeated())
                        {
                            CurrentRoom.CreateDoor(0);
                            LoadContent();
                        }

                        levelBoss.Update1(m_MainPlayer);
                        foreach (Bullet b in m_MainPlayer.m_BulletList)
                        {
                            if (b.m_HitBox.Intersects(levelBoss.m_HitBox) || b.m_HitBox.Intersects(levelBoss.m_HitBox2))
                            {
                                levelBoss.TakeDamage(m_MainPlayer.m_Damage);
                                b.m_IsVisible = false;
                            }
                        }



                    }
                    else if (CurrentEnemyType == 3)
                    {
                        //Right not the boss is set to move to players last location then stop till the move delay hits zero
                        //what i should do is have enemy type 3 be for the next boss and it will move this way or based off of the boss's speed
                        //it will either chase the player or it will do this. if the boss' speed is too high it will always be hitting the player if it chases the player
                        //so in that event make it do this movement.
                        Boss levelBoss = CurrentRoom.GetBoss();
                        if (levelBoss.CanMove() && levelBoss.IsMoving() == false)
                        {
                            //not sure why this works but the moving to the players exact position works
                            float PlayerLastX = m_MainPlayer.m_PlayerPosition.X;// - levelBoss.GetTextureOriginX();
                            float PlayerLastY = m_MainPlayer.m_PlayerPosition.Y;// - levelBoss.GetTextureOriginY();
                            // levelBoss.SetIsActive(true);
                            //levelBoss.SetCanMove();
                            levelBoss.SetMoveLocation(new Vector2(PlayerLastX, PlayerLastY));
                            levelBoss.SetIsMoving(true);
                        }
                        else if (levelBoss.CanMove() && levelBoss.IsMoving())
                        {
                            levelBoss.MoveToPlayer();
                        }

                    }

                    
                    foreach (Enemy en in CurrentEnemies) //Damage enemies if a bullets hitbox intersects with the enemy
                    {
                        foreach (Bullet b in m_MainPlayer.m_BulletList)
                        {
                            if (b.m_HitBox.Intersects(en.m_HitBox))
                            {
                                en.TakeDamage(m_MainPlayer.m_Damage);
                                b.m_IsVisible = false;
                            }
                        }
                    }
                    if (CurrentRoom.IsBossRoom() == false)//allow player to traverse rooms as long as the room in not a boss room
                    {
                        TraverseRooms(a_GameTime);
                    }
                    else
                    {
                        if (CurrentRoom.GetBoss().m_IsAlive == false && CurrentRoom.GetDoorList().Count != 0)//if room is a boss room, allow player to go to the next level
                        {
                            TraveseLevel(a_GameTime, CurrentRoom);
                        }

                    }

                    foreach (Heart h in m_MainPlayer.GetHearts()) //Load the players current heart textures
                    {
                        h.LoadContent(Content);
                    }
                    if (m_Level.GetItemType() == 1)//Allows player to pick up the fast shot item if the players hitbox intersects with the item hitbox
                    {
                        if (m_MainPlayer.m_HitBox.Intersects(m_Level.m_FastShot.m_HitBox))
                        {
                            m_Level.m_FastShot.IncreaseShotSpeed(m_MainPlayer);
                            m_Level.m_FastShot.LoadContent(Content);
                        }
                    }
                    else if (m_Level.GetItemType() >= 2)//allows player to pick up the heartsUp item if the players hit box intersects with the item hitbox
                    {
                        if (m_MainPlayer.m_HitBox.Intersects(m_Level.m_HealthUp.m_HitBox) && m_Level.m_HealthUp.GetUsed() == false)
                        {
                            m_Level.m_HealthUp.IncreasePlayerHealth(m_MainPlayer);
                            List<Heart> PlayerHearts = m_MainPlayer.GetHearts();
                            Heart addHeart = new Heart(new Vector2(CurrentRoom.GetRoomCoord_X() + ((PlayerHearts.Count + 1) * 40), CurrentRoom.GetRoomCoord_Y() + 16));
                            addHeart.LoadContent(Content);
                            PlayerHearts.Add(addHeart);
                            m_MainPlayer.ChangeHeartTexture();
                            foreach(Heart h in PlayerHearts)
                            {
                                h.LoadContent(Content);
                            }
                            m_Level.m_HealthUp.LoadContent(Content);

                        }
                    }

                    if (m_MainPlayer.GetHealth() <= 0)//if the players health is less or equal to 0 then they have died and the game switches game state to game over state
                    {
                        m_GameState = m_GameOver;
                        LoadContent();

                    }
                    break;
                case m_NextLevel: //if in this game state, create a new level and set the game state to game play and load content for the next level
                    m_Camera.Update(a_GameTime, 0, 0);
                    m_Level = new Level(m_Level.GetLevelCount() + 1);
                    m_MainPlayer.SetPosition(new Vector2(480, 462));
                    m_GameState = m_GamePlay;
                    LoadContent();
                    break;
                case m_GameWin: //if in this game state then the player has beaten the game. ask if they would like to start a new game or exit
                    ms = Mouse.GetState();
                    t_MousePosition = new Rectangle(ms.X + (int)m_WinMenu.m_Position.X, ms.Y + (int)m_WinMenu.m_Position.Y, 10, 10);
                    foreach (MenuItem mi in m_WinMenu.m_MenuOptions)
                    {
                        if (t_MousePosition.Intersects(mi.m_Hitbox) && ms.LeftButton == ButtonState.Pressed && mi.m_Option == 5)
                        {
                            m_Camera.Update(a_GameTime, 0, 0);

                            m_GameState = m_Start;
                            LoadContent();
                        }
                        else if (t_MousePosition.Intersects(mi.m_Hitbox) && ms.LeftButton == ButtonState.Pressed && mi.m_Option == 1)
                        {
                            this.Exit();
                        }
                    }
                    break;
                case m_GameOver: //if in this game state then the player has lost and will be asked if they wish to play again
                    ms = Mouse.GetState();
                    t_MousePosition = new Rectangle(ms.X + (int)m_GameOverMenu.m_Position.X, ms.Y + (int)m_GameOverMenu.m_Position.Y, 10, 10);
                    foreach (MenuItem mi in m_GameOverMenu.m_MenuOptions)
                    {
                        if (t_MousePosition.Intersects(mi.m_Hitbox) && ms.LeftButton == ButtonState.Pressed && mi.m_Option == 5)
                        {
                            m_Camera.Update(a_GameTime, 0, 0);

                            m_GameState = m_Start;
                            LoadContent();
                        }
                        else if (t_MousePosition.Intersects(mi.m_Hitbox) && ms.LeftButton == ButtonState.Pressed && mi.m_Option == 1)
                        {
                            this.Exit();
                        }
                    }
                    break;
            }
            base.Update(a_GameTime);
        }






        /// <name>TopDownGame::Draw()</name>
        /// <summary>
        /// This function is used to draw the game to the screen
        /// </summary>
        /// <param name="a_GameTime">Provides a snapshot of timing values.</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        protected override void Draw(GameTime a_GameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            m_SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, m_Camera.m_Transform);
            //Switch statement to draw the correct images based off of gamestate
            switch (m_GameState)
            {
                case m_Start:
                    m_StartMenu.Draw(m_SpriteBatch);
                    foreach (MenuItem mi in m_StartMenu.m_MenuOptions)
                    {


                        if (m_SpriteBatch != null)
                        {
                            DrawBorder(mi.m_Hitbox, 2, Color.Blue, m_SpriteBatch);
                        }
                    }
            
                    break;
                case m_GamePaused:
                    m_PauseMenu.Draw(m_SpriteBatch);
                    foreach (MenuItem mi in m_PauseMenu.m_MenuOptions)
                    {
                        if (m_SpriteBatch != null)
                        {
                            DrawBorder(mi.m_Hitbox, 2, Color.Blue, m_SpriteBatch);
                        }
                    }

                    break;
                case m_GamePlay:
                    m_Level.Draw(m_SpriteBatch, 1);
                    m_MainPlayer.Draw(m_SpriteBatch);

                    //temporary function calls to see bounding boxes/////////////////////////////////////////////////
                    Rooms temp = m_Level.GetCurrentRoom();
                    if (temp.m_RoomEnemies.Count != 0)
                    {
                        foreach (Enemy tempEnemy in temp.m_RoomEnemies)
                        {
                            DrawBorder(tempEnemy.m_HitBox, 2, Color.Red, m_SpriteBatch);///from internet
                        }
                        //Enemy tempEnemy = temp.m_RoomEnemies[0];
                        //DrawBorder(tempEnemy.m_HitBox, 2, Color.Red, m_SpriteBatch);///from internet
                    }
                    DrawBorder(m_MainPlayer.m_HitBox, 2, Color.Red, m_SpriteBatch);
                    /////////////////////////////////////////////////////////////////////////////////////////////////

                    //Temporary function call to see bullet hit boxes////////////////////////////////////////////////
                    if (m_MainPlayer.m_BulletList.Count != 0)
                    {
                        foreach (Bullet b in m_MainPlayer.m_BulletList)
                        {
                            DrawBorder(b.m_HitBox, 2, Color.Red, m_SpriteBatch);
                        }
                    }
                    /////////////////////////////////////////////////////////////////////////////////////////////////
                    //Temporary function call to see door hit boxes//////////////////////////////////////////////////
                    foreach (Door tempDoor in temp.m_RoomDoors)
                    {
                        DrawBorder(tempDoor.m_HitBox, 2, Color.Red, m_SpriteBatch);
                    }
                    /////////////////////////////////////////////////////////////////////////////////////////////////
                    //Temporary function call to see item bounding box///////////////////////////////////////////////
                    DrawBorder(m_Level.m_FastShot.m_HitBox, 2, Color.Red, m_SpriteBatch);
                    /////////////////////////////////////////////////////////////////////////////////////////////////
                    if(m_Level.GetCurrentRoom().GetBoss() != null)
                    {
                        Boss tempBoss = m_Level.GetCurrentRoom().GetBoss();
                        DrawBorder(tempBoss.m_HitBox, 2, Color.Red, m_SpriteBatch);
                        DrawBorder(tempBoss.m_HitBox2, 2, Color.Red, m_SpriteBatch);
                    }
                    //m_SpriteBatch.End();

                    // TODO: Add your drawing code here

                    //base.Draw(a_GameTime);
                    break;

                case m_GameWin:
                    m_WinMenu.Draw(m_SpriteBatch);
                    foreach (MenuItem mi in m_WinMenu.m_MenuOptions)
                    {
                        if (m_SpriteBatch != null)
                        {
                            DrawBorder(mi.m_Hitbox, 2, Color.Blue, m_SpriteBatch);
                        }
                    }
                    break;

                case m_GameOver:
                    m_GameOverMenu.Draw(m_SpriteBatch);
                    foreach (MenuItem mi in m_GameOverMenu.m_MenuOptions)
                    {
                        if (m_SpriteBatch != null)
                        {
                            DrawBorder(mi.m_Hitbox, 2, Color.Blue, m_SpriteBatch);
                        }
                    }
                    break;
            }
            m_SpriteBatch.End();
            base.Draw(a_GameTime);
        }

        /// <name>TopDownGame::TraverseRooms()</name>
        /// <summary>
        /// Function allows the player to travel between rooms. if the players hitbox intersects with a doors hitbox
        /// the player will be moved to the next room that corresponds to the door they are trying to go through.
        /// It will also update the camera to show the correct room as well as activate the enemies in the room.
        /// </summary>
        /// <param name="a_GameTime"></param>
        /// <author>Douglas Wardle</author>
        ///<date></date>
        public void TraverseRooms(GameTime a_GameTime)
        {
            Rooms CurrentRoom = m_Level.GetCurrentRoom();

            //Allows player to walk through a door placed at placement top if the door is open
            if (CurrentRoom.DoorExists((int)Level.m_DoorPlacement.Up))
            {
                int TopDoor = CurrentRoom.FindDoor((int)Level.m_DoorPlacement.Up);
                if (CurrentRoom.m_RoomDoors[TopDoor].m_HitBox.Intersects(m_MainPlayer.m_HitBox))
                {
                    if (CurrentRoom.RoomClear() == false)
                    {
                        CurrentRoom.DeactivateEnemies();
                    }
                    m_Level.SetCurrentRoom(CurrentRoom.GetRoomRow() - 1, CurrentRoom.GetRoomCol());
                    CurrentRoom = m_Level.GetCurrentRoom();
                    m_Camera.Update(a_GameTime, (int)CurrentRoom.m_RoomPosition.X, (int)CurrentRoom.m_RoomPosition.Y);
                    m_MainPlayer.m_PlayerPosition.Y = (int)CurrentRoom.m_RoomPosition.Y + 704;
                    m_MainPlayer.m_PlayerPosition.X = (int)CurrentRoom.m_RoomPosition.X + 480;
                    CurrentRoom.ActivateEnemies();
                }
            }

            //Allows player to walk through a door placed at placement down if the door is open
            if (CurrentRoom.DoorExists((int)Level.m_DoorPlacement.Down))
            {
                int DownDoor = CurrentRoom.FindDoor((int)Level.m_DoorPlacement.Down);
                if (CurrentRoom.m_RoomDoors[DownDoor].m_HitBox.Intersects(m_MainPlayer.m_HitBox))
                {
                    if (CurrentRoom.RoomClear() == false)
                    {
                        CurrentRoom.DeactivateEnemies();
                    }
                    m_Level.SetCurrentRoom(CurrentRoom.GetRoomRow() + 1, CurrentRoom.GetRoomCol());
                    CurrentRoom = m_Level.GetCurrentRoom();
                    m_Camera.Update(a_GameTime, (int)CurrentRoom.m_RoomPosition.X, (int)CurrentRoom.m_RoomPosition.Y);
                    m_MainPlayer.m_PlayerPosition.Y = (int)CurrentRoom.m_RoomPosition.Y + 128;
                    m_MainPlayer.m_PlayerPosition.X = (int)CurrentRoom.m_RoomPosition.X + 480;
                    CurrentRoom.ActivateEnemies();
                }
            }

            //Allows player to walk through a door placed at placement left if the door is open
            if (CurrentRoom.DoorExists((int)Level.m_DoorPlacement.Left))
            {
                int LeftDoor = CurrentRoom.FindDoor((int)Level.m_DoorPlacement.Left);
                if (CurrentRoom.m_RoomDoors[LeftDoor].m_HitBox.Intersects(m_MainPlayer.m_HitBox))
                {
                    if (CurrentRoom.RoomClear() == false)
                    {
                        CurrentRoom.DeactivateEnemies();
                    }
                    m_Level.SetCurrentRoom(CurrentRoom.GetRoomRow(), CurrentRoom.GetRoomCol() - 1);
                    CurrentRoom = m_Level.GetCurrentRoom();
                    m_Camera.Update(a_GameTime, (int)CurrentRoom.m_RoomPosition.X, (int)CurrentRoom.m_RoomPosition.Y);
                    m_MainPlayer.m_PlayerPosition.Y = (int)CurrentRoom.m_RoomPosition.Y + 416;
                    m_MainPlayer.m_PlayerPosition.X = (int)CurrentRoom.m_RoomPosition.X + 832;
                    CurrentRoom.ActivateEnemies();
                }
            }

            //Allows player to walk through a door placed at placement right if the door is open
            if (CurrentRoom.DoorExists((int)Level.m_DoorPlacement.Right))
            {

                int RightDoor = CurrentRoom.FindDoor((int)Level.m_DoorPlacement.Right);
                if (CurrentRoom.m_RoomDoors[RightDoor].m_HitBox.Intersects(m_MainPlayer.m_HitBox))
                {
                    if (CurrentRoom.RoomClear() == false)
                    {
                        CurrentRoom.DeactivateEnemies();
                    }
                    m_Level.SetCurrentRoom(CurrentRoom.GetRoomRow(), CurrentRoom.GetRoomCol() + 1);
                    CurrentRoom = m_Level.GetCurrentRoom();
                    m_Camera.Update(a_GameTime, (int)CurrentRoom.m_RoomPosition.X, (int)CurrentRoom.m_RoomPosition.Y);
                    m_MainPlayer.m_PlayerPosition.Y = (int)CurrentRoom.m_RoomPosition.Y + 416;
                    m_MainPlayer.m_PlayerPosition.X = (int)CurrentRoom.m_RoomPosition.X + 128;
                    CurrentRoom.ActivateEnemies();
                }
            }
        }

        /// <name>TopDownGames::TraverseLevel()</name>
        /// <summary>
        /// This function gets called when the player is in the boss room. If the player is in the boss room,
        /// the boss is dead, and the players hitbox intersects with the door, the function will change the game state
        /// to allow the next level to be created
        /// </summary>
        /// <param name="a_GameTime"></param>
        /// <param name="a_BossRoom">Room object that holds the boss room</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void TraveseLevel(GameTime a_GameTime, Rooms a_BossRoom)
        {
            Door NextLevelDoor = a_BossRoom.GetDoorList().ElementAt(0);
            if(NextLevelDoor.m_HitBox.Intersects(m_MainPlayer.m_HitBox))
            {
                m_GameState = m_NextLevel;

            }
        }


        //draw method from the internet to draw my bounding box rectangles
        //will be deleted before I turn in the project
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