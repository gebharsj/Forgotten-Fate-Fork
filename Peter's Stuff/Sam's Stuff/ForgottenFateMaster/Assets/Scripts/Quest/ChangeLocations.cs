﻿using UnityEngine;
using System.Collections;

public class ChangeLocations : MonoBehaviour 
{
	public GameObject _player;
	//public GameObject _camera;
	public Transform _destination;

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	void OnTriggerEnter2D(Collider2D _other)
	{
		if (_other.tag.Equals ("Player")) 
		{
			_player.transform.position = _destination.position;
			//_camera.transform.position.x = _destination.position.x;
			//_camera.transform.position.y = _destination.position.y;
		}
	}

}
