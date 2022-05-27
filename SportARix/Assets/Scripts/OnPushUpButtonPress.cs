using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class OnPushUpButtonPress : MonoBehaviour
{
    [SerializeField] private Button boxJumpButton;
    
    [SerializeField] private GameObject source;
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject player;

    private ObserverBehaviour _observerBehaviourSource;
    private ObserverBehaviour _observerBehaviourTarget;

    void Start()
    {
        boxJumpButton.onClick.AddListener(OnButtonPress);
        
        _observerBehaviourSource = source.GetComponent<ObserverBehaviour>();
        _observerBehaviourTarget = target.GetComponent<ObserverBehaviour>();
    }
    void Update()
    {
        if (IsTracked(_observerBehaviourSource) && IsTracked(_observerBehaviourTarget))
        {
            boxJumpButton.interactable = true;
        }
        else
        {
            boxJumpButton.interactable = false;
        }
    }

    void OnButtonPress()
    {
        player.SetActive(!player.activeSelf);
    }
    
    bool IsTracked(ObserverBehaviour observerBehaviour)
    {
        return observerBehaviour.TargetStatus.Status == Status.TRACKED;
    }

}