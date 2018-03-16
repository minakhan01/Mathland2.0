using UnityEngine;
using System.Collections.Generic;
using HoloToolkit.Unity;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class ProjectileBehavior : MonoBehaviour
{
    /// <summary>
    /// Keeps track of when the shot was fired for auto cleanup.
    /// </summary>
    float StartTime;

    /// <summary>
    /// Tracks max lifetime of the projectile
    /// </summary>
    public float MaxLifetime = 3;

    /// <summary>
    /// Owner of the projectile.
    /// </summary>
    public long OwningUserId { get; set; }

    public Vector3 startDir { get; set; }

    /// <summary>
    /// Keeps track of whether or not this projectile hit something and should be 'destroyed'.
    /// </summary>
    protected bool firstContact = false;

    /// <summary>
    /// Game object to spawn when projectile explodes.
    /// </summary>
    public GameObject explosionEffect;

    public AudioClip bounceSoundEffect;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        StartTime = Time.time;

        LaunchProjectile();
    }

    protected virtual void LaunchProjectile()
    {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
        rigidBody.velocity = 4 * startDir;
        rigidBody.angularVelocity = Random.onUnitSphere * 20;
    }

    // Update is called once per frame.
    void Update()
    {
        UpdateProjectile();
    }

    protected virtual void UpdateProjectile()
    {
        // If the projectile hasn't hit something, check for time out.
        if (firstContact == false)
        {
            if (Time.time - StartTime > MaxLifetime)
            {
                // If we 'time out' just pretend that we hit something.
                firstContact = true;
            }
        }
        else
        {
            // We hit something (or timed out), so play the explosion effect.
            if (explosionEffect != null)
            {
                Instantiate(explosionEffect, transform.position, Quaternion.identity);
            }

            // And destroy the projectile.
            Destroy(transform.parent.gameObject);
        }
    }

    /// <summary>
    /// When the projectile hits something this will be called.
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        ProcessCollision(collision);
    }

    protected virtual void ProcessCollision(Collision collision)
    {
        // If we hit the player, we will clean up this projectile.
        if (HitPlayer(collision))
        {
            return;
        }

        List<Vector3> collisionPoints = new List<Vector3>();

        // this loop sets up spawning particles for the collision.
        for (int index = 0; index < collision.contacts.Length; index++)
        {
            collisionPoints.Add(collision.contacts[index].point);
        }
    }

    /// <summary>
    /// Checks to see if the projectile should continue with destruction or 
    /// safely bounce.
    /// </summary>
    /// <param name="collision"></param>
    /// <returns>true = use the collision for exploding, false = bounce.</returns>
    protected virtual bool HitPlayer(Collision collision)
    {
        
        return false;
    }
}
