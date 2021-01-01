using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cylinder : MonoBehaviour
{
   //Referencing
    Transform mainCylinder;
    [SerializeField] GameObject particle;
    [SerializeField] GameObject cylinderWater;
    [SerializeField] GameObject cylinderBroken;
    [SerializeField] float radius;

    #region Variables for Check Cutting
    Vector3 hingePointLeft;
    Vector3 hingePointBottom;
    Vector3 hingePointRight;
    Vector3 hingePointAbove;

    bool hasHitLeft;
    bool hasHitRight;
    bool hasHitBottom;
    bool hasHitAbove;

    #endregion
    CapsuleCollider capsuleCollider;
    int layerMask = 1 << 9;


    // Start is called before the first frame update
    void Start()
    {
        //Initialize variables with game objects etc.
        Initialize();

    }

    private void Update()
    {
        CheckforCuttingCompletely();
        if (hasCutCompletely()) CutPhaseActivate();
    }



    //Checks if the cutter is in a position where it cuts the pipe completely
    private void CheckforCuttingCompletely()
    {
        //Raycasts are for checking if the cutter has pass all four positions which are in the bottom, left, right, and top of the circle
        hasHitBottom = Physics.Raycast(GetBottomRay(), 5f, layerMask);
        hasHitLeft = Physics.Raycast(GetLeftRay(), 5f, layerMask);
        hasHitRight = Physics.Raycast(GetRightRay(), 5f, layerMask);
        hasHitAbove = Physics.Raycast(GetAboveRay(), 5f, layerMask);
    }
    private bool hasCutCompletely()
    {
        return hasHitBottom && hasHitRight && hasHitLeft && hasHitAbove;
    }
    private void CutPhaseActivate()
    {

        mainCylinder.gameObject.SetActive(false);
        cylinderWater.SetActive(true);
        cylinderBroken.SetActive(true);
        particle.SetActive(true);
    }
    private void Initialize()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
        radius = capsuleCollider.radius;
        hingePointBottom = capsuleCollider.center + new Vector3(0, -radius, -1);
        hingePointLeft = capsuleCollider.center + new Vector3(radius, 0, -1);
        hingePointRight = capsuleCollider.center + new Vector3(-radius, 0, -1);
        hingePointAbove = capsuleCollider.center + new Vector3(0, radius, -1);
        mainCylinder = GetComponent<Transform>();
    }
    
    
    #region Debugging With Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(hingePointBottom, 0.1f);
        Gizmos.DrawWireSphere(hingePointLeft, 0.1f);
        Gizmos.DrawWireSphere(hingePointRight, 0.1f);

        Gizmos.DrawRay(GetBottomRay());
    }
    #endregion

    #region RayGetters
    Ray GetBottomRay()
    {
        return new Ray(hingePointBottom, Vector3.forward);
    }

    Ray GetLeftRay()
    {
        return new Ray(hingePointLeft, Vector3.forward);
    }

    Ray GetRightRay()
    {
        return new Ray(hingePointLeft, Vector3.forward);
    }

    Ray GetAboveRay()
    {
        return new Ray(hingePointAbove, Vector3.forward);
    }

    #endregion
}

