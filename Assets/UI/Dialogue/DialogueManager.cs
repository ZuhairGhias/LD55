using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogueManager : MonoBehaviour
{
    private DialogueScene scene;
    private int lineIndex;
    private VisualElement root;
    private AudioSource audioSource;
    private Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        root = GetComponent<UIDocument>().rootVisualElement;
        root.style.position = Position.Absolute;
        root.style.top = 0;
        root.style.bottom = 0;
        root.style.left = 0;
        root.style.right = 0;
        root.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (scene != null && Input.GetButtonDown("Jump"))
        {
            PlayNextLine();
        }
    }

    public void PlayDialogue(DialogueScene scene)
    {
        this.scene = scene;
        this.lineIndex = -1;
        dialogue = new Dialogue();
        dialogue.style.position = Position.Absolute;
        dialogue.style.top = 0;
        dialogue.style.bottom = 0;
        dialogue.style.left = 0;
        dialogue.style.right = 0;
        PlayNextLine();
        root.Add(dialogue);
    }

    private void PlayNextLine()
    {
        lineIndex += 1;

        // When scene is finished, hide dialogue
        if (lineIndex >= scene.lines.Length)
        {
            root.Clear();
            scene = null;
            return;
        }

        // Only switch the speaker when they change
        Speaker speaker = scene.speakers[scene.speakerIndexes[lineIndex]];
        if (lineIndex == 0 || scene.speakerIndexes[lineIndex] != scene.speakerIndexes[lineIndex - 1])
        {
            dialogue.SwitchSpeaker(speaker);
        }

        dialogue.UpdateText(scene.lines[lineIndex]);
        audioSource.pitch = speaker.pitch;
        audioSource.Play();
    }
}
