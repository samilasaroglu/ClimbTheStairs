using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using TMPro;

public class TouchController : MonoBehaviour ,IPointerDownHandler,IPointerUpHandler
{
    public static TouchController instance;
    public bool IsClickable;

    [SerializeField] private GameData _gameData;
    [SerializeField] private UIManager UIManager;
    [SerializeField] private ObjectPool _objectPool;
    [SerializeField] private GameObject _stairs, _scoreSign;
    [SerializeField] private float _depth;
    private int _stepNum;
    private bool _firstStep=true,_firstUp=true,_isHold,_isFinish,_gameStart;
    private TextMeshPro _scoreText;
    private int _currentStamina;
    private float _currentIncome;

    //oyun sonlandığında  _gameData.Money'e _currentMoneyi ekle

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        IsClickable = true; //GameStart olduktan sonra true yap

        _scoreText = _scoreSign.transform.GetChild(0).GetComponent<TextMeshPro>();

        _depth = -100.0f;
        _scoreText.text = "" + _depth;


        InvokeRepeating("StaminaControl", 2, 2);
    }

    private void Update()
    {

        if (_isHold && !_isFinish &&_gameStart)
        {
            if (IsClickable)
            {
                Climb();
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_gameStart)
        {
            _currentIncome = _gameData.Income;
            _currentStamina = _gameData.Stamina;

            _gameStart = true;
            UpgradeButton.instance.OffIncrementalMenu();
        }
        else
        {
            _isHold = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isHold = false;
        StartCoroutine(GiveStamina());
    }

    void Climb()
    {
        //Stamina azalt
        _currentStamina--;
        if (_currentStamina > 0)
        {
            StairsStepsOpen();

            if (_currentStamina < 5)
            {
                PathFollower.instance.Shake = true;
            }

        }
        else
        {
            UIManager.OnLosePanel();
            //GameOver
        }
    }

    void StaminaControl()
    {
        if (_currentStamina > 5)
        {
            PathFollower.instance.Shake = false;
            PathFollower.instance.StickManBackNormalShape();
        }
    }

    void StairsStepsOpen()
    {
        if (_stepNum < 250)
        {
            IsClickable = false;
            PathFollower.instance.TakeStep();
            PathFollower.instance.SetRotation = true;

            StartCoroutine(StepSetEnable());
        }
        else
        {
            PathFollower.instance.Finish();
            _isFinish = true;
        }
    }

    void EarnMoney()
    {
        _gameData.Money += _currentIncome;
        UpgradeButton.instance.PrintMoney();
    }

    void UpScoreSign()
    {
        if (_firstUp)
        {
            Vector3 desPos = _scoreSign.transform.position + new Vector3(0, 0.17f, 0);
            _scoreSign.transform.DOMove(desPos, .15f);
            _depth += .4f;
            _depth = (float)System.Math.Round(_depth, 1);
            _scoreText.text = "" + _depth;
            _firstUp = false;
        }
        else
        {
            Vector3 desPos = _scoreSign.transform.position + new Vector3(0, 0.1f, 0);
            _scoreSign.transform.DOMove(desPos, .15f);
            _depth += .4f;
            _depth = (float)System.Math.Round(_depth, 1);
            _scoreText.text = "" + _depth;
        }

    }

    void PrintIncome(Vector3 pos)
    {
        var obj = _objectPool.GetObject();
        obj.SetActive(true);
        obj.GetComponent<TextMeshPro>().text = "$" + _currentIncome;
        obj.transform.localScale = Vector3.one;
        pos.z -= 0.069164f;
        obj.transform.position = pos;
        obj.transform.DOScale(Vector3.zero, 2.5f).OnComplete(()=>obj.SetActive(false));
        EarnMoney();
    }

    IEnumerator GiveStamina()
    {
        for(int i = 0; i<5; i++)
        {
            if (!_isHold && _currentStamina < _gameData.Stamina)
            {
                yield return new WaitForSeconds(.5f);
                _currentStamina++;
            }
        }
    }

    IEnumerator StepSetEnable()
    {
        if (_firstStep)
        {
            for (int i = _stepNum; i < _stepNum + 3; i++)
            {
                _stairs.transform.GetChild(i).gameObject.SetActive(true);
                _stairs.transform.GetChild(i).transform.localScale = Vector3.zero;
                _stairs.transform.GetChild(i).DOScale(Vector3.one, .2f);
                UpScoreSign();
                PrintIncome(_stairs.transform.GetChild(i).transform.position);
                yield return new WaitForSeconds(.1f);
            }

            _stepNum += 3;
            _firstStep = false;
        }
        else
        {
            if (_stepNum != 249)
            {
                for (int i = _stepNum; i < _stepNum + 2; i++)
                {
                    _stairs.transform.GetChild(i).gameObject.SetActive(true);
                    _stairs.transform.GetChild(i).transform.localScale = Vector3.zero;
                    _stairs.transform.GetChild(i).DOScale(Vector3.one, .2f);
                    UpScoreSign();
                    PrintIncome(_stairs.transform.GetChild(i).transform.position);
                    yield return new WaitForSeconds(.1f);
                }

                _stepNum += 2;
            }
            else
            {
                for (int i = _stepNum; i < _stepNum + 1; i++)
                {
                    _stairs.transform.GetChild(i).gameObject.SetActive(true);
                    _stairs.transform.GetChild(i).transform.localScale = Vector3.zero;
                    _stairs.transform.GetChild(i).DOScale(Vector3.one, .2f);
                    UpScoreSign();
                    PrintIncome(_stairs.transform.GetChild(i).transform.position);
                    yield return new WaitForSeconds(.1f);
                }

                _stepNum += 1;
            }

        }
    }



}