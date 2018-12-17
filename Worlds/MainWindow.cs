using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
