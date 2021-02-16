﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    private Rigidbody2D body;

    private float horizontal;
    private float vertical;

    public float runSpeed = 10f;
    private float moveLimiter = 0.7f;

    public Sprite right;
    public Sprite left;
    public Sprite up;
    public Sprite down;
    private SpriteRenderer spr;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        spr = this.GetComponent<SpriteRenderer>();
        if (spr.sprite == null) spr.sprite = right;
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate()
    {
        if (horizontal != 0 && vertical != 0)
        {
            horizontal *= moveLimiter;
            vertical *= moveLimiter;
        }
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
        setSprite();
    }

    private bool HorizontalMagnitudeGreater()
    {
        return Mathf.Abs(body.velocity.x) > Mathf.Abs(body.velocity.y);
    }

    private void setSprite()
    {
        if (body.velocity == Vector2.zero) return;
        else if (HorizontalMagnitudeGreater())
        {
            if (body.velocity.x > 0) spr.sprite = right;
            else spr.sprite = left;
        }
        else
        {
            if (body.velocity.y > 0) spr.sprite = up;
            else spr.sprite = down;
        }
    }
}
