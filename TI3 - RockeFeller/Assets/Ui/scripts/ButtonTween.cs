using UnityEngine;

public class ButtonTween : MonoBehaviour
{
    public GameObject[] buttons;
    void Start()
    {
        LeanTween.init();
        float t = 0;

        foreach (GameObject o in buttons) 
        {
            LeanTween.scale(o, new Vector3(0, 0, 0), 0.0f);
            LeanTween.scale(o, new Vector3(1,1,1), 0.2f).setDelay(t);
            t += 0.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
