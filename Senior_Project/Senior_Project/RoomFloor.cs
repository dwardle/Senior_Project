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
    public class RoomFloor
    {
        public Texture2D texture;

        public Vector2 roomPos;

        public RoomFloor()
        {
            texture = null;
            roomPos = new Vector2(128 / 2, 128 / 2);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("roomFloor");
        }

        public void Draw(SpriteBatch sprtBatch)
        {
            sprtBatch.Draw(texture, roomPos, Color.White);
        }
    }
}