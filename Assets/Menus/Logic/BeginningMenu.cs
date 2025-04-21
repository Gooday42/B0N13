
using System.Collections.Generic;
using UnityEngine;

public class BeginningMenu : CommonButtonLogic
{
    public void OnNewScenePressed()
    {
        produceSFX();
        //MenuManager.instance.currentStation = StationsEnum.Reception;
        MenuManager.instance.ChangeToInGame();
        MenuManager.instance.changeScenes(1);
    }

    public void OnContinuePressed()
    {
        //TO DO
    }
}
