using NUnit.Framework.Constraints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _enemyspawn : MonoBehaviour
{
    public GameObject EnemyPrefab;

    public CircleCollider2D MinCircle;
    public CircleCollider2D MaxCircle;
    float _minDistance;
    float _maxDistance;
    int _sortLayer = 10;

    public float _spawnTime;
    float _time = 0f;

    private void Start()
    {
        _minDistance = MinCircle.radius;
        _maxDistance = MaxCircle.radius;
    }

    private void Update()
    {
        if(_time < _spawnTime)
        {
            _time += Time.deltaTime;
        }
        else if(_time >= _spawnTime)
        {
            _time = 0f;
            GameObject enemy = Instantiate(EnemyPrefab, transform, false);
            enemy.transform.localPosition = RandomPosition();
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
