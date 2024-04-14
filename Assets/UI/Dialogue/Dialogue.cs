using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Dialogue : VisualElement
{
    [UnityEngine.Scripting.Preserve]
    public new class UxmlFactory : UxmlFactory<Dialogue> { }

    private VisualElement speakerImage;
    private Label speakerText;
    private Label dialogueText;

    public Dialogue() : this(AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Portrait/PlayerPortrait.png"), "Chef Raccoon") { }

    public Dialogue(Sprite speakerSprite, string speakerName)
    {
        VisualElement dialogue = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UI/Dialogue/Dialogue.uxml").Instantiate();
        dialogue.style.position = Position.Absolute;
        dialogue.style.top = 0;
        dialogue.style.bottom = 0;
        dialogue.style.left = 0;
        dialogue.style.right = 0;
        hierarchy.Add(dialogue);
        speakerImage = dialogue.Query("Speaker").First();
        speakerImage.style.backgroundImage = new StyleBackground(speakerSprite);
        speakerText = (Label)dialogue.Query("SpeakerName").First();
        speakerText.text = speakerName;
        dialogueText = (Label)dialogue.Query("DialogueText").First();
    }

    public void SwitchSpeaker(Speaker speaker)
    {
        speakerImage.style.backgroundImage = new StyleBackground(speaker.speakerSprite);
        speakerText.text = speaker.speakerName;
    }

    public void UpdateText(string text)
    {
        dialogueText.text = text;
    }
}
