using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRemovalScript : MonoBehaviour
{
    public void Initialize() {
        StartCoroutine(check(5));
    }
   private IEnumerator check(int times) {
       if(times == 0){
           StopAllCoroutines();
       }else{

 if(Physics2D.OverlapCircle(transform.position,0.5f,LayerMask.GetMask("Tile"))){
           Destroy(gameObject);

       }
       yield return new WaitForSeconds(0.4f);
       StartCoroutine(check(times-1));
       }
      
   }
}
