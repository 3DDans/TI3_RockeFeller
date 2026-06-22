using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EndingCutscenePlayer : MonoBehaviour
{
    public SceneController sceneController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sceneController.EndingCutscene();
            AreaManager.Instance.SetArea(TaskArea.Global);
        }
    }
}
