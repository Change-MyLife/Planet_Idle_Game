using Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainSlot : MonoBehaviour
{
    Bunker _bunker;

    [SerializeField] Button Slot_AttackDmg;
    [SerializeField] Button Slot_AttackSpeed;
    [SerializeField] Button Slot_CriticalProbability;
    [SerializeField] Button Slot_AttackRange;

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
    }

    void OnClickAttackRangeButton()
    {
        DataManager.instance.Bunker.IncreaseAttackRange();
        _bunker.SetAttackRange();
    }

    
    void OnClickAttackSpeedButton()
    {
        DataManager.instance.Bunker.IncreaseFireRate();
    }

    void OnClickCriticalProbabilityButton()
    {

    }

    void OnClickCriticalDmageButton()
    {

    }
}
