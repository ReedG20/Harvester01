using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    //Prefabs
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

    //Larger = more detail
    float noiseScale = 0.111f;
    float scale = 2;

    //^2 is amount of tiles
    float size = 100f;

    //Higher = less
    float grassCutoff = 0.32f;
    float darkGrassCutoff = 0.05f;
    float rockCutoff = 0.65f;
    float treeCutoff = 0.47f;
    float darkTreeCutoff = 0.7f;
    float redGroundCutoff = 0.5f;

    float randomPosition;
    float randomPosition02;
    float randomPosition03;

    void Start()
    {
        randomPosition = Random.Range(-1000, 1000);
        randomPosition02 = Random.Range(-1000, 1000);
        randomPosition03 = Random.Range(-1000, 1000);
        CreateTerrain();
    }

    public void CreateTerrain()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (Mathf.PerlinNoise((i + randomPosition) * noiseScale, (j + randomPosition) * noiseScale) >= grassCutoff)
                {
                    //Land
                    if (Mathf.PerlinNoise((i + randomPosition03) * noiseScale, (j + randomPosition03) * noiseScale) >= rockCutoff)
                    {
                        //Rock base
                        Instantiate(ground_rock01, new Vector3((i - (size / 2)) * scale, 0f, (j - (size / 2)) * scale), Quaternion.Euler(-90f, 0f, 0f));

                        //Rock
                        if (Mathf.PerlinNoise((i + randomPosition03) * noiseScale, (j + randomPosition03) * noiseScale) >= (rockCutoff + ((1f - rockCutoff) / 6f) * 5f))
                        {
                            Instantiate(rock_rock_01, new Vector3((i - (size / 2)) * scale, 0f, (j - (size / 2)) * scale), Quaternion.Euler(-90f, 0f, 0f));
                        }
                        else if (Mathf.PerlinNoise((i + randomPosition03) * noiseScale, (j + randomPosition03) * noiseScale) >= (rockCutoff + ((1f - rockCutoff) / 6f) * 4f))
                        {
                            Instantiate(rock_rock00, new Vector3((i - (size / 2)) * scale, 0f, (j - (size / 2)) * scale), Quaternion.Euler(-90f, 0f, 0f));
                        }
                        else if (Mathf.PerlinNoise((i + randomPosition03) * noiseScale, (j + randomPosition03) * noiseScale) >= (rockCutoff + ((1f - rockCutoff) / 6f) * 3f))
                        {
                            Instantiate(rock_rock01, new Vector3((i - (size / 2)) * scale, 0f, (j - (size / 2)) * scale), Quaternion.Euler(-90f, 0f, 0f));
                        }
                        else if (Mathf.PerlinNoise((i + randomPosition03) * noiseScale, (j + randomPosition03) * noiseScale) >= (rockCutoff + ((1f - rockCutoff) / 6f) * 2f))
                        {
                            Instantiate(rock_rock02, new Vector3((i - (size / 2)) * scale, 0f, (j - (size / 2)) * scale), Quaternion.Euler(-90f, 0f, 0f));
                        }
                        else if (Mathf.PerlinNoise((i + randomPosition03) * noiseScale, (j + randomPosition03) * noiseScale) >= (rockCutoff + ((1f - rockCutoff) / 6f)))
                        {
                            Instantiate(rock_rock03, new Vector3((i - (size / 2)) * scale, 0f, (j - (size / 2)) * scale), Quaternion.Euler(-90f, 0f, 0f));
                        }
                        else
                        {
                            Instantiate(rock_rock04, new Vector3((i - (size / 2)) * scale, 0f, (j - (size / 2)) * scale), Quaternion.Euler(-90f, 0f, 0f));
                        }
                    }
                    else if (Mathf.PerlinNoise((i + randomPosition02) * noiseScale, (j + randomPosition02) * noiseScale) >= treeCutoff)
                    {
                        //Red ground
                        if (Random.Range(0f, 1f) >= redGroundCutoff)
                        {
                            Instantiate(ground_redDirt01, new Vector3((i - (size / 2)) * scale, 0f, (j - (size / 2)) * scale), Quaternion.Euler(-90f, 0f, 0f));
                        }
                        else
                        {
                            Instantiate(ground_redDirt02, new Vector3((i - (size / 2)) * scale, 0f, (j - (size / 2)) * scale), Quaternion.Euler(-90f, 0f, 0f));
                        }

                        //Trees
                        if (Random.Range(0f, 1f) >= darkTreeCutoff)
                        {
                            Instantiate(nature_tree01, new Vector3((i - (size / 2)) * scale, 0f, (j - (size / 2)) * scale), Quaternion.Euler(-90f, Random.Range(0f, 360f), 0f));
                        }
                        else
                        {
                            Instantiate(nature_tree02, new Vector3((i - (size / 2)) * scale, 0f, (j - (size / 2)) * scale), Quaternion.Euler(-90f, Random.Range(0f, 360f), 0f));
                        }
                    }
                    else
                    {
                        //Grass
                        if (Random.Range(0f, 1f) >= darkGrassCutoff)
                        {
                            Instantiate(ground_grass01, new Vector3((i - (size / 2)) * scale, 0f, (j - (size / 2)) * scale), Quaternion.Euler(-90f, 0f, 0f));
                        }
                        else
                        {
                            Instantiate(ground_grass02, new Vector3((i - (size / 2)) * scale, 0f, (j - (size / 2)) * scale), Quaternion.Euler(-90f, 0f, 0f));
                            Instantiate(nature_grass01, new Vector3((i - (size / 2)) * scale, 0f, (j - (size / 2)) * scale), Quaternion.Euler(-90f, 180f, 0f));
                        }
                    }
                }
                else
                {
                    //Water
                    Instantiate(ground_water01, new Vector3((i - (size / 2)) * scale, 0f, (j - (size / 2)) * scale), Quaternion.Euler(-90f, 0f, 0f));
                }
            }
        }
    }
}
