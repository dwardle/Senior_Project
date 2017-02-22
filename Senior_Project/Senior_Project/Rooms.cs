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
    public class Rooms
    {
        public Texture2D texture;
        public Vector2 roomPos;

        public Rooms()
        {
            texture = null;
            roomPos = new Vector2(0, 0);
        }

        public Rooms(int X, int Y)
        {
            roomPos = new Vector2(X, Y);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("test4");
        }

        public void Draw(SpriteBatch sprtBatch)
        {
            sprtBatch.Draw(texture, roomPos, Color.White);
        }
    }
}
