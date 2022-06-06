using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    //we want a reference to the renderer so that we can set its texture
    public Renderer textureRender;
    
    //Then create a method called 'DrawNoisemap' that takes in the 2D float array generated in 'Noise' class as 'noiseMap'.
    public void DrawTexture(Texture2D texture)
    {
        
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(texture.width, 1, texture.height);


    }

}
