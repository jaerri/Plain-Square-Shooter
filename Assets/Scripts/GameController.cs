using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Sprite square;

    // Start is called before the first frame update
    void Start()
    {
        float min = float.MaxValue;
        float max = float.MinValue;

        const float ratio = 1.5f;

        for (int x = 0; x < 100; x++)
        {
            for (int y = 0; y < 100; y++)
            {
                float p = Mathf.PerlinNoise(x * ratio, y * ratio);

                if (p < min) min = p;
                if (p > max) max = p;

                Instantiate(square, new Vector3(x, y, 0), Quaternion.identity, gameObject.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
