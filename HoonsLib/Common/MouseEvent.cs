using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HoonsLib.Common
{
  public class MouseEvent
  {
    [DllImport("user32.dll")]
    static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);

    const uint MOUSEMOVE = 0x0001;      // 마우스 이동
    const uint ABSOLUTEMOVE = 0x8000;   // 전역 위치
    const uint LBUTTONDOWN = 0x0002;    // 왼쪽 마우스 버튼 눌림
    const uint LBUTTONUP = 0x0004;      // 왼쪽 마우스 버튼 떼어짐
    const uint RBUTTONDOWN = 0x0008;    // 오른쪽 마우스 버튼 눌림
    const uint RBUTTONUP = 0x00010;      // 오른쪽 마우스 버튼 떼어짐    

    public static void MouseClick()
    {
      mouse_event(LBUTTONDOWN, 0, 0, 0, 0);
      Thread.Sleep(10);
      mouse_event(LBUTTONUP, 0, 0, 0, 0);
    }

    public static void MouseDown()
    {
      mouse_event(LBUTTONDOWN, 0, 0, 0, 0);
    }

    public static void MouseUp()
    {
      mouse_event(LBUTTONUP, 0, 0, 0, 0);
    }

    public static void MouseRightClick()
    {
      mouse_event(RBUTTONDOWN, 0, 0, 0, 0);
      Thread.Sleep(10);
      mouse_event(RBUTTONUP, 0, 0, 0, 0);
    }

    public static void MouseRightDown()
    {
      mouse_event(RBUTTONDOWN, 0, 0, 0, 0);
    }

    public static void MouseRightUp()
    {
      mouse_event(RBUTTONUP, 0, 0, 0, 0);
    }
  }
}
