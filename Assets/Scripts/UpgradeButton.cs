using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    public static UpgradeButton instance;

    [SerializeField] private GameData _gameData;
    [SerializeField] private GameObject _incremental;
    [SerializeField] TextMeshProUGUI _moneyText;
    [SerializeField] TextMeshProUGUI _staminaLevelText, _staminaMoneyText;
    [SerializeField] TextMeshProUGUI _incomeLevelText, _incomeMoneyText;
    [SerializeField] TextMeshProUGUI _speedLevelText, _speedMoneyText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        SetIncrementalValue();
    }

    public void StaminaUpgrade()
    {
        if(_gameData.StaminaUpgradeMoney <= _gameData.Money)
        {
            _gameData.Money -= _gameData.StaminaUpgradeMoney;
            _gameData.Stamina += 10;
            _gameData.StaminaIncrementalLevel++;
            _gameData.StaminaUpgradeMoney += 5;
            _staminaLevelText.text = "Level "+_gameData.StaminaIncrementalLevel;
            _staminaMoneyText.text = "" + _gameData.StaminaUpgradeMoney;
            PrintMoney();
        }
    }

    public void IncomeUpgrade()
    {
        if(_gameData.IncomeUpgradeMoney <= _gameData.Money)
        {
            _gameData.Money -= _gameData.IncomeUpgradeMoney;
            _gameData.Income += .2f;
            _gameData.IncomeIncrementalLevel++;
            _gameData.IncomeUpgradeMoney += 5;
            _incomeLevelText.text = "Level " + _gameData.IncomeIncrementalLevel;
            _incomeMoneyText.text = "" + _gameData.IncomeUpgradeMoney;
            PrintMoney();
        }
    }

    public void SpeedUpgrade()
    {
        if(_gameData.SpeedUpgradeMoney <= _gameData.Money)
        {
            _gameData.Money -= _gameData.SpeedUpgradeMoney;
            _gameData.ClimbingTime -= .005f;
            _gameData.SpeedIncrementalLevel++;
            _gameData.SpeedUpgradeMoney += 5;
            _speedLevelText.text = "Level " + _gameData.SpeedIncrementalLevel;
            _speedMoneyText.text = "" + _gameData.SpeedUpgradeMoney;
            PrintMoney();
        }
    }

    public void PrintMoney()
    {
        _moneyText.text = "" +System.Math.Round(_gameData.Money,0);
    }

    public void OffIncrementalMenu()
    {
        _incremental.SetActive(false);
    }
    public void OnIncrementalMenu()
    {
        _incremental.SetActive(true);
    }

    void SetIncrementalValue()
    {
        _staminaLevelText.text = "Level " + _gameData.StaminaIncrementalLevel;
        _staminaMoneyText.text = "" + _gameData.StaminaUpgradeMoney;
        _incomeLevelText.text = "Level " + _gameData.IncomeIncrementalLevel;
        _incomeMoneyText.text = "" + _gameData.IncomeUpgradeMoney;
        _speedLevelText.text = "Level " + _gameData.SpeedIncrementalLevel;
        _speedMoneyText.text = "" + _gameData.SpeedUpgradeMoney;
        PrintMoney();
    }
}
