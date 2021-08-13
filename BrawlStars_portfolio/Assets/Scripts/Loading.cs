using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    Slider loading_progress;

    private void Awake()
    {
        loading_progress = this.GetComponentInChildren<Slider>();
        loading_progress.value = 0.0f;

       // StartCoroutine(Loding());
    }
    private IEnumerator Loding()
    {
        float fillAmonut = 0.0f;

        AsyncOperation operation = SceneManager.LoadSceneAsync("Browl_Stars");
        operation.allowSceneActivation = false;

        while (loading_progress.value < 1.0f)
        {
            if (loading_progress.value >= 1.0f && operation.progress >= 1.0f)
            {
                operation.allowSceneActivation = true;
            }

            fillAmonut += Time.deltaTime / 3.0f;

            loading_progress.value = fillAmonut;
                        
            yield return null;
        }
    }
 }
