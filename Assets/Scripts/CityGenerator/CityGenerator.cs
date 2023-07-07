using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityGenerator : MonoBehaviour
{
    [Range(11,55)]

    //Number of tiles on the side city
    public int CitySize = 15;
    //Tile size
    public float CellSize = 6;

    // Arrays of tiles
    public GameObject[] Building, Terrain, Road;


    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {
    }

    //It works in edit mode and play mode
    // Use it for generate new city
    // City is CitySize x CitySize array of tiles. Where each tile is a GameObject from Building, Terrain, Road arrays
    public void GenerateCity()
    {
        ClearCity();
        
        string[][] cityData = GenerateData(CitySize);
        for(int j=0;j<cityData.Length;j++)
        {
            for(int i=0;i<cityData[j].Length;i++)
            {
                if(cityData[j][i]==" ")AddCell(Terrain[Random.Range(0, Terrain.Length)],i,j,0);
                if(cityData[j][i]=="_")AddRoad(i,j,cityData);
                if(cityData[j][i]=="a")AddHouse(i,j,cityData);
            }
        }
    }
    // remove all tiles in container of the city
    public void ClearCity()
    {
        for(int i = this.transform.childCount-1;i>=0;i--)
        {
            Destroy(this.transform.GetChild(i).gameObject);
        }
    }

    // Adding a random House Tile
    void AddHouse(int x, int y, string[][] cityData)
    {
        List<int> d = new List<int>{0,1,2,3};
        string[][] g = cityData; 
        int i;
        for(i=0;i<d.Count;i++)
        {
            int j = Random.Range(0, 4);
            int temp = d[j];
            d[j] = d[i];
            d[i] = temp;
        }
        int[] p = new[]{1,0,0,1,-1,0,0,-1};
        for(i=0;i<d.Count;i++)
        {
            int dy = y+p[d[i]*2+1];
            if(dy<0||dy>=g.Length) break;
            int dx = x+p[d[i]*2+0];
            if(dx<0||dx>=g[dy].Length) break;
            if(g[dy][dx]=="_")
            {
                AddCell(Building[Random.Range(0, Building.Length)],x,y,d[i]*0);
                return;
            } 
        }
    }
    // Adding a Road Tile
    void AddRoad(int x, int y, string[][] g)
    {
        int[][] d = new[]{
        new[]{4,0,0,1,2,3},
        new[]{3,3,0,2,3},
        new[]{3,2,1,2,3},
        new[]{3,1,0,1,2},
        new[]{3,0,0,1,3},
        new[]{2,3,3,0},
        new[]{2,2,2,3},
        new[]{2,1,1,2},
        new[]{2,0,0,1},
        new[]{1,0,1,3},
        new[]{1,1,0,2}
        };
        int[] p = new[]{1,0,0,1,-1,0,0,-1};
        for(int j=0;j<d.Length;j++)
        {
            int i=2;
            for(i=2;i<d[j].Length;i++)
            {
                int w = d[j][i];
                int dy = y+p[w*2+1];
                if(dy<0||dy>=g.Length) break;
                int dx = x+p[w*2+0];
                if(dx<0||dx>=g[dy].Length) break;
                if(g[dy][dx]!="_") break;
            }
            if(i==d[j].Length)
            {
                int roadIndex = d[j][0] - 1;
                int rotation = d[j][1]*90;
                AddCell(Road[roadIndex],x,y,rotation);
                return;
            }
        }
    }
    // Adding the GameObject
    public void AddCell(GameObject cell,int x,int y,float deg)
    {
        Quaternion rotation = Quaternion.Euler(0,0,deg);
        Matrix4x4 m = Matrix4x4.Translate(new Vector3(x*CellSize,y*CellSize,0)) * Matrix4x4.Rotate(rotation) * Matrix4x4.Translate(new Vector3(-CellSize/2,-CellSize/2,0));

        GameObject a = Instantiate(cell) as GameObject;
        
        a.name = "tile_"+x.ToString()+"x"+y.ToString();
        a.transform.position = new Vector3(m[0,3], m[1,3], m[2,3]);
        a.transform.rotation = m.rotation;
        a.transform.SetParent(this.transform);
    }

    // Generate CitySize x CitySize Array of cells
    // where cell is Symbol:
    // "_" is Road Cell
    // "a" is Building Cell
    // " " is Terrain Cell
    string[][] GenerateData(int n)
    {
        string[][][] squareData = GetSquareData();
        int i;
        int j;
        string[][] g = new string[n][];
        for(i=0;i<n;i++)
        {
            g[i] = new string[n];
            for(j=0;j<n;j++)g[i][j]=" ";
        }

        string[][] p = squareData[Random.Range(0, squareData.Length)];
        SetRegion(p, g, Mathf.FloorToInt(n/2-p[0].Length/2), Mathf.FloorToInt(n/2-p.Length/2), false);
        int changesLeft = 400;
        for (int k = 5000; k > 0 && changesLeft > 0; k--) 
        {
            p = squareData[Random.Range(0, squareData.Length)];
            //var p = SquareData[0];
            int w = p[0].Length;
            int h = p.Length;
            int d = 1;
            int x = Mathf.RoundToInt(Random.value * (g.Length - w - d*2))+d;
            int y = Mathf.RoundToInt(Random.value * (g.Length - h - d*2))+d;
            if (IsEmpty(g, x, y, w, h)) continue;
            if (!IsEmpty(g, x+1, y+1, w-2, h-2)) continue;
            
            string[][] r = GetRegion(g, x, y, w, h);
            SetRegion(p, g, x, y, false);
            var revert = false;
            for (j = y - 2; !revert&&j <= y + h; j++)
            for (i = x - 2; !revert&&i <= x + w; i++)
            {
                if(GetCell(g,i,j)=="_"&&
                GetCell(g,i+1,j)=="_"&&
                GetCell(g,i+1,j+1)=="_"&&
                GetCell(g, i, j + 1) == "_") revert = true;
                if(GetCell(g,i,j)!="_"&&
                GetCell(g,i+1,j)=="_"&&
                GetCell(g,i+1,j+1)!="_"&&
                GetCell(g, i, j + 1) == "_") revert = true;
                if(GetCell(g,i,j)=="_"&&
                GetCell(g,i+1,j)!="_"&&
                GetCell(g,i+1,j+1)=="_"&&
                GetCell(g, i, j + 1) != "_") revert = true;

                if (
                GetCell(g,i,j)=="_"&&
                GetCell(g,i+1,j)==" "&&
                GetCell(g,i+2,j)=="_") revert = true;
                if (
                GetCell(g,i,j)=="_"&&
                GetCell(g,i,j+1)==" "&&
                GetCell(g,i,j+2)=="_") revert = true;
            }
            if (revert) SetRegion(r, g, x, y, true);
            else changesLeft--;
        }
        return g;
    }

    void SetRegion(string[][] pattern,string[][] g, int x, int y,bool replace)
    {
        for (var j = 0; j < pattern.Length; j++) 
        for (var i = 0; i < pattern[j].Length; i++) 
        {
            if (!replace&&GetCell(g, x+i, y+j) != " ") continue;
            SetCell(g, x + i, y + j, GetCell(pattern, i, j));
        }
    }
    string GetCell(string[][] pattern, int i, int j)
    {
	    if (j<0||j>=pattern.Length) return null;
	    if (i<0||i>=pattern[j].Length) return null;
        return pattern[j][i];
    }
    void SetCell(string[][] pattern, int i, int j, string s)
    {
        pattern[j][i]=s;
    }
    bool IsEmpty(string[][] g, int x, int y, int w, int h)
    {
        for (int j = 0; j < h; j++) 
        for (int i = 0; i < w; i++) 
        {
            string s = GetCell(g, x + i, y + j);
            if (s!=null && s != " ") return false;
        }
        return true;
    }
    string[][] GetRegion(string[][] g, int x, int y, int w, int h)
    {
        string[][] p = new string[h][];

        for (int j = 0; j < h; j++) 
        for (int i = 0; i < w; i++) 
        {
            if (i==0) p[j] = new string[w];
            string s = GetCell(g, x + i, y + j);
            if (s==null) s = " ";
            SetCell(p, i, j, s);
        }
        return p;
    }

    // initialize array of Insert Template
    // "_" is Road Cell
    // "a" is Building Cell
    // " " is Terrain Cell

    string[][][] GetSquareData()    
    {
        return new[]{
			new[]{
				new[]{"_","_","_","_","_","_","_","_","_"},
				new[]{"_","a","a","a","_","a","a","a","_"},
				new[]{"_","a","a","a","_","a","a","a","_"},
				new[]{"_","_","_","_","_","_","_","_","_"},
				new[]{"_","a","a","a","_","a","a","a","_"},
				new[]{"_","a","a","a","_","a","a","a","_"},
				new[]{"_","_","_","_","_","_","_","_","_"}
			},new[]{
				new[]{"_","_","_","_","_","_","_"},
				new[]{"_","a","a","_","a","a","_"},
				new[]{"_","a","a","_","a","a","_"},
				new[]{"_","_","_","_","_","_","_"}
			},new[]{
				new[]{"_","_","_","_"},
				new[]{"_","a","a","_"},
				new[]{"_","_","_","_"}
			},new[]{
				new[]{"_","_","_","_"},
				new[]{"_","a","a","_"},
				new[]{"_","a","a","_"},
				new[]{"_","a","a","_"},
				new[]{"_","_","_","_"}
			},new[]{
				new[]{"_","_","_","_","_","_","_","_","_"},
				new[]{"_","a","a","a","_","a","a","a","_"},
				new[]{"_","a","a","a","_","a","a","a","_"},
				new[]{"_","_","_","_","_","_","_","_","_"}
			}
        };        
    }    
}
