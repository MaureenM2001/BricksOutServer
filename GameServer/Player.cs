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
        public uint frzRow;
        public uint[] bricks = new uint[5];
        public bool alive;
        public int points = 0;
        public int attackID = -1;

        //////// Not Using /////////
        /*        
        public List attackedfrom; // id
        public bool attackstate; // 0: no, 1: yes
        public Vector2 ranking; // <username, points> 
        public bool start; // 0: wait, 1: Start

        private float moveSpeed = 5f / Constants.TICKS_PER_SEC;
        */

        public Player(int _id, string _username, float _spawnbarpos, Vector2 _spawnballpos, Vector2 _spawnballv, uint _spawnfrzRow, uint[] _spawnbrickpos, int _spawnpoints, bool _spawnalive) //, Quaternion _rotation)
        {
            id = _id;
            username = _username;
            bar_position = _spawnbarpos;
            ball_position = _spawnballpos;
            ball_velocity = _spawnballv;
            frzRow = _spawnfrzRow;
            bricks = _spawnbrickpos;
            points = _spawnpoints;
            alive = _spawnalive;
            //rotation = Quaternion.Identity;
        }


        public void SetInput(float _newbarpos, Vector2 _newballpos, Vector2 _newballv, uint _newfrzRow, uint[] _newbrickpos, int _newpoints, bool _newalive) //, Quaternion _rotation)
        {
            bar_position = _newbarpos;
            ball_position = _newballpos;
            ball_velocity = _newballv;
            frzRow = _newfrzRow;
            bricks = _newbrickpos;
            points = _newpoints;
            alive = _newalive;
        }
    }
}
