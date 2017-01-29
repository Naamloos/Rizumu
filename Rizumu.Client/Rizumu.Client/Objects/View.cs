using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rizumu.Client
{
    public class View
    {
        /*
         * This is a View.
         * It represents a Game View like Main Menu, Song Select, etc etc
         */
        public event EventHandler<UpdateEventArgs> UpdateEvent;
        public event EventHandler<DrawEventArgs> DrawEvent;

        public void Draw(DrawEventArgs e)
        {
            DrawEvent(null, e);
        }

        public void Update(UpdateEventArgs e)
        {
            UpdateEvent(null, e);
        }
    }
}
