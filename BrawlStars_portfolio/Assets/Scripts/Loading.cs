using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    Slider loading_progress;
    TMPro.TMP_Text explane_Text;

    private void Awake()
    {
        loading_progress = this.GetComponentInChildren<Slider>();
        loading_progress.value = 0.0f;
        explane_Text = GameObject.Find("Explane").GetComponent<TMPro.TMP_Text>();
    }

    private void Start()
    {
        StartCoroutine(Loding());       
    }

    private IEnumerator Loding()
    {
        StartCoroutine(Explane());
        float fillAmonut = 0.0f;

        AsyncOperation operation = SceneManager.LoadSceneAsync("Browl_Stars");
        operation.allowSceneActivation = false;

        while (loading_progress.value < 1.0f)
        {
            if (loading_progress.value >= 1.0f && operation.progress >= 1.0f)
            {
                operation.allowSceneActivation = true;
            }

            fillAmonut += Time.deltaTime / 7.0f;

            loading_progress.value = fillAmonut;

            yield return null;
        }

        operation.allowSceneActivation = true;
    }
    private IEnumerator Explane()
    {
        explane_Text.text = "Boxman and Jester can set targets directly through the mouse button hold.";
        yield return new WaitForSeconds(4.0f);
        explane_Text.text = "If you kill a regular monster, you can get the items you need for the game playing.";
        yield return new WaitForSeconds(4.0f);
        explane_Text.text = "The jumpers can jump after about 1 second of waiting time above.";
        yield return new WaitForSeconds(5.0f);
    }
}