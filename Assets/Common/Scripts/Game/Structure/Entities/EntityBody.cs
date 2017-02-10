﻿using UnityEngine;
using Assets.Scripts.Utilities;
using Assets.Scripts.GameBase.Interfaces.Entities;
using Assets.Scripts.GameBase.Interfaces.Items;
using UnityEngine.UI;
using Assets.Scripts.GameBase.Interfaces.Entities.Body;
using Assets.Scripts;
using System;

public class EntityBody : MonoBehaviour, IEntityBody
{
    private bool _isJumping;

    public bool IsJumping
    {
        get
        {
            return _isJumping;
        }

        set
        {
            _isJumping = value;
        }
    }

    #region MonoBehaviour
    void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (Vector3.Dot(contact.normal, Vector3.up) > Consts.Eps)
            {
                IsJumping = false;
                break;
            }
        }
    }
    #endregion

    #region IEntityBody
    public void Move(Vector3 direction)
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        
        ResetHorizontalVelocity();

        // Rotate towards direction
        rigidbody.transform.LookAt(direction);
        // "Push" body to direction
        rigidbody.AddForce(direction);
              
        GetComponent<Animator>().SetBool("IsRunning", true);
    }

    public void Stop()
    {
        ResetHorizontalVelocity();
        GetComponent<Animator>().SetBool("IsRunning", false);
    }

    public void Jump(float strength)
    {
        if (_isJumping)
            return;
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        IsJumping = true;
        rigidbody.velocity = new Vector3(rigidbody.velocity.x, strength, rigidbody.velocity.z);
    }

    public void SetPosition(Vector3 position)
    {
        GetComponent<Rigidbody>().position = position;
        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0f, 0f, 0f);
    }
    #endregion

    public void Reset(Vector3 position)
    {
        _isJumping = false;
        SetPosition(position);
    }

    private void ResetHorizontalVelocity()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, 0);
        rigidbody.angularVelocity = new Vector3(0, rigidbody.velocity.y, 0);
    }
}