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

	public int maxLives=3;
	[HideInInspector]
	public int currentLives;
	public HealthBar healthBar;
	public SpriteRenderer sprite;
	private int flickerAmount=6;
	private float flickerDuration=0.1f;
	public bool canBeHit = true;

	[SerializeField]
	private Button receiveButton, deliverButton;
	public bool carryingOrder = false;

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
        if (collision.CompareTag("Vehicles") && canBeHit){
            currentLives -= 1;
			healthBar.SetHealth(currentLives);
			transform.position = initialPosition;
			StartCoroutine(GetHitFlicker());

			if (currentLives <= 0){
				// TODO Switch to game over screen, end the game
				FindObjectOfType<GamePlayManager>().gameOver();
			}
        }
    }

	private void OnCollisionEnter2D(Collision2D collision){
		if (collision.gameObject.CompareTag("Shop")){
			if (collision.gameObject.GetComponent<Shop>().havingOrder){
				receiveButton.gameObject.SetActive(true);
			}
		}
		else if (collision.gameObject.CompareTag("House") && carryingOrder){
			if (collision.gameObject.GetComponent<House>().isDesination && carryingOrder){
				deliverButton.gameObject.SetActive(true);
			}
		}
	}

	private void OnCollisionExit2D(Collision2D collision){
		deliverButton.gameObject.SetActive(false);
		receiveButton.gameObject.SetActive(false);
	}

	IEnumerator GetHitFlicker(){
		canBeHit = false;
		for (int i=0; i<flickerAmount; i++){
			sprite.color = new Color(1f, 1f, 1f, 0.5f);
			yield return new WaitForSeconds(flickerDuration);
			sprite.color = Color.white;
			yield return new WaitForSeconds(flickerDuration);
		}
		canBeHit = true;
	}
}