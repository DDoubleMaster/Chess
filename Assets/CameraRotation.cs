using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    private void Update()
    {
        float speed = Time.deltaTime * 90;

        transform.Rotate(0, -Input.GetAxis("Horizontal") * speed, 0);
        transform.GetChild(0).transform.Rotate(Input.GetAxis("Vertical") * speed, 0, 0);
    }
}
