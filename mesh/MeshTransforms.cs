﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace g3
{
    public static class MeshTransforms
    {

        public static void Translate(IDeformableMesh mesh, double tx, double ty, double tz)
        {
            int NV = mesh.MaxVertexID;
            for ( int vid = 0; vid < NV; ++vid ) {
                if (mesh.IsVertex(vid)) {
                    Vector3d v = mesh.GetVertex(vid);
                    v.x += tx; v.y += ty; v.z += tz;
                    mesh.SetVertex(vid, v);
                }
            }
        }


        public static void Rotate(IDeformableMesh mesh, Vector3d origin, Quaternionf rotation)
        {
            int NV = mesh.MaxVertexID;
            for ( int vid = 0; vid < NV; ++vid ) {
                if (mesh.IsVertex(vid)) {
                    Vector3d v = mesh.GetVertex(vid);
                    v -= origin;
                    v = (Vector3d)(rotation * (Vector3f)v);
                    v += origin;
                    mesh.SetVertex(vid, v);
                }
            }
        }

        public static void Scale(IDeformableMesh mesh, double sx, double sy, double sz)
        {
            int NV = mesh.MaxVertexID;
            for ( int vid = 0; vid < NV; ++vid ) {
                if (mesh.IsVertex(vid)) {
                    Vector3d v = mesh.GetVertex(vid);
                    v.x *= sx; v.y *= sy; v.z *= sz;
                    mesh.SetVertex(vid, v);
                }
            }
        }
        public static void Scale(IDeformableMesh mesh, double s)
        {
            Scale(mesh, s, s, s);
        }




        public static void ConvertZUpToYUp(IDeformableMesh mesh)
        {
            int NV = mesh.MaxVertexID;
            for ( int vid = 0; vid < NV; ++vid ) {
                if ( mesh.IsVertex(vid) ) {
                    Vector3d v = mesh.GetVertex(vid);
                    mesh.SetVertex(vid, new Vector3d(v.x, v.z, -v.y));
                }
            }
        }



        public static void FlipLeftRightCoordSystems(IDeformableMesh mesh)
        {
            int NV = mesh.MaxVertexID;
            for ( int vid = 0; vid < NV; ++vid ) {
                if ( mesh.IsVertex(vid) ) {
                    Vector3d v = mesh.GetVertex(vid);
                    v.z = -v.z;
                    mesh.SetVertex(vid, v);

                    if (mesh.HasVertexNormals) {
                        Vector3f n = mesh.GetVertexNormal(vid);
                        n.z = -n.z;
                        mesh.SetVertexNormal(vid, n);
                    }
                }
            }

            if ( mesh is DMesh3 ) {
                (mesh as DMesh3).ReverseOrientation(false);
            } else {
                throw new Exception("argh don't want this in IDeformableMesh...but then for SimpleMesh??");
            }

        }

    }
}
