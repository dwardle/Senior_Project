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
    public class Door
    {
        public Texture2D texture;

        public Vector2 doorPos;

        public Door()
        {
            texture = null;
            doorPos = new Vector2(448, 0);
        }

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("door2");
        }

        public void Draw(SpriteBatch sprtBatch)
        {
            sprtBatch.Draw(texture, doorPos, Color.White);
        }
    }
}