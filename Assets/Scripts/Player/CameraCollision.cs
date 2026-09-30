using UnityEngine;

// Runs after PlayerLook (order 0) so the holder has already been rotated and moved this frame.
[DefaultExecutionOrder(100)]
public class CameraCollision : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The pivot the camera orbits (the CameraHolder).")]
    [SerializeField] private Transform cameraHolder;

    [Header("Collision")]
    [Tooltip("Layers the camera should not pass through. Exclude the Player layer.")]
    [SerializeField] private LayerMask collisionMask = ~0;
    [SerializeField] private float cameraRadius = 0.25f;
    [SerializeField] private float wallPadding = 0.1f;
    [SerializeField] private float minDistance = 0.5f;

    [Header("Smoothing")]
    [Tooltip("How quickly the camera returns outward after clearing an obstacle.")]
    [SerializeField] private float returnSpeed = 6f;

    private Vector3 _desiredLocalPosition; // where the camera sits with no obstruction
    private float _maxDistance;
    private float _currentDistance;

    private void Awake()
    {
        if (cameraHolder == null)
            cameraHolder = transform.parent;

        _desiredLocalPosition = transform.localPosition;
        _maxDistance = _desiredLocalPosition.magnitude;
        _currentDistance = _maxDistance;
    }

    private void LateUpdate()
    {
        Vector3 pivot = cameraHolder.position;
        Vector3 direction = cameraHolder.TransformDirection(_desiredLocalPosition.normalized);

        float targetDistance = _maxDistance;

        // Change camera's target distance if it collides with anything
        if (Physics.SphereCast(pivot, cameraRadius, direction, out RaycastHit hit,
                _maxDistance, collisionMask, QueryTriggerInteraction.Ignore))
        {
            targetDistance = Mathf.Max(hit.distance - wallPadding, minDistance);
        }

        // Snap in instantly so the camera never shows the wall, ease back out smoothly
        if (targetDistance < _currentDistance)  // Ensure camera never goes farther than target distance
            _currentDistance = targetDistance;
        else  // Smoothly push camera to new target distance after collision
            _currentDistance = Mathf.Lerp(_currentDistance, targetDistance, returnSpeed * Time.deltaTime);

        transform.localPosition = _desiredLocalPosition.normalized * _currentDistance;
    }
}
