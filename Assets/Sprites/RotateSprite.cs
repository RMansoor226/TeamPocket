using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RotateSprite : MonoBehaviour
{
    [SerializeField] private Transform characterRoot;
    [SerializeField] private float idleSpeedThreshold = 0.05f;

    private Animator animator;
    private Transform mainCameraTransform;
    
    private Rigidbody parentRigidbody;
    private CharacterController parentController;
    private Vector3 lastParentPosition;
    private float calculatedSpeed;

    void Start()
    {
        animator = GetComponent<Animator>();
        
        if (Camera.main != null)
        {
            mainCameraTransform = Camera.main.transform;
        }

        if (characterRoot == null)
        {
            characterRoot = transform.parent != null ? transform.parent : transform;
        }

        // Try to grab movement components from the root object
        parentRigidbody = characterRoot.GetComponent<Rigidbody>();
        parentController = characterRoot.GetComponent<CharacterController>();
        lastParentPosition = characterRoot.position;
    }

    void LateUpdate()
    {
        if (mainCameraTransform == null || animator == null) return;

        // 1. Force the sprite plane to look at the camera (keeps it billboarded)
        Vector3 lookTarget = mainCameraTransform.position;
        lookTarget.y = transform.position.y; 
        transform.LookAt(lookTarget);

        // 2. Determine if the character is moving or idling
        bool isMoving = CheckIfMoving();

        // 3. Get flattened 2D directional vectors on the XZ plane
        Vector3 playerForward = Vector3.ProjectOnPlane(characterRoot.forward, Vector3.up).normalized;
        Vector3 cameraForward = Vector3.ProjectOnPlane(mainCameraTransform.forward, Vector3.up).normalized;

        // 4. Get the signed angle (-180 to 180 degrees) between player and camera
        float signedAngle = Vector3.SignedAngle(playerForward, cameraForward, Vector3.up);

        // Convert angle space from (-180, 180) into a clean (0, 360) space
        if (signedAngle < 0)
        {
            signedAngle += 360f;
        }

        // 5. Map the angle to 4 segments (Front, Back, Left, Right)
        int angleIndex = Mathf.FloorToInt((signedAngle + 45f) / 90f) % 4;

        // 6. Swap the sprite animation based on calculated sector and movement state
        if (isMoving)
        {
            switch (angleIndex)
            {
                case 0: animator.Play("VyxWalkForward"); break;
                case 1: animator.Play("VyxWalkRight"); break;
                case 2: animator.Play("VyxWalkBackward"); break;
                case 3: animator.Play("VyxWalkLeft"); break;
            }
        }
        else
        {
            switch (angleIndex)
            {
                case 0: animator.Play("VyxForward"); break;
                case 1: animator.Play("VyxRight"); break;
                case 2: animator.Play("VyxBackward"); break;
                case 3: animator.Play("VyxLeft"); break;
            }
        }
    }

    private bool CheckIfMoving()
    {
        // If parent has a 3D Rigidbody, use its velocity
        if (parentRigidbody != null)
        {
            return parentRigidbody.linearVelocity.magnitude > idleSpeedThreshold;
        }

        // If parent uses a CharacterController, use its velocity
        if (parentController != null)
        {
            return parentController.velocity.magnitude > idleSpeedThreshold;
        }

        // Fallback: Manually calculate velocity based on transform position changes
        float distanceMoved = Vector3.Distance(characterRoot.position, lastParentPosition);
        calculatedSpeed = distanceMoved / Time.deltaTime;
        lastParentPosition = characterRoot.position;

        return calculatedSpeed > idleSpeedThreshold;
    }
}
