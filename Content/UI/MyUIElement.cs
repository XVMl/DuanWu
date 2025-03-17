using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria.UI;

namespace DuanWu.Content.UI
{
    public class MyUIElement:UIElement
    {
        public virtual void MyLeftClick(UIMouseEvent evt)
        {
        }

        public virtual void MyScrollWheel(UIMouseEvent evt) 
        { 
        }

        public virtual void MyUpdate(GameTime gameTime) 
        { 
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            MyUpdate(gameTime);
        }
    }
}
