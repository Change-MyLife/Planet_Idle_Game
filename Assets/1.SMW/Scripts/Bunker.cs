using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour
{
    public DrawradiusAround DrawradiusAround;

    BunkerData _data;
    CircleCollider2D _circleCollider;

    Queue<Enemy> Q_enemys = new Queue<Enemy>();
    Enemy _target;

    float _time = 0f;

    private void Start()
    {
        Q_enemys.Clear();

        _circleCollider = GetComponent<CircleCollider2D>();

        Bind();
    }

    void Bind()
    {
        _data = DataManager.instance.Bunker;
        SetAttackRange();
    }

    void Update()
    {
        Attack();
    }

    /// <summary>
    /// 플레이어 사망
    /// </summary>
    void Die()
    {
        Debug.Log("DIE");
    }

    // 공격
    void Attack()
    {
        if (_time < 1)
        {
            _time += Time.deltaTime;
        }
        if (_time >= 1 / _data.Status.fireRate)
        {
            _time = 0f;

            if (_target == null && Q_enemys.Count > 0)
            {
                _target = Q_enemys.Dequeue();
            }

            if (_target != null)
            {
                if(CalculateProbability(_data.Status.criticalProbability))
                {
                    _target.UnderAttack(_data.Status.damage * _data.Status.criticalDamage);
                }
                else
                {
                    _target.UnderAttack(_data.Status.damage);
                }
            }
        }
    }

    // 공격받음
    public void UnderAttack(float _dmg)
    {
        if(!CalculateProbability(_data.Status.evasionRate))
        {
            DataManager.instance.Bunker.SetHp(_dmg - _data.Status.defense);
        }

        if (_data.Status.hp <= 0)
        {
            Die();
        }
    }

    // 확률계산기
    bool CalculateProbability(float _probability)
    {
        float n = Random.Range(0.00f, 100.00f);
        n = Mathf.Floor(n * 100f) / 100f;

        if(n <= _probability)
        {
            // Critical
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Q_enemys.Enqueue(collision.gameObject.transform.GetComponent<Enemy>());
        }
    }

    public void SetAttackRange()
    {
        _circleCollider.radius = _data.Status.attackRange;
        DrawradiusAround.ChangeRadius(_data.Status.attackRange);
    }

    public void DestoryTarget()
    {
        _target = null;
    }
}
