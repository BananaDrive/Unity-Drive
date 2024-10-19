using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AnimationActivation : MonoBehaviour
{
    public Animator animator;
    public Transform player;
    public float range = 5f;
    public bool inRange = false;
    // Start is called before the first frame update
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {
              float distance = Vector3.Distance(player.transform.position, transform.position);

        if (distance <= range)
        {
            inRange = true;
        }
        else
        {
            inRange = false;
        }

        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            TriggerAnimation(); 
        }
    }

    void TriggerAnimation()
    {
        animator.SetTrigger("Interact");
    }
}
