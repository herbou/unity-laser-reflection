using UnityEngine ;

public class Laser : MonoBehaviour {
   [SerializeField] private float maxLength = 8f ;
   [SerializeField] private int maxReflections = 8 ;
   [SerializeField] private LineRenderer lineRenderer ;
   [SerializeField] private LayerMask mirrorsLayerMask ;

   private Ray ray ;
   private RaycastHit2D hit ;


   private void Update () {
      ray = new Ray (transform.position, transform.up) ;

      lineRenderer.positionCount = 1 ;
      lineRenderer.SetPosition (0, transform.position) ;

      float remainingLength = maxLength ;

      for (int i = 0; i < maxReflections; i++) {
         hit = Physics2D.Raycast (ray.origin, ray.direction, remainingLength, mirrorsLayerMask.value) ;
         lineRenderer.positionCount += 1 ;

         if (hit) {
            lineRenderer.SetPosition (lineRenderer.positionCount - 1, hit.point) ;
            remainingLength -= Vector2.Distance (ray.origin, hit.point) ;
            ray = new Ray (hit.point - (Vector2)ray.direction * 0.01f, Vector2.Reflect (ray.direction, hit.normal)) ;

         } else
            lineRenderer.SetPosition (lineRenderer.positionCount - 1, ray.origin + ray.direction * remainingLength) ;
      }

   }

}