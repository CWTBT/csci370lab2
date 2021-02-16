using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearThrow : MonoBehaviour
{
    public GameObject projectile;
    public Sprite spr;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //https://gamebridgeu.wordpress.com/2017/02/12/instantiate/
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(pos);
            GameObject shot = Instantiate(projectile, transform.position, transform.rotation);
            shot.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
        }
    }
}
