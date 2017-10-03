using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


//Have done Commenting 

namespace Senior_Project
{
    public class EnemyNoGun : Enemy
    {
        //Random test = new Random();
        //from rectangle draw method
        //Texture2D pixel;

        /// <name>EnemyNoGun::EnemyNoGun()</name>
        /// <summary>
        /// Basic constructor for an instance of this object
        /// </summary>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public EnemyNoGun()
        {
            
        }

        
        /// <name>EnemyNoGun::LoadContent()</name>
        /// <summary>
        /// Function will load the texture for an EnemyNoGun objcet
        /// </summary>
        /// <param name="a_Content">Content manager containing the conetent for all game textures</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void LoadContent(ContentManager a_Content)
        {
            m_Texture = a_Content.Load<Texture2D>("Enemies/Enemy_No_Gun");
        }

        /// <name>EnemyNoGun::Update()</name>
        /// <summary>
        /// Function gets called every time an EnemyNoGun object needs to be updated in the game. This includes enemy movement, activation, and removal
        /// </summary>
        /// <param name="a_GameTime"></param>
        /// <param name="a_MainPlayer">The current Player object for the game</param>
        /// <param name="a_RoomEnemies">List of all the enemies that are currently in the room and need to be updated</param>
        /// <param name="a_RandMoveCount">integer value to allow movement of a specific length toward the player. usually a random number</param>
        /// <param name="a_RandMoveDelay">integer value for the enemy move delay. usually a random number</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void Update(GameTime a_GameTime, Player a_MainPlayer, List<Enemy> a_RoomEnemies, int a_RandMoveCount, int a_RandMoveDelay)//, Rooms a_CurrentRoom)
        {
            if(this.m_IsActive)
            {
                //this.m_HitBox = new Rectangle((int)m_Position.X, (int)m_Position.Y, m_Texture.Width, m_Texture.Height);
                //this.m_HitBox = new Rectangle((int)this.m_Position.X - 32, (int)this.m_Position.Y - 28, this.m_Texture.Width, this.m_Texture.Height);
                if (this.m_MoveCount > 0)
                {
                    this.MoveToPlayer(a_MainPlayer, a_RoomEnemies);//, a_CurrentRoom);
                    this.m_MoveCount--;
                }
                else if (this.m_MoveDelay > 0)
                {
                    this.m_MoveDelay--;
                }
                else
                {
                    m_MoveCount = a_RandMoveCount;
                    this.m_MoveDelay = a_RandMoveDelay;
                }

                if(this.m_HitBox.Intersects(a_MainPlayer.m_HitBox))
                {
                    a_MainPlayer.TakeDamage(this.m_Damage);
                }
                
            }
        }

        /// <name>Draw2::</name>
        /// <summary>
        /// Function will draw the enemy to the screen
        /// </summary>
        /// <param name="a_SpriteBatch">a SpriteBatch object passed from the main game to allow for drawing of sprites</param>
        /// <author>Douglas Wardle</author>
        /// <date></date>
        public void Draw2(SpriteBatch a_SpriteBatch)
        {
            m_EnemyOrigin.X = m_Texture.Width / 2;
            m_EnemyOrigin.Y = m_Texture.Height / 2;
            a_SpriteBatch.Draw(m_Texture, m_Position, null, Color.White, m_Rotation, m_EnemyOrigin, 1.0f, SpriteEffects.None, 0f);
        }
    }
}
