using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningPool : MonoBehaviour
{
    // ������ �ֱ��?
    // ���� �ִ� ������?
    // ����?

    float _spawnInterval = 0.1f;
    int _max_MonsterCount = 100;
    Coroutine _coUpdateSpawningPool;

    public bool stopped { get; set; } = false;
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
        if (stopped)
            return; 

        int monsterCount = Managers.Object.Monsters.Count;
        if (monsterCount >= _max_MonsterCount)
            return;

        //Data��Ʈ���� ??
        Vector3 randPos = Utils.GenerateMonsterSpawnPosition(Managers.Game.Player.transform.position, 10, 15);
        MonsterController mc = Managers.Object.Spawn<MonsterController>(randPos, 1 + Random.Range(0, 2));

    }

}
