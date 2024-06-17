using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour
{
    int Level = 0;

    Queue<Enemy> Q_enemys = new Queue<Enemy>();

    public float attactSpeed = 1f;         // 1sec
    float _attackDmg = 1f;
    float attactRange = 1f;
    float CriticalProbability = 1f;
    float CriticalDamage = 1.5f;

    Enemy _target;

    float _time = 0f;

    private void Start()
    {
        Q_enemys.Clear();
    }

    void Update()
    {
        Attack();
    }

    void Attack()
    {
        if (_time < 1)
        {
            _time += (Time.deltaTime * attactSpeed);
        }
        else if (_time >= 1)
        {
            _time = 0f;
            if (Q_enemys.Count > 0)
            {
                if (_target != null)
                {
                    _target.UnderAttack(_attackDmg);
                }
                else
                {
                    _target = Q_enemys.Dequeue();
                    _target.UnderAttack(_attackDmg);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            Q_enemys.Enqueue(collision.gameObject.transform.GetComponent<Enemy>());
        }
    }
}
