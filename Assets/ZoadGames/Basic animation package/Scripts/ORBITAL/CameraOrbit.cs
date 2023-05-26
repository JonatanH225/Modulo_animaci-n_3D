using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrbit : MonoBehaviour
{
    private Vector2 angle = new Vector2(-90*Mathf.Deg2Rad, 0);
    private new Camera camera;
    public Transform follow;
    public float maxDistance;
    public Vector2 sensitivity;
    public Vector2 nearPlanesize;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        camera = GetComponent<Camera>();

        calculatePlaneSize();
    }

    private void calculatePlaneSize()
    {
        float alt = Mathf.Tan(camera.fieldOfView * Mathf.Deg2Rad/2) * camera.nearClipPlane;
        float anc = alt * camera.aspect;

        nearPlanesize = new Vector2(anc, alt);
    }

    private Vector3[] getCameraCollision(Vector3 direction)
    {
        Vector3 position = follow.position;
        Vector3 center = position + direction * (camera.nearClipPlane + 0.2f);

        Vector3 right = transform.right * nearPlanesize.x;
        Vector3 up = transform.up * nearPlanesize.y;

        return new Vector3[] {center - right + up, 
                              center + right + up, 
                              center - right - up, 
                              center + right - up};
    }

    private void Update()
    {
        float hor = Input.GetAxis("Mouse X");

        if(hor != 0)
        {
            angle.x -= hor * Mathf.Deg2Rad * sensitivity.x;
        }

        float ver = Input.GetAxis("Mouse Y");

        if (ver != 0)
        {
            angle.y -= ver * Mathf.Deg2Rad * sensitivity.y;
            angle.y = Mathf.Clamp(angle.y, -80*Mathf.Deg2Rad, 80 * Mathf.Deg2Rad);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 direction = new Vector3(Mathf.Cos(angle.x)*Mathf.Cos(angle.y), 
                                    Mathf.Sin(angle.y), 
                                    Mathf.Sin(angle.x) * Mathf.Cos(angle.y)
                                    );

        RaycastHit hit;
        float distance = maxDistance;
        Vector3[] points = getCameraCollision(direction);

        foreach(Vector3 point in points)
        {
            if (Physics.Raycast(point, direction, out hit, maxDistance))
            {
                distance = Mathf.Min((hit.point - follow.position).magnitude, distance);
            }
        }

        transform.position = follow.position + direction * distance;
        transform.rotation = Quaternion.LookRotation(follow.position - transform.position);
    }
}
