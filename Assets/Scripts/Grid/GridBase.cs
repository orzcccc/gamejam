using System;
using UnityEngine;

public class GridBase : MonoBehaviour
{
    private Grid grid;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    { 
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Init(Vector2Int coord,GridType type)
    {
        grid.type = type;
    }
}
