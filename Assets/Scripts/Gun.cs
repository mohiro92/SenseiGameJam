﻿using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Vector3 BulletSpawnOffset = Vector3.zero;
    public Bullet Bullet;
    public int ShootCooldown = 30;

    private DateTime? _lastShootDateTime;

    // Use this for initialization
    void Start()
    {
        if(Bullet == null)
            throw new NullReferenceException("Bullet should be not null");
    }

    public void Shoot(Vector3 direction)
    {
        int lastTime = (DateTime.Now - (_lastShootDateTime ?? DateTime.Now)).Milliseconds;

        if (_lastShootDateTime == null || lastTime > ShootCooldown)
        {
            _lastShootDateTime = DateTime.Now;

            var bullet = Instantiate(Bullet);

            var spawnPosition = transform.position + BulletSpawnOffset;
            bullet.Init(direction, spawnPosition);
        }
    }
}
