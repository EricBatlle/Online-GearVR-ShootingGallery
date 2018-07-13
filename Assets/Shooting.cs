﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using VRStandardAssets.Utils;

public class Shooting : NetworkBehaviour {

    [SerializeField] private VREyeRaycaster m_EyeRaycaster;                         // Used to detect whether the gun is currently aimed at something.
    [SerializeField] private VRInput m_VRInput;
    [SerializeField] private LineRenderer m_GunFlare;
    [SerializeField] private Text m_scoreText;
    [SerializeField] private Text m_pointsText;

    public Vector3 cameraViewportVector;
    public int gunDamage = 1;                                           // Set the number of hitpoints that this gun will take away from shot objects with a health script
    public float fireRate = 0.25f;                                      // Number in seconds which controls how often the player can fire
    public float weaponRange = 50f;                                     // Distance in Unity units over which the player can fire
    public float hitForce = 100f;                                       // Amount of force which will be added to objects with a rigidbody shot by the player
    public Transform gunEnd;                                            // Holds a reference to the gun end object, marking the muzzle location of the gun

    [SerializeField] private Camera fpsCam;                                              // Holds a reference to the first person camera
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);    // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible
    
    [SerializeField] private LineRenderer laserLine;                                     // Reference to the LineRenderer component which will display our laserline
    private float nextFire;                                             // Float to store the time the player will be allowed to fire again, after firing

    private int m_score = 0;

    private void Start()
    {
        if (isLocalPlayer)
        {
            m_VRInput.OnDown += HandleDown;            
        }
        else
        {
            m_scoreText.rectTransform.position = new Vector3(m_scoreText.rectTransform.position.x, 5f, m_scoreText.rectTransform.position.z);
        }
    }
    private void OnDisable()
    {
        if (isLocalPlayer)        
            m_VRInput.OnDown -= HandleDown;
    }
    
    private void HandleDown()
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

    #region shoot

    [Command]
    private void CmdShoot()
    {
        RpcShoot();
    }
    [ClientRpc]
    private void RpcShoot()
    {
        // Update the time when our player can fire next
        nextFire = Time.time + fireRate;

        // Start our ShotEffect coroutine to turn our laser line on and off
        StartCoroutine(ShotEffect());

        // Create a vector at the center of our camera's viewport
        Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(cameraViewportVector);

        // Declare a raycast hit to store information about what our raycast has hit
        RaycastHit hit;

        // Set the start position for our visual effect for our laser to the position of gunEnd
        laserLine.SetPosition(0, gunEnd.position);

        // Check if our raycast has hit anything
        if (Physics.Raycast(rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
        {
            // Set the end position for our laser line 
            laserLine.SetPosition(1, hit.point);
            print(hit.transform.gameObject.name);
            if (hit.transform.gameObject.tag == "Target")
            {
                GalleryTarget target = hit.transform.gameObject.GetComponent<GalleryTarget>();
                target.DestroyObject();
                m_pointsText.text = m_score++.ToString();
            }
            // Get a reference to a health script attached to the collider we hit
            //ShootableBox health = hit.collider.GetComponent();

            // If there was a health script attached
            //if (health != null)
            //{
            //    // Call the damage function of that script, passing in our gunDamage variable
            //    health.Damage(gunDamage);
            //}

            // Check if the object we hit has a rigidbody attached
            //if (hit.rigidbody != null)
            //{
            //    // Add force to the rigidbody we hit, in the direction from which it was hit
            //    hit.rigidbody.AddForce(-hit.normal * hitForce);
            //}
        }
        else
        {
            print("nothing");
            // If we did not hit anything, set the end of the line to a position directly in front of the camera at the distance of weaponRange
            laserLine.SetPosition(1, rayOrigin + (fpsCam.transform.forward * weaponRange));
        }
    }
    #endregion

    private IEnumerator ShotEffect()
    {

        // Turn on our line renderer
        laserLine.enabled = true;

        //Wait for .07 seconds
        yield return shotDuration;

        // Deactivate our line renderer after waiting
        laserLine.enabled = false;
    }
    
}