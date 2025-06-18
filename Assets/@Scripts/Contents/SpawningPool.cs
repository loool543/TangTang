using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    // 리스폰 주기는?
    // 몬스터 최대 개수는?
    // 스톱?

    float _spawnInterval = 0.1f;
    int _max_MonsterCount = 100;
    Coroutine _coUpdateSpawningPool;

    // Start is called before the first frame update
    void Start()
    {
        _coUpdateSpawningPool = StartCoroutine(CoUpdateSpawningPool());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator CoUpdateSpawningPool()
    {
        while(true) 
        {
            TrySpawn();
            yield return new WaitForSeconds(_spawnInterval);
        }
    }

    void TrySpawn()
    {
        int monsterCount = Managers.Object.Monsters.Count;
        if (monsterCount >= _max_MonsterCount)
            return;

        //Data시트에서 ??
        Vector3 randPos = Utils.GenerateMonsterSpawnPosition(Managers.Game.Player.transform.position, 10, 15);
        MonsterController mc = Managers.Object.Spawn<MonsterController>(randPos, Random.Range(0, 2));

    }

}
