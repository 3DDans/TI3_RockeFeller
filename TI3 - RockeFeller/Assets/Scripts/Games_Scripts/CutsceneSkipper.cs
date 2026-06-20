using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class CutsceneSkipper : MonoBehaviour
{
    public PlayableDirector director;
    public DialogueSystemCutscene dialogueCutscene;
    public Slider sliderSkip;
    bool isHoldingTab = false;
    float timeHolding = 0f;
    float timeToHold = 2f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)){
            isHoldingTab = true;
            timeHolding = 0f;
            sliderSkip.gameObject.SetActive(true);
        }
        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            isHoldingTab = false;
            sliderSkip.gameObject.SetActive(false);
        }

        if (isHoldingTab)
        {
            timeHolding += Time.deltaTime;
            sliderSkip.value = timeHolding;

            if (timeHolding >= timeToHold)
            {
                SkipCutscene();
            }
        }
    }

    public void SkipCutscene()
    {
        StartCoroutine(SkipCutsceneCoroutine());
    }

    IEnumerator SkipCutsceneCoroutine()
    {
        TransitionController.instance.FadeOut();
        yield return new WaitForSeconds(3f);
        dialogueCutscene.EndDialogue();
        director.Pause();
        director.time = director.duration - 0.2f;
        director.Play();
        TransitionController.instance.FadeIn();
    }
}
