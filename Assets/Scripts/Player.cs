using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Sprite player_right;
    public Sprite player_left;
    public Sprite player_center;
    Rigidbody2D rb;
    float dirX;
    float moveSpeed = 20f;
    float ROTATE_AMOUNT = 2; // at full tilt, rotate at 2 degrees per update

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal") * 15f * Time.deltaTime, 0f, 0f);
        float tiltValue = GetTiltValue();

        if (tiltValue < 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = player_left;
            transform.Translate(Vector3.left * 30.0f * Time.deltaTime);
        }
        if (tiltValue > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = player_right;
            transform.Translate(Vector3.right * 30.0f * Time.deltaTime);
        }
        if(tiltValue == 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = player_center;
        }
    }

    float GetTiltValue()
    {
        float TILT_MIN = 0.05f;
        float TILT_MAX = 0.2f; 

        // Work out magnitude of tilt
        float tilt = Mathf.Abs(Input.acceleration.x);

        // If not really tilted don't change anything
        if (tilt < TILT_MIN)
        {
            return 0;
        }
        float tiltScale = (tilt - TILT_MIN) / (TILT_MAX - TILT_MIN);
        
        if (Input.acceleration.x < 0)
        {
            return tiltScale;
        }
        else
        {
            return -tiltScale;
        }
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(dirX, 0f);
    }
}
