﻿using System;
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
        
        public static void Update()
        {
            Ranking(); // Get all clients' data and count ranking

            foreach (Client _client_local in Server.clients.Values)
            {
                Attack(_client_local.id);  // Get all clients' data and make attack list
                
                ServerSend.SendUpdata(_client_local);
            }

            ThreadManager.UpdateMain();
        }

        public static void Ranking()
        {
            foreach (Client _client in Server.clients.Values)
            {
                if (_client.player != null)
                {
                    ranking.Add(_client.player.id, _client.player.points);
                }
            }
            //SortedRanking.Clear();
            int count = 0;
            foreach (KeyValuePair<int, int> author in ranking.OrderByDescending(key => key.Value))  
            {  
                SortedRanking[count,0] = author.Key;
                SortedRanking[count,1] = author.Value;
                count++;
                //Console.WriteLine("Key: {0}, Value: {1}", author.Key, author.Value);  
            }  
        }

        public static void Attack(int localID)
        {
            AttackedList.Clear();
            foreach (Client _client in Server.clients.Values)
            {
                if ((_client.player != null)&&(_client.player.attackID == localID))
                {
                    AttackedList.Add(_client.id);
                }
            }
            AttackedList.Sort();
        }

        public static void UpdateBoard(Packet _packet, int localID)
        {
            for (int i = 0; i < 5; i++){
                for (int j = 0; j < 2; j++){
                    _packet.Write(SortedRanking[i,j]);
                }
            }
        }
        public static void UpdateRanking(Packet _packet, int localID)
        {
            int myranking = 0;
            for (int i = 0; i < Constants.Max_player_num; i++)
            {
                if (SortedRanking[i,0] == localID)
                {
                    myranking = i + 1;
                    _packet.Write(myranking);
                    break;
                }
            }
        }
        public static void UpdateAttacked(Packet _packet, int localID)
        {
            bool[] attackfromwho = new bool[10];
            int j = 0;
            for (int i = 1; i < Constants.Max_player_num; i++)
            {
                if (i == AttackedList[j]) // i = id
                {
                    attackfromwho[i-1] = true;
                    j++;
                }
            }
            _packet.Write(attackfromwho);
        }
    }
}


/*
        public static void Update()
        {
            Ranking();
            foreach (Client _client in Server.clients.Values)
            {
                if (_client.player != null)
                {
                    _client.player.Update();
                }
            }

            ThreadManager.UpdateMain();
        }
*/