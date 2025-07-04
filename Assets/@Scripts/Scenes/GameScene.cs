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

    SpawningPool _spawningPool;

    Define.StageType _stageType;
    public Define.StageType StageType
    {
        get { return _stageType; }
        set 
        { 
            _stageType = value; 

            if(_spawningPool != null)
            {
                switch(value)
                {
                    case Define.StageType.Normal:
                        _spawningPool.stopped = false;
                        break;
                    case Define.StageType.Boss:
                        _spawningPool.stopped = true;
                        break;
                }
            }

        }
    }

    void StartLoaded()
    {
        Managers.Data.Init();

        Managers.UI.ShowSceneUI<UI_GameScene>();

        _spawningPool = gameObject.AddComponent<SpawningPool>();

        var player = Managers.Object.Spawn<PlayerController>(Vector3.zero);
        //GameObject go = new GameObject() { name = "@Monsters" };

        for (int i = 0; i < 10; i++)
        {
            Vector3 randPos = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
            MonsterController mc = Managers.Object.Spawn<MonsterController>(randPos, 1+Random.Range(0, 2));
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
        Managers.Game.OnGemCountChanged -= HandleOnGemCountChanged;
        Managers.Game.OnGemCountChanged += HandleOnGemCountChanged;

        Managers.Game.OnKillCountChanged -= HandleOnKillCountChanged;
        Managers.Game.OnKillCountChanged += HandleOnKillCountChanged;

    }

    int _collectedGemCount = 0;
    int _remainingTotalGemCount = 10;
    public void HandleOnGemCountChanged(int gemCount)
    {
        _collectedGemCount++;

        if (_collectedGemCount == _remainingTotalGemCount)
        {
            Managers.UI.ShowPopup<UI_SkillSelectPopup>();
            _collectedGemCount = 0;
            _remainingTotalGemCount *= 2;
        }

        Managers.UI.GetSceneUI<UI_GameScene>().SetGemCountRatio((float)_collectedGemCount / _remainingTotalGemCount);
    }

    public void HandleOnKillCountChanged(int killCount)
    {
        Managers.UI.GetSceneUI<UI_GameScene>().SetKillCount(killCount);

        if(killCount == 30)
        {
            //BOSS
            StageType = Define.StageType.Boss;

            Managers.Object.DespawnAllMonsters();

            Vector2 spawnPos = Utils.GenerateMonsterSpawnPosition(Managers.Game.Player.transform.position, 5, 10);

            Managers.Object.Spawn<MonsterController>(spawnPos, Define.BOSS_ID);
        }
    }

    private void OnDestroy()
    {
        if (Managers.Game != null)
            Managers.Game.OnGemCountChanged -= HandleOnGemCountChanged;
    }
}