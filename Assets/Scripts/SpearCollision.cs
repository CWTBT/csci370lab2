using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearCollision : MonoBehaviour
{

    public AudioSource hitSound;
    public AudioSource  wallSound;

    // https://answers.unity.com/questions/175995/can-i-play-multiple-audiosources-from-one-gameobje.html
    void Start()
    {
        var sources = GetComponents<AudioSource>();
        hitSound = sources[0];
        wallSound = sources[1];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) return;
        else if (collision.gameObject.CompareTag("Wall"))
        {
            StartCoroutine("Thwack");
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            GameManager.Instance.decEnemyCount();
            StartCoroutine("Hit");
        }
        
    }

    IEnumerator Hit()
    {
        SpriteRenderer spr = gameObject.GetComponentInChildren<SpriteRenderer>();
        TrailRenderer tr = gameObject.GetComponentInChildren<TrailRenderer>();
        spr.enabled = false;
        tr.enabled = false;
        hitSound.Play();
        yield return new WaitForSeconds(hitSound.clip.length);
        Destroy(gameObject);
    }

    IEnumerator Thwack()
    {
        SpriteRenderer spr = gameObject.GetComponentInChildren<SpriteRenderer>();
        TrailRenderer tr = gameObject.GetComponentInChildren<TrailRenderer>();
        spr.enabled = false;
        tr.enabled = false;
        wallSound.Play();
        yield return new WaitForSeconds(wallSound.clip.length);
        Destroy(gameObject);
    }
}
