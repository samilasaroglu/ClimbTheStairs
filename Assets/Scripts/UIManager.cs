using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    [SerializeField] private GameObject _losePanel, _winPanel;


    public void OnLosePanel()
    {
        _losePanel.SetActive(true);
    }
    public void OnWinPanel()
    {
        _winPanel.SetActive(true);
    }
}
