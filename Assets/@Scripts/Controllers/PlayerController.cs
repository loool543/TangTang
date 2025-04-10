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
        UpdateInput();
        MovePlayer();
    }


    //DeviceSimulator에서는 먹통이다 핸드폰에는 키보드가 없으니까
    void UpdateInput() //키보드 입력을 받아서 Dir을 바꿔주는 함수
    {
        Vector2 moveDir = Vector2.zero;

        //if else가 아니니까 동시에 적용이 가능하다!
        if (Input.GetKey(KeyCode.W))
            moveDir.y += 1;
        if (Input.GetKey(KeyCode.S))
            moveDir.y -= 1;
        if (Input.GetKey(KeyCode.A))
            moveDir.x -= 1;
        if (Input.GetKey(KeyCode.D))
            moveDir.x += 1;

        _moveDir = moveDir.normalized;
    }//애는 어차피 나중에 UI로 대체


    void MovePlayer()
    {
        Vector3 dir = _moveDir * _speed *Time.deltaTime;
        transform.position += dir;
    }
}
