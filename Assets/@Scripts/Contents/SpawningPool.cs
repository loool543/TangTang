using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    // 리스폰 주기는?
    // 몬스터 최대 개수는?
    // 스톱?

    float _spawnInterval = 1.0f;
    int _max_MonsterCount = 200;
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

        //
        Vector3 randPos = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
        MonsterController mc = Managers.Object.Spawn<MonsterController>(randPos, Random.Range(0, 2));
    }

}
