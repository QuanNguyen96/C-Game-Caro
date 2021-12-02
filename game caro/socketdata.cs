using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace game_caro
{
    [Serializable]
    public class socketdata
    {
        private int command;
        public int Command
        {
            get { return command; }
            set { command = value; }
        }
        private Point point;
        public Point Point
        {
            get { return point; }
            set { point = value; }
        }

        public string Message
        {
            get { return message; }
            set {  message = value;}
        }

        private string message;
        public socketdata(int COMMAND,string MESSAGE,Point POINT)
        {
            this.Command = COMMAND;
            this.Point = POINT;
            this.Message = MESSAGE;
        }
        
    }
    public enum socketCommand
    {
     
        SEND_POINT,
        NOTIFY,
        NEW_GAME,
        END_GAME,
        TIME_OUT,
        UNDO,
        QUIT,
        TEST,
        CHAT,
        IMAGE_FILENAME,
        CHAT_ICON
    }
}
