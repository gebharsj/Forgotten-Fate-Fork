﻿using UnityEngine;
using System.Collections;

public class AiIntermediate : MonoBehaviour {

	[HideInInspector]
	public float targetDistance;
	public float attackDistance;

	public float sprintDistance;
	
	public float normalMovementSpeed;
	public float sprintMovementSpeed;
	[HideInInspector]
	public float slowMovementSpeed;
	[HideInInspector]
	public float fastMovementSpeed;
	
	private int	random;
	public float attackTimer;
	private float permentTimer;

	//private int		fleeNumber;
	//public 	int		fleePercent;
	public 	float 	fleeHealthPercent;
	private float 	fleeHealth;

	private float	xfloat;
	private float 	yfloat;

	public Transform target;
	
	public GameObject _player;
	public GameObject _enemy;

	public Transform _self;

	public Vector2 force;

	bool	isNotTouching = true;
	bool	noDamage 	= true;
	bool	runAway 	= false;
	bool	stayFight	= true;
	bool	enemyTouch  = true;

	// Use this for initialization
	void Start ()
	{
		fastMovementSpeed = sprintMovementSpeed;
		slowMovementSpeed = normalMovementSpeed;

		permentTimer = attackTimer;

		fleeHealth = this.gameObject.GetComponent<EnemiesReceiveDamage> ().maxHp * fleeHealthPercent / 100;
	}

	// Update is called once per frame
	void Update ()
	{
		if (attackTimer > 0) 
		{
			//print (attackTimer + " : AttackTimer");
			attackTimer -= 1 * Time.deltaTime;
		}

		targetDistance = Vector3.Distance (target.position, transform.position);

		//-------Speed Changes Based Upon Distance------------
		if (targetDistance > sprintDistance)
		{
			normalMovementSpeed = fastMovementSpeed;
		} 
		else
			normalMovementSpeed = slowMovementSpeed;

		//--------If Hit, always Chances------------
		if (noDamage) 
		{
			if (this.gameObject.GetComponent<EnemiesReceiveDamage> ().hp < this.gameObject.GetComponent<EnemiesReceiveDamage> ().maxHp) 
			{
				attackDistance = 1000;
				noDamage = false;
				//print ("I've Been Hit!");
			}
		}

		2DCollider.
		//-------------Random Chance of Fleeing-------
		if (stayFight)
		{
			random = 0;
			if (this.gameObject.GetComponent<EnemiesReceiveDamage> ().hp < fleeHealth)
			{
				//fleeNumber = 1 * 100 / fleePercent;
				stayFight = false; //this is here CHRIS
				random = Random.Range (1, 5);//fleeNumber);
				//print (fleeNumber + ": Flee Int");
				//print (random + ": Random Number");
				
				if (random == 1) 
				{
					runAway = true;
				}
			}
		}

		//------Moves Towards the Player-------------
		if (targetDistance < attackDistance)
		{
			MovingPhase();
		}
	}

	void MovingPhase()
	{
		if (runAway) 
		{
			//print ("RUN AWAY!");
			//---------Opposite of isNotTouching------------
			if (target.position.y > transform.position.y) {
				transform.position += transform.up * -normalMovementSpeed * Time.deltaTime;
				//Debug.Log ("We;re going up!");
			} else {
				transform.position += transform.up * normalMovementSpeed * Time.deltaTime;
				//Debug.Log ("We;re going down!");
			}
			
			if (target.position.x > transform.position.x) {
				transform.position += transform.right * -normalMovementSpeed * Time.deltaTime;
				//Debug.Log ("We;re going right!!");
			} else {
				transform.position += transform.right * normalMovementSpeed * Time.deltaTime;
				//Debug.Log ("We;re going left!");
			}
		}

		else if (isNotTouching) 
		{
			if (target.position.y > transform.position.y) 
			{
				transform.position += transform.up * normalMovementSpeed * Time.deltaTime;
			} 
			else 
			{
				transform.position += transform.up * -normalMovementSpeed * Time.deltaTime;
			}

			if (target.position.x > transform.position.x)
			{
				transform.position += transform.right * normalMovementSpeed * Time.deltaTime;
			}
			else 
			{
				transform.position += transform.right * -normalMovementSpeed * Time.deltaTime;
			}
		}
	}

	void OnCollisionEnter2D (Collision2D playerC)
	{
		if (playerC.gameObject.tag == "Enemy")
		{
			enemyTouch = true;
		}
	 }
	void OnCollisionStay2D(Collision2D playerC)
	{
		if (playerC.gameObject.tag == "Player")
		{
			playerC.gameObject

			if (attackTimer < 1) 
			{
				target.GetComponent<PlayerReceivesDamage> ().meleeHits++;
				attackTimer = permentTimer;
			}
		}
		//-------Prevents Enemy From Pushing Player---------------
		else if (enemyTouch)
		{ 
			if (isNotTouching)
			{
					Vector2 pos = transform.position;
					xfloat	=	pos.x;
					yfloat	= 	pos.y;

					transform.position = new Vector2(xfloat	, yfloat - 1.0f);

					enemyTouch = false;
			}


			_enemy = playerC.gameObject;


			/*if (_enemy.GetComponent<AiIntermediate> ().isNotTouching == false)
			{
				isNotTouching = false;
			}
			else
				isNotTouching = true;*/
		}
	}

	void OnCollisionExit2D (Collision2D playerC)
	{
		isNotTouching = true;
	}
}