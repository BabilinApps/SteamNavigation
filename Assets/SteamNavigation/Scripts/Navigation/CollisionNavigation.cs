using UnityEngine;
using System.Collections;
using System;

public class CollisionNavigation : INavigationStrategy {


    public Rigidbody rigidBody { get; private set; }

    public CollisionNavigation(Rigidbody rigidbody)
    {
        this.rigidBody = rigidbody;
        rigidBody.freezeRotation = true;
    }

    public void Navigate(Transform player, Vector3 direction, float speed)
    {
        direction.y = 0;

        Vector3 targetVelocity = direction.normalized;
        targetVelocity = player.TransformDirection(targetVelocity);
        targetVelocity = targetVelocity * speed;
        Vector3 velocity = rigidBody.velocity;
        Vector3 velocityChange = targetVelocity - velocity;
        velocityChange.x = Mathf.Clamp(velocityChange.x, -.5f, .5f);
        velocityChange.z = Mathf.Clamp(velocityChange.z, -.5f, .5f);
        velocityChange.y = 0;
        rigidBody.AddForce(velocityChange, ForceMode.VelocityChange);
    }

    public void Stop()
    {
        rigidBody.velocity = Vector3.zero;
    }

}
