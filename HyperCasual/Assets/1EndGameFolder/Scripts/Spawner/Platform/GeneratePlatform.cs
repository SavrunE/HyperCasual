using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlatform : MonoBehaviour
{
    public Sprite StartBlock;
    public Sprite commonBlock;
    public Sprite EndBlock;

    private int completedLevels;
    public int BlockToLevels;
    private void Start()
    {
        StartCoroutine(GenerateLevel());
    }
    private IEnumerator GenerateLevel() 
    {
        Vector2 size = new Vector2(1, 1);
        Vector2 position = new Vector2(0, 0);
        GameObject newBlock = new GameObject("Start block");
        newBlock.transform.position = position;
        newBlock.transform.localScale = size;
        SpriteRenderer renderer = newBlock.AddComponent<SpriteRenderer>();
        renderer.sprite = this.StartBlock;

        int count = this.completedLevels + BlockToLevels;

        for (int i = 0; i < count; i++)
        {
            newBlock = new GameObject("Middle block");
            renderer = newBlock.AddComponent<SpriteRenderer>();
            renderer.sprite = this.commonBlock;

            newBlock.transform.localScale = size;
            position.x += size.x;
            position.y += size.y * Random.Range(-1, 2);
            newBlock.transform.position = position;
            newBlock.transform.localScale = size;

            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
    }
    public void CompleteLevel()
    {
        this.completedLevels++;
        StartCoroutine(GenerateLevel());
    }
}
