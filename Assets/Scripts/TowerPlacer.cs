    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.XR.ARFoundation;
    using UnityEngine.XR.ARSubsystems;

    public class TowerPlacer : MonoBehaviour
{
    [SerializeField] private GameObject towerPrefab;
    [SerializeField] private int maxTowers = 5;

    private ARRaycastManager raycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    private int placedTowers = 0;

    private void Awake()
    {
        raycastManager = FindAnyObjectByType<ARRaycastManager>();
    }

    private void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Placing) return;

        // Handle both editor mouse click and mobile touch
        #if UNITY_EDITOR
            if (!Input.GetMouseButtonDown(0)) return;
            Vector2 inputPosition = Input.mousePosition;
        #else
            if (Input.touchCount == 0) return;
            Touch touch = Input.GetTouch(0);
            if (touch.phase != TouchPhase.Began) return;
            inputPosition = touch.position;
        #endif

        if (raycastManager.Raycast(inputPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            Pose hitPose = hits[0].pose;
            Instantiate(towerPrefab, hitPose.position, hitPose.rotation);
            placedTowers++;

            if (placedTowers >= maxTowers)
            {
                UIManager.Instance.ShowStartWaveButton();
            }
        }
    }
}
