using UnityEngine;

public class GameController : MonoBehaviour
{

    public Theme theme;

    public int amountOfWords = 3;

    public string[] themeWords { get; private set; }
    public string[] gameWords { get; private set; }

    public bool[] availableThemeWords { get; private set; }
    public bool[] wordsFound { get; private set; }

    private void Awake()
    {
        themeWords = GetThemeWords();
        availableThemeWords = new bool[themeWords.Length];

        gameWords = GetGameWords();
        wordsFound = new bool[gameWords.Length];
    }

    private void Update()
    {
        if (GameOver())
            Time.timeScale = 0f;
    }

    private string[] GetThemeWords()
    {
        if (theme == Theme.Frutas)
            return new string[] { "Banana", "Uva", "Morango", "Abacaxi", "Acerola", "Manga"};
        if (theme == Theme.Legumes)
            return new string[] { "Batata", "Cebola", "Milho", "Cenoura", "Abobrinha", "Gengibre" };
        return null;
    }

    private string[] GetGameWords()
    {
        string[] selectedWords = new string[amountOfWords];

        for (int i = 0; i < amountOfWords; i++)
            selectedWords[i] = RandomThemeWord();

        return selectedWords;
    }

    private string RandomThemeWord()
    {
        int r = Random.Range(0, themeWords.Length);
        while (availableThemeWords[r] == true)
            r = Random.Range(0, themeWords.Length);

        availableThemeWords[r] = true;
        return themeWords[r];
    }

    public bool GameOver()
    {
        int count = 0;
        for (int i = 0; i < wordsFound.Length; i++)
        {
            if (wordsFound[i])
                count++;
        }
        return count == amountOfWords;
    }

    public enum Theme { Frutas, Legumes }
}
