using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    class Constants
    {
        public const int TICKS_PER_SEC = 30;
        public const float MS_PER_TICK = 1000f / TICKS_PER_SEC;
        public const int Max_player_num = 10;
        public const int BrickConst = 5;
        public const int row = 24;
        public const int column = 8;
        public const int row_start = 11;
        public const int row_end = 20;
        public const string serverIPaddress = "192.168.43.12";
    }
}
