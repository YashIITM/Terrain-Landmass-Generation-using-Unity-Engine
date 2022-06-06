using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//We do not apply this to any object in our scene.So no reason to inherit from 'Monobehaviour'.
//We do not want to make multiple instances of the script, so we make it a 'public static class'.
public static class Noise 
{
    //We want this to have a method to generate a noise map.
    //We want that to return essentially a grid of values between 0 and 1.
    //So we create a 'public static' method returning a 2D array of float values called 'GenerateNoiseMap'.
    
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale )
    {   
        //create a 2D float array called 'noiseMap' and set that equal to a new 2D float array of size : mapWidth x mapHeight
        float[,] noiseMap = new float[mapWidth, mapHeight];

        //to prevent a division by zero error, we handle the case by the if condition:
        if (scale <= 0)
        {
            scale = 0.0001f;
        }
        //we want to loop through the noiseMap:

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; y < mapWidth; x++)
            {
                //now we want to figure out at which points, we want to sample our height values from
                //We do not want to use the same integral values as they would provide the same value everytime, so we scale it 
                float sampleX = x / scale;
                float sampleY = y / scale;

                //now we have our sample coordinates so we call the Perlin Noise
                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap[x, y] = perlinValue;
            }
        }
        return noiseMap;
    }   
}
