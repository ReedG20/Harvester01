using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    // Prefabs
    public GameObject ground_grass01;
    public GameObject ground_grass02;

    public GameObject ground_redDirt01;
    public GameObject ground_redDirt02;

    public GameObject nature_grass01;

    public GameObject rock_rock_01;
    public GameObject rock_rock00;
    public GameObject rock_rock01;
    public GameObject rock_rock02;
    public GameObject rock_rock03;
    public GameObject rock_rock04;

    public GameObject ground_rock01;

    public GameObject ground_water01;

    public GameObject nature_tree01;
    public GameObject nature_tree02;

    float currentSample;

    Vector3 defaultRotation = new Vector3(-90f, 0f, 0f);

    // Larger = more detail
    float noiseScale = 0.111f;
    float scale = 2;

    // ^2 is amount of tiles
    float size = 100f;

    // Higher = less
    float grassCutoff = 0.32f;
    float darkGrassCutoff = 0.05f;
    float rockCutoff = 0.65f;
    float treeCutoff = 0.47f;
    float darkTreeCutoff = 0.7f;
    float redGroundCutoff = 0.5f;

    float randomPosition01;
    float randomPosition02;
    float randomPosition03;

    int _i;
    int _j;

    void Start()
    {
        randomPosition01 = Random.Range(-1000, 1000);
        randomPosition02 = Random.Range(-1000, 1000);
        randomPosition03 = Random.Range(-1000, 1000);
        CreateTerrain();
    }

    /*
    void CreateTerrain()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                GenerateTile(GetBiome())
            }
        }
    }

    int GetBiome(int i, int j)
    {
        
    }

    void GenerateTile(int biome)
    {

    }
    */

    void CreateTerrain()
    {
        for (int i = 0; i < size; i++)
        {
            _i = i;
            for (int j = 0; j < size; j++)
            {
                _j = j;
                if (GenerateBool(randomPosition01, grassCutoff))
                {
                    // Land
                    if (GenerateBool(randomPosition03, rockCutoff))
                    {
                        // Rock base
                        InstantiateObject(ground_rock01, defaultRotation);

                        // Rock
                        if (GenerateBool(randomPosition03, (rockCutoff + ((1f - rockCutoff) / 6f) * 5f)))
                        {
                            InstantiateObject(rock_rock_01, defaultRotation);
                        }
                        else if (GenerateBool(randomPosition03, (rockCutoff + ((1f - rockCutoff) / 6f) * 4f)))
                        {
                            InstantiateObject(rock_rock00, defaultRotation);
                        }
                        else if (GenerateBool(randomPosition03, (rockCutoff + ((1f - rockCutoff) / 6f) * 3f)))
                        {
                            InstantiateObject(rock_rock01, defaultRotation);
                        }
                        else if (GenerateBool(randomPosition03, (rockCutoff + ((1f - rockCutoff) / 6f) * 2f)))
                        {
                            InstantiateObject(rock_rock02, defaultRotation);
                        }
                        else if (GenerateBool(randomPosition03, (rockCutoff + ((1f - rockCutoff) / 6f))))
                        {
                            InstantiateObject(rock_rock03, defaultRotation);
                        }
                        else
                        {
                            InstantiateObject(rock_rock04, defaultRotation);
                        }
                    }
                    else if (GenerateBool(randomPosition02, treeCutoff))
                    {
                        // Red ground
                        if (Random.Range(0f, 1f) >= redGroundCutoff)
                        {
                            InstantiateObject(ground_redDirt01, defaultRotation);
                        }
                        else
                        {
                            InstantiateObject(ground_redDirt02, defaultRotation);
                        }

                        // Trees
                        if (Random.Range(0f, 1f) >= darkTreeCutoff)
                        {
                            InstantiateObject(nature_tree01, new Vector3(-90f, Random.Range(0f, 360f), 0f));
                        }
                        else
                        {
                            InstantiateObject(nature_tree02, new Vector3(-90f, Random.Range(0f, 360f), 0f));
                        }
                    }
                    else
                    {
                        // Grass
                        if (Random.Range(0f, 1f) >= darkGrassCutoff)
                        {
                            InstantiateObject(ground_grass01, defaultRotation);
                        }
                        else
                        {
                            InstantiateObject(ground_grass02, defaultRotation);
                            InstantiateObject(nature_grass01, new Vector3(-90f, 180f, 0f));
                        }
                    }
                }
                else
                {
                    // Water
                    InstantiateObject(ground_water01, defaultRotation);
                }
            }
        }
    }

    // Function instantiates an object given the prefab and rotation
    void InstantiateObject(GameObject prefab, Vector3 rotation)
    {
        Instantiate(prefab, new Vector3((_i - (size / 2)) * scale, 0f, (_j - (size / 2)) * scale), Quaternion.Euler(rotation));
    }

    // Generates boolean based on the perlin noise and the location of the tile
    bool GenerateBool(float randomPosition, float cutoff)
    {
        return Mathf.PerlinNoise((_i + randomPosition) * noiseScale, (_j + randomPosition) * noiseScale) >= cutoff;
    }
}