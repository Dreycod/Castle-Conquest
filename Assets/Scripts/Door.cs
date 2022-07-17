using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour
{
    [SerializeField] float SecondsToLoad = 1f;
    [SerializeField] AudioClip OpeningDoorSFX,ClosingDoorSFX;
    Animator myAnimator;
    BoxCollider2D myBoxCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myBoxCollider2D = GetComponent<BoxCollider2D>();
    }

   private void OnTriggerEnter2D(Collider2D other) {
    myAnimator.SetTrigger("Open");
   }

   public void StartLoadingNextLevel()
   {
    myAnimator.SetTrigger("Close");
    AudioSource.PlayClipAtPoint(ClosingDoorSFX, Camera.main.transform.position);
    StartCoroutine(LoadNextLevel());
   }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(SecondsToLoad);

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    void PlayOpeningDoorSFX()
    {
      AudioSource.PlayClipAtPoint(OpeningDoorSFX, Camera.main.transform.position);
    }
}
