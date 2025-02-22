using UnityEngine;

public class Start_Menu : MonoBehaviour
{
    public void OnButtonClicked(){
        Debug.Log("Clicked start cutton");
        Data_Holder.Instance.OnGameStatus.Invoke(GameStatus.GameBoard);
    }
}