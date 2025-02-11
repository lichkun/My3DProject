using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private GameObject medkitPrefab;
    [SerializeField]
    private GameObject character;
    void Start()
    {
        GameEventSystem.AddListener(OnCoinEvent, "Medkit");
    }
    private void OnCoinEvent(string type, object payload)
    {
        if (payload.Equals("Destroy"))
        {
            var coin = GameObject.Instantiate(medkitPrefab);
            coin.transform.position= character.transform.position + Vector3.forward*3;
        }
        Debug.Log($"Event {type} {payload} ");
    }

    private void OnDestroy()
    {
        GameEventSystem.RemoveListener(OnCoinEvent, "Medkit");

    }
}
