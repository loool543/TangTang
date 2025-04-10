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


    //DeviceSimulator������ �����̴� �ڵ������� Ű���尡 �����ϱ�
    void UpdateInput() //Ű���� �Է��� �޾Ƽ� Dir�� �ٲ��ִ� �Լ�
    {
        Vector2 moveDir = Vector2.zero;

        //if else�� �ƴϴϱ� ���ÿ� ������ �����ϴ�!
        if (Input.GetKey(KeyCode.W))
            moveDir.y += 1;
        if (Input.GetKey(KeyCode.S))
            moveDir.y -= 1;
        if (Input.GetKey(KeyCode.A))
            moveDir.x -= 1;
        if (Input.GetKey(KeyCode.D))
            moveDir.x += 1;

        _moveDir = moveDir.normalized;
    }//�ִ� ������ ���߿� UI�� ��ü


    void MovePlayer()
    {
        Vector3 dir = _moveDir * _speed *Time.deltaTime;
        transform.position += dir;
    }
}
