using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer
{
    class ServerSend
    {
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        private static void SendUDPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].udp.SendData(_packet);
        }

        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }
        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].tcp.SendData(_packet);
                }
            }
        }

        private static void SendUDPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].udp.SendData(_packet);
            }
        }
        private static void SendUDPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].udp.SendData(_packet);
                }
            }
        }

        #region Packets
        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);
                //Console.WriteLine($"Welcome send {_msg}, {_toClient}");

                SendTCPData(_toClient, _packet);
            }
        }

        public static void SpawnPlayer(int _toClient, Player _player)
        {
            using (Packet _packet = new Packet((int)ServerPackets.spawnPlayer))
            {
                _packet.Write(_player.id);
                _packet.Write(_player.username);
                _packet.Write(_player.row);
                _packet.Write(_player.bar_position);
                _packet.Write(_player.ball_position);
                _packet.Write(_player.ball_velocity);
                _packet.Write(_player.frzRow);
                _packet.Write(_player.bricks);
                _packet.Write(_player.attackID);
                _packet.Write(_player.points);
                _packet.Write(_player.alive);
                _packet.Write(_player.ballactive);
                //Console.WriteLine($"SpawnPlayer send {_player.id}");

                SendTCPData(_toClient, _packet);
            }
        }
        public static void RemotePlayer(Packet _packet, Player _player)
        {
            _packet.Write(_player.id);
            _packet.Write(_player.row);
            _packet.Write(_player.bar_position);
            _packet.Write(_player.ball_position);
            _packet.Write(_player.ball_velocity);
            _packet.Write(_player.frzRow);
            _packet.Write(_player.bricks);
            _packet.Write(_player.alive);
            _packet.Write(_player.ballactive);
            //Console.WriteLine($"RemotePlayer send {_player.id}");

            //SendUDPData(_toClient, _packet);
        }
        public static void SendUpdata(Client _client_local)
        {
            using (Packet _packet = new Packet((int)ServerPackets.sendupdata))
            {
                foreach (Client _client_remotes in Server.clients.Values)
                {
                    if (_client_remotes.id != _client_local.id)
                    {
                        if ((_client_local.player != null)&&(_client_remotes.player != null))
                        {
                            RemotePlayer(_packet, _client_remotes.player); // means: send target's data
                        }
                    }
                }
                GameLogic.UpdateBoard(_packet, _client_local.id);
                GameLogic.UpdateRanking(_packet, _client_local.id);
                GameLogic.UpdateAttacked(_packet, _client_local.id);

                SendUDPData(_client_local.id, _packet);
            }
        }
        public static void SendStart(Client _client_local)
        {
            using (Packet _packet = new Packet((int)ServerPackets.startGame))
            {
                SendTCPData(_client_local.id, _packet);
            }
            //Console.WriteLine($"{_client_local.id}:SendStart Called.");
        }
        #endregion
    }
}
