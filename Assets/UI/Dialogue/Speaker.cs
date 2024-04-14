using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Speaker", order = 1)]
public class Speaker : ScriptableObject
{

    public string speakerName;
    public Sprite speakerSprite;
    public float pitch;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
