using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace GameServer
{
    class Player
    {
        //////// Client Input /////////
        public int id;
        public string username;
        public float bar_position;
        public Vector2 ball_position;
        public Vector2 ball_velocity;
        public int[] bricks = new int[5];
        public int frzRow;
        public bool alive;
        public int points;
        public int attackID;

        //////// Not Using /////////
        /*        
        public List attackedfrom; // id
        public bool attackstate; // 0: no, 1: yes
        public Vector2 ranking; // <username, points> 
        public bool start; // 0: wait, 1: Start

        private float moveSpeed = 5f / Constants.TICKS_PER_SEC;
        */

        public Player(int _id, string _username, float _spawnbarpos, Vector2 _spawnballpos, Vector2 _spawnballv, int[] _spawnbrickpos, int _spawnfrzRow, bool _spawnalive) //, Quaternion _rotation)
        {
            id = _id;
            username = _username;
            bar_position = _spawnbarpos;
            ball_position = _spawnballpos;
            ball_velocity = _spawnballv;
            bricks = _spawnbrickpos;
            frzRow = _spawnfrzRow;
            alive = _spawnalive;
            //rotation = Quaternion.Identity;
        }

        /*
        public void Update()
        {
            ServerSend.RemotePlayer(this);
            /*
            ServerSend.PlayerBarPosition(this);
            ServerSend.PlayerBallPosition(this);
            ServerSend.PlayerBallVelocity(this);
            ServerSend.PlayerBricks(this);
            ServerSend.PlayerFreeze(this);
            ServerSend.PlayerAlive(this);
            
        }*/


        public void SetInput(float _newbarpos, Vector2 _newballpos, Vector2 _newballv, int[] _newbrickpos, int _newfrzRow, bool _newalive) //, Quaternion _rotation)
        {
            bar_position = _newbarpos;
            ball_position = _newballpos;
            ball_velocity = _newballv;
            bricks = _newbrickpos;
            frzRow = _newfrzRow;
            alive = _newalive;
        }
    }
}
