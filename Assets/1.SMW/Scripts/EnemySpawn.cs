using Data;
using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    public WaveData _data;
    public GameObject EnemyPrefab;

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

    private void Start()
    {
        _data = DataManager.instance.Wave;
        Text_Wave.text = _data.count.ToString();

        _minDistance = MinCircle.radius;
        _maxDistance = MaxCircle.radius;
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
            Spawn();
        }
    }

    void Spawn()
    {
        _spawnCount = _data.Wave.enemy;
        _maxSpawnCount = _data.Wave.maxEnemy;

        for(int i = 0; i < _spawnCount; i++)
        {
            if(_enemyCount >= _maxSpawnCount)
            {
                break;
            }

            //spawn enemy
            GameObject enemy = Instantiate(EnemyPrefab, transform, false);
            enemy.transform.localPosition = RandomPosition();
            enemy.GetComponent<Enemy>().SetValue(_data.Wave.hp, _data.Wave.damage);
            enemy.GetComponent<Enemy>()._enemySpawn = transform.GetComponent<EnemySpawn>();
            _enemyCount++;
        }
    }

    // 다음 웨이브
    void NextWave()
    {
        _data.NextWave();
        Text_Wave.text = _data.count.ToString();
    }

    public void DeCountEnemy()
    {
        _enemyCount--;
        _waveCount++;

        if(_waveCount >= _spawnCount)
        {
            _waveCount = 0;
            NextWave();
        }
    }

    Vector3 RandomPosition()
    {
        Vector3 v;

        float distance = Random.Range(_minDistance, _maxDistance);
        float angle = Random.Range(0, 360);
        float rad = angle * Mathf.Deg2Rad;
        v = new Vector3(Mathf.Cos(rad) * distance, Mathf.Sin(rad) * distance, _sortLayer);

        return v;
    }
}
