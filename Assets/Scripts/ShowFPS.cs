using UnityEngine;
using UnityEngine.UI;

public class ShowFPS : MonoBehaviour
{
    private float timer, refresh, avgFramerate=0f;
    public string display = "{0} FPS";
    public Text m_Text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float timelapse = Time.smoothDeltaTime;
        timer = timer <= 0 ? refresh : timer -= timelapse;

        if (timer <= 0) avgFramerate = (int)(1f / timelapse);
        m_Text.text = string.Format(display,avgFramerate.ToString());
    }
}
