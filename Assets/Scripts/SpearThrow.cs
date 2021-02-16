using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearThrow : MonoBehaviour
{
    public GameObject projectile;
    private AudioSource throwSound;
    
    // Start is called before the first frame update
    void Start()
    {
        throwSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //https://gamebridgeu.wordpress.com/2017/02/12/instantiate/
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 pos = transform.position;
            pos.x += .75f;
            pos.y += .75f;
            GameObject shot = Instantiate(projectile, transform.position, transform.rotation);
            SetShotVelocity(shot);
            throwSound.Play();
        }
    }

    private void SetShotVelocity(GameObject shot)
    {
        Facing dir = gameObject.GetComponent<PlayerControl>().direction;
        if (dir == Facing.Left)
        {
            shot.transform.Rotate(new Vector3(0, 0, 180));
            shot.GetComponent<Rigidbody2D>().velocity = new Vector2(-10, 0);
        }
        else if (dir == Facing.Up)
        {
            shot.transform.Rotate(new Vector3(0, 0, 90));
            shot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 10);
        }
        else if (dir == Facing.Down)
        {
            shot.transform.Rotate(new Vector3(0, 0, 270));
            shot.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -10);
        }
        else
        {
            shot.GetComponent<Rigidbody2D>().velocity = new Vector2(10, 0);
        }
    }
}
