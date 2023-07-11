using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BuildSystem : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private BuldingScriptableObject bso;
    [SerializeField] private GameObject player;
    private Transform indicator;
    private Vector3 FrontObjectCheck;
    public GameObject temp;

    [SerializeField] private ItemInventory itemInventoryScript;

    [SerializeField] private Material canBuildMaterial, cannotBuildMaterial, invisibleMaterial;
    void Start()
    {
        

    }

    // // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        FrontObjectCheck = transform.TransformDirection(Vector3.forward) * 10f;
        Debug.DrawRay(transform.position, FrontObjectCheck, Color.yellow);
        if(bso != null)
        {
            InstantiateIndicator(EnoughMaterials());
            if (Physics.Raycast(transform.position, transform.forward, out hit, 10f))
            {
                
                if ((hit.collider.tag == "Ground" || hit.collider.tag == "Floor" || hit.collider.tag == "Wall"))
                {
                    temp.gameObject.SetActive(true);
                    Debug.Log(indicator.position.ToString());
                   // temp.gameObject.SetActive(true);
                   
                    if(bso.isFloor && (hit.collider.tag == "Ground" || hit.collider.tag == "Wall")   && hit.distance>4.0f)
                    {
                        if(hit.collider.tag == "Ground")
                        {
                            indicator.position = new Vector3(Mathf.RoundToInt(hit.point.x / 6) * 6,
                                Mathf.RoundToInt(hit.point.y / 6) * 6,
                                Mathf.RoundToInt(hit.point.z / 6) * 6);
                        }

                        if (hit.collider.tag == "Wall")
                        {
                            indicator.position = new Vector3(Mathf.RoundToInt(hit.point.x / 6) * 6,
                                hit.collider.transform.position.y+3,
                                Mathf.RoundToInt(hit.point.z / 6) * 6);
                        }
                        
                    }
                    
                    if (bso.isWall && hit.collider.tag == "Floor" && hit.distance>=1.5f)
                    {
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
                    
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                    {
                        if(EnoughMaterials())
                        {
                            BuildObject();
                            bso.building.GetComponent<Transform>().position= indicator.transform.position;
                            bso.building.GetComponent<Transform>().eulerAngles = indicator.transform.eulerAngles;
                            Instantiate(bso.building);
                        }
                        
                    }
                }
                else
                {
                    temp.gameObject.SetActive(false);
                }
            }
        }
       
    }
    
    public void setBSO(BuldingScriptableObject _bso)
    {
        bso = _bso;
        if(temp != null) Destroy(temp.gameObject);
        
        if (bso != null)
        {
            temp = Instantiate(bso.canBuildIndicator);
            indicator = temp.transform;
            InstantiateIndicator(EnoughMaterials());
        }
    }

    public bool EnoughMaterials()
    {
        for (int i = 0; i < bso.countOfMaterials.Length; i++)
        {
            int matCount = bso.countOfMaterials[i];
            ItemScriptableObject iso = bso.buildingMaterials[i];
            if (itemInventoryScript.GetItemCount(iso) < matCount) return false;
        }

        return true;
    }

    public void InstantiateIndicator(bool validBuild)
    {
        if (validBuild) temp.GetComponent<MeshRenderer>().material = canBuildMaterial;
        else temp.GetComponent<MeshRenderer>().material = cannotBuildMaterial;
    }

    public void BuildObject()
    {
        for (int i = 0; i < bso.countOfMaterials.Length; i++)
        {
            int matCount = bso.countOfMaterials[i];
            ItemScriptableObject iso = bso.buildingMaterials[i];
            itemInventoryScript.ConsumeMultiple(iso, matCount);
        }
    }
    
}
