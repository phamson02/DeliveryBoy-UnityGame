using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalVehicle : MonoBehaviour
{
    [HideInInspector]
    public float speed;
    [HideInInspector]
    public string direction = "top";

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
        myBody.velocity = new Vector2(myBody.velocity.x, speed);
        frontPosition = transform.position;
        if (direction == "top"){
            frontPosition.y -= 1.5f;
            for (int i=2; i<4; i++){
                frontPosition.y -= 0.5f;
                if (isObjectHere(frontPosition)){
                    myBody.velocity = new Vector2(myBody.velocity.x, 0);
                }
            }
        }
        else if (direction == "bottom"){
            frontPosition.y += 1.5f;
            for (int i=2; i<4; i++){
                frontPosition.y += 0.5f;
                if (isObjectHere(frontPosition)){
                    myBody.velocity = new Vector2(myBody.velocity.x, 0);
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
