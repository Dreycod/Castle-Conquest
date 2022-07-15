using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Door : MonoBehaviour
{
    [SerializeField] float SecondsToLoad = 1f;
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
    StartCoroutine(LoadNextLevel());
   }

    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(SecondsToLoad);

        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
