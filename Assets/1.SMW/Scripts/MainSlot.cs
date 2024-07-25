using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSlot : MonoBehaviour
{
    Bunker _bunker;

    [Header("Button")]
    [SerializeField] Button Slot_AttackDmg;
    [SerializeField] Button Slot_AttackSpeed;
    [SerializeField] Button Slot_CriticalProbability;
    [SerializeField] Button Slot_AttackRange;

    [Header("Text")]
    public Text Text_AttackDmg;
    public Text Text_AttackSpeed;
    public Text Text_AttackRange;
    public Text Text_CriticalProbability;

    private void Start()
    {
        _bunker = FindFirstObjectByType<Bunker>();

        Bind();
    }

    void Bind()
    {
        Slot_AttackDmg.onClick.AddListener(OnClickAttackDmgButton);
        Slot_AttackSpeed.onClick.AddListener(OnClickAttackSpeedButton);
        Slot_AttackRange.onClick.AddListener(OnClickAttackRangeButton);
        Slot_CriticalProbability.onClick.AddListener (OnClickCriticalProbabilityButton);
    }

    void OnClickAttackDmgButton()
    {
        if(GameManager.Instance.UpgradeCoin(DataManager.instance.Bunker.Cost.damage))
        {
            DataManager.instance.Bunker.IncreaseDamage();
            DataManager.instance.Bunker.SetCost();
            Text_AttackDmg.text = "dmg : " + DataManager.instance.Bunker.Cost.damage;
            GameManager.Instance.UpdateCredit();
        }
    }

    void OnClickAttackRangeButton()
    {
        if(GameManager.Instance.UpgradeCoin(DataManager.instance.Bunker.Cost.attackRange))
        {
            DataManager.instance.Bunker.IncreaseAttackRange();
            _bunker.SetAttackRange();
            DataManager.instance.Bunker.SetCost();
            Text_AttackRange.text = "range : " + DataManager.instance.Bunker.Cost.attackRange;
            GameManager.Instance.UpdateCredit();
        }
    }

    
    void OnClickAttackSpeedButton()
    {
        if(GameManager.Instance.UpgradeCoin(DataManager.instance.Bunker.Cost.fireRate))
        {
            DataManager.instance.Bunker.IncreaseFireRate();
            DataManager.instance.Bunker.SetCost();
            Text_AttackSpeed.text = "firerate : " + DataManager.instance.Bunker.Cost.fireRate;
            GameManager.Instance.UpdateCredit();
        }
    }

    void OnClickCriticalProbabilityButton()
    {
        GameManager.Instance.UpdateCredit();
    }

    void OnClickCriticalDmageButton()
    {
        GameManager.Instance.UpdateCredit();
    }
}
