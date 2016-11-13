using UnityEngine;
using System.Collections;
using System;

public class NonCollisionNavigation : INavigationStrategy
{
    Transform controller;
    public Rigidbody rigidBody { get; private set; }

    public NonCollisionNavigation(Rigidbody rigidbody)
    {
       this.rigidBody = rigidbody;
       rigidBody.freezeRotation = true;
       rigidBody.constraints = RigidbodyConstraints.FreezeAll;
    }
    public void Navigate(Transform player, Vector3 direction, float speed)
    {
        direction.y = 0;
        player.Translate(direction * speed * Time.deltaTime);
    }

    public void Stop()
    {
        rigidBody.velocity = Vector3.zero;
    }
}
