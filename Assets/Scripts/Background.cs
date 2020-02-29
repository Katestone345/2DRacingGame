using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
// This creates a way to enter the amount of stretches of track you want
// It lets you enter a curvature amount as well for each
public struct VecTrack
{
	[Range(-1,1)]
	public float curvature;
	public float distance;

	public VecTrack(float curvature,float distance)
    {
		this.curvature = curvature;
		this.distance = distance;
	}
}

public class Background : MonoBehaviour
{

    public Transform Car;
	private Vector3 camToCarOffset;

	//Background
	public int screenWeidth = 160, screenHeight = 100;
	public SpriteRenderer pixelPerf;                                            // To make the pixels show up clearly
	public Color grassColor, shouderColor, shouderLowColor, roadColor, 
        roadLowColor, grassLowColor, whitLineColor;                             // Color variables

	private GameObject grassHolder, roadShouderHolder, roadHolder, whiteLineHolder;

	private List<Vector2> defaultRoad = new List<Vector2>();
	private List<Vector2> defaultRoadShouder = new List<Vector2>();
	private List<Vector2> defaultWhiteLine = new List<Vector2>();
	private List<Vector2> defaultGrass = new List<Vector2>();
	
	private float CarPosition = 0.0f;
	private float trackDistance = 0.0f;
    private float fSpeed = 1f;

    private float fCurvatrue = 0.0f;  
	private float fTrackCurvature = 0.0f;
	private float fPlayerCurvature = 0.0f;

	[Header("Fill in colors and variables: ")]
	public List <VecTrack> vecTrack = new List<VecTrack>();  
	public float sumDistance = 0.0f;

    // List of sprites that populate during the game
    public List<SpriteRenderer> grassList = new List<SpriteRenderer>();         
	public List<SpriteRenderer> roadShouderList = new List<SpriteRenderer>();
	public List<SpriteRenderer> whiteLineList = new List<SpriteRenderer>();
	public List<SpriteRenderer> roadList = new List<SpriteRenderer>();

