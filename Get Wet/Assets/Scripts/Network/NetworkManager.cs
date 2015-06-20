﻿using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    const string typeName = "Get Wet";
    const string gameName = "Get Wet room";

    public GameObject playerPrefab;
    public GameObject mainCam;
    
    public GameObject WhiteBa = GameObject.Find("whiteBazooka");
    public GameObject BlackBa = GameObject.Find("blackBazooka");
    public GameObject WhiteGr = GameObject.Find("whiteGrenade");
    public GameObject BlackGr = GameObject.Find("blackGrenade");
    public GameObject WhiteShot = GameObject.Find("whiteShotgun");
    public GameObject BlackShot = GameObject.Find("blackShotgun");
    public GameObject AmyCac = GameObject.Find("MagicAmyCac");
    public GameObject AmySniper = GameObject.Find("MagicAmySniper");
    public GameObject flag;
    public int SavedChar = 1;
    public int SavedSpawn = 0;
    public int SavedWeapon = 1;
    public int team = 0;
    GameObject player;
    GameObject objective;
    int ctf;

    void Awake()
    {
       MasterServer.ipAddress = PlayerPrefs.GetString("DaIP");
       SavedChar = PlayerPrefs.GetInt("SelectedCharacter");
       SavedSpawn = PlayerPrefs.GetInt("SelectedSpawn");
       SavedWeapon = PlayerPrefs.GetInt("SelectedWeapon");
    }

    void Start()
    {
        NetworkConnectionError err = Network.InitializeServer(4, 25000, !Network.HavePublicAddress());
        
        if (err == NetworkConnectionError.NoError)
            MasterServer.RegisterHost(typeName, gameName);
        else
            MasterServer.RequestHostList(typeName);
    }

    void OnMasterServerEvent(MasterServerEvent msEvent)
    {
        if (msEvent == MasterServerEvent.HostListReceived)
        {
            HostData[] hostList = MasterServer.PollHostList();
            if (hostList.Length != 0)
                Network.Connect(hostList[0]);
            else
                MasterServer.RequestHostList(typeName);
        }
    }

    void OnServerInitialized()
    {
        team = PlayerPrefs.GetInt("SelectedTeam");
        SpawnFlag();
        //BLUESPAWN
        if (team == 1)
            SpawnPlayer(427, 22, 703);
        //REDSPAWN
        if (team == 2)
            SpawnPlayer(435, 22, 533);
    }

    void OnConnectedToServer()
    {
        team = PlayerPrefs.GetInt("SelectedTeam");
        //REDSPAWN
        if (team == 2)
            SpawnPlayer(435,22, 533);
        //BLUESPAWN
        if (team == 1)
            SpawnPlayer(427, 22, 703);
    }

    public void SpawnFlag()
    {
        ctf = PlayerPrefs.GetInt("SelectedGameMode");
        Debug.Log(ctf);
        if (ctf == 2)
        {
            Debug.Log("Spawnflag");
            objective = Network.Instantiate(flag, new Vector3(389.3f, 28.2f, 654.3f), Quaternion.identity, 0) as GameObject;

        }
    
    }
    public void SpawnPlayer(float x, float y, float z)
    {
        //GameObject player = Network.Instantiate(playerPrefab, new Vector3(x, y, z), Quaternion.identity, 0) as GameObject;
        #region spawning
        if (SavedChar == 1)
        {
            if (SavedWeapon <= 1)
            {
                player = Network.Instantiate(WhiteBa, new Vector3(x, y, z), Quaternion.identity, 0) as GameObject;
            }

            if (SavedWeapon == 2)
            {
               player = Network.Instantiate(WhiteGr, new Vector3(x, y, z), Quaternion.identity, 0) as GameObject;
            }

            if (SavedWeapon == 3)
            {
                 player = Network.Instantiate(WhiteShot, new Vector3(x, y, z), Quaternion.identity, 0) as GameObject;
            }
        }
        if (SavedChar == 2)
        {
            if (SavedWeapon == 1)
            {
                 player = Network.Instantiate(BlackBa, new Vector3(x, y, z), Quaternion.identity, 0) as GameObject;
            }

            if (SavedWeapon == 2)
            {
                player = Network.Instantiate(BlackGr, new Vector3(x, y, z), Quaternion.identity, 0) as GameObject;
            }

            if (SavedWeapon == 3)
            {
                 player = Network.Instantiate(BlackShot, new Vector3(x, y, z), Quaternion.identity, 0) as GameObject;
            }
        }

        if (SavedChar == 3)
        {
            if (SavedWeapon <= 1)
            {
                player = Network.Instantiate(AmyCac, new Vector3(x, y, z), Quaternion.identity, 0) as GameObject;
            }

            if (SavedWeapon == 2)
            {
                 player = Network.Instantiate(AmySniper, new Vector3(x, y, z), Quaternion.identity, 0) as GameObject;
            }
        }
        

        #endregion

        

        
        WoWcamera cam =Instantiate<GameObject>(mainCam).GetComponent<WoWcamera>();
        cam.target = player.transform;
        cam.here = mainCam.GetComponent<Camera>();
        
        cam.weapon = player.transform.Find("Bip01/Bip01 Pelvis/Bip01 Spine/Bip01 Spine1/Bip01 Neck/Bip01 R Clavicle/Bip01 R UpperArm/Bip01 R Forearm/bazooka");
        player.AddComponent<PlayerNetworking>();
        player.AddComponent<PlayerInputManager>();
    }
}