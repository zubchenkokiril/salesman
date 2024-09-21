using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    public TextMeshProUGUI fpsText; 
    public float smoothing = 0.1f; 
    private float fps;
    private float fpsSmooth;

    private void Update()
    {
        fps = 1.0f / Time.deltaTime;

        fpsSmooth = Mathf.Lerp(fpsSmooth, fps, smoothing);

        fpsText.text = $"FPS: {Mathf.Round(fpsSmooth)}";
    }
}
