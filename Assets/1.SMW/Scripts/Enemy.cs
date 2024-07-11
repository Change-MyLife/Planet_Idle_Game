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
    public float _hp;
    public float _damage;
    public float Speed;

    [Header("ETC")]
    public GameObject Effect;
    bool isAttack;

    private void Start()
    {
        _bunker = FindFirstObjectByType<Bunker>();
    }

    public void SetValue(float hp, float damage)
    {
        _hp = hp;
        _damage = damage;
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

    // 공격받음
    public void UnderAttack(float _dmg)
    {
        Effect.SetActive(true);
        Invoke("OffEffect", 0.1f);

        _hp -= _dmg;
        if (_hp <= 0)
        {
            _enemySpawn.DeCountEnemy();
            GameManager.Instance.AddScore();
            Destroy(gameObject);
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

    // 공격
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
