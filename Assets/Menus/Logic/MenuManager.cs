using AYellowpaper.SerializedCollections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    //public int MoneyEarned;

    public static MenuManager instance;

    public MenuStates currentMenuState;
    public GameObject lastMenuOpened;
    public MenuStates lastMenuEnumOpened;
    //public InGameStates currentInGameState;
    //public StationsEnum currentStation;
    public GameObject ObjectGrabbed;
    public SerializedDictionary<MenuStates, GameObject> Menus;

    public List<AudioClip> ShowMenusListsfx;

    public int r;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        //Global Logic
        if (currentMenuState != MenuStates.Beginning && currentMenuState != MenuStates.Options)
        {
            if (Input.GetButton("PauseButton"))
            {
                r = Random.Range(0, ShowMenusListsfx.Count-1);
                //AudioManager.instance.PlaySFXOnce(ShowMenusListsfx[r]);
                ChangeToPause();
            }
        }
    }

    #region Change the current state protocol
    public void ManageGameObjectsActiveState(MenuStates menuStateToOpen)
    {

        if (lastMenuOpened != null )
        {
            lastMenuOpened.SetActive(false);
        }
        else
        {
            foreach (var item in Menus)
            {
                if (item.Value.activeSelf)
                {
                    lastMenuOpened = item.Value;
                    lastMenuOpened.SetActive(false);
                    break;
                }
            }
        }
        Menus[menuStateToOpen].SetActive(true);
        lastMenuOpened = Menus[menuStateToOpen];
    }

    #endregion

    #region Change current state
    public void ChangeToBegginning()
    {
        currentMenuState = MenuStates.Beginning;
        ManageGameObjectsActiveState(MenuStates.Beginning);
        lastMenuEnumOpened = MenuStates.Beginning;
    }


    public void ChangeToPause()
    {
        currentMenuState = MenuStates.Pause;
        ManageGameObjectsActiveState(MenuStates.Pause);
        lastMenuEnumOpened = MenuStates.Pause;
    }

    public void ChangeToAfterHours()
    {
        currentMenuState = MenuStates.AfterHours;
        ManageGameObjectsActiveState(MenuStates.AfterHours);
        lastMenuEnumOpened = MenuStates.AfterHours;
    }

    public void ChangeToInGame()
    {
        currentMenuState = MenuStates.InGame;
        ManageGameObjectsActiveState(MenuStates.InGame);
        lastMenuEnumOpened = MenuStates.InGame;
    }

    public void ChangeToAlbum()
    {
        currentMenuState = MenuStates.Album;
        ManageGameObjectsActiveState(MenuStates.Album);
        lastMenuEnumOpened = MenuStates.Album;
    }

    public void ChangeToOptions()
    {
        currentMenuState = MenuStates.Options;
        ManageGameObjectsActiveState(MenuStates.Options);
    }

    public void changeToCustom(MenuStates menuState)
    {
        currentMenuState = menuState;
        ManageGameObjectsActiveState(menuState);
        lastMenuEnumOpened = menuState;
    }
    #endregion

    #region change Scenes
    public void changeScenes(int scene)
    {
        SceneManager.LoadScene(scene);
    }
    #endregion
}

public enum MenuStates
{
    Beginning,
    Pause,
    AfterHours,
    InGame,
    Options,
    Album,
}
