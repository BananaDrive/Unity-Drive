using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Settings")]
    public Transform Player;
    public float shootRange = 15f;
    public float moveSpeed = 3.5f;
    public float shotTime = 2.5f;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 5f;

    [Header("Enemy Adv Settings")]
    private NavMeshAgent Agent;
    private float lastShotTime;

    // Start is called before the first frame update
    void Start()
    {
        Agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(Player.position, transform.position);

        if (distanceToPlayer > shootRange)
        {
            FollowPlayer();
        }
        else
        {
            Agent.isStopped = true;
            LookAtPlayer();
            ShootAtPlayer();
        }
        
    }


    void FollowPlayer()
    {
        Agent.isStopped = false;
        Agent.SetDestination(Player.position);
        Agent.speed = moveSpeed;
    }

    void LookAtPlayer()
    {
        Vector3 direction = (Player.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void ShootAtPlayer()
    {
        if (Time.time > lastShotTime + shotTime)
        {
            lastShotTime = Time.time;
            Debug.Log("Shooting");
            FireBullet();
        }
    }

    void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.velocity = firePoint.forward * bulletSpeed;
    }
}
