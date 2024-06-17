using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float Hp;
    public float Speed;

    Transform _bunker;

    public GameObject Effect;

    private void Start()
    {
        _bunker = FindFirstObjectByType<Bunker>().transform;
    }

    private void Update()
    {
        Vector3 v;
        v = Vector2.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime * Speed);
        transform.localPosition = new Vector3(v.x, v.y, transform.localPosition.z);
    }

    public void UnderAttack(float _dmg)
    {
        Effect.SetActive(true);
        Invoke("OffEffect", 0.1f);
        
        Hp -= _dmg;
        if (Hp <= 0)
        {
            Destroy(gameObject);
            GameManager.Instance.AddScore();
        }
    }

    void OffEffect()
    {
        Effect.SetActive(false);
    }
}
