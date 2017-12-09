public class GameData
{

    private static GameData instance;
    private int score = 0;
    private int lives = 3;
    public bool Paused
    {
        get;
        set;
    }

    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
        }
    }
    public int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            lives = value;
        }
    }

    private GameData()
    {
        if (instance != null)
            return;
        instance = this;
        Paused = false;
    }

    public static GameData Instance
    {

        get
        {
            if (instance == null)
            {
                instance = new GameData();
            }
            return instance;
        }
    }
}
