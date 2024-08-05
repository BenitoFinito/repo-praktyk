using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{

    public float speed;

    [SerializeField]
    private Renderer background;
    void Update()
    {
        background.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0);
    }
}
