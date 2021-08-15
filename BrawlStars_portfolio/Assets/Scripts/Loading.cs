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
        explane_Text.text = "�ڽ��ǰ� �����ʹ� ���콺 ��ư Ȧ�带 ���Ͽ� ���� Ÿ���� ���� �� �� �ֽ��ϴ�";
        yield return new WaitForSeconds(2.0f);
        explane_Text.text = "�Ϲ� ���͸� ���̸� ���ӿ� �ʿ��� �������� ȹ�� �� �� �ֽ��ϴ�.";
        yield return new WaitForSeconds(0.5f);
    }
}