	// Use this for initialization
	void Start ()
    {
        camToCarOffset = Car.position - transform.position;                         // camera tracks car

		foreach(VecTrack vec in vecTrack)
        {
			sumDistance += vec.distance;
		}
		
		grassHolder = new GameObject();
		grassHolder.name = "grass";
		roadShouderHolder = new GameObject();
		roadShouderHolder.name = "roadShouder";
		roadHolder = new GameObject();
		roadHolder.name = "road";
		whiteLineHolder = new GameObject();
		whiteLineHolder.name = "Lines";
		

		for (int y = 0; y < screenHeight; y++)
        {
			for (int x = -100; x < screenWeidth + 100; x++)
            {
				float fPerspective = (float)y / (screenHeight / 2.0f);

				float fMiddlepoint = 0.5f;
				float fRoadWidth =  -0.05f+fPerspective * 1.2f;
				float fClipWidth = fRoadWidth * 0.15f;
				float fMiddleWidth = fRoadWidth * 0.015f;
				fRoadWidth *= 0.5f;

				float nLeftGrass = (fMiddlepoint- fRoadWidth - fClipWidth) * screenWeidth;
				float nLeftClip = (fMiddlepoint - fRoadWidth) * screenWeidth;
				float nRightClip = (fMiddlepoint + fRoadWidth) * screenWeidth;
				float nRightGrass = (fMiddlepoint + fRoadWidth + fClipWidth) * screenWeidth;

				int nRow = screenHeight / 2 + y;
			}
		}
		
		for (int y = 0; y < 2 * screenHeight / 3; y++)
        {
			for(int x = -100; x < screenWeidth + 100; x++)
            {
				float fPerspective =  (float)y / (screenHeight / 2.0f)  ;
				float fMiddlepoint = 0.5f;
				float fRoadWidth = 0.05f + fPerspective * 1.2f;
				float fClipWidth = fRoadWidth * 0.12f;
				float fMiddleWidth = fRoadWidth * 0.015f;
				fRoadWidth *= 0.5f;

				float nLeftGrass = (fMiddlepoint - fRoadWidth - fClipWidth) * screenWeidth;
				float nLeftClip = (fMiddlepoint - fRoadWidth) * screenWeidth;
				float nLeftMiddleClip = (fMiddlepoint - fMiddleWidth) * screenWeidth;
				float nRightMiddleClip = (fMiddlepoint + fMiddleWidth) * screenWeidth;
				float nRightClip = (fMiddlepoint + fRoadWidth) * screenWeidth;
				float nRightGrass = (fMiddlepoint + fRoadWidth + fClipWidth) * screenWeidth;
				int nRow = screenHeight / 2 + y;

				Color nGrassColor = Mathf.Sin (20.0f * Mathf.Pow(1.0f - fPerspective,3) + trackDistance * 0.1f) > 0.0f ? grassColor : grassLowColor;

				if(x >= -1000 && x < nLeftGrass)
                {
					SpriteRenderer grassPixTemp = (SpriteRenderer)Instantiate (pixelPerf,new Vector3(x,nRow),Quaternion.identity);grassPixTemp.color = nGrassColor;
					grassPixTemp.transform.parent = grassHolder.transform;
					grassList.Add (grassPixTemp);
					defaultGrass.Add(grassPixTemp.transform.position);
				}
				if(x >= nLeftGrass && x < nLeftClip)
                {
					SpriteRenderer roadShouderPixTemp = (SpriteRenderer)Instantiate (pixelPerf,new Vector3(x,nRow),Quaternion.identity);roadShouderPixTemp.color = shouderColor;
					roadShouderPixTemp.transform.parent = roadShouderHolder.transform;
					roadShouderList.Add(roadShouderPixTemp);
					defaultRoadShouder.Add(roadShouderPixTemp.transform.position);
				}
				// Road And Line
				if(x >= nLeftClip && x <nLeftMiddleClip)
                {
					SpriteRenderer roadPixLeftTemp = (SpriteRenderer)Instantiate (pixelPerf,new Vector3(x,nRow),Quaternion.identity);roadPixLeftTemp.color = roadColor;
					roadPixLeftTemp.transform.parent = roadHolder.transform;
					roadList.Add(roadPixLeftTemp);
					defaultRoad.Add(roadPixLeftTemp.transform.position);
				}
				if(x >= nLeftMiddleClip && x < nRightMiddleClip)
                {
					SpriteRenderer linePixLeftTemp = (SpriteRenderer)Instantiate (pixelPerf,new Vector3(x,nRow),Quaternion.identity);linePixLeftTemp.color = whitLineColor;
					linePixLeftTemp.transform.parent = whiteLineHolder.transform;
					whiteLineList.Add(linePixLeftTemp);
					defaultWhiteLine.Add(linePixLeftTemp.transform.position);
				}
				if(x >= nRightMiddleClip && x <nRightClip)
                {
					SpriteRenderer roadPixLeftTemp = (SpriteRenderer)Instantiate (pixelPerf,new Vector3(x,nRow),Quaternion.identity);roadPixLeftTemp.color = roadColor;
					roadPixLeftTemp.transform.parent = roadHolder.transform;
					roadList.Add(roadPixLeftTemp);
					defaultRoad.Add(roadPixLeftTemp.transform.position);
				}
				// Road And Line
				if(x>= nRightClip && x<nRightGrass)
                {
					SpriteRenderer roadShouderPixTemp = (SpriteRenderer)Instantiate (pixelPerf,new Vector3(x,nRow),Quaternion.identity);roadShouderPixTemp.color = shouderColor;
					roadShouderPixTemp.transform.parent = roadShouderHolder.transform;
					roadShouderList.Add(roadShouderPixTemp);
					defaultRoadShouder.Add(roadShouderPixTemp.transform.position);
				}
				if(x >= nRightGrass && x < screenWeidth + 1000)
                {
					SpriteRenderer grassPixTemp = (SpriteRenderer)Instantiate (pixelPerf,new Vector3(x,nRow),Quaternion.identity);grassPixTemp.color = nGrassColor;
					grassPixTemp.transform.parent = grassHolder.transform;
					grassList.Add (grassPixTemp);
					defaultGrass.Add(grassPixTemp.transform.position);
				}
			}
		}
	}
	void Update ()
    {
        // This makes the scene move
        trackDistance += (300f * fSpeed) * Time.deltaTime;

        // Get the location on the track
        float fOffset = 0;
		int nTrackSection = 0;

		// Find car position On track
		while(nTrackSection < vecTrack.Count && fOffset <= trackDistance)
        {
			fOffset += vecTrack[nTrackSection].distance;
			nTrackSection ++;
		}

		for (int i = 0; i < grassList.Count ; i++)
        {
			float fPerspective = (float)(grassList[i].transform.position.y - screenHeight/2)  / (screenHeight / 2.0f);     // y = nRow - screenHeight/2
			grassList[i].color = Mathf.Sin (10.0f * Mathf.Pow (1.0f - fPerspective, 3) + trackDistance * 0.1f) < 0.0f ? grassColor : grassLowColor;
			grassList[i].transform.position = new Vector2(defaultGrass[i].x + (fCurvatrue * Mathf.Pow(1.0f - fPerspective,3))*screenWeidth, grassList[i].transform.position.y); 
		}
		for(int i  = 0; i < roadShouderList.Count; i++)
        {
			float fPerspective = (float)(roadShouderList[i].transform.position.y -screenHeight/2)  / (screenHeight / 2.0f);     //y = nRow - screenHeight/2
			roadShouderList[i].color = Mathf.Sin (15.0f * Mathf.Pow (1.0f - fPerspective, 3) + trackDistance * 0.1f) < 0.0f ? shouderColor : shouderLowColor;
			roadShouderList[i].transform.position = new Vector2(defaultRoadShouder[i].x + (fCurvatrue * Mathf.Pow(1.0f - fPerspective,3)) * screenWeidth, roadShouderList[i].transform.position.y);
		}
		for(int i = 0; i < roadList.Count; i++)
        {
			float fPerspective = (float)(roadList[i].transform.position.y -screenHeight/2)  / (screenHeight / 2.0f);   
			roadList[i].color = Mathf.Sin(29.0f * Mathf.Pow (1.0f - fPerspective, 3) + trackDistance * 0.1f) < 0.0f ? roadLowColor : roadColor;
			roadList[i].transform.position = new Vector2(defaultRoad[i].x + (fCurvatrue * Mathf.Pow(1.0f - fPerspective,3)) * screenWeidth, roadList[i].transform.position.y);
		}
		for(int i = 0; i < whiteLineList.Count; i++)
        {
			float fPerspective = (float)(whiteLineList[i].transform.position.y -screenHeight/2)  / (screenHeight / 2.0f);   
			whiteLineList[i].color = Mathf.Sin(29.0f * Mathf.Pow (1.0f - fPerspective, 3) + trackDistance * 0.1f) < 0.0f ? whitLineColor : roadColor;
			whiteLineList[i].transform.position = new Vector2(defaultWhiteLine[i].x + (fCurvatrue * Mathf.Pow(1.0f - fPerspective,3)) * screenWeidth, whiteLineList[i].transform.position.y);
		}

	}
}
