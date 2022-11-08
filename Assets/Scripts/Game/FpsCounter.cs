using TMPro;
using UnityEngine;

public class FpsCounter : MonoBehaviour
{
    [SerializeField] 
    private TextMeshProUGUI text;
    [SerializeField] 
    private float refreshRate = 1f;

    private float timer;

    private void Update()
    {
        if (Time.unscaledTime > timer)
        {
            int fps = (int)(1.0f / Time.unscaledDeltaTime);
            text.text = fps.ToString();
            timer = Time.unscaledTime + refreshRate;
        }
    }
}
