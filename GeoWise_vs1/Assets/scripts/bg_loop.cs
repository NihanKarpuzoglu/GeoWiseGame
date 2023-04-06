using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bg_loop : MonoBehaviour
{
    // Start is called before the first frame update
    private Material mat;
    public float speed = 0.5f;
    private Vector2 offset;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        offset = new Vector2(Time.time * speed, 0);

        mat.SetTextureOffset("_MainTex", offset);
        
    }
}
