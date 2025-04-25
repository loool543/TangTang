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

    void Update()
    {
        
    }
}
 