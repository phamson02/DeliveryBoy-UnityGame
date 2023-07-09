using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementxin : MonoBehaviour
{
	public float moveSpeed = 5f; 
	public FixedJoystick joystick;
	public Rigidbody2D rb; 

	public Animator animator;
	Vector2 movement; 
	
	void Update() 
	{
		movement.x = joystick.Horizontal; //Input.GetAxisRaw("Horizontal");
		movement.y = joystick.Vertical; //Input.GetAxisRaw("Vertical");
		animator.SetFloat("Horizontal", movement.x); 
		animator.SetFloat("Vertical", movement.y);
		animator.SetFloat("Speed", movement.sqrMagnitude);
	}
	void FixedUpdate() 
	{
		rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime); 
	}
}