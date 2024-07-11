using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [Serializable]
    public class BunkerData
    {
        public int id;
        public string name;
        public BunkerUpgrade count;
        public BunkerValue original;
        public BunkerValue increase;

        BunkerValue _status;
        public BunkerValue Status { get { return _status; } }
        public virtual void SetValue()
        {
            _status.damage = original.damage + (increase.damage * count.damage);
            _status.fireRate = original.fireRate + (increase.fireRate * count.fireRate);
            _status.attackRange = original.attackRange + (increase.attackRange * count.attackRange);
            _status.criticalProbability = original.criticalProbability + (increase.criticalProbability * count.criticalProbability);
            _status.criticalDamage = original.criticalDamage + (increase.criticalDamage * count.criticalDamage);
            _status.hp = original.hp + (increase.hp * count.hp);
            _status.defense = original.defense + (increase.defense * count.defense);
            _status.evasionRate = original.evasionRate + (increase.evasionRate * count.evasionRate);
        }
        public virtual void IncreaseDamage()
        {
            count.damage++;
            _status.damage += increase.damage;
        }
        public virtual void IncreaseFireRate()
        {
            count.fireRate++;
            _status.fireRate += increase.fireRate;
        }
        public virtual void IncreaseAttackRange()
        {
            count.attackRange++;
            _status.attackRange += increase.attackRange;
        }
        public virtual void SetHp(float hp)
        {
            _status.hp = hp;
        }
    }

    [Serializable]
    public class WaveData
    {
        public int id;
        public string name;
        public int count;
        public WaveValue original;
        public WaveValue increase;

        WaveValue _wave;
        public WaveValue Wave { get { return _wave; } }
        public virtual void SetValue()
        {
            _wave.hp = original.hp + (count * increase.hp);
            _wave.damage = original.damage + (count * increase.damage);
            _wave.maxEnemy = original.maxEnemy + (count * increase.maxEnemy);
            _wave.spawnTime = original.spawnTime + (count * increase.spawnTime);
            _wave.enemy = original.enemy + (count * increase.enemy);
        }

        public virtual void NextWave()
        {
            count++;
            SetValue();
        }
    }
    #region Struct

    [Serializable]
    public struct BunkerValue
    {
        public float damage;
        public float fireRate;
        public float attackRange;
        public float criticalProbability;
        public float criticalDamage;
        public float hp;
        public float defense;
        public float evasionRate;
    }

    [Serializable]
    public struct WaveValue
    {
        public float hp;
        public float damage;
        public int maxEnemy;
        public float spawnTime;
        public int enemy;
    }

    [Serializable]
    public struct BunkerUpgrade
    {
        public int damage;
        public int fireRate;
        public int attackRange;
        public int criticalProbability;
        public int criticalDamage;
        public int hp;
        public int defense;
        public int evasionRate;
    }

    #endregion
}
