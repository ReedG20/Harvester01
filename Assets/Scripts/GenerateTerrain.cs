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
    /*
    float defaultNoiseScale = 0.111f;
    float biomeNoiseScale = 0.033f;
    */
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

    float randomBiomePosition01;
    float randomBiomePosition02;
    float randomBiomePosition03;

    int _i;
    int _j;

    // ----Biomes----

    // 1. Forest
    // 2. Desert
    // 3. Mountains
    // 4. Ocean
    // 5. Snowy

    // --------------

    public Biome[] biomes;
    public float[] randomNums;

    float biomeNoiseScale = 0.01f;

    void Start()
    {
        randomPosition01 = Random.Range(-1000, 1000);
        randomPosition02 = Random.Range(-1000, 1000);
        randomPosition03 = Random.Range(-1000, 1000);

        randomBiomePosition01 = Random.Range(-1000, 1000);
        randomBiomePosition02 = Random.Range(-1000, 1000);
        randomBiomePosition03 = Random.Range(-1000, 1000);

        CreateTerrain();
    }

    void CreateTerrain()
    {
        for (int i = 0; i < biomes.Length; i++)
        {
            randomNums[i] = Random.Range(-1000f, 1000f);
        }

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                // Each tile

                float value = 0f;
                int valueIndex = 0;

                for (int i = 0; i < biomes.Length; i++)
                {
                    float currentValue = Mathf.PerlinNoise((x + randomNums[i]) * biomeNoiseScale, y * biomeNoiseScale);

                    if (value < currentValue)
                    {
                        value = currentValue;
                        valueIndex = i;
                    }
                }

                // We know the coords of the tile, as well as which biome it is

                CreateBiomeTile(x, y, valueIndex);
            }
        }
    }

    void CreateBiomeTile(int x, int y, int biome)
    {
        for (int i = 0; i < biomes[biome].biomeElements.Length; i++)
        {
            // This is where I am
        }
    }

    // Function instantiates an object given the prefab and rotation
    void InstantiateObject(GameObject prefab, Vector3 rotation)
    {
        Instantiate(prefab, new Vector3((_i - (size / 2)) * scale, 0f, (_j - (size / 2)) * scale), Quaternion.Euler(rotation));
    }

    // Generates boolean based on the perlin noise and the location of the tile
    bool GenerateBool(float randomPosition, float cutoff, float noiseScale)
    {
        //Higher cutoff -> less likely return true
        return Mathf.PerlinNoise((_i + randomPosition) * noiseScale, (_j + randomPosition) * noiseScale) >= cutoff;
    }

    /*
    void CreateTerrain()
    {
        for (int i = 0; i < size; i++)
        {
            _i = i;
            for (int j = 0; j < size; j++)
            {
                _j = j;
                GenerateTile(GetBiome(i, j));

            }
        }
    }
    //*/

    // Newish functions
    /*
    // Returns biome as a number between 1 and 5 based on the given tile
    int GetBiome(int i, int j)
    {
        //1. Forest - 000, 001
        //2. Desert - 010
        //3. Mountains - 011
        //4. Ocean - 100, 101
        //5. Snowy - 110, 111

        int biome;

        bool b01 = false;
        bool b02 = false;
        bool b03 = false;

        if (GenerateBool(randomBiomePosition01, 0.5f, biomeNoiseScale))
        {
            b01 = true;
        }
        if (GenerateBool(randomBiomePosition02, 0.5f, biomeNoiseScale))
        {
            b02 = true;
        }
        if (GenerateBool(randomBiomePosition03, 0.5f, biomeNoiseScale))
        {
            b03 = true;
        }

        // Calclating biome based on bools
        if (b01)
        {
            if (b02)
            {
                if (b03)
                {
                    //111
                    biome = 5;
                }
                else
                {
                    //110
                    biome = 5;
                }
            }
            else
            {
                if (b03)
                {
                    //101
                    biome = 4;
                }
                else
                {
                    //100
                    biome = 4;
                }
            }
        }
        else
        {
            if (b02)
            {
                if (b03)
                {
                    //011
                    biome = 3;
                }
                else
                {
                    //010
                    biome = 2;
                }
            }
            else
            {
                if (b03)
                {
                    //001
                    biome = 1;
                }
                else
                {
                    //000
                    biome = 1;
                }
            }
        }

        return biome;
    }

    // Instatiates a tile based on the biome its in and the coordinates of the tile
    void GenerateTile(int biome)
    {
        if (biome == 1)
        {
            // Forest
            // Generate biome tile
            InstantiateObject(rock_rock01, defaultRotation);
        }
        else if (biome == 2)
        {
            // Desert
            //InstantiateObject(ground_water01, defaultRotation);
        }
        else if (biome == 3)
        {
            // Mountains
            //InstantiateObject(nature_tree01, defaultRotation);
        }
        else if (biome == 4)
        {
            // Ocean
            //InstantiateObject(nature_tree02, defaultRotation);
        }
        else if (biome == 5)
        {
            // Snowy
            //InstantiateObject(ground_redDirt01, defaultRotation);
        }
        else
        {
            Debug.LogError("Biome int not in range!");
        }
    }
    */

    // Old create terrain function
    /*
    void CreateTerrain()
    {
        for (int i = 0; i < size; i++)
        {
            _i = i;
            for (int j = 0; j < size; j++)
            {
                _j = j;
                if (GenerateBool(randomPosition01, grassCutoff, defaultNoiseScale))
                {
                    // Land
                    if (GenerateBool(randomPosition03, rockCutoff, defaultNoiseScale))
                    {
                        // Rock base
                        InstantiateObject(ground_rock01, defaultRotation);

                        // Rock
                        if (GenerateBool(randomPosition03, (rockCutoff + ((1f - rockCutoff) / 6f) * 5f), defaultNoiseScale))
                        {
                            InstantiateObject(rock_rock_01, defaultRotation);
                        }
                        else if (GenerateBool(randomPosition03, (rockCutoff + ((1f - rockCutoff) / 6f) * 4f), defaultNoiseScale))
                        {
                            InstantiateObject(rock_rock00, defaultRotation);
                        }
                        else if (GenerateBool(randomPosition03, (rockCutoff + ((1f - rockCutoff) / 6f) * 3f), defaultNoiseScale))
                        {
                            InstantiateObject(rock_rock01, defaultRotation);
                        }
                        else if (GenerateBool(randomPosition03, (rockCutoff + ((1f - rockCutoff) / 6f) * 2f), defaultNoiseScale))
                        {
                            InstantiateObject(rock_rock02, defaultRotation);
                        }
                        else if (GenerateBool(randomPosition03, (rockCutoff + ((1f - rockCutoff) / 6f)), defaultNoiseScale))
                        {
                            InstantiateObject(rock_rock03, defaultRotation);
                        }
                        else
                        {
                            InstantiateObject(rock_rock04, defaultRotation);
                        }
                    }
                    else if (GenerateBool(randomPosition02, treeCutoff, defaultNoiseScale))
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
    //*/
}

// Environment element class
[System.Serializable]
public class Biome
{
    public BiomeElement[] biomeElements;
}

// Environment element class
[System.Serializable]
public class BiomeElement
{
    // 
    public GameObject prefab;

    // 
    public float noiseScale;

    public float noisePosition;

    public float noiseCutoff;

    public float randomCutoff;
}