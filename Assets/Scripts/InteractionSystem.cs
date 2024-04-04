using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    [Header("Detection Fields")]
    public Transform detectionPoint;
    private const float detectionRadius = 0.2f;
    public LayerMask detectionLayer;
    public GameObject detectedObject;
    [Header("Examine Fields")]
    public GameObject examineWindow;
    public GameObject grabbedObject;
    public float grabbedObjectYValue;
    public Transform grabPoint;
    public Image examineImage;
    public Text examineText;
    public bool isExamining;
    public bool isGrabbing;

    void Update()
    {
        if(DetectObject())
        {
            if(InteractInput())
            {
                if(isGrabbing)
                {
                    GrabDrop();
                    return;
                } 

                detectedObject.GetComponent<Item>().Interact();
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(detectionPoint.position, detectionRadius);
    }

    bool InteractInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    bool DetectObject()
    {        
                   
        Collider2D obj = Physics2D.OverlapCircle(detectionPoint.position,detectionRadius,detectionLayer); 
        
        if(obj==null)
        {
            detectedObject = null;
            return false;
        }
        else
        {
            detectedObject = obj.gameObject;
            return true;
        }
    }

    public void ExamineItem(Item item)
    {
        if(isExamining)
        {
            examineWindow.SetActive(false);
            isExamining = false;
        }
        else
        {
            examineImage.sprite = item.GetComponent<SpriteRenderer>().sprite;
            examineText.text = item.descriptionText;
            examineWindow.SetActive(true);
            isExamining = true;
        }        
    }

    public void GrabDrop()
    {        
        if(isGrabbing)
        {
            isGrabbing=false;
            grabbedObject.transform.parent=null;            
            grabbedObject.transform.position = 
                new Vector3(grabbedObject.transform.position.x,grabbedObjectYValue,grabbedObject.transform.position.z);
            grabbedObject=null;
        }
        else
        {
            isGrabbing=true;
            grabbedObject=detectedObject;
            grabbedObject.transform.parent=transform;
            grabbedObjectYValue=grabbedObject.transform.position.y;
            grabbedObject.transform.localPosition=grabPoint.localPosition;
        }
    }
}