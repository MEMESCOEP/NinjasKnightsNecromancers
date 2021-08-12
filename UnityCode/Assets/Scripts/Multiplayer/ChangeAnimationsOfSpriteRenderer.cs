using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeAnimationsOfSpriteRenderer : MonoBehaviour
{

    public Sprite[] Sprites;
    public SpriteRenderer sprenderer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sprenderer.sprite = Sprites[0];
        sprenderer.sprite = Sprites[1];
        sprenderer.sprite = Sprites[2];
        sprenderer.sprite = Sprites[3];
        sprenderer.sprite = Sprites[2];
        sprenderer.sprite = Sprites[1];
        sprenderer.sprite = Sprites[0];
    }
}
