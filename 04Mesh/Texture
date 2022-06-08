using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class TextureGenerator

{
    //will contain a method to create a texture out of a 1D color Map
    public static Texture2D TextureFromColourMap(Color[] colourMap,int width,int height)
    {
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colourMap);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromHeightMap(float[,] heightMap)
    {
        //first we need to figure out the dimensions of the noiseMap that we have been given:
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);
        //lets go ahead and set the color of each of the pixels of the texture
        //you can use 'texture.SetPixel(x,y,color)'
        //But it turns out its faster to generate an array of colors for all of the pixels and then to just set them all at once:
        //Create a color array first : Note 'colourMap' is a 1D array and out 'noiseMap' is a 2D array
        Color[] colourMap = new Color[width * height];
        //Now we assign the colow to each pixel
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {   //We want color at each pixel between black and white depending on its percentage
                //which is the same thing as value at the noiseMap(x,y)
                colourMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
            }
        }
        return TextureFromColourMap(colourMap, width, height);
    }
}
