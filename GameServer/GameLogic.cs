using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Linq;

namespace GameServer
{
    class GameLogic
    {
        public static Dictionary<int, int> ranking = new Dictionary<int, int>();
        public static int[,] SortedRanking = new int[Constants.Max_player_num,2];            
        public static List<int> AttackedList = new List<int>();
        public static int listCnt;
        public static int temp = 0;
        public static int playercount = 0;
        
        public static void Update()
        {
            Ranking(); // Get all clients' data and count ranking // 跑完ranking

            foreach (Client _client_local in Server.clients.Values) // 送每個人的包裹
            {
                Attack(_client_local.id);  // Get all clients' data and make attack list
                
                ServerSend.SendUpdata(_client_local);
            }

            ThreadManager.UpdateMain();
        }

        public static void Ranking()
        {
            ranking.Clear();
            foreach (Client _client in Server.clients.Values) 
            {
                if (_client.player != null)
                {
                    ranking.Add(_client.player.id, _client.player.points);
                }
            }
            //SortedRanking.Clear();
            int count = 0;
            playercount = 0;
            foreach (KeyValuePair<int, int> author in ranking.OrderByDescending(key => key.Value))  
            {  
                SortedRanking[count,0] = author.Key;
                SortedRanking[count,1] = author.Value;
                count++;
                playercount++;
                //Console.WriteLine("Key: {0}, Value: {1}", author.Key, author.Value);  
            }  
        }

        public static void Attack(int localID)
        {
            AttackedList.Clear();
            listCnt = 0;
            foreach (Client _client in Server.clients.Values)
            {
                if ((_client.player != null)&&(_client.player.attackID == localID))
                {
                    AttackedList.Add(_client.id);
                    listCnt++;
                }
            }
            AttackedList.Sort();
        }

        public static void UpdateBoard(Packet _packet, int localID)
        {
            int min = Math.Min(5, playercount);
            for (int i = 0; i < min; i++){
                for (int j = 0; j < 2; j++){
                    _packet.Write(SortedRanking[i,j]);                    
                }
            }
           /* if (temp == 300)
            {
                Console.WriteLine($"Board to {localID}:");
                for (int i = 0; i < min; i++){
                    for (int j = 0; j < 2; j++){
                        Console.Write($"UpdateBoard send: {SortedRanking[i,j]} ");                 
                    }
                    Console.WriteLine();
                }
                temp = 0;
            }
            else temp++;*/
           // Console.WriteLine($"UpdateBoard playercount = {playercount}");
        }
        public static void UpdateRanking(Packet _packet, int localID)
        {
            int myranking = 0;
            //Console.WriteLine($"UpdateRanking playercount = {playercount}");
            for (int i = 0; i < playercount; i++)
            {
                if (SortedRanking[i,0] == localID)
                {
                    myranking = i + 1;
                    _packet.Write(myranking);
                    //Console.WriteLine($"UpdateRanking send: {myranking}");
                    break;
                }
            }
        }
        public static void UpdateAttacked(Packet _packet, int localID)
        {
            bool[] attackfromwho = new bool[10];
            int j = 0;
            for (int i = 1; i <= Constants.Max_player_num&&j < listCnt; i++)
            {
                if (i == AttackedList[j]) // i = id
                {
                    attackfromwho[i-1] = true;
                    j++;
                    Console.WriteLine($"{localID} be attacked by {i}");
                }
            }
            _packet.Write(attackfromwho);
           // Console.WriteLine("UpdateAttacked send: ");
           /* for (int i = 0; i < 10; i++){
                Console.Write($"{attackfromwho[i]} ");
            }*/
        //    /Console.WriteLine();
        }
    }
}
