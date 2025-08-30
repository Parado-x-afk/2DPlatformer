using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    //Oxygen
    private float startPos,length;
    public GameObject cam;
    public float parallaxEffect; //Speed of Parallax :P

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect; // 0 move with camera & 1 not move at all [Closer to 0 faster it moves]
        float movement = cam.transform.position.x*(1-parallaxEffect);

        transform.position = new Vector3(startPos + distance,transform.position.y,transform.position.z);
        if(movement>startPos+length)
        {
            startPos += length;
        }
        else if(movement<startPos-length)
        {
            startPos -= length;
        }
    }
}
