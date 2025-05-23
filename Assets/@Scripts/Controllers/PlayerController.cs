using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : CreatureController
{
    Vector2 _moveDir = Vector2.zero;
    float _speed = 5.0f;

    float EnvCollectDist { get; set; } = 1.0f;


    public Vector2 MoveDir
    {
        get { return _moveDir; }
        set { _moveDir = value.normalized; }
    }

    void Start()
    { 
        //Temp3 ������û
        Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged; //���⿡�� �Լ� 1���� �������־�� �Ѵ�!
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
    }

    void CollectEnv()
    {
        float sqrCollectDist = EnvCollectDist * EnvCollectDist; //������ ���ָ� �Ÿ��񱳸� �� �� �������� ������ �ʾƵ� �ȴ�

        List<GemController> gems = Managers.Object.Gems.ToList();//spawn�� �Ǿ��ִ� ��� ������ �ܾ�� �� �ִ�
        foreach(GemController gem in gems)
        {
            Vector3 dir = gem.transform.position - transform.position;
            if( dir.sqrMagnitude <= EnvCollectDist)
            {
                Managers.Game.Gem += 1; //���� �����ϱ� 1 ����
                Managers.Object.Despawn(gem);
            }
        }


        var findGems = GameObject.Find("@Grid").GetComponent<GridController>().GatherObjects(transform.position, EnvCollectDist+0.5f);

        Debug.Log($"SearchGems({findGems.Count}) TotalGems({gems.Count})");
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
