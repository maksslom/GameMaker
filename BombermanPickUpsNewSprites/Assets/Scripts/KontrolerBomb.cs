using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;

public class KontrolerBomb : MonoBehaviour
{
    [Header("Bomba")]
    public GameObject bombPrefab;
    public KeyCode inputKey = KeyCode.Space;
    public float bombCooldown = 3f;
    public int bombLimit = 1;
    private int bombsRemaining;

    [Header("Wybuch")]
    public WybuchScript wybuchPrefab;
    public LayerMask explosionLayerMask;
    public float explosionDuration = 1f;
    public int explosionRadius = 1;

    [Header("Zniszczalny")]
    public Tilemap zniszczalneKafelki;
    public ZniszczalnyScript zniszczalnyPrefab;



    private void OnEnable()
    {
        bombsRemaining = bombLimit;   
    }

    void Update()
    {
        if (bombsRemaining > 0 && Input.GetKeyDown(inputKey)) 
        {
            StartCoroutine(PlaceBomb());
        }
    }

    
    private IEnumerator PlaceBomb() 
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject bomba = Instantiate(bombPrefab, position, Quaternion.identity);
        bombsRemaining--;

        yield return new WaitForSeconds(bombCooldown);

        position = bomba.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        WybuchScript wybuch = Instantiate(wybuchPrefab, position, Quaternion.identity);
        wybuch.SetActiveRenderer(wybuch.start);
        wybuch.DestroyAfter(explosionDuration);
        Destroy(wybuch.gameObject, explosionDuration);

        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);

        Destroy(bomba);
        bombsRemaining++;

    }

    private void Explode(Vector2 position, Vector2 direction, int length) 
    { 
    if (length <= 0) 
        { 
            return; 
        }

        position += direction;

        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask)) 
        {
            ClearDestructible(position);
            return;
        }

        WybuchScript wybuch = Instantiate(wybuchPrefab, position, Quaternion.identity);
        // length > 1 to warunek. Jeœli warunek jest spe³niony to w tym wypadku zwraca wybuch.middle jeœli nie jest spe³niony to zwraca wybuch.end
        wybuch.SetActiveRenderer(length > 1 ? wybuch.middle : wybuch.end);
        wybuch.SetDirection(direction);
        wybuch.DestroyAfter(explosionDuration);

        Explode(position, direction, length - 1);
    }


    private void ClearDestructible(Vector2 position) 
    { 
        Vector3Int cell = zniszczalneKafelki.WorldToCell(position);
        TileBase tile = zniszczalneKafelki.GetTile(cell);

        if (tile != null) 
        {
            Instantiate(zniszczalnyPrefab, position, Quaternion.identity);
            zniszczalneKafelki.SetTile(cell, null);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
       if (other.gameObject.layer == LayerMask.NameToLayer("bomba")) 
        {
            other.isTrigger = false;
        }
    }

    public void AddBomb() 
    {
        bombLimit++;
        bombsRemaining++;
    }



}
