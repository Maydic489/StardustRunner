using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteInEditMode]
public class WorldCurver : MonoBehaviour
{
	[Range(-0.1f, 0.1f)]
	public float curveStrengthY = 0.01f;
	[Range(-0.1f, 0.1f)]
	public float curveStrengthX = 0.0f;
    public bool isTurning;

    int m_CurveStrengthID_Y;
	int m_CurveStrengthID_X;

    private int counting;
    private float ranNum;
    private float slideDirection;

    private void OnEnable()
    {
        m_CurveStrengthID_Y = Shader.PropertyToID("_CurveStrengthY");
		m_CurveStrengthID_X = Shader.PropertyToID("_CurveStrengthX");
	}

	void Update()
	{
		Shader.SetGlobalFloat(m_CurveStrengthID_Y, curveStrengthY);
		Shader.SetGlobalFloat(m_CurveStrengthID_X, curveStrengthX);

        //randomly curve road with shader
        if (isTurning)
        {
            counting++;
            if (counting == 300)
            {
                GetSlideDirection();
                counting = 0;
            }

            curveStrengthX = Mathf.Lerp(curveStrengthX, slideDirection, 0.01f);
        }
    }

    private void GetSlideDirection()
    {
        ranNum = Random.Range(0, 4);
        if (ranNum <= 1)
        {
            slideDirection = -0.01f;
        }
        else if (ranNum <= 2)
        {
            slideDirection = 0;
        }
        else if (ranNum <= 3)
        {
            slideDirection = 0.01f;
        }
    }
}
