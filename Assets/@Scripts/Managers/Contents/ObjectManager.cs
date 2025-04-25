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
    //        //���߿��� �ϵ��ڵ��� �ƴ϶� �����ͽ�Ʈ���� �ش� templateid�� ���ø��� �ҷ������

    //        go.name = "Player";

    //        PlayerController pc = go.GetorAddComponent<PlayerController>();
    //        Player = pc;

    //        return pc as T;
    //    }



    //}

}
