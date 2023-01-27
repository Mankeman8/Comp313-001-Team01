using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode { NoiseMap, ColorMap, Mesh, FalloffMap};
    public DrawMode drawMode;

    [Header("Noise Settings")]
    public Vector2Int size;
    public int seed;
    public float noiseScale;
    public int octaves;
    [Range(0,1)]
    public float persistance;
    public float lacunarity;
    public Vector2 offset;

    [Header("Map Settings")]
    public float MeshHeightMultiplier;
    public AnimationCurve meshHeightCurve;

    [Header("Falloff Settings")]
    public bool useFalloff;
    [Range(0, 1)]
    public float falloffStart;
    [Range(0, 1)]
    public float falloffEnd;

    //bool to auto update whenever we change a value in the editor
    [Header("Others")]
    public bool autoUpdate;
    public TerrainType[] regions;
    float[,] falloffMap;

    private void Awake()
    {
        falloffMap = FalloffGenerator.GenerateFalloffMap(size,falloffStart,falloffEnd);
    }

    public void DrawMapInEditor()
    {
        //call map and display it
        MapData mapData = GenerateMapData();
        MapDisplay display = FindObjectOfType<MapDisplay>();
        //Display the different types of maps in the editor
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(mapData.heightMap));
        }
        else if (drawMode == DrawMode.ColorMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColorMap(mapData.colorMap, size.x, size.y));
        }
        else if (drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(mapData.heightMap, MeshHeightMultiplier, meshHeightCurve), TextureGenerator.TextureFromColorMap(mapData.colorMap, size.x, size.y));
        }
        else if (drawMode == DrawMode.FalloffMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(FalloffGenerator.GenerateFalloffMap(size, falloffStart, falloffEnd)));
        }
    }

    MapData GenerateMapData()
    {
        //Complicated script, details will be either in there in a massive comment
        //or in a separate doc
        float[,] noiseMap = Noise.GenerateNoiseMap(size.x, size.y, seed, noiseScale, octaves, persistance, lacunarity, offset);
        Color[] colorMap = new Color[size.x * size.y];
        for (int y = 0; y < size.y; y++)
        {
            for (int x = 0; x < size.x; x++)
            {
                if (useFalloff)
                {
                    noiseMap[x, y] = Mathf.Clamp01(noiseMap[x, y] - falloffMap[x, y]);
                }
                float currentHeight = noiseMap[x, y];
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colorMap[y * (size.x) + x] = regions[i].color;
                        break;
                    }
                }
            }
        }
        return new MapData(noiseMap, colorMap);
    }
    //Whenever changing values in the editor, we don't want them to go below
    //or above a certain value
    private void OnValidate()
    {
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
        falloffMap = FalloffGenerator.GenerateFalloffMap(size, falloffStart, falloffEnd);
    }
}

//To make our terrain have color/name at certain heights
[System.Serializable]
public struct TerrainType
{
    public string terrainName;
    public float height;
    public Color color;

}

public struct MapData
{
    public float[,] heightMap;
    public Color[] colorMap;

    public MapData(float[,] heightMap, Color[] colorMap)
    {
        this.heightMap = heightMap;
        this.colorMap = colorMap;
    }
}