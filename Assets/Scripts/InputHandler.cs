using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;
    private GameObject lastEnemyMarked;
    public AttackTarget attackTarget;

    private void Awake()
    {
        _mainCamera = Camera.main;
        attackTarget = GameObject.Find("Player").GetComponent<AttackTarget>();
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) return;

        var rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));
        if (!rayHit.collider) return;
        
        //Debug.Log(rayHit.collider.gameObject.name);

        if(rayHit.collider.gameObject.CompareTag("Enemy"))
        {
            if(lastEnemyMarked != null)
            {
                if(lastEnemyMarked == rayHit.collider.gameObject)
                {
                    attackTarget.Attack(lastEnemyMarked);
                }
                else
                {
                    lastEnemyMarked.GetComponent<EnemyController>().ui.SetActive(false);
                    EnemyController enemyController = rayHit.collider.gameObject.GetComponent<EnemyController>();
                    enemyController.ui.SetActive(true);
                    if(lastEnemyMarked.GetComponent<EnemyController>().isBeingAttacked == true)
                    {
                        lastEnemyMarked.GetComponent<EnemyController>().isBeingAttacked = false;
                    }
                    lastEnemyMarked = rayHit.collider.gameObject;
                }
            }
            else
            {
                EnemyController enemyController = rayHit.collider.gameObject.GetComponent<EnemyController>();
                enemyController.ui.SetActive(true);
                lastEnemyMarked = rayHit.collider.gameObject;
            }
            
        }
    }
}