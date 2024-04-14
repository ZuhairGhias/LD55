using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Dialogue : VisualElement
{
    [UnityEngine.Scripting.Preserve]
    public new class UxmlFactory : UxmlFactory<Dialogue> { }

    private VisualElement speaker;
    private Label dialogueText;

    public Dialogue() : this(AssetDatabase.LoadAssetAtPath<Sprite>("Assets/Sprites/Portrait/PlayerPortrait.png")) { }

    public Dialogue(Sprite speakerSprite)
    {
        VisualElement dialogue = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/UI/Dialogue/Dialogue.uxml").Instantiate();
        dialogue.style.position = Position.Absolute;
        dialogue.style.top = 0;
        dialogue.style.bottom = 0;
        dialogue.style.left = 0;
        dialogue.style.right = 0;
        hierarchy.Add(dialogue);
        speaker = dialogue.Query("Speaker").First();
        speaker.style.backgroundImage = new StyleBackground(speakerSprite);
        dialogueText = (Label)dialogue.Query("DialogueText").First();
    }

    public void updateText(string text)
    {
        dialogueText.text = text;
    }
}
