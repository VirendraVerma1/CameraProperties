using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxReset : MonoBehaviour
{
    public enum ApplicationType
        {
            AR,
            VR,
        };
    
        public ApplicationType apptype = ApplicationType.VR;
        public float far=1000f;

        private void Start()
        {
            RaycastHandler._onInitializationComplete.AddListener(Reset);
        }

        public void Reset()
        {
            if (apptype == ApplicationType.VR)
            {
                GameObject LeftCamera = GameObject.Find("Left");
                GameObject RightCamera = GameObject.Find("Right");
                GameObject HeadCamera = GameObject.Find("Head");
    
                RenderSkybox(HeadCamera.GetComponent<Camera>());
                RenderSkybox(RightCamera.GetComponent<Camera>());
                RenderSkybox(LeftCamera.GetComponent<Camera>());
            }
        }
    
        public void RenderSkybox(Camera targetCamera = null)
        {
            if (targetCamera == null)
            {
                targetCamera = Camera.main;
            }
            targetCamera.clearFlags = CameraClearFlags.Skybox;
            targetCamera.farClipPlane=far;
        }
}
