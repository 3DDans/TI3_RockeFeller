using UnityEngine;

public class EndingCutscenePlayer : MonoBehaviour
{
    public SceneController sceneController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            sceneController.EndingCutscene();
        }
    }
}
