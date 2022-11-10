using UnityEngine;

namespace PathCreation.Examples
{

    [RequireComponent(typeof(PathCreator))]
    public class GeneratePathExample : MonoBehaviour
    {

        public bool closedLoop = true;
        public Transform[] waypoints;
        [SerializeField] private GameObject _stairs;

        private void Awake()
        {
            for (int i = 0; i < _stairs.transform.childCount; i++)
            {
                waypoints[i] = _stairs.transform.GetChild(i).transform.GetChild(0).transform;
            }
        }
        void Start()
        {
            if (waypoints.Length > 0)
            {
                BezierPath bezierPath = new BezierPath(waypoints, closedLoop, PathSpace.xyz);
                GetComponent<PathCreator>().bezierPath = bezierPath;
            }
        }
    }
}