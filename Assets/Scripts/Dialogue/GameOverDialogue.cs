using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameOverDialogue : IDialogue
{
    public IEnumerator<IDialogueBase> Next(GameManager manager)
    {
        yield return new DialogueText("", "You ran out of money.");
        yield return new DialogueText("", "Click next to restart.");

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}