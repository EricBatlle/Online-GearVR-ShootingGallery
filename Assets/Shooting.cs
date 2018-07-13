using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using VRStandardAssets.Utils;

public class Shooting : NetworkBehaviour {

    [Header("VR")]
    [SerializeField] private VREyeRaycaster m_EyeRaycaster;             // Used to detect whether the gun is currently aimed at something.
    [SerializeField] private VRInput m_VRInput;                         // Used to catch the VR input actions
    [Header("Gun Position")]    
    [SerializeField] private Transform m_gunEnd;                          // Holds a reference to the gun end object, marking the muzzle location of the gun
    [Header("Ray Position")]
    [SerializeField] private Camera m_fpsCam;                             // Holds a reference to the first person camera
    [SerializeField] private LineRenderer m_laserLine;                    // Reference to the LineRenderer component which will display our laserline

    [Header("Shoot Attributes")]
    public int gunDamage = 1;                                           // Set the number of hitpoints that this gun will take away from shot objects with a health script
    public float fireRate = 0.25f;                                      // Number in seconds which controls how often the player can fire
    public float weaponRange = 50f;                                     // Distance in Unity units over which the player can fire
    public float hitForce = 100f;                                       // Amount of force which will be added to objects with a rigidbody shot by the player    

    private Text m_scoreLocalText;                                      // Reference to canvas remote score text
    private Text m_scoreRemoteText;                                     // Reference to canvas remote score text
    private WaitForSeconds m_shotDuration = new WaitForSeconds(0.07f);    // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible            
    private int m_score = 0;                                            // Actual player score

    private void Start()
    {
        //Set score references
        m_scoreLocalText = GameObject.FindGameObjectWithTag("LocalScore").GetComponent<Text>();
        m_scoreRemoteText = GameObject.FindGameObjectWithTag("RemoteScore").GetComponent<Text>();

        if (isLocalPlayer)
        {            
            m_VRInput.OnDown += Shoot;
        }
    }
    private void OnDisable()
    {
        if (isLocalPlayer)        
            m_VRInput.OnDown -= Shoot;
    }
    
    #region shoot
    private void Shoot()
    {        
        if (isServer)
        {
            RpcShoot();
        }
        else
        {
            CmdShoot();
        }
    }

    [Command]
    private void CmdShoot()
    {
        RpcShoot();
    }

    [ClientRpc]
    private void RpcShoot()
    {
        // Start our ShotEffect coroutine to turn our laser line on and off
        StartCoroutine(ShotEffect());

        // Create a vector at the center of our camera's viewport
        Vector3 rayOrigin = m_fpsCam.ViewportToWorldPoint(new Vector3(0,0,0));

        // Declare a raycast hit to store information about what our raycast has hit
        RaycastHit hit;

        // Set the start position for our visual effect for our laser to the position of gunEnd
        m_laserLine.SetPosition(0, m_gunEnd.position);

        // Check if our raycast has hit anything
        if (Physics.Raycast(rayOrigin, m_fpsCam.transform.forward, out hit, weaponRange))
        {
            // Set the end position for our laser line 
            m_laserLine.SetPosition(1, hit.point);
            print(hit.transform.gameObject.name);
            if (hit.transform.gameObject.tag == "Target")
            {
                GalleryTarget target = hit.transform.gameObject.GetComponent<GalleryTarget>();
                target.DestroyObject();
                UpdateScore();                
            }            
        }
        else
        {            
            // If we did not hit anything, set the end of the line to a position directly in front of the camera at the distance of weaponRange
            m_laserLine.SetPosition(1, rayOrigin + (m_fpsCam.transform.forward * weaponRange));
        }
    }

    private IEnumerator ShotEffect()
    {

        // Turn on our line renderer
        m_laserLine.enabled = true;

        //Wait for .07 seconds
        yield return m_shotDuration;

        // Deactivate our line renderer after waiting
        m_laserLine.enabled = false;
    }
    #endregion

    private void UpdateScore()
    {
        Text text = (isLocalPlayer) ? m_scoreLocalText : m_scoreRemoteText;
        m_score++;
        text.text = "Score: " + m_score;
    }
}
