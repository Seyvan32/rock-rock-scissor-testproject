using System;
using System.Collections;
using UnityEngine;

public class HandBase : MonoBehaviour
{
    [SerializeField] private bool isMe;
    private Vector3 initPosition;
    [SerializeField] private GameObject out_place;
    [SerializeField] private GameObject rock;
    [SerializeField] private GameObject paper;
    [SerializeField] private GameObject scissors;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    private void Awake()
    {
        // Set init position
        initPosition = this.gameObject.transform.position;
        // Listen on Game status
        Data_Holder.Instance.OnGameStatus.AddListener(OnGameStatusChange);
    }

    private void OnGameStatusChange(GameStatus gameStatus)
    {
        Debug.Log("HandBase OnGameStatusChange: " + gameStatus);

        // if game status is round start deactive all hands
        if (gameStatus == GameStatus.RoundStarted)
        {
            transform.position = out_place.transform.position;
            DeactivateAllHands();
            return;
        }

        // countinue on Raise Hand, Raise Bot Hand
        if (gameStatus != GameStatus.RaiseHand && gameStatus != GameStatus.RaiseBotHand)
        {
            return;
        }

        // Check if status is RaiseHand and isMe is true, or RaiseBotHand and isMe is false, to know if its our turn or not
        if ((gameStatus == GameStatus.RaiseHand && isMe) || (gameStatus == GameStatus.RaiseBotHand && !isMe))
        {
            transform.position = out_place.transform.position;
            MoveTo(gameObject, initPosition, 0.3f);
        }
        else return;

        // activate desired hand
        ActivateDesiredHand();
    }

    private void ActivateDesiredHand()
    {

        // get player based on isMe
        PlayerData player = isMe ? Data_Holder.Instance.GetSelfPlayer() : Data_Holder.Instance.GetOponentPlayer();
        // Get hand type of the player
        HandType playerHand = player.hand;

        // switch on hand type of the player
        switch (playerHand)
        {
            case HandType.Rock:
                rock.SetActive(true);
                paper.SetActive(false);
                scissors.SetActive(false);
                break;
            case HandType.Paper:
                rock.SetActive(false);
                paper.SetActive(true);
                scissors.SetActive(false);
                break;
            case HandType.Scissors:
                rock.SetActive(false);
                paper.SetActive(false);
                scissors.SetActive(true);
                break;
        }
    }

    //deactive all hands
    public void DeactivateAllHands()
    {
        rock.SetActive(false);
        paper.SetActive(false);
        scissors.SetActive(false);
    }

    /// <summary>
    /// Moves an object smoothly from its current position to the target position over a specified duration.
    /// </summary>
    /// <param name="targetPosition">The destination position.</param>
    /// <param name="duration">Time in seconds for the movement.</param>
    private void MoveToPosition(Vector3 targetPosition, float duration)
    {
        StartCoroutine(MoveOverTime(targetPosition, duration));
    }

    private IEnumerator MoveOverTime(Vector3 targetPosition, float duration)
    {
        Vector3 startPosition = transform.position;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / duration);
            transform.position = Vector3.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        // Ensure final position is accurate
        transform.position = targetPosition;
    }

    /// <summary>
    /// Moves a GameObject smoothly to the target position using LeanTween.
    /// </summary>
    /// <param name="targetObject">The GameObject to move.</param>
    /// <param name="targetPosition">The destination position.</param>
    /// <param name="duration">Time in seconds for the movement.</param>
    /// <param name="easeType">Easing type for smooth motion.</param>
    /// <param name="onComplete">Optional callback when movement is finished.</param>
    private void MoveTo(GameObject targetObject, Vector3 targetPosition, float duration,
                              LeanTweenType easeType = LeanTweenType.easeInOutQuad, Action onComplete = null)
    {
        if (targetObject == null)
        {
            Debug.LogError("TweenMover: Target object is null.");
            return;
        }

        Debug.Log("Moving");

        LeanTween.move(targetObject, targetPosition, duration)
                 .setEase(easeType)
                 .setOnComplete(onComplete);
    }

}