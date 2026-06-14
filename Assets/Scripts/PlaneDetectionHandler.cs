using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class PlaneDetectionHandler : MonoBehaviour
{
    private ARPlaneManager planeManager;
    private bool hasTransitioned = false;

    private void Awake()
    {
        planeManager = FindAnyObjectByType<ARPlaneManager>();
    }

    private void OnEnable()
    {
        planeManager.trackablesChanged.AddListener(OnPlanesChanged);
    }

    private void OnDisable()
    {
        planeManager.trackablesChanged.RemoveListener(OnPlanesChanged);
    }

    private void OnPlanesChanged(ARTrackablesChangedEventArgs<ARPlane> args)
    {
        if (hasTransitioned) return;

        // Update state in game manager
        if (args.added.Count > 0)
        {
            hasTransitioned = true;
            GameManager.Instance.SetState(GameState.Placing);
        }
    }
}
