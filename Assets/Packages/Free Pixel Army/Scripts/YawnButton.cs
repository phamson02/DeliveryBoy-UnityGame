using UnityEngine;


public class YawnButton : MonoBehaviour
{
    public GameObject general,Helmet, Ordinary;
    Animator anim, anim2,anim3;
    void Start()
    {
        anim = general.GetComponent<Animator>();
        anim2 = Helmet.GetComponent<Animator>();
        anim3 = Ordinary.GetComponent<Animator>();
    }
    public void OnMouseUpAsButton()
    {
        anim.SetInteger("Animate", 1);
        anim2.SetInteger("Animate", 1);
        anim3.SetInteger("Animate", 1);
    }
}
