using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAi : MonoBehaviour
{
	public PlayerController player;
	public NavMeshAgent agent;

	[Header("Enemy Stats")]
	public int health = 5;
	public int maxHealth = 5;
	public int damageGiven = 1;
	public int damageRecieved = 1;
	public float pushBackForce = 10000;
	public float VisionRange = 5.0f;
    public Transform Player;

    // Start is called before the first frame update
    void Start()
	{
		player = GameObject.Find("Player").GetComponent<PlayerController>();
		agent = GetComponent<NavMeshAgent>();
	}

	// Update is called once per frame
	void Update()
	{
		float DistanceToPlayer = Vector3.Distance(transform.position, Player.position);

		if (DistanceToPlayer <= VisionRange)
		{
			agent.destination = Player.position;
			agent.isStopped = false;
		}

		else
			agent.isStopped = true;

		if (health <= 0)
			Destroy(gameObject);
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Bullet")
		{
			health -= damageRecieved;
			Destroy(collision.gameObject);
		}
	}
}