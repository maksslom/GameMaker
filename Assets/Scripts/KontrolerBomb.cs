using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KontrolerBomb : MonoBehaviour
{
    public GameObject bombPrefab;
    public KeyCode inputKey = KeyCode.Space;
    public float bombCooldown = 3f;
    public int bombLimit = 1;
    private int bombsRemaining;

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

        Destroy(bomba);
        bombsRemaining++;

    }

    private void OnTriggerExit2D(Collider2D other)
    {
       if (other.gameObject.layer == LayerMask.NameToLayer("bomba")) 
        {
            other.isTrigger = false;
        }
    }
}
