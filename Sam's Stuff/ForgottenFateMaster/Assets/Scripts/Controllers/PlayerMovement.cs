﻿using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	//NOTE: If the public float, speed, is too high, the player may experience some serious turbulance! Player may 
	//fly through solid objects or other objects not otherwise meant to be passable.
	public Rigidbody2D player;
	public float speed = 200.0f;
	public float sprint = 2;
	[HideInInspector]
	public bool isSprinting = false;
	private float stamina =5;
	public int maxStamina = 5;
	public float staminaRecoveryRate = 0.3f;
	private float staminaRecharge = 5.0f;
    float moveSpeed;
	
	private Vector3 targetPosition;
	private bool isMoving;
	
	[HideInInspector]
	public float moveX;
	[HideInInspector]
	public float moveY;
	
	//const int LEFT_MOUSE_BUTTON = 0;
	void start()
	{
		stamina = maxStamina;
		if (isSprinting == false) 
		{
            moveSpeed = speed;
		}
	}
	
	// Character controller - the mouse will always override the keypad. Also, this control type does not
	// apply to X and Y cordinates but X and Z coordinates. To change, switch the "forward" function to 
	// "up" and the "back" function to "down." Rotation of camera may also affect the control. 
	
	void Update()
	{
        if (Input.GetKey(PlayerPrefs.GetString("MoveRight")))
        {
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(PlayerPrefs.GetString("MoveLeft")))
        {
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(PlayerPrefs.GetString("MoveUp")))
        {
            transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(PlayerPrefs.GetString("MoveDown")))
        {
            transform.Translate(Vector3.down * moveSpeed * Time.deltaTime);
        }

        //sprinting
        if (Input.GetKeyDown("space") && isSprinting == false && stamina > 0)
			isSprinting = true;
		
		if (Input.GetKeyUp ("space") && isSprinting == true) 
			isSprinting = false;
		
		if (stamina <= 0) 
			isSprinting = false;

        //player.velocity = new Vector3(moveX, moveY, 0);        //use : transform.Translate(moveX, moveY, 0f); if we decide to go back to 3D
        
        // Only when left mouse button is not clicked, will the WSAD controls work.) 
        if (isSprinting == false) {
            moveSpeed = speed;
			//WSAD control
			//moveX = Input.GetAxis ("Horizontal") * speed * Time.deltaTime;
			//moveY = Input.GetAxis ("Vertical") * speed * Time.deltaTime;			
		} 
		else 
		{
            moveSpeed = speed * sprint;
			//sprinting and stamina drain
			//moveX = Input.GetAxis ("Horizontal") * speed * sprint * Time.deltaTime;
			//moveY = Input.GetAxis ("Vertical") * speed * sprint * Time.deltaTime;
			stamina -= 1 * Time.deltaTime;
			staminaRecharge = 0;
		}
		
		//stamina must recharge before it can recover
		if (staminaRecharge < 5) 
		{
			staminaRecharge += 1 * Time.deltaTime;
			
			//if staminaRecharge should happen to go above 5
			if (staminaRecharge > 5)
				staminaRecharge = 5; 			
		}
		
		//stamina recovery
		if (isSprinting == false && stamina < maxStamina && staminaRecharge == 5) 
		{
			stamina += 1 * Time.deltaTime * staminaRecoveryRate;
			
			//if stamina should happen to go above the max value
			if (stamina > maxStamina)
				stamina = maxStamina; 			
		}        
    }
}