using Data;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Bunker _bunker;
    public EnemySpawn _enemySpawn;

    [Header("Status")]
    int _index;
    public float _hp;
    public float _damage;
    public float Speed;

    EnemyType.Type _type;

    [Header("ETC")]
    public GameObject Effect;
    bool isAttack;

    private void Start()
    {
        _bunker = FindFirstObjectByType<Bunker>();
    }

    private void OnEnable()
    {
        isAttack = false;
    }

    // �븻 �� ����
    public void SetNormal(int index, float hp, float damage)
    {
        _type = EnemyType.Type.NORMAL;

        _index = index;
        _hp = hp;
        _damage = damage;
    }

    // ���� �� ����
    public void SetEpic(int index)
    {
        _type = EnemyType.Type.EPIC;
        _index = index;
    }

    private void Update()
    {
        if (!isAttack)
        {
            Vector3 v;
            v = Vector2.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * Speed);
            transform.localPosition = new Vector3(v.x, v.y, transform.localPosition.z);
        }
    }

    // ���ݹ���
    public void UnderAttack(float _dmg)
    {
        Effect.SetActive(true);
        Invoke("OffEffect", 0.1f);

        _hp -= _dmg;
        if (_hp <= 0)
        {
            Die();
        }
    }

    // �׾��� ���
    public void Die()
    {
        switch (_type)
        {
            case EnemyType.Type.NORMAL:
                {
                    GameManager.Instance.AddCoin(1);
                    _enemySpawn.DeCountNormalEnemy(gameObject, _index);
                    _bunker.DestoryTarget();
                }
                break;
            case EnemyType.Type.EPIC:
                {
                    GameManager.Instance.AddCoin(10);
                    _enemySpawn.DeCountEpicEnemy(gameObject, _index);
                    _bunker.DestoryTarget();
                }
                break;
        }
    }

    void OffEffect()
    {
        Effect.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isAttack = true;
            StartCoroutine(StartAttact());
        }
    }

    // ����
    IEnumerator StartAttact()
    {
        yield return null;
        while (true)
        {
            _bunker.UnderAttack(_damage);
            yield return new WaitForSeconds(1f);
        }
    }
}
