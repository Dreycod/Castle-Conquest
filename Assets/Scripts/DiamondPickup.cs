using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondPickup : MonoBehaviour
{
    [SerializeField] AudioClip DiamondPickupSFX;
    private void OnTriggerEnter2D(Collider2D other) {
        
        AudioSource.PlayClipAtPoint(DiamondPickupSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
