using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    //enum to lista/klasa reprezentuj�ca grup� sta�ych zmiennych liczbowych
   public enum ItemType 
    { 
        ExtraBomb,
        BlastRadius,
        SpeedIncrease,
    }

    public ItemType type;



    /*tworz� osobnego voida kt�rego przywo�uj� w 'OnTriggerEnter',
     w tym voidzie odnosz� si� do gracza i jego komponent�w 
    dodaj�c mu r�ne warto�ci*/
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



    /*other odnosi si� do innego colidera kt�ry wchodzi w kolizj� z obiektem na kt�rym 
    jest ten script, w tym wypadku other to b�dzie gracz*/
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        { 
            OnItemPickup(other.gameObject);
        }
    }



}
