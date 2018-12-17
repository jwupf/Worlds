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
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace DataView
{
    public class GameOfLife : IWorld
    {
        private int m_length;
        private int m_heigth;

        private bool[][] m_activePoints;
        private bool[][] m_stablePoints;

        private Random m_rand;
        private Timer m_timer;

        public GameOfLife(int maxX, int maxY)
        {
            m_rand = new Random(); // ToDo: use "random" seed later
            m_timer = new Timer(Run, this, 1000, 250);

            m_length = maxX;
            m_heigth = maxY;

            m_activePoints = new bool[m_length][];
            m_stablePoints = new bool[m_length][];
            for (uint i = 0; i < m_length; i++)
            {
                m_activePoints[i] = new bool[m_heigth];
                m_stablePoints[i] = new bool[m_heigth];
                for (int j = 0; j < m_heigth; j++)
                {
                    bool value = m_rand.Next(2) == 0;
                    m_activePoints[i][j] = value;
                    m_stablePoints[i][j] = value;
                }
            }

        }

        private bool isAliveInNextRound(int i, int j)
        {
            if (i < 0)
                i = m_length - 1;
            else if (i >= m_length)
                i = 0;

            if (j < 0)
                j = m_heigth - 1;
            else if (j >= m_heigth)
                j = 0;
            return m_stablePoints[i][j];
        }

        private int countNeighbours(int i, int j)
        {
            int neighbours
                        = (isAliveInNextRound(i - 1, j - 1) ? 1 : 0);
            neighbours += (isAliveInNextRound(i, j - 1) ? 1 : 0);
            neighbours += (isAliveInNextRound(i + 1, j - 1) ? 1 : 0);
            neighbours += (isAliveInNextRound(i - 1, j) ? 1 : 0);
            neighbours += (isAliveInNextRound(i + 1, j) ? 1 : 0);
            neighbours += (isAliveInNextRound(i - 1, j + 1) ? 1 : 0);
            neighbours += (isAliveInNextRound(i, j + 1) ? 1 : 0);
            neighbours += (isAliveInNextRound(i + 1, j + 1) ? 1 : 0);
            return neighbours;
        }

        public void Run(object state)
        {
            lock (m_stablePoints)
            {
                var tmp = m_activePoints;
                m_activePoints = m_stablePoints;
                m_stablePoints = tmp;
            }

            NewSampleEvent?.Invoke();

            for (int i = 0; i < m_length; i++)
            {
                for (int j = 0; j < m_heigth; j++)
                {
                    int neighbors = countNeighbours(i, j);

                    if (m_stablePoints[i][j] && (neighbors == 2 || neighbors == 3))
                        m_activePoints[i][j] = true;
                    else if (!m_stablePoints[i][j] && neighbors == 3)
                        m_activePoints[i][j] = true;
                    else
                        m_activePoints[i][j] = false;
                }
            }
        }

        // Declare the event.
        public event Action NewSampleEvent;

        IEnumerable<Point> IWorld.Squares
        {
            get
            {
                lock (m_stablePoints)
                {
                    for (int i = 0; i < m_length; i++)
                        for (int j = 0; j < m_heigth; j++)
                            if (m_stablePoints[i][j])
                                yield return new Point(i, j);
                }
            }
        }
    }
}
