using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  
  public enum Elitetype{
        Health,
        Ammo,
        Boom,
        Money,
        Item
    }
public class EliteZombieBehavior : MonoBehaviour
{
  
    public Elitetype type;
    public bool elite;
  private void OnEnable() {
    elite = isElite(Random.Range(0,20));
    if(elite)
    {
RandomizeEliteType();
    }else
    {
        return;
    }
 }
 bool isElite(int input){
     Debug.Log("Elitenumber "+input);
     if(input == 15){
         return true;
     }else{
          return false;
     }
 }
 void RandomizeEliteType(){
     int randomint = Random.Range(0,100);
     if(randomint <40)
     {
         type = Elitetype.Boom;
     }
     else if(randomint <80 && randomint >40)
     {
         type = Elitetype.Health;
     } 
     else if(randomint <90 && randomint >80)
     {
         type = Elitetype.Ammo;
     }else if(randomint <98 && randomint >90){
         type = Elitetype.Money;
     }else{
         type = Elitetype.Item;
     }
 }
}
