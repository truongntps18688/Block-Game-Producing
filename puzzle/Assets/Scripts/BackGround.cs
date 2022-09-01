using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public GameObject backgroundPrefab;

    void Start()
    {
        float startX = transform.position.x;
        float startY = transform.position.y;
        Vector2 v = backgroundPrefab.GetComponent<SpriteRenderer>().bounds.size;
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                GameObject background = (GameObject)Instantiate(backgroundPrefab, new Vector3(startX + (v.x * x), startY + (v.y * y), 0), Quaternion.identity);
                background.transform.parent = transform;

            }
        }
    }

    void Update()
    {
        
    }
    public Vector2 GetCenter(int x, int y)
    {
        return new Vector2(transform.position.x - 4 + x, transform.position.y + 4 - y);
    }
}
