using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMaterial : MonoBehaviour
{

    void Start()
    {
        Renderer renderer = GetComponent<Renderer>();
        renderer.material = new Material(renderer.material);
    }
}
