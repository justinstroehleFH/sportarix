using System.Collections;
using UnityEngine;
using Vuforia;

public class BoxJump : DefaultObserverEventHandler
{
    [SerializeField] private GameObject source;
    [SerializeField] private GameObject target;

    private ObserverBehaviour _observerBehaviourSource;
    private ObserverBehaviour _observerBehaviourTarget;

    private float _duration = 0.9f;
    private float _waitBefore = 0.5f;
    private float _waitAfter = 1.2f;
    

    protected override void Start()
    {
        _observerBehaviourSource = source.GetComponent<ObserverBehaviour>();
        _observerBehaviourTarget = target.GetComponent<ObserverBehaviour>();
        
        OnDisable();
    }

    void Update()
    {
        if (IsTracked(_observerBehaviourSource) && IsTracked(_observerBehaviourTarget))
        {
            transform.position = _observerBehaviourSource.transform.position;
            StartCoroutine(Move());
        }
        else
        {
            OnDisable();
        }
    }

    void OnDisable()
    {
        gameObject.SetActive(false);
        StopCoroutine(Move());
    }

    bool IsTracked(ObserverBehaviour observerBehaviour)
    {
        return observerBehaviour.TargetStatus.Status == Status.TRACKED;
    }

    IEnumerator Move()
    {
        while (true)
        {
            float timeBefore = 0f;
            while (timeBefore <= 1f)
            {
                timeBefore += Time.deltaTime / _waitBefore;
                yield return null;
            }

            float time = 0f;
            while (time <= 1f)
            {
                time += Time.deltaTime / _duration;
                yield return null;
                
                transform.position = Vector3.Lerp(source.transform.position, target.transform.position, time);
            }
            
            float timeAfter = 0f;
            while (timeAfter <= 1f)
            {
                timeAfter += Time.deltaTime / _waitAfter;
                yield return null;
            }

            while (timeBefore >= 0f)
            {
                timeBefore -= Time.deltaTime / _waitBefore;
                yield return null;
            }
            
            while (time >= 0f)
            {
                time -= Time.deltaTime / _duration;
                yield return null;

                transform.position = Vector3.Lerp(source.transform.position, target.transform.position, time);
            }
            
            while (timeAfter >= 0f)
            {
                timeAfter -= Time.deltaTime / _waitAfter;
                yield return null;
            }
        }
    }
}