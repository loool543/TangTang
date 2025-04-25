using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : MonoBehaviour
{
    void Start()
    {
        Managers.Resource.LoadAllASync<GameObject>("Prefabs", (key, count, totalCount) =>
        {
            Debug.Log($"{key} {count}/ {totalCount}");

            if(count == totalCount)
            {
                StartLoaded();
            }
        });

    }


    void StartLoaded()
    {

        GameObject player = Managers.Resource.Instantiate("Slime_01.prefab");
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

    void Update()
    {
        
    }
}
 