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
using System.Windows.Forms;

namespace DataView
{
    class MainWindow : Form
    {
        private PlayField m_playFieldControl;

        private MenuItem m_closeMenuItem;
        private MenuItem m_worldSelectionMenuItem;
        private MenuItem m_gameOfLiveMenuItem;

        public MainWindow()
        {
            Menu = new MainMenu();

            m_closeMenuItem = new MenuItem("Close");
            m_closeMenuItem.Click +=  (object sender, EventArgs e) => 
            {
                this.Close();
            };            
            Menu.MenuItems.Add(m_closeMenuItem);

            m_worldSelectionMenuItem = new MenuItem("Worlds");
            Menu.MenuItems.Add(m_worldSelectionMenuItem);

            m_gameOfLiveMenuItem = new MenuItem("Conways Game of Life");
            m_gameOfLiveMenuItem.Click += (object sender, EventArgs e) =>
            {
                m_playFieldControl.setWorld(new GameOfLife(100, 100));
            };
            m_worldSelectionMenuItem.MenuItems.Add(m_gameOfLiveMenuItem);
            
            m_playFieldControl = new PlayField(this, 0, 0, Width, Height);
            m_playFieldControl.Anchor = ~AnchorStyles.None;
            Controls.Add(m_playFieldControl);
            
        }
    }
}
