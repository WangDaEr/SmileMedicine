using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlatformSystem))]
public class PlatformSystemInspector : Editor
{
    class Comp : IComparer<PlatformGeneral>
    {
        public int Compare(PlatformGeneral l, PlatformGeneral r)
        {
            if (!l || !r) { return 0; }

            return l.index.x == r.index.x ? l.index.y .CompareTo(r.index.y) : l.index.x.CompareTo(r.index.x);
        }
    }

    private PlatformSystem ps;

    private void OnEnable()
    {
        ps = (PlatformSystem)target;
        if (ps.backgroundPage.GetComponent<PageGenerator>().ps == null) { ps.backgroundPage.GetComponent<PageGenerator>().ps = ps; }

        //Debug.Log("platforms: " + ps.platforms.Count);
    }

    private void checkPlatformUpdate()
    {
        //x for positive value, y for negative value.
        Vector2 ver = new Vector2(float.MinValue, float.MaxValue);
        Vector2 hor = new Vector2(float.MinValue, float.MaxValue);

        ps.platforms.Clear();

        List<PlatformGeneral> rec = new List<PlatformGeneral>();
        foreach (Transform child in ps.transform)
        {
            PlatformGeneral c_pg = child.GetComponent<PlatformGeneral>();
            rec.Add(c_pg);

            if (c_pg.platformCenter.x + c_pg.platformSize.x / 2 + c_pg.platformPadding.x > hor.x) { hor.x = c_pg.platformCenter.x + c_pg.platformSize.x / 2 + c_pg.platformPadding.x; }
            if (c_pg.platformCenter.x - c_pg.platformSize.x / 2 - c_pg.platformPadding.x < hor.y) { hor.y = c_pg.platformCenter.x - c_pg.platformSize.x / 2 - c_pg.platformPadding.x; }
            if (c_pg.platformCenter.y + c_pg.platformSize.y / 2 + c_pg.platformPadding.y > ver.x) { ver.x = c_pg.platformCenter.y + c_pg.platformSize.y / 2 + c_pg.platformPadding.y; }
            if (c_pg.platformCenter.y - c_pg.platformSize.y / 2 - c_pg.platformPadding.y < ver.y) { ver.y = c_pg.platformCenter.y - c_pg.platformSize.y / 2 - c_pg.platformPadding.y; }
        }

        ps.platformTotalSize.x = hor.x - hor.y;
        ps.platformTotalSize.y = ver.x - ver.y;

        ps.platformCenter.x = (hor.x + hor.y) / 2;
        ps.platformCenter.y = (ver.x + ver.y) / 2;

        Debug.Log("platformSize: " + ps.platformTotalSize.x + " " + ps.platformTotalSize.y + " " + ps.platformCenter);

        //reorganize the list record of platforms
        Comp comp = new Comp();
        rec.Sort(comp);

        foreach (PlatformGeneral pg in rec)
        {
            if (pg.index.x > ps.platforms.Count)
            {
                List<PlatformGeneral> next_line = new List<PlatformGeneral>() { pg };
                ps.platforms.Add(next_line);
            }
            else
            {
                ps.platforms[(int)pg.index.x - 1].Add(pg);
            }
        }

        Debug.Log("ps has: " + ps.platforms.Count);
    }

    public override void OnInspectorGUI()
    {
        ps.padding = EditorGUILayout.Vector2Field(new GUIContent("Padding for Vertical & Horizontal"), ps.padding);

        if (GUILayout.Button("Update Platform Layout"))
        {
            checkPlatformUpdate();
        }

        ps.backgroundPage = (GameObject)EditorGUILayout.ObjectField(new GUIContent("Background Page Generator"), ps.backgroundPage, typeof(GameObject), true);
    }
}
