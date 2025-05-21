using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : CreatureController
{
    public override bool Init()
    {
        if (base.Init())
            return false;

        //TODO
        ObjectType = Define.ObjectType.Monster;

        return true;
    }

    void FixedUpdate()
    {
        PlayerController pc = Managers.Object.Player;
        if (pc == null)
            return;

        Vector3 dir = pc.transform.position - transform.position;

        Vector3 newPos = transform.position + dir.normalized * Time.deltaTime * _speed;
        GetComponent<Rigidbody2D>().MovePosition(newPos);

        GetComponent<SpriteRenderer>().flipX = dir.x > 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerController target = collision.gameObject.GetComponent<PlayerController>();
        if (target == null)
            return;

        if (_coDotDamage != null)
            StopCoroutine(_coDotDamage);

        _coDotDamage = StartCoroutine(CoStartDotDamage(target));
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        PlayerController target = collision.gameObject.GetComponent<PlayerController>();
        if (target == null)
            return;

        if (_coDotDamage != null)
            StopCoroutine(_coDotDamage);

        _coDotDamage = null;
    }


    Coroutine _coDotDamage;
    public IEnumerator CoStartDotDamage(PlayerController target)
    {
        while(true)
        {
            //데미지를 주는 코드
            target.OnDamaged(this, 2); //공격한 사람과 데미지

            yield return new WaitForSeconds(0.1f);
        }
    }


    protected override void Ondead()
    {
        base.Ondead();

        if (_coDotDamage != null)
            StopCoroutine(_coDotDamage);
        _coDotDamage = null;

        //죽을때 보석 스폰
        GemController gc = Managers.Object.Spawn<GemController>(transform.position);


        Managers.Object.Despawn(this);
    }
}
 