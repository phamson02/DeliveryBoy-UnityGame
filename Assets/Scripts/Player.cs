using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player: MonoBehaviour
{
	public float moveSpeed = 5f; 
	public FixedJoystick joystick;
	public Rigidbody2D rb; 

	[HideInInspector]
	public int lives=5;

	[SerializeField]
	private Button receiveButton, deliverButton;

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

	private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Vehicles")){
            this.lives -= 1;
        }
    }

	private void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.CompareTag("Shop")){
			if (collision.gameObject.GetComponent<Shop>().havingOrder){
				receiveButton.gameObject.SetActive(true);
			}
		}
		else if (collision.gameObject.CompareTag("House")){
			if (collision.gameObject.GetComponent<House>().isDesination){
				deliverButton.gameObject.SetActive(true);
			}
		}
	}

	private void OnCollisionExit2D(Collision2D collision){
		deliverButton.gameObject.SetActive(false);
		receiveButton.gameObject.SetActive(false);
	}
}