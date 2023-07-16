using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalVehicle : MonoBehaviour
{
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public string direction = "left";

    private Rigidbody2D myBody;
    private Vector2 frontPosition;
    // Start is called before the first frame update
    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        myBody.velocity = new Vector2(speed, myBody.velocity.y);
        frontPosition = transform.position;
        if (direction == "left"){
            frontPosition.x += 2;
            for (int i=2; i<4; i++){
                frontPosition.x += 1f;
                if (isObjectHere(frontPosition)){
                    myBody.velocity = new Vector2(0, myBody.velocity.y);
                }
            }
        }
        else if (direction == "right"){
            frontPosition.x -= 2;
            for (int i=2; i<4; i++){
                frontPosition.x -= 1f;
                if (isObjectHere(frontPosition)){
                    myBody.velocity = new Vector2(0, myBody.velocity.y);
                }
            }
        }
    }

    bool isObjectHere(Vector2 position)
    {
        Collider2D intersecting = Physics2D.OverlapCircle(position, 0.01f);
        if (intersecting == null)
        {
            return false;
        }
        else if (intersecting.CompareTag("Vehicles"))
        {
            return true;
        }
        else{
            return false;
        }
    }
}
