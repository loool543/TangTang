using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonsterController
{
    public override bool Init()
    {
        base.Init();
        CreatureState = Define.CreatureState.Moving;
        Hp = 10000;

        return true;
    }
}
