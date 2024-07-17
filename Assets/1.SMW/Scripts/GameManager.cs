using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    CreditType _credit;

    public Text Text_Coin;
    public Text Text_Cash;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _credit = DataManager.instance.Credit;
        SetCredit();
    }

    void SetCredit()
    {
        Text_Coin.text = _credit.coin.ToString();
        Text_Cash.text = _credit.cash.ToString();
    }

    // 코인 추가
    public void AddCoin(int _coin)
    {
        _credit.AddCoin(_coin);
        Text_Coin.text = _credit.coin.ToString();
    }

    public bool UpgradeCoin(float _cost)
    {
        if(_credit.coin >= _cost)
        {
            _credit.coin -= _cost;
            return true;
        }
        return false;
    }
}
