using UnityEngine;

public class TowardCameraRotator : MonoBehaviour
{
    private Camera _mainCamera;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (_mainCamera is null)
        {
            return;
        }

        Vector3 directionToCamera = transform.position - _mainCamera.transform.position;

        transform.localRotation = Quaternion.LookRotation(directionToCamera);
    }
}
