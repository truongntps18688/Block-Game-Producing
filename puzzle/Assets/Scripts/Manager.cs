using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Manager : MonoBehaviour
{
    public static Manager instance;
    public bool IsShifting { get; set; }

    public List<Sprite> sprites = new List<Sprite>();
    public GameObject tile;

    public GameObject[,] tiles;


    // 
    Sprite[] previousLeft = new Sprite[8];
    Sprite previousBelow = null;


    void Start()
    {
        instance = GetComponent<Manager>();
        Vector2 v = tile.GetComponent<SpriteRenderer>().bounds.size;
        createBoard(v.x, v.y);
    }
    private void createBoard(float _x, float _y)
    {
        tiles = new GameObject[8, 8];

        float startX = transform.position.x;
        float startY = transform.position.y;
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {   
                // set position
                GameObject newTile = Instantiate(tile, new Vector3(startX + (_x * x), startY + (_y * y), 0), tile.transform.rotation);
                tiles[x, y] = newTile;
                tiles[x, y].name =  "(" + x + " : " + y +")";
                newTile.transform.parent = transform;

                // ngăn chặn lặp 3 
                List<Sprite> listSprite = new List<Sprite>();
                listSprite.AddRange(sprites);
                listSprite.Remove(previousLeft[y]);
                listSprite.Remove(previousBelow);

                int index = Random.Range(0, listSprite.Count);
                
                Sprite newSprite = listSprite[index];
                newTile.GetComponent<SpriteRenderer>().sprite = newSprite;

                previousLeft[y] = newSprite;
                previousBelow = newSprite;
            }
        }
    }
    public IEnumerator FindNullTiles()
    {
        for (int x = 0; x < 8; x++)
        {
            for (int y = 0; y < 8; y++)
            {
                if (tiles[x, y].GetComponent<SpriteRenderer>().sprite == null)
                {
                    yield return StartCoroutine(ShiftTilesDown(x, y));
                    break;
                }
            }
        }
    }
    
    private IEnumerator ShiftTilesDown(int x, int yStart, float shiftDelay = .1f)
    {
        IsShifting = true;
        List<SpriteRenderer> renders = new List<SpriteRenderer>();
        int nullCount = 0;

        for (int y = yStart; y < 8; y++)
        {  
            SpriteRenderer render = tiles[x, y].GetComponent<SpriteRenderer>();
            if (render.sprite == null)
            { 
                nullCount++;
            }
            renders.Add(render);
        }

        for (int i = 0; i < nullCount; i++)
        { 
            yield return new WaitForSeconds(shiftDelay);
            for (int k = 0; k < renders.Count - 1; k++)
            {
                renders[k].sprite = renders[k + 1].sprite;
                renders[k].size = new Vector2(1f, 1f);
                renders[k + 1].sprite = GetNewSprite(x, 8 - 1);
                renders[k + 1].size = new Vector2(1f, 1f);
            }
        }
        IsShifting = false;
    }
    private Sprite GetNewSprite(int x, int y)
    {
        List<Sprite> possibleCharacters = new List<Sprite>();
        possibleCharacters.AddRange(sprites);

        if (x > 0)
        {
            possibleCharacters.Remove(tiles[x - 1, y].GetComponent<SpriteRenderer>().sprite);
        }
        if (x < 8 - 1)
        {
            possibleCharacters.Remove(tiles[x + 1, y].GetComponent<SpriteRenderer>().sprite);
        }
        if (y > 0)
        {
            possibleCharacters.Remove(tiles[x, y - 1].GetComponent<SpriteRenderer>().sprite);
        }

        return possibleCharacters[Random.Range(0, possibleCharacters.Count)];
    }



}
