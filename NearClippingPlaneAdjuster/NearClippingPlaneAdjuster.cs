using UIExpansionKit.API;
using MelonLoader;
using UnityEngine;


[assembly: MelonModInfo(typeof(NearClipPlaneAdj.NearClipPlaneAdjMod), "NearClipPlaneAdj", "1.1", "Nirvash")]
[assembly: MelonModGame("VRChat", "VRChat")]

namespace NearClipPlaneAdj
{

    public class NearClipPlaneAdjMod : MelonMod
    {
        public override void OnApplicationStart()
        {
            ExpansionKitApi.RegisterSimpleMenuButton(ExpandedMenu.SettingsMenu, "Nearplane-0.01", (() => ChangeNearClipPlane(.01f)));
            ExpansionKitApi.RegisterSimpleMenuButton(ExpandedMenu.SettingsMenu, "Nearplane-0.001", (() => ChangeNearClipPlane(.001f)));
            ExpansionKitApi.RegisterSimpleMenuButton(ExpandedMenu.SettingsMenu, "Nearplane-0.0001", (() => ChangeNearClipPlane(.0001f)));
            MelonModLogger.Log("Near Plane Adjust Init");
        }

        private void ChangeNearClipPlane(float value)
        {
            VRCVrCamera vrCamera = VRCVrCamera.field_Private_Static_VRCVrCamera_0;
            if (!vrCamera)
                return;
            Camera screenCamera = vrCamera.screenCamera;
            if (!screenCamera)
                return;
            screenCamera.nearClipPlane = value;
            MelonModLogger.Log("Near plane adjusted: " + value);
        }

        public override void OnLevelWasLoaded(int level)
        { //Return the clipping distance to a safe, close value on load
            switch (level)
            {
                case 0: //App
                case 1: //ui
                    break;
                default:
                    MelonCoroutines.Start(setNearClipPlane(30, 0.01f));
                    break;

            }
        }

        System.Collections.IEnumerator setNearClipPlane(int secs, float nc)
        {
            MelonModLogger.Log("Near plane adjusted: " + nc + " for " + secs + " after world load");
            while (secs > 0)
            {
                yield return new WaitForSecondsRealtime(1);
                VRCVrCamera vrCamera = VRCVrCamera.field_Private_Static_VRCVrCamera_0;
                if (!vrCamera)
                    yield break;
                Camera screenCamera = vrCamera.screenCamera;
                if (!screenCamera)
                    yield break;
                screenCamera.nearClipPlane = nc;
                
                secs--;
            }

        }



    }
}
