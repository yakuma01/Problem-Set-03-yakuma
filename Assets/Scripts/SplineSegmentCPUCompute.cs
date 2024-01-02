using System.Collections.Generic;
using UnityEngine;


namespace PS03 {

    public class SplineSegmentCPUCompute : MonoBehaviour {

        // control points for a single spline segment curve:
        [SerializeField] private Transform control0, control1, control2, control3;

        // choice of spline type:
        [SerializeField] private SplineParametersC.SplineType splineType;

        // the two line renderers: the control polyline, and the spline curve itself:
        [SerializeField] private LineRenderer controlPolyline, splineCurve;

        private List<LineRenderer> renderedSpline;

        [SerializeField] private GameObject LineRendererPrefab, splineSegment;

		// how many points on the spline curve?
		//   (the more points you set, the smoother the curve will be)
        [Range(8, 512)] [SerializeField] private int curvePoints = 16;

        private PlaceControlPoints placeControlPoints;

        public void SetType(SplineParametersC.SplineType type) {
            splineType = type;
        }

        public void UseBezier() {
            SetType(SplineParametersC.SplineType.Bezier);
        }
        
        public void UseCatmullRom() {
            SetType(SplineParametersC.SplineType.CatmullRom);
        }

        public void UseB() {
             SetType(SplineParametersC.SplineType.Bspline);
        }

        private void Start()
        {
            placeControlPoints = GetComponent<PlaceControlPoints>();
            renderedSpline = new List<LineRenderer>();

        }

        private void Update() {
            if (placeControlPoints.controlPoints.Count < 4) return;

            float numSplines = Mathf.Floor((placeControlPoints.controlPoints.Count - 4) / 3) + 1;

            Debug.Log("NUMS SPLINES: " + numSplines);


            for (int k = 0; k < (int)numSplines; k++)
            {
                splineCurve = Instantiate(LineRendererPrefab, Vector3.zero, Quaternion.identity, splineSegment.transform).GetComponent<LineRenderer>();
                renderedSpline.Add(splineCurve);

                // update number of points on spline curve:
                if (splineCurve.positionCount != curvePoints)
                {
                    splineCurve.positionCount = curvePoints;
                }

                // get the correct matrix of spline parameters, from the SplineParameters script:
                // (e.g. for the Bezier spline matrix, as per Textbook Chapter 11.6.1
                //        in the 8th and 7th edition Textbook )
                //
                Matrix4x4 splineMatrix = SplineParametersC.GetMatrix(splineType);

                // and now compute the spline curve, point by point:
                for (int i = 0; i < curvePoints; i++)
                {
                    float u = (float)i / (float)(curvePoints - 1);

                    // you have to define the u vector, a 4-element vector...
                    //  (defined as in textbook Chapter 11.3 in the 8th and 7th edition Textbook 
                    //                        / Chapter 10.3 in the 6th edition Textbook)
                    // ...here, but always keep in mind that:
                    // Unity stores matrices in the Matrix4x4 data type
                    //    (as stated in the online Unity scripting manual)
                    // "Matrices in unity are column major."
                    // so ...
                    // 
                    Vector4 uRow = new Vector4(1, u, u * u, u * u * u);

                    // TODO - properly create the matrix of control points: 
                    control0 = placeControlPoints.controlPoints[k*3].transform;
                    control1 = placeControlPoints.controlPoints[k*3 + 1].transform;
                    control2 = placeControlPoints.controlPoints[k*3 + 2].transform;
                    control3 = placeControlPoints.controlPoints[k*3 + 3].transform;

                    Matrix4x4 controlMatrix = new Matrix4x4(
                       new Vector4(control0.transform.localPosition.x, control0.transform.localPosition.y, control0.transform.localPosition.z, 1),
                       new Vector4(control1.transform.localPosition.x, control1.transform.localPosition.y, control1.transform.localPosition.z, 1),
                       new Vector4(control2.transform.localPosition.x, control2.transform.localPosition.y, control2.transform.localPosition.z, 1),
                       new Vector4(control3.transform.localPosition.x, control3.transform.localPosition.y, control3.transform.localPosition.z, 1)
                    );

                    // finally, compute the splinePointPosition as from the Hermite Form
                    //  (defined as in textbook Chapter 11.5 in the 8th and 7th edition Textbook 
                    //                        / Chapter 10.5 in the 6th edition Textbook)
                    // i.e.
                    //  the form p(u) = u * M * p
                    //  really should be computed as:  p(u) = p * MB * u
                    //  as per assignment instructions!
                    //
                    Vector4 splinePointPosition = controlMatrix * splineMatrix * uRow;
                    Debug.Log("SplinePointPosition " + splinePointPosition + " u " + u + " i " + i);

                    // once the splinePointPosition is computed,
                    //   assign it to the related Position property in the splineCurve object:
                    renderedSpline[k].SetPosition(i, (Vector2)splinePointPosition);
                }
            }
        }
    }
} // end of namespace PS03