using UnityEngine;

public class CameraHolder : MonoBehaviour
{
    public Transform target;

    private void Start() => transform.LookAt(target);
}
