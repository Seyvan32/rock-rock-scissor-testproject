using UnityEngine;

public class Win_Page : UI_Element
{
    public override void init() { 
        // Listen on game status
        Data_Holder.Instance.OnGameStatus.AddListener(OnGameStatusChange);
    }

    private void OnGameStatusChange(GameStatus gameStatus) {
        // Continue if gameStatus is gameFinished
        if (gameStatus != GameStatus.GameFinished) return;

        // If we are winner (have 5 or more than 5 score) then open win page else lose page
        if (Data_Holder.Instance.GetSelfPlayer().score >= 5) UI_Manager.instance.openPage(this);
    }
}