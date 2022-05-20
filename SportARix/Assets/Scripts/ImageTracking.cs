using UnityEngine;
using UnityEngine.Events;
using Vuforia;

public class ImageTracking : DefaultObserverEventHandler
{

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    protected override void OnTrackingFound()
    {
        base.OnTrackingFound();
    }
}