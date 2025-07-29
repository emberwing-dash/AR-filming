using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class DinoController : MonoBehaviour
{
    ARTrackedImageManager trackedImageManager;
    Animator dinoAnimController;

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
        foreach (var trackedImage in eventArgs.added)
        {
            // 'trackedImage' is the instantiated tracked image GameObject
            Animator animator = trackedImage.GetComponentInChildren<Animator>();

            if (animator != null)
            {
                dinoAnimController = animator;
                Debug.Log("Animator found and assigned.");
            }
            else
            {
                Debug.LogWarning("Animator not found in tracked image prefab instance.");
            }
        }
    }


    public void Laugh()
    {
        if (dinoAnimController != null)
            dinoAnimController.SetTrigger("Laugh");
    }

    public void Jump()
    {
        if (dinoAnimController != null)
            dinoAnimController.SetTrigger("Jump");
    }

    public void LookAround()
    {
        if (dinoAnimController != null)
            dinoAnimController.SetTrigger("Look");
    }

    public void Wierd()
    {
        if (dinoAnimController != null)
            dinoAnimController.SetTrigger("Wierd");
    }

}
