using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingManager : MonoBehaviour
{
    public static PostProcessingManager Instance { get; private set;}
    private Volume volumen;
    private Vignette vignette;

    private void Awake()
    {
        if (Instance != null) {
            Debug.LogError("There is more than 1 instance of PostProcessingManager");
        }
        Instance = this;

        volumen = GetComponent<Volume>();
    }

    // Start is called before the first frame update
    void Start()
    {
        volumen.profile.TryGet(out vignette);
        VignetteOff();
    }

    public void VignetteOn()
    {
        vignette.active = true;
    }

    public void VignetteOff()
    {
        vignette.active = false;
    }
}
