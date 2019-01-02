using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Globe
{
    public static string nextSceneName;
}

public class AsyncLoadScene : MonoBehaviour
{
    //加载场景的滑动条
    public Slider loadingSlider;

    //百分之几
    public Text loadingText;
    private float loadingSpeed = 1;

    private float targetValue;

    /// <summary>
    /// 异步对象
    /// </summary>
    private AsyncOperation operation;
    
    void Start()
    {
        loadingSlider.value = 0.0f;

        //如果当前的场景是叫“Loading”
        //if (SceneManager.GetActiveScene().name == "Loading")
        {
            //启动协程
            StartCoroutine(AsyncLoading());
        }
    }
    
    IEnumerator AsyncLoading()
    {
        //要异步加载的场景
        operation = SceneManager.LoadSceneAsync(GameCtrl.Instance.nextSceneName);
        //阻止当加载完成自动切换
        operation.allowSceneActivation = false;//如果不写这一行，它会运行到0.9的时候自动切换场景，换一句话说就是看不到加载进度条的显示

        yield return operation;
    }
    
    void Update()
    {
        //operation.progress 异步加载的进度（0-1）
        targetValue = operation.progress;

        if (operation.progress >= 0.9f)
        {
            //operation.progress的值最大为0.9
            targetValue = 1.0f;
        }

        if (targetValue != loadingSlider.value)
        {
            //插值运算
            loadingSlider.value = Mathf.Lerp(loadingSlider.value, targetValue, Time.deltaTime * loadingSpeed);
            if (Mathf.Abs(loadingSlider.value - targetValue) < 0.01f)
            {
                loadingSlider.value = targetValue;
            }
        }

        loadingText.text = ((int)(loadingSlider.value * 100)).ToString() + "%";

        if ((int)(loadingSlider.value * 100) == 100)
        {
            Destroy(GameObject.Find("LodingPanel"));
            //允许异步加载完毕后自动切换场景
            operation.allowSceneActivation = true;
        }
    }
}
