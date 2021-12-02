using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_caro
{
    public class playinfo
    {
        private Point point;

        public Point Point
        {
            get  {  return point; }
            set {  point = value;  }
        }
        
        private int currentplayer;

        public int Currentplayer
        {
            get { return currentplayer; }
            set { currentplayer = value; }
        }
        public playinfo(Point point , int currentplayer)
        {
            this.Point = point;
            this.Currentplayer = currentplayer;
        }
    }
}
