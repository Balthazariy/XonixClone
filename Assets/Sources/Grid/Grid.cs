using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public Transform _grass;
    public Transform _water;
    public Transform city;

    public int randNum = 0;
    public int xCubes;
    public int zCubes;

    private void Start()
    {
        for (int z = 0; z < zCubes; z++)
        {
            for (int x = 0; x < xCubes; x++)
            {
                float rnd = Random.value;

                if (rnd < 0.05)
                {
                    Instantiate(city, new Vector3(x, 0, z), Quaternion.identity);
                }
                else if (rnd < 0.15)
                {
                    Instantiate(_water, new Vector3(x, 0, z), Quaternion.identity);
                }
                else
                {
                    Instantiate(_grass, new Vector3(x, 0, z), Quaternion.identity);
                    randNum = Random.Range(0, 50);
                }
            }
        }
    }
}
