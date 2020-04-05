using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car01 : MonoBehaviour
{
    public float speedForward = 4f;
    public float speedBackwards = 1f;
    Rigidbody2D rb;
    public Sprite car1;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    private float time = 0.0f;
    private float periodForward = 1f;
    private float periodBackwards = 2.5f;
    private float periodReset = 3f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //scale = new Vector3(defaultWidth / -1f, defaultHeight / -1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= periodForward && time < periodBackwards) {
            transform.Translate(new Vector3(Random.Range(0.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0) * speedForward * Time.deltaTime);
        }
        if(time >= periodBackwards && time < periodReset)
        {
            transform.Translate(new Vector3(Random.Range(-10.0f, 0.0f), Random.Range(-10.0f, 10.0f), 0) * speedBackwards * Time.deltaTime);
        }
        if(time >= periodReset)
        {
            time = 0.0f;
        }
        //gameObject.transform.localScale = Vector3.Scale(transform.localScale, scale);
        //transform.position = Vector3.Scale(transform.position, scale); //not sure that you need this

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
