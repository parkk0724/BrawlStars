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
        StartCoroutine(Explane());
    }

    private IEnumerator Loding()
    {
        float fillAmonut = 0.0f;

        AsyncOperation operation = SceneManager.LoadSceneAsync("Browl_Stars");
        operation.allowSceneActivation = false;

        while (loading_progress.value < 1.0f)
        {
            Debug.Log(operation.progress);

            if (loading_progress.value >= 1.0f && operation.progress >= 1.0f)
            {
                operation.allowSceneActivation = true;
            }

            fillAmonut += Time.deltaTime / 8.0f;

            loading_progress.value = fillAmonut;

            yield return null;
        }

        operation.allowSceneActivation = true;
    }
    private IEnumerator Explane()
    {
        explane_Text.text = "박스맨과 제스터는 마우스 버튼 홀드를 통하여 직접 타겟을 설정 할 수 있습니다";
        yield return new WaitForSeconds(2.0f);
        explane_Text.text = "일반 몬스터를 죽이면 게임에 필요한 아이템을 획득 할 수 있습니다.";
        yield return new WaitForSeconds(0.5f);
    }
}