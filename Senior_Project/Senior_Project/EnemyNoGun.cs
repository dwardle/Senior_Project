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
        //from rectangle draw method
        //Texture2D pixel;

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
                //this.m_BoundingBox = new Rectangle((int)m_Position.X, (int)m_Position.Y, m_Texture.Width, m_Texture.Height);
                //this.m_BoundingBox = new Rectangle((int)this.m_Position.X - 32, (int)this.m_Position.Y - 28, this.m_Texture.Width, this.m_Texture.Height);
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

                if(this.m_BoundingBox.Intersects(a_MainPlayer.m_BoundingBox))
                {
                    a_MainPlayer.TakeDamage(this.m_Damage);
                }
                
            }
        }
        public void Draw2(SpriteBatch a_SpriteBatch)
        {
            //GraphicsDevice x;
            //var rect = new Texture2D(x, 1, 1);

            m_EnemyOrigin.X = m_Texture.Width / 2;
            m_EnemyOrigin.Y = m_Texture.Height / 2;
            a_SpriteBatch.Draw(m_Texture, m_Position, null, Color.White, m_Rotation, m_EnemyOrigin, 1.0f, SpriteEffects.None, 0f);
        }
    }
}
