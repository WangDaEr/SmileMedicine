using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class PageGenerator : MonoBehaviour
{
    public PlatformSystem ps;

    public List<Vector3> vertices = new List<Vector3>();
    public List<int> tris = new List<int>();
    
    // Start is called before the first frame update
    void Start()
    {
    }

    private void BuildMesh()
    {
        /*
        MeshFilter mf = GetComponent<MeshFilter>();
        Mesh meshCopy = Mesh.Instantiate(mf.sharedMesh);
        Mesh mesh = mf.mesh = meshCopy;
        */
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.vertices = vertices.ToArray();
        mesh.triangles = tris.ToArray();
        mesh.RecalculateNormals();
    }

    private void BuildQuad(int[] verIdx)
    {
        List<int> newTris = new List<int>();

        newTris.Add(verIdx[0]);
        newTris.Add(verIdx[1]);
        newTris.Add(verIdx[2]);

        newTris.Add(verIdx[0]);
        newTris.Add(verIdx[2]);
        newTris.Add(verIdx[3]);

        tris.AddRange(newTris);
    }

    public void BuildPage()
    {
        vertices.Clear();
        tris.Clear();

        if (ps.platforms.Count == 0) { /*display error message*/ Debug.Log("PageGenerator: the platforms in PlatformSystem is empty"); return; }

        List<Vector3> plat_sys_bounds = ps.GetPlatformSystemBoundingBoxCorners();

        string de = "PlatformSystemBounds";
        foreach (Vector3 ver in plat_sys_bounds)
        {
            de += ver;
        }
        Debug.Log(de);

        //Generate new quads for Top;
        {
            List<PlatformGeneral> firstRow = ps.platforms[0];
            List<Vector3> plat_bounds = firstRow[0].GetPlatformBoundingBoxCorners();

            //add leftmost two vertices for the first row ID: 0, 3;
            vertices.Add(plat_sys_bounds[0]);
            Vector3 bl = plat_sys_bounds[0];
            bl.y = plat_bounds[0].y;
            vertices.Add(new Vector3());
            vertices.Add(new Vector3());
            vertices.Add(bl);

            /*
            int[] pre_ver_idx = new int[2];
            pre_ver_idx[0] = 0;
            pre_ver_idx[1] = 1;
            */
            Vector3 pre_top = vertices[0];
            Vector3 pre_bot = vertices[3];
            for (int i = 0; i < firstRow.Count; ++i)
            {
                int ver_ind_fir = vertices.Count - 4;
                vertices[ver_ind_fir + 1] = new Vector3(plat_bounds[0].x, pre_top.y, 0.0F);
                vertices[ver_ind_fir + 2] = plat_bounds[0];
                BuildQuad(new int[] { ver_ind_fir, ver_ind_fir + 1, ver_ind_fir + 2, ver_ind_fir + 3 });



                Vector3 new_top = new Vector3(plat_bounds[1].x, pre_top.y, 0.0F);
                Vector3 new_bot = new_top;
                new_bot.y = plat_bounds[1].y;
                vertices.Add(new_top);
                vertices.Add(new Vector3());
                vertices.Add(new Vector3());
                vertices.Add(new_bot);
                BuildQuad(new int[] { ver_ind_fir + 1, ver_ind_fir + 4, ver_ind_fir + 7, ver_ind_fir + 2 });

                pre_top = new_top;
                pre_bot = new_bot;

                PlatformGeneral platform = firstRow[i];
                if (i + 1 < firstRow.Count) { platform = firstRow[i + 1]; }
                plat_bounds = platform.GetPlatformBoundingBoxCorners();
            }

            int size = vertices.Count;
            vertices[size - 3] = new Vector3(plat_sys_bounds[1].x, pre_top.y, 0.0F);
            vertices[size - 2] = new Vector3(plat_sys_bounds[1].x, pre_bot.y, 0.0F);

            BuildQuad(new int[] { size - 4, size - 3, size - 2, size - 1 });
        }

        //Generate new quads for mid
        for (int i = 0; i < ps.platforms.Count; ++i)
        {
            List<PlatformGeneral> row = ps.platforms[i];

            List<Vector3> plat_bounds = row[0].GetPlatformBoundingBoxCorners();

            Vector3 lt = new Vector3(plat_sys_bounds[0].x, plat_bounds[0].y, 0.0F);
            Vector3 lb = new Vector3(plat_sys_bounds[0].x, plat_bounds[3].y, 0.0F);

            for (int j = 0; j < row.Count; ++j)
            {
                plat_bounds = row[j].GetPlatformBoundingBoxCorners();
                int size = vertices.Count;
                vertices.Add(lt);
                vertices.Add(plat_bounds[0]);
                vertices.Add(plat_bounds[3]);
                vertices.Add(lb);

                BuildQuad(new int[] { size - 4, size - 3, size - 2, size - 1 });

                lt = plat_bounds[1];
                lb = plat_bounds[2];
            }
        }

        //Generate new quads for Bottom;
        {
            List<PlatformGeneral> lastRow = ps.platforms[ps.platforms.Count - 1];
        }

        BuildMesh();

        Debug.Log("has vertices:" + vertices.Count + " has tris: " + tris.Count);
    }

    public void BuildPagePrototype()
    {
        vertices.Clear();
        tris.Clear();

        if (ps.platforms.Count == 0) { /*display error message*/ Debug.Log("PageGenerator: the platforms in PlatformSystem is empty"); return; }

        List<Vector3> plat_sys_bounds = ps.GetPlatformSystemBoundingBoxCorners();
        //List<Vector3> plat_bounds = ps.platforms[0][0].GetPlatformBoundingBoxCorners();

        Vector3 left_most_top = plat_sys_bounds[0];
        

        foreach (List<PlatformGeneral> row in ps.platforms)
        {
            List<Vector3> plat_bounds = row[0].GetPlatformBoundingBoxCorners();

            Vector3 left_top = left_most_top;
            Vector3 left_mid = new Vector3(plat_sys_bounds[0].x, plat_bounds[0].y, 0.0F);
            Vector3 left_bot = new Vector3(plat_sys_bounds[0].x, plat_bounds[3].y, 0.0F);
            left_most_top.y = left_bot.y;

            foreach (PlatformGeneral plat in row)
            {
                plat_bounds = plat.GetPlatformBoundingBoxCorners();

                /*
                 * 0 1 2
                 * 3 4 5
                 * 6 7 8
                 */

                /*
                 * 0 3 6
                 * 1 4 7
                 * 2 5 8
                 */

                vertices.Add(left_top); //0 0
                vertices.Add(left_mid); //3 1
                vertices.Add(left_bot); //6 2
                vertices.Add(new Vector3(plat_bounds[0].x, left_top.y, 0.0F)); //1 3
                vertices.Add(new Vector3(plat_bounds[0].x, left_mid.y, 0.0F)); //4 4
                vertices.Add(new Vector3(plat_bounds[0].x, left_bot.y, 0.0F)); //7 5
                vertices.Add(new Vector3(plat_bounds[1].x, left_top.y, 0.0F)); //2 6
                vertices.Add(new Vector3(plat_bounds[1].x, left_mid.y, 0.0F)); //5 7
                vertices.Add(new Vector3(plat_bounds[1].x, left_bot.y, 0.0F)); //8 8

                int first = vertices.Count - 9;

                BuildQuad(new int[] { first, first + 3, first + 4, first + 1});
                BuildQuad(new int[] { first + 1, first + 4, first + 5, first + 2});
                BuildQuad(new int[] { first + 3, first + 6, first + 7, first + 4});

                left_top = vertices[first + 6];
                left_mid = vertices[first + 7];
                left_bot = vertices[first + 8];
                
            }

            vertices.Add(new Vector3(plat_sys_bounds[1].x, left_top.y, 0.0F));
            vertices.Add(new Vector3(plat_sys_bounds[1].x, left_bot.y, 0.0F));

            int l_first = vertices.Count - 5;

            BuildQuad(new int[] { l_first, l_first + 3, l_first + 4, l_first + 2 });

            //left_most_top.y = vertices[l_first + 4].y;
        }

        //Build last padding
        if (ps.padding.y != 0)
        {
            vertices.Add(left_most_top);
            vertices.Add(new Vector3(plat_sys_bounds[2].x, left_most_top.y, 0.0F));
            vertices.Add(plat_sys_bounds[2]);
            vertices.Add(plat_sys_bounds[3]);

            

            int first = vertices.Count - 4;
            BuildQuad(new int[] { first, first + 1, first + 2, first + 3 });

            string de = "ver: ";
            for (int i = 0; i < 4; ++i)
            {
                de += vertices[first + i];
            }
            Debug.Log(de);
        }
        

        BuildMesh();
    }
}
