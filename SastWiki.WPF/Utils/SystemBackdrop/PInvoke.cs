using System;
using System.Runtime.InteropServices;

namespace SastWiki.WPF.Utils.SystemBackdrop;

public class PInvoke
{
    public class ParameterTypes
    {
        [Flags]
        public enum DWMWINDOWATTRIBUTE
        {
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
            DWMWA_SYSTEMBACKDROP_TYPE = 38
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MARGINS
        {
            public int cxLeftWidth; // width of left border that retains its size
            public int cxRightWidth; // width of right border that retains its size
            public int cyTopHeight; // height of top border that retains its size
            public int cyBottomHeight; // height of bottom border that retains its size
        };
    }

    public static class Methods
    {
        [DllImport("DwmApi.dll")]
        static extern int DwmExtendFrameIntoClientArea(
            nint hwnd,
            ref ParameterTypes.MARGINS pMarInset
        );

        [DllImport("dwmapi.dll")]
        static extern int DwmSetWindowAttribute(
            nint hwnd,
            ParameterTypes.DWMWINDOWATTRIBUTE dwAttribute,
            ref int pvAttribute,
            int cbAttribute
        );

        public static int ExtendFrame(nint hwnd, ParameterTypes.MARGINS margins) =>
            DwmExtendFrameIntoClientArea(hwnd, ref margins);

        public static int SetWindowAttribute(
            nint hwnd,
            ParameterTypes.DWMWINDOWATTRIBUTE attribute,
            int parameter
        ) => DwmSetWindowAttribute(hwnd, attribute, ref parameter, Marshal.SizeOf<int>());
    }
}
