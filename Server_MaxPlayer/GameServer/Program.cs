using System;
using System.Threading;

namespace GameServer
{
    class Program
    {
        private static bool isRunning = false;
        public static bool GameStart = false;
        public static bool AskStart = false;
        public static DateTime ServerStartTime;

        static void Main(string[] args)
        {
            Console.Title = "Game Server";
            isRunning = true;

            ServerStartTime = DateTime.Now;
            Console.WriteLine($"Server Start at: {ServerStartTime}");
            ServerStartTime = ServerStartTime.AddSeconds(30);

            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();

            Server.Start(Constants.Max_player_num, 26950);
        }
        private static void MainThread()
        {
            Console.WriteLine($"Main thread started. Running at {Constants.TICKS_PER_SEC} ticks per second.");
            DateTime _nextLoop = DateTime.Now;

            Console.WriteLine($"ServerStartTime: {ServerStartTime}");
            Console.WriteLine($"NowTime: {DateTime.Now}");

            while ((isRunning)&&(!GameStart)&&(ServerStartTime > DateTime.Now))
            {
                while (_nextLoop < DateTime.Now)
                {
                    ThreadManager.UpdateMain();
                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);

                    if (_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
            if (!AskStart){
                Console.WriteLine("Game Start.");
                foreach (Client _client_local in Server.clients.Values) // 送每個人的包裹
                {
                    ServerSend.SendStart(_client_local);
                }
                AskStart = true;
            }
            while (isRunning)
            {
                while (_nextLoop < DateTime.Now)
                {
                    GameLogic.Update();
                    //Console.WriteLine("GameUpdate.");

                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);

                    if (_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }
        /*private static void MainThread()
        {
            Console.WriteLine($"Main thread started. Running at {Constants.TICKS_PER_SEC} ticks per second.");
            DateTime _nextLoop = DateTime.Now;

            while ((isRunning)&&(ServerStartTime < DateTime.Now))
            {
                Console.WriteLine("Waiting.");
                while (_nextLoop < DateTime.Now)
                {
                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);

                    if (_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
            if (GameStart == false){
                foreach (Client _client_local in Server.clients.Values) // 送每個人的包裹
                {
                    ServerSend.SendStart(_client_local);
                    Console.WriteLine("Game Start.");
                }
                GameStart = true;
            }
            while (isRunning)
            {
                while (_nextLoop < DateTime.Now)
                {
                    GameLogic.Update();

                    _nextLoop = _nextLoop.AddMilliseconds(Constants.MS_PER_TICK);

                    if (_nextLoop > DateTime.Now)
                    {
                        Thread.Sleep(_nextLoop - DateTime.Now);
                    }
                }
            }
        }*/
    }
}
