using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player: MonoBehaviour
{
	public float moveSpeed = 5f; 
	public FixedJoystick joystick;
	public Rigidbody2D rb; 
	private Vector3 initialPosition;

	[HideInInspector]
	public int maxLives=5;
	[HideInInspector]
	public int currentLives;
	public HealthBar healthBar;
	public SpriteRenderer sprite;
	private int flickerAmount=5;
	private float flickerDuration=0.1f;

	[SerializeField]
	private Button receiveButton, deliverButton;

	public Animator animator;
	Vector2 movement; 

	void Start(){
		currentLives = maxLives;
		healthBar.SetMaxHealth(maxLives);
		initialPosition = transform.position;
	}
	
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
            currentLives -= 1;
			healthBar.SetHealth(currentLives);
			transform.position = initialPosition;
			StartCoroutine(GetHitFlicker());

			if (currentLives == 0){
				// TODO Switch to game over screen, end the game
			}
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

	IEnumerator GetHitFlicker(){
		for (int i=0; i<5; i++){
			sprite.color = new Color(1f, 1f, 1f, 0.5f);
			yield return new WaitForSeconds(flickerDuration);
			sprite.color = Color.white;
			yield return new WaitForSeconds(flickerDuration);
		}
	}
}