using UnityEngine;
using Vuforia;

public class PushUp : DefaultObserverEventHandler
{
    [SerializeField] private GameObject source;
    [SerializeField] private GameObject target;

    private ObserverBehaviour _observerBehaviourSource;
    private ObserverBehaviour _observerBehaviourTarget;

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
            var srcVec = _observerBehaviourSource.transform.position;
            var destVec = _observerBehaviourTarget.transform.position;
            
            var srcX = srcVec.x;
            var destX = destVec.x;
            var x = srcX + (destX - srcX) / 2;
            
            var srcY = srcVec.y;
            var destY = destVec.y;
            var y = srcY + (destY - srcY) / 2;
            
            var srcZ = srcVec.z;
            var destZ = destVec.z;
            var z = srcZ + (destZ - srcZ) / 2;

            transform.position = new Vector3(x, y, z);
        }
        else
        {
            OnDisable();
        }
    }
    void OnDisable()
    {
        gameObject.SetActive(false);
    }
    
    bool IsTracked(ObserverBehaviour observerBehaviour)
    {
        return observerBehaviour.TargetStatus.Status == Status.TRACKED;
    }
}