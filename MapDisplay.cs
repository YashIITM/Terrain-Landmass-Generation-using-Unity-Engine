using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapDisplay : MonoBehaviour
{
    //we want a reference to the renderer so that we can set its texture
    public Renderer textureRender;
    //Then create a method called 'DrawNoisemap' that takes in the 2D float array generated in 'Noise' class as 'noiseMap'.
    public void DrawNoiseMap(float[,] noiseMap)
    {
        //first we need to figure out the dimensions of the noiseMap that we have been given:
        int width = noiseMap.GetLength(0);
        int height = noiseMap.GetLength(1);

        //now we just create a texture
        Texture2D texture = new Texture2D(width, height);

        //lets go ahead and set the color of each of the pixels of the texture
        //you can use 'texture.SetPixel(x,y,color)'
        //But it turns out its faster to generate an array of colors for all of the pixels and then to just set them all at once:
        //Create a color array first : Note 'colourMap' is a 1D array and out 'noiseMap' is a 2D array
        Color[] colourMap = new Color[width * height];
        //Now we assign the colow to each pixel
        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {   //We want color at each pixel between black and white depending on its percentage
                //which is the same thing as value at the noiseMap(x,y)
                colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }
        texture.SetPixels(colourMap);
        texture.Apply();

        //We want to apply texture to the texture renderer:
        //we do not want to enter game mode everytime we want to preview our maps
        //That means  we cannot use'textureRenderer.material' as that is only instantiated at Run Time
        textureRender.sharedMaterial.mainTexture = texture;
        textureRender.transform.localScale = new Vector3(width, 1, height);


    }
    
}
