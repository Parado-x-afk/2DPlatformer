using UnityEngine;

public class OffsetCameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(3f, 2f, -10f);
    public float smoothSpeed = 5f;
    public float minY = 0f;

    void LateUpdate()
    {
        if (player != null)
        {
            Vector3 targetPosition = player.position + offset;


            targetPosition.y = Mathf.Max(minY, targetPosition.y);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        }
    }
}