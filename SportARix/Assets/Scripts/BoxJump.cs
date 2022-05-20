using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Vuforia;

public class BoxJump : DefaultObserverEventHandler
{
    [SerializeField] private GameObject _source;
    [SerializeField] private GameObject _target;

    private ObserverBehaviour _observerBehaviourSource;
    private ObserverBehaviour _observerBehaviourTarget;

    private bool _active;
    private bool _shouldRun;
    private float _duration = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().enabled = false;
        _active = false;

        _observerBehaviourSource = _source.GetComponent<ObserverBehaviour>();
        _observerBehaviourTarget = _target.GetComponent<ObserverBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsTracked(_observerBehaviourSource) && IsTracked(_observerBehaviourTarget))
        {
            if (!_active)
            {
                GetComponent<Renderer>().enabled = true;
                transform.position = _observerBehaviourSource.transform.position;

                _shouldRun = true;
            }

            StartCoroutine(Move());
        }
        else
        {
            _active = false;
            _shouldRun = false;
        }
    }

    private bool IsTracked(ObserverBehaviour observerBehaviour)
    {
        return observerBehaviour.TargetStatus.Status == Status.TRACKED;
    }

    private IEnumerator Move()
    {
        while (_shouldRun)
        {
            float time = 0f;

            while (time <= 1f)
            {
                time += Time.deltaTime / _duration;
                yield return null;
                transform.position = Vector3.Lerp(_source.transform.position, _target.transform.position, time);
            }

            while (time >= 0f)
            {
                time -= Time.deltaTime / _duration;
                yield return null;
                transform.position = Vector3.Lerp(_source.transform.position, _target.transform.position, time);
            }
        }
    }
}