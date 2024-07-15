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
        DataManager.instance.Bunker.IncreaseDamage();
        Text_AttackDmg.text = "dmg : " + DataManager.instance.Bunker.Status.damage;
    }

    void OnClickAttackRangeButton()
    {
        DataManager.instance.Bunker.IncreaseAttackRange();
        _bunker.SetAttackRange();
        Text_AttackRange.text = "range : " + DataManager.instance.Bunker.Status.attackRange;
    }

    
    void OnClickAttackSpeedButton()
    {
        DataManager.instance.Bunker.IncreaseFireRate();
        Text_AttackSpeed.text = "firerate : " + DataManager.instance.Bunker.Status.fireRate;
    }

    void OnClickCriticalProbabilityButton()
    {

    }

    void OnClickCriticalDmageButton()
    {

    }
}
