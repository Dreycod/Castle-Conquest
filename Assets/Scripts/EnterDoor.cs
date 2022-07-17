using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterDoor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] AudioClip OpeningDoorSFX;
    Animator myAnimator;
    void Start()
    {
        myAnimator = GetComponent<Animator>();

        myAnimator.SetTrigger("Open");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     void PlayOpeningDoorSFX()
    {
      AudioSource.PlayClipAtPoint(OpeningDoorSFX, Camera.main.transform.position);
    }
}
