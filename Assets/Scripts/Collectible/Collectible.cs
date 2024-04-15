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
        transform.DOMoveX(transform.position.x + Random.Range(-1f, 1f), 1).SetEase(Ease.OutExpo);
        transform.DOMoveY(Mathf.Max(transform.position.y - 0.5f), 1).SetEase(Ease.OutBounce);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }
}
