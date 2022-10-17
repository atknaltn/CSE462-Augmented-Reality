using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    public Animator jumpAnimation;
    string btnName;
    // Start is called before the first frame update
    void Start()
    {
        jumpAnimation.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            RaycastHit Hit;
            if (Physics.Raycast(ray, out Hit))
            {
                btnName = Hit.transform.name;
                Debug.Log(btnName);
                switch (btnName)
                {
                    case "plane2":
                        jumpAnimation.Play("Jump");
                        Debug.Log("Button pressed");
                        break;
                    default:
                        Debug.Log("Button released");
                        jumpAnimation.Play("None");
                        break;
                }
            }
        }
    }
}
