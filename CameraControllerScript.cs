using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraControllerScript : MonoBehaviour
{
    //Variables globales
    float speed = 0.06f;
    float zoomSpeed = 10.0f;
    public float rotateSpeed = 0.20f;
    float maxHeight = 40.0f;
    float minHeight = 2.0f;
    float maxRotate = 60.0f;
    float minRotate = 0.0f;

    Vector2 p1;
    Vector2 p2;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKey(KeyCode.LeftShift)){
            speed = 0.04f;
            zoomSpeed = 20.0f;
        }
        else{
            speed = 0.035f;
            zoomSpeed = 10.0f;
        }

        float hsp = transform.position.y * speed * Input.GetAxis("Horizontal");
        float vsp = transform.position.y * speed * Input.GetAxis("Vertical");
        float scrollSp = Mathf.Log(transform.position.y) * -zoomSpeed * Input.GetAxis("Mouse ScrollWheel");

        if(((transform.position.y >= maxHeight) && (scrollSp > 0)) || ((transform.position.y <= minHeight) && (scrollSp < 0))){
            scrollSp = 0;
        }

        if((transform.position.y + scrollSp) > maxHeight){
            scrollSp = maxHeight - transform.position.y;
        }
        else if((transform.position.y + scrollSp) < minHeight){
            scrollSp = minHeight - transform.position.y;
        }


        Vector3 verticalMove = new Vector3(0,scrollSp,0);
        Vector3 lateralMove = hsp * transform.right;
        Vector3 forwardMove = transform.forward;
        forwardMove.y = 0;
        forwardMove.Normalize();
        forwardMove *= vsp;

        Vector3 move = verticalMove + lateralMove + forwardMove;

        transform.position += move;
        cameraRotation();
    }



 public Transform rotate;
 public Transform tilt;
 void cameraRotation()
 {
    if (Input.GetMouseButton(2)){
        tilt.Rotate(Vector2.up * Input.GetAxis("Mouse X") * rotateSpeed * Time.deltaTime);
        Vector2 newRotation = Vector2.left * Input.GetAxis("Mouse Y") * rotateSpeed * Time.deltaTime;

        if(((rotate.eulerAngles.x + newRotation.x >= maxRotate) && (newRotation.x > 0)) || ((rotate.eulerAngles.x + newRotation.x <= minRotate) && (newRotation.x < 0))){
            newRotation.x = 0;
        }

        if((rotate.eulerAngles.x + newRotation.x) > maxRotate){
            newRotation.x = maxRotate - rotate.eulerAngles.x;
        }
        else if((rotate.eulerAngles.x + newRotation.x) < minRotate){
            newRotation.x = minRotate - rotate.eulerAngles.x;
        }
        rotate.Rotate(newRotation);

    }
}

}

