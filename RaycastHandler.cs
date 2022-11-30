using System.Collections;
using JMRSDK;
using JMRSDK.InputModule;
using JMRSDK.UX;
using UnityEngine;
using UnityEngine.Events;

public class RaycastHandler : MonoBehaviour
{
    public static UnityEvent _onInitializationComplete;
    private PointerLaserUnity _pointerLaser;
    public bool hideRay = false;

    private void Awake()
    {
        _onInitializationComplete = new UnityEvent();
    }

    private void Start()
    {
        if(hideRay)
            HidePointer();
        
        StartCoroutine(JMRInitializationCheck());

        if (hideRay)
        {
            HideRay();
            HideController();
        }
    }
    
    private bool IsJMRSetupCompleted() => _pointerLaser != null;

    private IEnumerator JMRInitializationCheck()
    {
        do {
            _pointerLaser = JMRInputManager.Instance.gameObject.GetComponentInChildren<PointerLaserUnity>();
            yield return null;
        } while (_pointerLaser == null);
        _onInitializationComplete?.Invoke();
    }

    /// <summary>
    /// Hides the pointer at the center of the screen, or at the end of the controller ray when controller is connected.
    /// </summary>
    /// <remarks> Singleton initialized on Awake </remarks>
    void HidePointer()
    {
        JMRPointerManager.Instance.GetCursor().gameObject.SetActive(false);
    }
    
    void HideRay()
    {
        if (!IsJMRSetupCompleted())
        {
            _onInitializationComplete.AddListener(HideRay);
            return;
        }
        _pointerLaser.enabled = false;
        _pointerLaser.gameObject.GetComponent<LineRenderer>().enabled = false;
    }

    void HideController()
    {
        if (!IsJMRSetupCompleted())
        {
            _onInitializationComplete.AddListener(HideController);
            return;
        }
        JMRInputManager.Instance.gameObject.GetComponentInChildren<JMRObjectHider>().Hide();
    }
}
