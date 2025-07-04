using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballSkill : RepeatSkill
{
    public FireballSkill()
    {

    }

    protected override void DoSkillJob()
    {
        if (Managers.Game.Player == null)
            return;

        Vector3 spawnpos = Managers.Game.Player.FireSocket;
        Vector3 dir = Managers.Game.Player.ShootDir;

        GenerateProjectile(1, Owner, spawnpos, dir, Vector3.zero);
    }
}
