using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{
    public float scrollSpeed = 4f;
    private Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(translation: Vector3.right * scrollSpeed * Time.deltaTime);
        if (transform.position.x < 21)
        {
            transform.position = startPosition;
        }
    }
}
