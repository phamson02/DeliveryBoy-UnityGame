using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    [SerializeField]
    private float multiplier = 2f;
    private float duration = 5f;
    void OnTriggerEnter2D(Collider2D collider){
        if (collider.CompareTag("Player")){
            StartCoroutine(PickUp(collider));
        }
    }

    IEnumerator PickUp(Collider2D player){
        player.GetComponent<Player>().moveSpeed *= multiplier;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(duration);

        player.GetComponent<Player>().moveSpeed /= multiplier;

        Destroy(gameObject);
    }
}
