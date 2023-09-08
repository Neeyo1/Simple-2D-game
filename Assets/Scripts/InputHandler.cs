using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private Camera _mainCamera;
    private GameObject lastEnemyMarked;
    public PlayerController playerController;

    private void Awake()
    {
        _mainCamera = Camera.main;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
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
                    playerController.AttackEnemy(lastEnemyMarked);
                }
                else
                {
                    lastEnemyMarked.GetComponent<EnemyController>().ui.SetActive(false);
                    EnemyController enemyController = rayHit.collider.gameObject.GetComponent<EnemyController>();
                    enemyController.ui.SetActive(true);
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