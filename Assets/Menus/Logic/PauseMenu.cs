
public class PauseMenu : CommonButtonLogic
{
    public void OnResumePressed()
    {
        UnityEngine.Debug.Log("Pressed");
        MenuManager.instance.ChangeToInGame();
    }

    public void OnMainMenuPressed()
    {
        MenuManager.instance.ChangeToBegginning();
    }
    public void OnAlbumPressed()
    {
        MenuManager.instance.ChangeToAlbum();
    }
}
