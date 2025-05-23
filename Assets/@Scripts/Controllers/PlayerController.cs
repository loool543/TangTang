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
        //Temp3 구독신청
        Managers.Game.OnMoveDirChanged += HandleOnMoveDirChanged; //여기에서 함수 1개를 연결해주어야 한다!
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
        //Temp2  매프레임마다 값을 가져오는 방식
        //_moveDir = Managers.Game.MoveDir;

        Vector3 dir = _moveDir * _speed *Time.deltaTime;
        transform.position += dir;
    }

    void CollectEnv()
    {
        float sqrCollectDist = EnvCollectDist * EnvCollectDist; //제곱을 해주면 거리비교를 할 때 제곱근을 구하지 않아도 된다

        List<GemController> gems = Managers.Object.Gems.ToList();//spawn이 되어있는 모든 젬들을 긁어올 수 있다
        foreach(GemController gem in gems)
        {
            Vector3 dir = gem.transform.position - transform.position;
            if( dir.sqrMagnitude <= EnvCollectDist)
            {
                Managers.Game.Gem += 1; //젬을 먹으니까 1 증가
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
        //pooling 실험 하려고 일단 반격해서 몬스터들을 죽이기 위해서! 만 데미지 주기
        CreatureController cc = attacker as CreatureController;
        cc?.OnDamaged(this, 10000);
    }
}
