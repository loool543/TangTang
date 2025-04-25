using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager 
{
    public PlayerController Player { get; private set; }
    public HashSet<MonsterController> Monsters { get; } = new HashSet<MonsterController>();
    public HashSet<ProjectileController> Projectiles { get; } = new HashSet<ProjectileController>();

    //public T Spawn<T>(int templateID) where T : BaseController
    //{
    //    System.Type type = typeof(T);

    //    if(type == typeof(PlayerController))
    //    {
    //        GameObject go = Managers.Resource.Instantiate("Slime_01.prefab");
    //        //나중에는 하드코딩이 아니라 데이터시트에서 해당 templateid의 템플릿을 불러오면됨

    //        go.name = "Player";

    //        PlayerController pc = go.GetorAddComponent<PlayerController>();
    //        Player = pc;

    //        return pc as T;
    //    }



    //}

}
