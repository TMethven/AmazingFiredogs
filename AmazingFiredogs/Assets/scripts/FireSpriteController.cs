using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpriteController : MonoBehaviour
{
    public SpriteRenderer[] sprites;
    public float flickerTime = 0.2f;
    private int spriteOn = 0;
    private float timeSinceLastFlicker = 0;

	void Start()
    {
		
	}
	
	void Update()
    {
        timeSinceLastFlicker += Time.deltaTime;

        if(timeSinceLastFlicker > flickerTime)
        {
            sprites[spriteOn].gameObject.SetActive(false);

            spriteOn = (spriteOn + 1) % sprites.Length;

            sprites[spriteOn].gameObject.SetActive(true);
            timeSinceLastFlicker = 0;
        }
	}

    public void setTransparency(float transparency)
    {
        for(int i = 0; i < sprites.Length; i++)
        {
            sprites[i].color = new Color(1f, 1f, 1f, transparency);
        }
    }
}
