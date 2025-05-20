using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    void Start()
    {
        Managers.Resource.LoadAllASync<Object>("PreLoad", (key, count, totalCount) =>
        {
            Debug.Log($"{key} {count}/ {totalCount}");

            if(count == totalCount)
            {
                StartLoaded();
            }
        });

    }

    //복습용으로 남겨둠! 
    void StartLoaded2()
    {
        var player = Managers.Resource.Instantiate("Slime_01.prefab");
        player.AddComponent<PlayerController>();

        var _goblin = Managers.Resource.Instantiate("Goblin_01.prefab");
        var _snake = Managers.Resource.Instantiate("Snake_01.prefab");
        var joystick = Managers.Resource.Instantiate("UI_Joystick.prefab");
        joystick.name = "@UI_Joystick";

        GameObject go = new GameObject() { name = "@Monsters" };
        _snake.transform.parent = go.transform;
        _goblin.transform.parent = go.transform;


        var map = Managers.Resource.Instantiate("Map.prefab");
        map.name = "@Map";
        Camera.main.GetComponent<CameraController>().Target = player;
    }

    SpawningPool spawningPool;
    void StartLoaded()
    {
         spawningPool = gameObject.AddComponent<SpawningPool>();

        var player = Managers.Object.Spawn<PlayerController>();
        //GameObject go = new GameObject() { name = "@Monsters" };

        for (int i = 0; i < 10; i++)
        {
            MonsterController mc = Managers.Object.Spawn<MonsterController>(Random.Range(0, 2));
            mc.transform.position = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
            //mc.transform.parent = go.transform;
        }

        var joystick = Managers.Resource.Instantiate("UI_Joystick.prefab");
        joystick.name = "@UI_Joystick";

        var map = Managers.Resource.Instantiate("Map.prefab");
        map.name = "@Map";
        Camera.main.GetComponent<CameraController>().Target = player.gameObject;

        //Data Test
        Managers.Data.Init();

        foreach(var playerData in Managers.Data.PlayerDic.Values)
        {
            Debug.Log($"Lv : {playerData.level}, Hp{playerData.maxHp}");
        }
    }

    void Update()
    {
        
    }
}