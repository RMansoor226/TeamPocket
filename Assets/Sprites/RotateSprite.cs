using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RotateSprite : MonoBehaviour
{
    [Header("Directional Sprites")]
    [Tooltip("Sprite seen when the player is facing directly AT the camera.")]
    [SerializeField] private Sprite frontSprite;
    
    [Tooltip("Sprite seen when the player is facing directly AWAY from the camera.")]
    [SerializeField] private Sprite backSprite;
    
    [Tooltip("Sprite seen when the player's profile faces the camera's left.")]
    [SerializeField] private Sprite leftSprite;
    
    [Tooltip("Sprite seen when the player's profile faces the camera's right.")]
    [SerializeField] private Sprite rightSprite;

    [Header("References")]
    [Tooltip("Optional: The parent or root object that handles actual movement/rotation. If empty, uses this object's transform.")]
    [SerializeField] private Transform characterRoot;

    private SpriteRenderer spriteRenderer;
    private Transform mainCameraTransform;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (Camera.main != null)
        {
            mainCameraTransform = Camera.main.transform;
        }

        if (characterRoot == null)
        {
            characterRoot = transform.parent != null ? transform.parent : transform;
        }
    }

    void LateUpdate()
    {
        if (mainCameraTransform == null || spriteRenderer == null) return;

        // 1. Force the sprite plane to look at the camera (keeps it billboarded)
        Vector3 lookTarget = mainCameraTransform.position;
        lookTarget.y = transform.position.y; 
        transform.LookAt(lookTarget);

        // 2. Get flattened 2D directional vectors on the XZ plane
        Vector3 playerForward = Vector3.ProjectOnPlane(characterRoot.forward, Vector3.up).normalized;
        Vector3 cameraForward = Vector3.ProjectOnPlane(mainCameraTransform.forward, Vector3.up).normalized;

        // 3. Get the signed angle (-180 to 180 degrees) between player and camera
        float signedAngle = Vector3.SignedAngle(playerForward, cameraForward, Vector3.up);

        // Convert angle space from (-180, 180) into a clean (0, 360) space
        if (signedAngle < 0)
        {
            signedAngle += 360f;
        }

        // 4. Map the angle to 4 segments (Front, Back, Left, Right)
        // Adding 45 shifts the 90-degree zones so 0 degrees rests centered in the middle of "Back"
        int angleIndex = Mathf.FloorToInt((signedAngle + 45f) / 90f) % 4;

        // 5. Swap the sprite based on the calculated sector
        switch (angleIndex)
        {
            case 0:
                spriteRenderer.sprite = backSprite;
                break;
            case 1:
                spriteRenderer.sprite = rightSprite;
                break;
            case 2:
                spriteRenderer.sprite = frontSprite;
                break;
            case 3:
                spriteRenderer.sprite = leftSprite;
                break;
        }
    }
}
