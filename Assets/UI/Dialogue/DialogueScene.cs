using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/DialogueScene", order = 1)]
public class DialogueScene : ScriptableObject
{

    public Speaker[] speakers; // List of speakers in the scene
    public string[] lines; // List of lines to be read
    public int[] speakerIndexes; // Index of speakers to map to lines

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
