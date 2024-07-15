using Data;
using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    public WaveData _data;
    public List<GameObject> Enemies;
    private List<GameObject> _normalEnemies = new List<GameObject>();
    private List<GameObject> _epicEnemies = new List<GameObject>();

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

    int _normalIndex = 0;
    int _epicIndex = 0;

    private void Start()
    {
        _data = DataManager.instance.Wave;
        Text_Wave.text = _data.count.ToString();

        _minDistance = MinCircle.radius;
        _maxDistance = MaxCircle.radius;

        BindEnemy();
    }

    void BindEnemy()
    {
        foreach (GameObject enemy in Enemies)
        {
            if(enemy.layer == LayerMask.NameToLayer("NormalEnemy"))
            {
                _normalEnemies.Add(enemy);
            }
            else if(enemy.layer == LayerMask.NameToLayer("EpicEnemy"))
            {
                _epicEnemies.Add(enemy);
            }
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

            if (_normalEnemies.Count == 0) return;
            //spawn enemy
            GameObject enemy = Instantiate(_normalEnemies[_normalIndex], transform, false);
            enemy.transform.localPosition = RandomPosition();
            enemy.GetComponent<Enemy>().SetValue(_data.Wave.hp, _data.Wave.damage);
            enemy.GetComponent<Enemy>()._enemySpawn = transform.GetComponent<EnemySpawn>();
            _enemyCount++;
        }
    }

    void EpicEnmeySpawn()
    {
        if(_epicEnemies.Count == 0) return;
        GameObject enemy = Instantiate(_epicEnemies[_epicIndex], transform, false);
        enemy.transform.localPosition = RandomPosition();
        enemy.GetComponent<Enemy>()._enemySpawn = transform.GetComponent<EnemySpawn>();
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
