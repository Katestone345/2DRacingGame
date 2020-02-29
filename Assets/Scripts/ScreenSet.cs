using UnityEngine;

public class ScreenSet : MonoBehaviour 
{

	void Awake()
	{
		Application.targetFrameRate = 60;
		Screen.SetResolution(640, 400, false);
	}

	void Update()
	{
		//  按ESC退出全屏
		if (Input.GetKey(KeyCode.Escape))
		{
			Screen.fullScreen = false;  //退出全屏         
		}
		//设置为1080*720不全屏
		if (Input.GetKey(KeyCode.Escape))
		{
			Screen.SetResolution(640, 400, false);
		}
		//设置1080*720的全屏
		if (Input.GetKey(KeyCode.RightControl))
		{
			Screen.SetResolution(640, 400, true);
		}
	}
		
}
