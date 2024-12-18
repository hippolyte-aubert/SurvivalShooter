using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
	[HideInInspector] public Vector2 move;
	[HideInInspector] public Vector2 look;
	[HideInInspector] public bool sprint;
	
	public bool cursorLocked = true;
	public bool cursorInputForLook = true;
	
	[Header("Shooting Settings")]
	public PlayerShoot playerShoot;
	
	public void OnMove(InputValue value)
	{
		MoveInput(value.Get<Vector2>());
	}

	public void OnLook(InputValue value)
	{
		if(cursorInputForLook)
		{
			LookInput(value.Get<Vector2>());
		}
	}
	
	public void OnSprint(InputValue value)
	{
		SprintInput(value.isPressed);
	}
	
	public void OnFire(InputValue value)
	{
		FireInput(value.isPressed);
	}
	
	public void OnReload(InputValue value)
	{
		ReloadInput(value.isPressed);
	}
	
	public void OnPause(InputValue value)
	{
		if (value.isPressed)
		{
			PauseInput();
		}
	}
	
	private void MoveInput(Vector2 newMoveDirection)
	{
		move = newMoveDirection;
	} 

	private void LookInput(Vector2 newLookDirection)
	{
		look = newLookDirection;
	}
	
	private void SprintInput(bool newSprintState)
	{
		sprint = newSprintState;
	}
	
	private void FireInput(bool newFireState)
	{
		playerShoot.Fire(newFireState);
	}
	
	private void ReloadInput(bool newReloadState)
	{
		playerShoot.Reload(newReloadState);
	}
	
	private void PauseInput()
	{
		GameManager.instance.PauseGame();
	}
	
}