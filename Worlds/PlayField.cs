using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DataView
{
    class PlayField : Control
    {
        private IWorld m_world;

        public PlayField(Control parent, int x, int y, int width, int height)
            : base(parent, string.Empty, x, y, width, height)
        {
            DoubleBuffered = true;
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
            if (m_world == null)
                pe.Graphics.FillRectangle(Brushes.Red, 0, 0, this.Width, this.Height);
            else
            {
                foreach (var square in m_world.Squares)
                {
                    pe.Graphics.FillRectangle(Brushes.Red, new Rectangle(square.X*2, square.Y*2, 2, 2));
                }
            }
        }


        public void setWorld(IWorld world)
        {
            m_world = world;
            m_world.NewSampleEvent += () =>
            {
                if (this.InvokeRequired)
                {
                    Action a = Refresh;
                    this.Invoke(a);
                }
                else
                    this.Refresh();
            };
        }
    }
}
