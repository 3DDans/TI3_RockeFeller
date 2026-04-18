using Unity.VisualScripting;
using UnityEngine;
using Unity.Cinemachine;

public class SceneController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Opening()
    {
        GameObject camera = GameObject.FindWithTag("MainCamera");
        camera.GetComponent<CinemachineOrbitalFollow>().enabled = false;
        camera.GetComponent<CinemachineOrbitalFollow>().enabled = true;
    }
}
