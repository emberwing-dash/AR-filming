using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DinoMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    public float speed = 1.5f;
    public FixedJoystick joystick;

    [Header("AR Settings")]
    public GameObject dinoPrefab;  // The prefab to instantiate
    private ARTrackedImageManager trackedImageManager;

    private GameObject dinoInstance;
    private Animator dinoAnimController;
    private Transform dinoTransform;

    void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            if (dinoInstance == null)
            {
                // Instantiate the dino prefab at the tracked image position
                dinoInstance = Instantiate(dinoPrefab, trackedImage.transform.position, Quaternion.identity);
                dinoTransform = dinoInstance.transform;

                // Optional: match rotation of image
                dinoTransform.rotation = trackedImage.transform.rotation;

                // Fix scale so it's visible
                dinoTransform.localScale = Vector3.one;

                // Assign animator
                dinoAnimController = dinoInstance.GetComponentInChildren<Animator>();

                if (dinoAnimController != null)
                    Debug.Log("Dino instantiated with Animator.");
                else
                    Debug.LogWarning("Animator not found on dino prefab.");
            }
        }
    }

    void Update()
    {
        if (dinoTransform == null || dinoAnimController == null)
            return;

        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        Vector3 direction = new Vector3(horizontal, 0f, vertical);

        if (direction.magnitude > 1f)
            direction.Normalize();

        if (direction.magnitude > 0.1f)
        {
            // Move and rotate the dino
            dinoTransform.Translate(direction * speed * Time.deltaTime, Space.World);
            dinoTransform.rotation = Quaternion.Slerp(
                dinoTransform.rotation,
                Quaternion.LookRotation(direction),
                Time.deltaTime * 10f
            );

            dinoAnimController.SetBool("isWalk", true);
        }
        else
        {
            dinoAnimController.SetBool("isWalk", false);
        }
    }
}
