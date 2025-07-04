using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonsterController
{
    public override bool Init()
    {
        base.Init();

        _animator = GetComponent<Animator>();
        CreatureState = Define.CreatureState.Moving;
		Hp = 10000;

        CreatureState = Define.CreatureState.Skill;
        skills.AddSkill<Move>(transform.position);
        skills.AddSkill<Dash>(transform.position);
        skills.AddSkill<Dash>(transform.position);
        skills.AddSkill<Dash>(transform.position);
        skills.StartNextSequenceSkill(); 

        return true;
    }

    public override void UpdateAnimation()
    {
        switch(CreatureState)
        {
            case Define.CreatureState.Idle:
                _animator.Play("Idle");
                break;
            case Define.CreatureState.Moving:
                _animator.Play("Moving");
                break;
            case Define.CreatureState.Skill:
                //_animator.Play("Attack");
                break;
            case Define.CreatureState.Dead:
                _animator.Play("Death");
                break;

        }
    }

    //protected override void UpdateSkill()
    //{
    //    if (_coWait == null)
    //        CreatureState = Define.CreatureState.Moving;
    //}

    protected override void UpdateDead()
    {
        if (_coWait == null)
            Managers.Object.Despawn(this);
    }

    #region Wait Coroutine
    Coroutine _coWait;

    void Wait(float waitSeconds)
    {
        if (_coWait != null)
            StopCoroutine(_coWait);
        _coWait = StartCoroutine(CoStartWait(waitSeconds));
    }

    IEnumerator CoStartWait(float waitSeconds)
    {
        yield return new WaitForSeconds(waitSeconds);
        _coWait = null;
    }

    #endregion


    public override void OnDamaged(BaseController attacker, int damage)
    {
        base.OnDamaged(attacker, damage);
    }

    protected override void Ondead()
    {
        CreatureState = Define.CreatureState.Dead;
        Wait(2.0f);
    }
}
