using UnityEngine;

public class Mask : MonoBehaviour
{
    public bool Closed {get; private set;}

    private Animator animator;

    void Start() {
        animator = GetComponent<Animator>();
        if(animator == null) {
            Debug.LogError("Mask must have animator");
        }
    }

    public void Close() {
        animator.SetBool("isClosed", true);
    }

    public void Open() {
        animator.SetBool("isClosed", false);
    }
}
