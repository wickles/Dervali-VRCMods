using UIExpansionKit.API;
using MelonLoader;
using VRC.UserCamera;

[assembly: MelonModInfo(typeof(CameraResChanger.CamResChangerMod), "CameraResChanger", "1.1", "Nirvash")]
[assembly: MelonModGame("VRChat", "VRChat")]

namespace CameraResChanger
{

    public class CamResChangerMod : MelonMod
    {

        public override void OnApplicationStart()
        {
                ExpansionKitApi.RegisterSimpleMenuButton(ExpandedMenu.CameraQuickMenu, "8k Res", () => ChangeCamRes(4320, 7680));
                ExpansionKitApi.RegisterSimpleMenuButton(ExpandedMenu.CameraQuickMenu, "6k Res", () => ChangeCamRes(3240, 5760));
                ExpansionKitApi.RegisterSimpleMenuButton(ExpandedMenu.CameraQuickMenu, "4k Res", () => ChangeCamRes(2160, 3840));
                ExpansionKitApi.RegisterSimpleMenuButton(ExpandedMenu.CameraQuickMenu, "Default Res", () => ChangeCamRes(1080, 1920));
                MelonModLogger.Log("Camera Res Changer Init");
        }

        private static void ChangeCamRes(int H, int W)
        {
            var cameraController = UserCameraController.field_Internal_Static_UserCameraController_0;
            if (cameraController == null) return;
            cameraController.photoHeight = H;
            cameraController.photoWidth = W;
            MelonModLogger.Log("Set Camera Res to: " + H + "x" + W);
        }

        
    }
}
