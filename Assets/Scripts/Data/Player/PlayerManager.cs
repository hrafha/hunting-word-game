using Data.User;

public static class PlayerManager
{
    private static PlayerData player = null;

    public static PlayerData Player
    {
        get
        {
            if (player == null)
                Reload();

            return player;
        }
    }


    public static void Reload()
    {
        PlayerData loadedPlayer = PlayerData.Load();

        if (player != null) PlayerData.CopyChangeEvents(player, loadedPlayer);
        else PlayerData.BindChangeEvents(loadedPlayer);

        player = loadedPlayer;
    }
}
