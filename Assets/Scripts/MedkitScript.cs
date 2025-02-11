using UnityEngine;

public class MedkitScript : MonoBehaviour
{
    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        animator.SetTrigger("OnCollected");
        GameEventSystem.EmitEvent("Medkit", "OnCollected");
    }

    private void OnAnimationEnd()
    {
        GameEventSystem.EmitEvent("Medkit", "Destroy");
        Destroy(gameObject);
    }
}
