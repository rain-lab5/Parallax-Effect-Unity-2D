using UnityEngine;

public class ParallaxEff : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float startPos, length;
    public GameObject cam;
    public float parallaxEffect;

    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;  
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        float move = cam.transform.position.x * (1 - parallaxEffect);
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        if(move > startPos + length)
        {
            startPos += length;
        }
        else if(move < startPos - length)
        {
            startPos -= length;
        }


    }
}
