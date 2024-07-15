using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawradiusAround : MonoBehaviour
{
    [Range(0, 50)]
    public int segments = 50;
    [Range(0, 5)]
    public float xradius = 5;
    [Range(0, 5)]
    public float yradius = 5;
    LineRenderer line;

    private void Awake()
    {
        init();
    }

    void init()
    {
        line = gameObject.GetComponent<LineRenderer>();

        line.positionCount = segments + 1;
        line.useWorldSpace = false;
        line.widthMultiplier = 0.05f;
    }

    public void ChangeRadius(float _radius)
    {
        xradius = _radius;
        yradius = _radius;
        CreatePoints();
    }

    void CreatePoints()
    {
        float x;
        float y;
        float z;

        float angle = 20f;

        for (int i = 0; i < (segments + 1); i++)
        {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * xradius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * yradius;

            line.SetPosition(i, new Vector3(x, y, 0));

            angle += (380f / segments);
        }
    }

    [ContextMenu("CreatePoints")]
    public void ApplyCreatePoints()
    {
        init();
        CreatePoints();
    }
}
