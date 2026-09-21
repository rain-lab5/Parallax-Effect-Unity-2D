using UnityEngine;

public class ParallaxEff : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float startPos;
    public GameObject cam;
    public float parallaxEffect;

    void Start()
    {
        startPos = transform.position.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect;
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
    }
}
