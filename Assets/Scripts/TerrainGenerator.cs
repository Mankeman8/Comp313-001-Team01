using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    //Height of the terrain
    public int height = 20;

    //Length x Width of the terrain
    public int width = 500;
    public int length = 500;

    //Scale to multiply for the height generator, to zoom in and out
    public float scale = 20;

    //Randomize the terrain each time
    public float offsetX = 100f;
    public float offsetY = 100f;

    //The terrain
    private Terrain terrain;

    private void Start()
    {
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);
    }
    private void Update()
    {
        //Start of the level, generate a new terrain
        terrain = GetComponent<Terrain>();
        terrain.terrainData = GenerateTerrain(terrain.terrainData);
    }
    
    TerrainData GenerateTerrain(TerrainData terrainData)
    {
        terrainData.heightmapResolution = width + 1;
        //Set the dimensions of the terrain
        terrainData.size = new Vector3(width, height, length);

        //Modify the height of the terrain
        terrainData.SetHeights(0, 0, GenerateHeights());
        return terrainData;
    }

    float[,] GenerateHeights()
    {
        //Create float array
        float[,] heights = new float[width, length];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < length; j++)
            {
                //For height and length, we calculate the height and put it into a variable
                heights[i, j] = CalculateHeight(i, j);
            }
        }
        return heights;
    }
    float CalculateHeight(int x, int y)
    {
        //Calculate the noise coordinates
        float xCoord = (float)x / width * scale + offsetX;
        float yCoord = (float)y / length * scale + offsetY;

        //Return the noise coordinates
        return Mathf.PerlinNoise(xCoord, yCoord);
    }
}
