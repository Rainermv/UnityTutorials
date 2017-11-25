using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Bezier {


    // A bezier curve is a linear interpolation between the points in the curve

    // QUADRATIC CURVE
    
    // This method uses the faster quadratic formula to achieve the interpolation
    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t) {

        /* The linear curve can be writen as: 
         * 
         *  B(t) = (1 - t) * p0 + t * p1
         * 
         * so, applying the formula replacing p0 and p1 with two new linear curves
         * where p0 = p0 to p1) and p1 = p1 to p2
         * 
         *                         p0                      p1
         *  B(t) = (1 - t) * ((1-t)*p0+t*p1) + t * ((1-t)*p1+t*p2)
         *  
         * This formula can be reritten into the more compact form:
         * 
         *  B(t) = (1 - t)² * p0 + 2 * (1-t) * t * P1 + t² * P2
         * 
         * 
         */

        t = Mathf.Clamp01(t);
        float mt = 1f - t;
        return (mt * mt) * p0 + 2f * mt * t * p1 + (t * t) * p2;

    }

    // This method uses the "Lerp" method to achieve the interpolation
    public static Vector3 GetPoint2(Vector3 p0, Vector3 p1, Vector3 p2, float t) {
        
        Vector3 np0 = Vector3.Lerp(p0, p1, t);
        Vector3 np1 = Vector3.Lerp(p1, p2, t);

        Vector3 fp  = Vector3.Lerp(np0, np1, t);

        /*
         * if (t = 0.5)
         *           p1
         *        .      .
         *   np0. ...fp... .np1
         *    .              .
         *p0.                  .p2
         * 
         * 
         * 
         * */

        return fp;

    }

    public static Vector3 GetFirstDerivative (Vector3 p0, Vector3 p1, Vector3 p2, float t) {

        /*
         * A derivative measures the rate of change of a function
         * ex: Velocity is the derivative of position, just as acceleration is the derivative of velocity
         * 
         * f(t) = n    -  then  -   f'(t) = 0
         * f(t) = t    -  then  -   f'(t) = 1
         * f(t) = nt   -  then  -   f'(t) = n
         * f(t) = t^n  -  then  -   f'(t) = n*t ^ n-1 (only if n > 0)  
         * 
         * So, the first derivative of a quadratic Bezier curve is
         * 
         *  B'(t) = 2 * (1 - t ) * (P1 - P0) + 2 * t * (P2 - P1)
         * 
         */

        return 2f * (1f - t) * (p1 - p0) + 2f * t * (p2 - p1);

    }

    // CUBIC CURVE

    public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {

        // B(t) = (1 - t)³ P0 + 3 (1 - t)² t P1 + 3 (1 - t) t² P2 + t³ P3

        t = Mathf.Clamp01(t);
        float mt = 1f - t;

        return (mt * mt * mt) * p0 + 3f * (mt * mt) * t * p1 + 3f * mt * (t * t) * p2 + (t * t * t) * p3;
    }

    public static Vector3 GetFirstDerivative (Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t) {

        // B'(t) = 3 (1 - t)² (P1 - P0) + 6 (1 - t) t (P2 - P1) + 3 t² (P3 - P2).
        t = Mathf.Clamp01(t);
        float mt = 1f - t;

        return 3f * (mt * mt) * (p1 - p0) + 6f * mt * t * (p2 - p1) + 3f * (t * t) * (p3 - p2);

    }


}
