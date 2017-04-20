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


namespace Senior_Project
{
    public class EnemyNoGun : Enemy
    {
        //Random test = new Random();

        public EnemyNoGun()
        {
            
        }

        public void LoadContent(ContentManager a_Content)
        {
            m_Texture = a_Content.Load<Texture2D>("Enemy_No_Gun");
        }

        public void Update(GameTime a_GameTime, Player a_MainPlayer, List<Enemy> a_RoomEnemies, int a_RandMoveCount, int a_RandMoveDelay)//, Rooms a_CurrentRoom)
        {            
            if(this.m_IsActive)
            {
                this.m_BoundingBox = new Rectangle((int)m_Position.X, (int)m_Position.Y, m_Texture.Width, m_Texture.Height);
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

                
            }
        }
    }
}
