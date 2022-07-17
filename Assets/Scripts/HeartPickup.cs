using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    [SerializeField] AudioClip HeartPickupSFX;
    private void OnTriggerEnter2D(Collider2D other) {
        FindObjectOfType<GameSession>().AddLives();
        AudioSource.PlayClipAtPoint(HeartPickupSFX, Camera.main.transform.position);
        Destroy(gameObject);
    }
}
