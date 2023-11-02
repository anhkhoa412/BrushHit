using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
   
    public RectTransform transit;

    public float transitionTime = 1f;
    public Button quitGame;

    public void OnEnable()
    {
       transit.gameObject.SetActive(false);
    }
    public void Start()
    {
        transit.gameObject.SetActive(true);

        LeanTween.scale(transit, new Vector3(1, 1, 1), 0);
        LeanTween.scale(transit, Vector3.zero, 0.5f).setEase(LeanTweenType.easeInBack).setOnComplete(() => {
            transit.gameObject.SetActive(false);
        });
    }
    public void LoadGameMode(int sceneIndex)
    {
        transit.gameObject.SetActive(true) ;

        LeanTween.scale(transit, Vector3.zero, 0f);
        LeanTween.scale(transit, new Vector3(1, 1, 1), 0.5f).setEase(LeanTweenType.easeInBounce).setOnComplete(() => {
           
           
            // Example for little pause before laoding the next scene
            StartCoroutine(ChooseMode(sceneIndex)); ;
        });

    }

    public IEnumerator ChooseMode(int sceneIndex)
    {
       
        yield return new WaitForSeconds(transitionTime);
       
        SceneManager.LoadScene(sceneIndex);
       
    }

    public void onQuit()
    {
        Application.Quit(); 
    }



}
