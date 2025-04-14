using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Vector2 _moveDir = Vector2.zero;
    float _speed = 5.0f;

    public Vector2 MoveDir
    {
        get { return _moveDir; }
        set { _moveDir = value.normalized; }
    }

    void Start()
    {
        
    }

    void Update()
    {
       //UpdateInput();
        MovePlayer();
    }



    void MovePlayer()
    {
        //Temp2
        _moveDir = Managers.Game.MoveDir;


        Vector3 dir = _moveDir * _speed *Time.deltaTime;
        transform.position += dir;
    }
}
