using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject gameUI;
    public GameObject settingsMenu;
    public GameObject playerBulletCount;
    public GameObject spawnerList;

    public Transform playerTransform;
    public StarterAssetsInputs playerInput;
    public PlayerInput inputs;
    public ThirdPersonController playerController;
    public CinemachineVirtualCamera playerCamera;
    public Transform followTransform;
    public Transform lookAtTransform;
    
    void Awake()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
        spawnerList.SetActive(false);
        gameUI.SetActive(false);
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerController.enabled = false;
        playerInput.enabled = false;
        inputs.enabled = false;
        playerBulletCount.SetActive(false);
        playerInput.cursorLocked = false;
        playerInput.cursorInputForLook = false;
        playerCamera.Follow = null;
        playerCamera.LookAt = null;
    }
    
    public void PlayGame()
    {
        mainMenu.SetActive(false);
        playerBulletCount.SetActive(true);
        inputs.enabled = true;
        playerController.enabled = true;
        playerInput.enabled = true;
        playerInput.cursorLocked = true;
        playerInput.cursorInputForLook = true;
        playerCamera.Follow = followTransform;
        playerCamera.LookAt = lookAtTransform;
        gameUI.SetActive(true);
        spawnerList.SetActive(true);
        playerTransform.rotation = Quaternion.Euler(playerTransform.rotation.eulerAngles.x, playerTransform.rotation.eulerAngles.y + 180f, playerTransform.rotation.eulerAngles.z);
    }
    
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
    
    public void OpenSettings()
    {
        settingsMenu.SetActive(true);
    }
    
    public void CloseSettings()
    {
        settingsMenu.SetActive(false);
    }
}