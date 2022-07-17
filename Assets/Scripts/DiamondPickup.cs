using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondPickup : MonoBehaviour
{   
    [SerializeField] int diamondValue = 100;
    [SerializeField] AudioClip DiamondPickupSFX;
    private void OnTriggerEnter2D(Collider2D other) {
        FindObjectOfType<GameSession>().AddToScore(diamondValue);
        AudioSource.PlayClipAtPoint(DiamondPickupSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
