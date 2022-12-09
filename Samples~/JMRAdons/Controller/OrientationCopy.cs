using JMRSDK.InputModule;
using UnityEngine;

public class OrientationCopy : MonoBehaviour
{
    public GameObject objectToCopy;
    JMRInteractionManager.InteractionDeviceType deviceType;

    private void Start()
    {
        if (objectToCopy == null)
        {
            objectToCopy = gameObject;
        }
    }

    void Update()
    {
        IInputSource source = JMRInteractionManager.Instance.GetCurrentSource();
        Quaternion controllerOrientation;
        source.TryGetPointerRotation(out controllerOrientation);
        objectToCopy.transform.rotation = controllerOrientation;
        
        
        deviceType = JMRInteractionManager.Instance.GetSupportedInteractionDeviceType();
        if (deviceType == JMRInteractionManager.InteractionDeviceType.JIOGLASS_CONTROLLER)
        {
            //Jio Glass Pro
        }
        else if (deviceType == JMRInteractionManager.InteractionDeviceType.VIRTUAL_CONTROLLER)
        {
            //Jio Glass Lite
            objectToCopy.transform.eulerAngles += new Vector3(0, 90, 0);
        }
        
    }
}