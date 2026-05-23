using UnityEngine;

public class WaveEffect : MonoBehaviour
{
    //public Vector3 speed = Vector3.zero;
    public float amplitude = 0.5f;
    public float velocidade = 1.0f;
    private Vector3 posInicial;

    void Start()
    {
        posInicial = transform.position;
    }

    void Update()
    {
        //transform.Rotate(speed * Time.deltaTime);
        float novoY = posInicial.y + Mathf.Sin(Time.time * velocidade) * amplitude;
        transform.position = new Vector3(posInicial.x, novoY, posInicial.z);
    }
}
