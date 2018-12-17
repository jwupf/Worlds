using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataView
{
    public interface IWorld
    {
        event Action NewSampleEvent;

        IEnumerable<Point> Squares
        {
            get;
        }         
    }
}
