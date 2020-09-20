using UIExpansionKit.API;
using MelonLoader;
using UnityEngine;


[assembly: MelonInfo(typeof(NearClipPlaneAdj.NearClipPlaneAdjMod), "NearClipPlaneAdj", "1.3.1", "Nirvash")]
[assembly: MelonGame("VRChat", "VRChat")]

namespace NearClipPlaneAdj
{

    public class NearClipPlaneAdjMod : MelonMod
    {
        public override void OnApplicationStart()
        {
            ExpansionKitApi.RegisterSimpleMenuButton(ExpandedMenu.SettingsMenu, "Nearplane-0.01", (() => ChangeNearClipPlane(.01f)));
            ExpansionKitApi.RegisterSimpleMenuButton(ExpandedMenu.SettingsMenu, "Nearplane-0.001", (() => ChangeNearClipPlane(.001f)));
            ExpansionKitApi.RegisterSimpleMenuButton(ExpandedMenu.SettingsMenu, "Nearplane-0.0001", (() => ChangeNearClipPlane(.0001f)));
            MelonLogger.Log("Near Plane Adjust Init");
        }

        private void ChangeNearClipPlane(float value)
        {
            VRCVrCamera vrCamera = VRCVrCamera.field_Private_Static_VRCVrCamera_0;
            if (!vrCamera)
                return;
            Camera screenCamera = vrCamera.screenCamera;
            if (!screenCamera)
                return;
            MelonLogger.Log("Near plane previous: " + screenCamera.nearClipPlane);
            screenCamera.nearClipPlane = value;
            MelonLogger.Log("Near plane adjusted: " + value);
        }

        public override void OnLevelWasLoaded(int level)
        {
            //Return the clipping distance to a safe, close value on load
            switch(level)//Without switch this would run 3 times at world load
            {
                case 0: //App
                case 1: //ui
                    break;
                default:
                    MelonCoroutines.Start(SetNearClipPlane(0.01f));
                    break;
            }
        }

        System.Collections.IEnumerator SetNearClipPlane(float znear)
        {
            yield return new WaitForSecondsRealtime(15); //Wait 15 seconds after world load before setting the clipping value. Waiting for the next/first frame does not work
            ChangeNearClipPlane(znear);
            MelonLogger.Log("Near plane adjusted after world load");
        }

    }
}
