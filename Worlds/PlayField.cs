/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
 *  Worlds - program to visualize cellular automata                          *
 *  Copyright (C) 2018  Jörg Wunderlich-Pfeiffer                             *
 *                                                                           *
 *  This program is free software: you can redistribute it and/or modify     *
 *  it under the terms of the GNU General Public License as published by     *
 *  the Free Software Foundation, either version 3 of the License, or        *
 *  (at your option) any later version.                                      *
 *                                                                           *
 *  This program is distributed in the hope that it will be useful,          *
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of           *
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the            *
 *  GNU General Public License for more details.                             *
 *                                                                           *
 *  You should have received a copy of the GNU General Public License        *
 *  along with this program.  If not, see <https://www.gnu.org/licenses/>.   *
 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

using System;
using System.Drawing;
using System.Windows.Forms;

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
