using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    // config param
    [SerializeField] AudioClip breakSound;
    [SerializeField] GameObject blockSparklesVFX;
    [SerializeField] Sprite[] hitSprites;

    // cache ref
    Level level;
    GameSession gameSession;
    SpriteRenderer spriteRenderer;

    // state variables
    [SerializeField] int timesHit;          // TODO remove serialize

    private void Start()
    {
        gameSession = FindObjectOfType<GameSession>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        CountBreakableBlocks();
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag.Equals("Breakable"))
        {
            level.CountBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (tag.Equals("Breakable"))
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            DestroyBlock();
        }
        else
        {
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        spriteRenderer.sprite = hitSprites[spriteIndex];
    }

    private void DestroyBlock()
    {
        PlayBlockDestroySFX();
        Destroy(gameObject);
        level.RemoveBlock();
        TriggerSparklesVFX();
    }

    private void PlayBlockDestroySFX()
    {
        gameSession.AddToScore();
        AudioSource.PlayClipAtPoint(breakSound, Camera.main.transform.position);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 1f);
    }

}
