using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildSystem : MonoBehaviour
{
    // Start is called before the first frame update
    [FormerlySerializedAs("floorIndicator")] [SerializeField] public Transform indicator;
    [SerializeField] public GameObject building;
    [SerializeField] private GameObject player;
    public bool wallBuilding, floorBuilding;
    private Vector3 FrontObjectCheck;
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        FrontObjectCheck = transform.TransformDirection(Vector3.forward) * 10f;
        Debug.DrawRay(transform.position, FrontObjectCheck, Color.yellow);
        if(isBuildingOn())
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 10f))
            {
                
                if ((hit.collider.tag == "Ground" || hit.collider.tag == "Floor"))
                {
                    indicator.gameObject.SetActive(true);
                    Debug.Log(hit.collider.tag);
                    if(floorBuilding && hit.collider.tag == "Ground" && hit.distance>4.0f)
                    {
                        indicator.position = new Vector3(Mathf.RoundToInt(hit.point.x / 6) * 6,
                            Mathf.RoundToInt(hit.point.y) >= hit.point.y
                                ? Mathf.RoundToInt(hit.point.y/6)*6
                                : Mathf.RoundToInt(hit.point.y/6)*6,
                            Mathf.RoundToInt(hit.point.z / 6) * 6);
                        if (Input.GetKeyDown(KeyCode.Mouse0))
                        {
                            building.GetComponent<Transform>().position= indicator.position;
                            Instantiate(building);
                        }
                    }
                    
                    if (wallBuilding && hit.collider.tag == "Floor" && hit.distance>=1.5f)
                    {
                        // indicator.position = new Vector3(Mathf.RoundToInt(hit.point.x / 6) * 6,
                        //     Mathf.RoundToInt(hit.point.y) >= hit.point.y
                        //         ? Mathf.RoundToInt(hit.point.y/6)*6
                        //         : Mathf.RoundToInt(hit.point.y/6)*6,
                        //     Mathf.RoundToInt(hit.point.z / 6) * 6);
                        
                        float modRotation = player.transform.eulerAngles.y % 360;
                        if (modRotation > 0.0f && modRotation <= 90.0f)
                        {
                            indicator.transform.eulerAngles = new Vector3(0, 90, 90);
                            indicator.position = new Vector3(Mathf.RoundToInt(hit.point.x / 6) * 6,
                                hit.collider.transform.position.y+3,
                                hit.collider.transform.position.z +3);
                            // Debug.Log("1");
                            // Debug.Log(indicator.transform.eulerAngles.ToString());
                        }
                        if(modRotation>90.0f && modRotation <= 180.0f)
                        {
                            indicator.transform.eulerAngles = new Vector3(0, 180, 90);
                            
                            indicator.position = new Vector3(hit.collider.transform.position.x +3,
                                hit.collider.transform.position.y+3,
                                Mathf.RoundToInt(hit.point.z / 6) * 6);
                            // Debug.Log("2");
                            // Debug.Log(indicator.transform.eulerAngles.ToString());
                        }
                        if(modRotation>180.0f && modRotation <= 270.0f)
                        {
                            indicator.transform.eulerAngles = new Vector3(0, 270, 90);
                            indicator.position = new Vector3(Mathf.RoundToInt(hit.point.x / 6) * 6,
                                hit.collider.transform.position.y+3,
                                hit.collider.transform.position.z - 3);
                            // Debug.Log("3");
                            // Debug.Log(indicator.transform.eulerAngles.ToString());
                        }
                        if(modRotation>270.0f && modRotation <= 360.0f)
                        {
                            indicator.transform.eulerAngles = new Vector3(0, 360, 90);
                            indicator.position = new Vector3(hit.collider.transform.position.x - 3,
                                hit.collider.transform.position.y+3,
                                Mathf.RoundToInt(hit.point.z / 6) * 6);
                        }
                    }
                    
                }
                else
                {
                    indicator.gameObject.SetActive(false);
                }
            }
        }
       
    }

    private bool isBuildingOn()
    {
        if (wallBuilding || floorBuilding) return true;
        return false;
    }
}
