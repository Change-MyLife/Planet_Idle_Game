using Data;
using NUnit.Framework.Constraints;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class EnemyType
{
    public int index;
    public string name;
    public GameObject obj;
    public Type type;

    public enum Type
    {
        NORMAL,
        EPIC
    }
}

public class EnemySpawn : MonoBehaviour
{

    WaveData _data;

    public List<EnemyType> list_NormalEnemys = new List<EnemyType>();
    public List<EnemyType> list_EpicEnemys = new List<EnemyType>();

    Dictionary<int, Queue<GameObject>> dic_Enemy = new Dictionary<int, Queue<GameObject>>();

    public CircleCollider2D MinCircle;
    public CircleCollider2D MaxCircle;
    float _minDistance;
    float _maxDistance;
    int _sortLayer = 10;
    float _time = 0f;

    [SerializeField] Text Text_Wave;

    int _spawnCount = 0;
    int _maxSpawnCount = 0;
    int _enemyCount = 0;
    int _waveCount = 0;

    int _normalIndex = 0;       // 노말 몹 번호
    int _epicIndex = 999;         // 에픽 몹 번호

    private void Start()
    {
        _data = DataManager.instance.Wave;
        Text_Wave.text = _data.count.ToString();

        _minDistance = MinCircle.radius;
        _maxDistance = MaxCircle.radius;

        init();
    }

    void init()
    {
        for(int i = 0; i < list_NormalEnemys.Count; i++)
        {
            Queue<GameObject> pool = new Queue<GameObject>();
            dic_Enemy.Add(list_NormalEnemys[i].index, pool);
        }

        for(int j = 0; j < list_EpicEnemys.Count; j++)
        {
            Queue<GameObject> pool = new Queue<GameObject>();
            dic_Enemy.Add(list_EpicEnemys[j].index, pool);
        }
    }

    private void Update()
    {
        if(_time < 1)
        {
            _time += Time.deltaTime * _data.Wave.spawnTime;
        }
        else if(_time >= 1)
        {
            _time = 0f;
            NormalEnmeySpawn();
        }
    }

    // 노말몹 생성
    void NormalEnmeySpawn()
    {
        _spawnCount = _data.Wave.enemy;
        _maxSpawnCount = _data.Wave.maxEnemy;

        for (int i = 0; i < _spawnCount; i++)
        {
            if (_enemyCount >= _maxSpawnCount)
            {
                break;
            }

            EnemyType _type;
            GameObject enemy = null;

            if (dic_Enemy[_normalIndex].Count > 0)
            {
                enemy = dic_Enemy[_normalIndex].Dequeue();
                enemy.gameObject.SetActive(true);
            }
            else
            {
                _type = list_NormalEnemys.FirstOrDefault(item => item.index == _normalIndex);
                enemy = Instantiate(_type.obj, transform, false);
            }
            enemy.transform.localPosition = RandomPosition();
            enemy.GetComponent<Enemy>().SetNormal(_normalIndex, _data.Wave.hp, _data.Wave.damage);
            enemy.GetComponent<Enemy>()._enemySpawn = transform.GetComponent<EnemySpawn>();
            _enemyCount++;
        }
    }

    // 에픽몹 생성
    void EpicEnmeySpawn()
    {
        GameObject enemy;
        if (dic_Enemy[_epicIndex].Count > 0)
        {
            enemy = dic_Enemy[_epicIndex].Dequeue();
            enemy.gameObject.SetActive(true);
        }
        else
        {
            EnemyType _type;
            _type = list_EpicEnemys.FirstOrDefault(item => item.index == _epicIndex);
            enemy = Instantiate(_type.obj, transform, false);
        }
        enemy.transform.localPosition = RandomPosition();
        enemy.GetComponent<Enemy>()._enemySpawn = transform.GetComponent<EnemySpawn>();
        enemy.GetComponent<Enemy>().SetEpic(_epicIndex);
    }

    // 다음 웨이브
    void NextWave()
    {
        _data.NextWave();
        Text_Wave.text = _data.count.ToString();

        // 10라운드
        if(_data.count % 10 == 0 )
        {
            EpicEnmeySpawn();
        }
    }

    // 노말 적 사망 시
    public void DeCountNormalEnemy(GameObject _enemy, int _index)
    {
        _enemyCount--;
        _waveCount++;

        if(_waveCount >= _spawnCount)
        {
            _waveCount = 0;
            NextWave();
        }

        // 풀에 반납
        dic_Enemy[_index].Enqueue(_enemy);
        _enemy.SetActive(false);
    }


    // 에픽 적 사망 시
    public void DeCountEpicEnemy(GameObject _enemy, int _index)
    {
        dic_Enemy[_index].Enqueue(_enemy);
        _enemy.SetActive(false);
    }

    Vector3 RandomPosition()
    {
        Vector3 v;

        float distance = UnityEngine.Random.Range(_minDistance, _maxDistance);
        float angle = UnityEngine.Random.Range(0, 360);
        float rad = angle * Mathf.Deg2Rad;
        v = new Vector3(Mathf.Cos(rad) * distance, Mathf.Sin(rad) * distance, _sortLayer);

        return v;
    }
}
