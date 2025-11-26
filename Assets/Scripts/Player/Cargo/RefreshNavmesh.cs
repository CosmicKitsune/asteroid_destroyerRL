using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Tilemaps;

public class RefreshNavmesh : MonoBehaviour
{
    public static void Build(NavMeshSurface navSurface)
    {
        navSurface.BuildNavMeshAsync();
    }

    public static void Refresh(NavMeshSurface navSurface)
    {
        navSurface.UpdateNavMesh(navSurface.navMeshData);
    }
}
