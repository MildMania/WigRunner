using UnityEngine;

public class CharacterMovementBehaviour : MonoBehaviour
{
    [SerializeField] private CharacterController _characterController = null;

    public void Move(Vector3 speed)
    {
        _characterController.SimpleMove(speed);
        
        _characterController.Move(new Vector3(0, speed.y, 0));
    }
}
