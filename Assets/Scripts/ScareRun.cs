using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScareRun : MonoBehaviour
{
    EnemyMove enemy;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<EnemyMove>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // return
        if (GameManager.instance.state == GameManager.State.Title
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Ground")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Swallow")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Pick")
            || animator.GetCurrentAnimatorStateInfo(0).IsName("Throw")) return;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // collision with bomb
        if (collision.gameObject.layer == LayerMask.NameToLayer("Bomb"))
        {
            animator.SetTrigger("ScareRun");
        }
    }
}
