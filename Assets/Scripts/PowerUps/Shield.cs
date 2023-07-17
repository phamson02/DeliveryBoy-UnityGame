using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField]
    private float duration = 7.5f;
    void OnTriggerEnter2D(Collider2D collider){
        if (collider.CompareTag("Player")){
            StartCoroutine(PickUp(collider));
        }
    }

    IEnumerator PickUp(Collider2D player){
        player.GetComponent<Player>().canBeHit = false;

        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;

        yield return new WaitForSeconds(duration);

        player.GetComponent<Player>().canBeHit = true;

        Destroy(gameObject);
    }
}
