using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tile : MonoBehaviour
{
    private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
    private static tile previousSelected = null;

    private SpriteRenderer sprite;
    private bool click;

    private Vector2[] v2 = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };

    void Awake()
	{
		sprite = GetComponent<SpriteRenderer>();
	}
	private void Select()
	{
		click = true;
		sprite.color = selectedColor;
		previousSelected = gameObject.GetComponent<tile>();
	}

    private void Deselect()
	{
		click = false;
		sprite.color = Color.white;
		previousSelected = null;
	}
    void FixedUpdate()
    {
        if (previousSelected != null)
        {

        }
    }

    void OnMouseDown()
	{
        if (sprite.sprite == null || Manager.instance.IsShifting)
        {
            return;
        }
        if (click)
        {
            Deselect();
        }
        else
        {
            if (previousSelected == null)
            {
                Select();
            }
            else
            {
                Vector2 v = previousSelected.transform.position - transform.position;
                if (((v.x == 1 || v.x == -1) && v.y == 0) || ((v.y == 1 || v.y == -1) && v.x == 0))
                {
                    swap(previousSelected.sprite);
                    previousSelected.Deselect();
                    TimeGame.Instance.addTime(-0.5f);
                }
                else
                {
                    previousSelected.GetComponent<tile>().Deselect();
                    Select();
                }
            }
        }

    }
    public void swap(SpriteRenderer _sprite)
    {
        if (sprite.sprite == _sprite.sprite) return;
        Sprite rd = sprite.sprite;
        sprite.sprite = _sprite.sprite;
        _sprite.sprite = rd;
    }

}
