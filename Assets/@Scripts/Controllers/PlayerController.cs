using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : CreatureController
{
    Vector2 _moveDir = Vector2.zero;

    float EnvCollectDist { get; set; } = 1.0f;

    [SerializeField]
    Transform _indicator;

    [SerializeField]
    Transform _fireSocket;

    public Transform Indicator { get { return _indicator; } }
    public Vector3 FireSocket { get { return _fireSocket.position; } }
    public Vector3 ShootDir { get { return (_fireSocket.position - _indicator.position).normalized; } }


    public Vector2 MoveDir
    {
        get { return _moveDir; }
        set { _moveDir = value.normalized; }
    }

    public override bool Init()
    { 
        if(base.Init() == false)
            return false;

        _speed = 5.0f;
        Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged;

        //TODO

        return true;
    }

    private void OnDestroy()
    {
        if(Managers.Game != null)
            Managers.Game.OnMoveDirChanged -= HandleOnMoveDirChanged;
    }

    void HandleOnMoveDirChanged(Vector2 dir)
    {
        _moveDir = dir;
    }

    void Update()
    {
       //UpdateInput();
        MovePlayer();
        CollectEnv();
    }



    void MovePlayer()
    {
        //Temp2  �������Ӹ��� ���� �������� ���
        //_moveDir = Managers.Game.MoveDir;

        Vector3 dir = _moveDir * _speed *Time.deltaTime;
        transform.position += dir;

        if (_moveDir != Vector2.zero)
        {
            _indicator.eulerAngles = new Vector3(0, 0, Mathf.Atan2(-dir.x, dir.y) * 180 / Mathf.PI); // -dir.x, dir.y�� �־��ָ� z���� �������� ȸ���Ѵ�
        }

        GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    void CollectEnv()
    {
        float sqrCollectDist = EnvCollectDist * EnvCollectDist; //������ ���ָ� �Ÿ��񱳸� �� �� �������� ������ �ʾƵ� �ȴ�

        var findGems = GameObject.Find("@Grid").GetComponent<GridController>().GatherObjects(transform.position, EnvCollectDist + 0.5f);

        foreach (var go in findGems)
        {
            GemController gem = go.GetComponent<GemController>();
            Vector3 dir = gem.transform.position - transform.position;
            if( dir.sqrMagnitude <= EnvCollectDist)
            {
                Managers.Game.Gem += 1; //���� �����ϱ� 1 ����
                Managers.Object.Despawn(gem);
            }
        }

    }


    public override void OnDamaged(BaseController attacker, int damage)
    {
        base.OnDamaged(attacker, damage);

        Debug.Log($"OnDamaged !  {Hp}");

        //Temp
        //pooling ���� �Ϸ��� �ϴ� �ݰ��ؼ� ���͵��� ���̱� ���ؼ�! �� ������ �ֱ�
        CreatureController cc = attacker as CreatureController;
        cc?.OnDamaged(this, 10000);
    }

}
