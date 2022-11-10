using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameData", menuName = "Data/Game Data")]
public class GameData : ScriptableObject
{
    [Header("InGame Settings")]
    public float ClimbingTime;
    public float Income;
    public float Money;
    public int Stamina;

    [Header("Incremental Settings")]

    public int IncomeUpgradeMoney;
    public int IncomeIncrementalLevel;
    public int SpeedUpgradeMoney;
    public int SpeedIncrementalLevel;
    public int StaminaUpgradeMoney;
    public int StaminaIncrementalLevel;

}
