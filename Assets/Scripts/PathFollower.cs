using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;
using DG.Tweening;

public class PathFollower : MonoBehaviour
{
    public static PathFollower instance;
    public PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public bool SetRotation = false, Shake;

    [SerializeField] private GameObject _stickManSpine,_stickManMatBody,_finish;
    [SerializeField] private GameData _gameData;
    [SerializeField] private UIManager UIManager;
    [SerializeField] private Animator _animator;
    private Color _startColor, _endColor;
    private Renderer _stickManRen;
    private float distanceTravelled;
    private bool _firstStep=true;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        _startColor = _stickManMatBody.GetComponent<Renderer>().material.color;
        _endColor = Color.red;
        _stickManRen = _stickManMatBody.GetComponent<Renderer>();
    }
    void Start()
    {
        if (pathCreator != null)
        {
            pathCreator.pathUpdated += OnPathChanged;
        }
    }

    void Update()
    {

        if (SetRotation)
        {
            transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 0);
        }
       if (Input.GetKeyDown(KeyCode.Space))
       {
            _animator.SetTrigger("Jump");

        }
        if (Input.GetKeyDown(KeyCode.A))
        {

        }

        if (Shake)
        {
            ShakeStickMan();
        }
    }

    public void TakeStep()
    {
        if (_firstStep)
        {
            _firstStep = false;
            distanceTravelled = 0;
            _animator.SetTrigger("Climb");
            transform.DOMove(pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction), _gameData.ClimbingTime).OnComplete(() => Yapilacaklar()); 
        }
        else
        {
            distanceTravelled += 0.2648824f;
            _animator.SetTrigger("Climb");
            transform.DOMove(pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction), _gameData.ClimbingTime).OnComplete(() => Yapilacaklar()); 
        }
    }

    public void StickManBackNormalShape()
    {
        _stickManSpine.transform.DOScale(Vector3.one, .5f);
        _stickManRen.material.color = Color.Lerp(_stickManRen.material.color, _startColor, 1);
    }

    public void Finish()
    {
        transform.DOMove(_finish.transform.localPosition, 2f);
        _animator.SetTrigger("Jump");
        StartCoroutine(WinPanel());
    }

    void ShakeStickMan()
    {
        Vector3 vec = Vector3.one * 1.3f;
        vec.y = 1;
        _stickManSpine.transform.localScale = Vector3.Lerp(Vector3.one, vec, Mathf.PingPong(Time.time * 4, 1));
        _stickManRen.material.color = Color.Lerp(_startColor, _endColor, Mathf.PingPong(Time.time * 4, 1));
    }

    void Yapilacaklar()
    {
        TouchController.instance.IsClickable = true;
        SetRotation = false;
        _animator.SetTrigger("Idle");
    }

    void OnPathChanged()
    {
        distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }

    IEnumerator WinPanel()
    {
        yield return new WaitForSeconds(3.5f);
        UIManager.OnWinPanel();
    }
}
