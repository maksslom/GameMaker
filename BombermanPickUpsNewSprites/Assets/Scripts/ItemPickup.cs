using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    //enum to lista/klasa reprezentuj¹ca grupê sta³ych zmiennych liczbowych
   public enum ItemType 
    { 
        ExtraBomb,
        BlastRadius,
        SpeedIncrease,
    }

    public ItemType type;



    /*tworzê osobnego voida którego przywo³ujê w 'OnTriggerEnter',
     w tym voidzie odnoszê siê do gracza i jego komponentów 
    dodaj¹c mu ró¿ne wartoœci*/
    private void OnItemPickup(GameObject player)
    { //switch to inny rodzaj if, elif itd
        switch (type)
        {
            case ItemType.ExtraBomb:
                player.GetComponent<KontrolerBomb>().AddBomb();
                break;

            case ItemType.BlastRadius:
                player.GetComponent<KontrolerBomb>().explosionRadius++;
                break;

            case ItemType.SpeedIncrease:
                player.GetComponent<KontrolerRuchu>().speed++;
                break;
        }

        Destroy(gameObject);
    }



    /*other odnosi siê do innego colidera który wchodzi w kolizjê z obiektem na którym 
    jest ten script, w tym wypadku other to bêdzie gracz*/
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        { 
            OnItemPickup(other.gameObject);
        }
    }



}
