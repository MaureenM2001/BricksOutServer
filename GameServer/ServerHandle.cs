﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

namespace GameServer
{
    class ServerHandle
    {
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
        }

        public static void PlayerMovement(int _fromClient, Packet _packet)
        {
            float _newbarpos = _packet.ReadFloat();
            Vector2 _newballpos = _packet.ReadVector2();
            Vector2 _newballv = _packet.ReadVector2();
            int[] _newbricks = new int[Constants.BrickConst];
            for (int i = 0; i < Constants.BrickConst; i++){
                _newbricks[i] =  _packet.ReadInt();
            }
            int _newfrzRow = _packet.ReadInt();
            bool _newalive = _packet.ReadBool();
            //int _newpoints = _packet.ReadInt();
            //Quaternion _rotation = _packet.ReadQuaternion();

            Server.clients[_fromClient].player.SetInput(_newbarpos, _newballpos, _newballv, _newbricks, _newfrzRow, _newalive); //, _rotation);
        }
    }
}
