using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static InventoryItem;

public class Collectible : MonoBehaviour
{
    public ItemClass itemClass;

    void Start()
    {
        //transform.DOPunchPosition(Vector3.up/2, 2f, 2).SetEase(Ease.OutBounce);

        //HACK
        float targetx = FindObjectOfType<PlayerController>().transform.position.x;
        transform.DOMoveX(transform.position.x + ((targetx > transform.position.x ? 1 : -1) * Random.Range(0.1f,1f)), 1).SetEase(Ease.OutExpo);

    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
