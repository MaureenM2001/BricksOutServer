using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer
{
    class ServerHandle
    {
        private static int temp = 0;
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            string _username = _packet.ReadString();

            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} connected successfully and is now player {_fromClient}.");
            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player \"{_username}\" (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }
            Server.clients[_fromClient].SendIntoGame(_username);
            if (_fromClient == Constants.Max_player_num)
            {
                Program.GameStart = true;
            }
        }

        public static void PlayerMovement(int _fromClient, Packet _packet)
        {
            int _newRow = _packet.ReadInt();
            float _newbarpos = _packet.ReadFloat();
            //Console.WriteLine("Get new Bar");
            Vector2 _newballpos = _packet.ReadVector2();
            Vector2 _newballv = _packet.ReadVector2();
            uint[] _newbricks = new uint[Constants.BrickConst];
            uint _newfrzRow = _packet.ReadUInt();
            for (int i = 0; i < Constants.BrickConst; i++){
                _newbricks[i] =  _packet.ReadUInt();
            }
            int _newattack = _packet.ReadInt();
            int _newpoints = _packet.ReadInt();
            bool _newalive = _packet.ReadBool();
            bool _newballactive = _packet.ReadBool();
            /*if (temp == 30)
            {
                Console.WriteLine($"client {_fromClient}: newpoints = {_newpoints}");
                //if (_newalive == false) {
                Console.WriteLine($"client {_fromClient}: {_newalive}");
                //}
                temp = 0;
            }
            else temp++;*/
            //int _newpoints = _packet.ReadInt();
            //Quaternion _rotation = _packet.ReadQuaternion();
            //Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} Set input.");
            Server.clients[_fromClient].player.SetInput(_newRow, _newbarpos, _newballpos, _newballv, _newfrzRow, _newbricks, _newattack, _newpoints, _newalive, _newballactive); //, _rotation);
        }
    }
}
