using UnityEngine;

public class Menu_Page : UI_Element
{
    public override void init() { 
        // Listen on game status
        Data_Holder.Instance.OnGameStatus.AddListener(OnGameStatusChange);
    }

    private void OnGameStatusChange(GameStatus gameStatus) {
        // Open page if gameStatus is Menu
        if (gameStatus == GameStatus.Menu) UI_Manager.instance.openPage(this);
    }
}