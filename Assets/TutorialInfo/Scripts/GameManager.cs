using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float score = 0;
    [SerializeField] private BallController ball;
    [SerializeField] private GameObject pinCollection;
    [SerializeField] private Transform pinAnchor;
    [SerializeField] private InputManager inputManager;
    [SerializeField] private TextMeshProUGUI scoreText;
     private FallTrigger[] fallTriggers;
    private GameObject pinObjects;
    // private FallTrigger[] pins;

    private void Start() 
    {
        inputManager.OnResetPressed.AddListener(HandleReset);
        SetPins();
    }

    private void HandleReset()
    {
        score = 0; 
        scoreText.text = $"Score: {score}";
        ball.ResetBall();
        SetPins();
    }

    private void SetPins() 
    {
        if(pinObjects)
        {
            foreach (Transform child in pinObjects.transform)
            {
                Destroy(child.gameObject);
            }
            Destroy(pinObjects);
        }
        
        pinObjects = Instantiate(pinCollection,pinAnchor.transform.position,Quaternion.identity, transform);

        fallTriggers = FindObjectsByType<FallTrigger>(FindObjectsInactive.Include, FindObjectsSortMode.None);

        foreach (FallTrigger pin in fallTriggers)
        {
            pin.OnPinFall.RemoveAllListeners();
            pin.OnPinFall.AddListener(IncrementScore);
            pin.isPinFallen = false;
        }
    }

    private void IncrementScore() 
    {
        score++;
        scoreText.text = $"Score: {score}";
    }
}
