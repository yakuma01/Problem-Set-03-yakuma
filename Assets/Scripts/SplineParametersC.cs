using UnityEngine;


namespace PS03
{

    public static class SplineParametersC
    {

        public enum SplineType { Bezier, CatmullRom, Bspline }

        public static Matrix4x4 GetMatrix(SplineType type)
        {

            switch (type)
            {
                // TODO: generate Bezier spline matrix,
                //   with constants as per Textbook Chapter 11.6.1 :
                case SplineType.Bezier:
                    return new Matrix4x4( // COLUMN MAJOR! performed by Yash: (transposed)
                                          new Vector4(1, 0, 0, 0), // TODO
                                          new Vector4(-3, 3, 0, 0), // TODO
                                          new Vector4(3, -6, 3, 0), // TODO
                                          new Vector4(-1, 3, -3, 1) // TODO
                         //new Vector4(1, -3, 3, -1), // TODO
                         //new Vector4(0, 3, -6, 3), // TODO
                         //new Vector4(0, 0, 3, -3), // TODO
                         //new Vector4(0, 0, 0, 1) // TODO
                    );
                // TODO: generate Catmull-Rom spline matrix,
                //   with constants as per Textbook Chapter 11.8.5 :
                case SplineType.CatmullRom:
                    return new Matrix4x4( // COLUMN MAJOR! performed by Yash: (transposed)
                                           new Vector4(0, 2, 0, 0) / 2, // TODO
                                           new Vector4(-1, 0, 1, 0) / 2, // TODO
                                           new Vector4(2, -5, 4, -1) / 2, // TODO
                                           new Vector4(-1, 3, -3, 1) / 2 // TODO
                        //new Vector4(0, -1, 2, -1) / 2, // TODO
                        //new Vector4(2, 0, -5, 3) / 2, // TODO
                        //new Vector4(0, 1, 4, -3) / 2, // TODO
                        //new Vector4(0, 0, -1, 1) / 2 // TODO
                    );
                // TODO: generate B-spline matrix,
                //   with constants as per Textbook Chapter 11.7.1 :
                case SplineType.Bspline:
                    return new Matrix4x4( // COLUMN MAJOR! performed by Yash: (transposed)
                                           new Vector4(1, 4, 1, 0) / 6, // TODO
                                           new Vector4(-3, 0, 3, 0) / 6, // TODO
                                           new Vector4(3, -6, 3, 0) / 6, // TODO
                                           new Vector4(-1, 3, -3, 1) / 6 // TODO
                        //new Vector4(1, -3, 3, -1) / 6, // TODO
                        //new Vector4(4, 0, -6, 3) / 6, // TODO
                        //new Vector4(1, 3, 3, -3) / 6, // TODO
                        //new Vector4(0, 0, 0, 1) / 6 // TODO
                    );
                default:
                    return Matrix4x4.identity;
            }
        } // end of GetMatrix()

        // this could be useful for multi-segment spline curves:
        public static bool UsesConnectedEndpoints(SplineType type)
        {
            switch (type)
            {
                case SplineType.Bezier: return true;
                case SplineType.CatmullRom: return false;
                case SplineType.Bspline: return false;
                default: return false;
            }
        } // end of UsesConnectedEndpoints()

    } // end of class SplineParameters

} // end of namespace PS02