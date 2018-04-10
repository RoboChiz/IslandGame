using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingModeManager : MonoBehaviour
{
    public bool isActivated = false, isLocked = false;
    private bool isAnimating = false, isHiding = false;

    public Image buildModeImage, playModeImage;
    private const float fadedValue = 0.2f, travelTime = 0.5f, radius = 20f;
    private readonly Vector2 spinPoint = new Vector2(775f, -475f);

    private float hideTimer;
    Coroutine buildHideCoroutine, playHideCoroutine;

    public GameObject cursor, fluidCursor, player, itemsPanel, grid, cameraTarget;
    private PlayerMovement playerMovement;
    private WorldStateManager worldStateManager;

    private Vector3 actualCursorPos;
    private float rotationAmount = 0f, lerpingRotationAmount = 0f;

    public float lerpAmount = 10f;

    private Vector3 inputRecieved;

    private const float maxInputLockTimer = 0.03f;
    private float inputLockTimer = maxInputLockTimer;

    private GameObject cursorObject;
    private Material currentCursorMaterial;

    public Color validCursor, invalidCursor;

    private int currentSelection = 1;

    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        worldStateManager = FindObjectOfType<WorldStateManager>();

        itemsPanel.SetActive(false);
    }

    private void Update()
    {
        cameraTarget.GetComponent<FluidCursor>().isLocked = isLocked;

        //User Inputs
        if (!isLocked && InputManager.controllers.Count > 0 && !PauseMenu.isPaused)
        {
            InputDevice inputDevice = InputManager.controllers[0];

            if (!isAnimating && inputDevice.GetButtonWithLock("BuildMode"))
            {
                //Stop Any Hiding
                if (buildHideCoroutine != null)
                {
                    StopCoroutine(buildHideCoroutine);
                    StopCoroutine(playHideCoroutine);
                }

                if (isActivated)
                {
                    EndBuildMode();
                }
                else
                {
                    StartBuildMode();
                }

                hideTimer = 5f;
                isHiding = false;
            }

            if (isActivated)
            {
                //Map Cursor to Grid
                Vector3 mousePos;

                if (inputDevice.inputType == InputType.Keyboard)
                {
                    mousePos = fluidCursor.transform.position;
                }
                else
                {
                    mousePos = cameraTarget.transform.position;
                }

                mousePos.y = cameraTarget.GetComponent<FluidCursor>().GetActualPos().y;

                WorldChunk insideChunk = worldStateManager.IsInsideWorldChunk(mousePos);
                bool validCursor = insideChunk != null;
                if (validCursor)
                {
                    actualCursorPos = worldStateManager.ChunkToWorld(insideChunk, worldStateManager.WorldToChunk(insideChunk, mousePos));
                    cursor.transform.position = actualCursorPos;
                }

                //Move the Cursor
                mousePos.y = cameraTarget.transform.position.y;
                fluidCursor.transform.position = mousePos;

                bool build = false, delete = false;
                int rotate = 0;

                //Build Controls
                build = inputDevice.GetButton("Create");
                delete = inputDevice.GetButton("Delete");

                //Rotation
                rotate = inputDevice.GetIntInputWithDelay("Rotate", 0.3f, Time.deltaTime);

                //Item Swapping
                int maxKeys = 10;
                if (inputDevice.inputType == InputType.Keyboard)
                {
                    for (int i = 1; i < maxKeys; i++)
                    {
                        if (inputDevice.GetButtonWithLock("Item" + i))
                        {
                            ChangeSelection(i);
                        }
                    }
                }
                else
                {
                    int itemSwitch = inputDevice.GetIntInputWithDelay("ItemSwitch", 0.25f, Time.deltaTime);

                    if (itemSwitch != 0)
                    {
                        int max = FindObjectOfType<BuildingPartDatabaseManager>().GetBuildingPartCount() + 1;
                        ChangeSelection(MathHelper.NumClamp(currentSelection + itemSwitch, 1, Mathf.Min(max, maxKeys)));
                    }
                }

                IsoCam isoCam = playerMovement.playerCamera.GetComponent<IsoCam>();

                //Mouse Controls
                if (inputDevice.inputType == InputType.Keyboard)
                {
                    Ray ray = playerMovement.playerCamera.ScreenPointToRay(Input.mousePosition);
                    float hit;

                    Plane plane = new Plane(Vector3.up, -fluidCursor.transform.position.y);

                    if (plane.Raycast(ray, out hit))
                    {
                        Vector3 hitPoint = ray.GetPoint(hit);
                        hitPoint.y = fluidCursor.transform.position.y;
                        fluidCursor.transform.position = hitPoint;
                    }

                    Debug.DrawRay(ray.origin, ray.direction, Color.red);
                }

                if (validCursor && inputDevice.inputType == InputType.Keyboard)
                {
                    if (!RectTransformUtility.RectangleContainsScreenPoint(itemsPanel.GetComponent<RectTransform>(), Input.mousePosition, Camera.main))
                    {
                        build = InputManager.GetClickHold(0);
                        delete = InputManager.GetClickHold(1);
                    }             
                }

                grid.transform.position = Vector3.Scale(grid.transform.position, new Vector3(1f, 0f, 1f)) + Vector3.Scale(actualCursorPos, new Vector3(0f, 1f, 0f)) + new Vector3(0f,0.06f,0f);

                //Do Build Inputs
                if (build)
                {
                    worldStateManager.CreateItem(currentSelection, actualCursorPos, rotationAmount);
                }
                else
                {
                    worldStateManager.buildLock = false;
                }

                if (delete)
                {
                    worldStateManager.DeleteItem(actualCursorPos);
                }

                //Do Rotating
                if (rotate != 0)
                {
                    rotationAmount -= rotate * 90f;
                }

                lerpingRotationAmount = Mathf.Lerp(lerpingRotationAmount, rotationAmount, Time.deltaTime * lerpAmount);
                cursor.transform.rotation = Quaternion.identity * Quaternion.AngleAxis(lerpingRotationAmount, Vector3.up);
            }
        }

        if (cursorObject != null)
        {
            if (worldStateManager.CanPlace(currentSelection, actualCursorPos))
            {
                currentCursorMaterial.color = validCursor;
            }
            else
            {
                currentCursorMaterial.color = invalidCursor;
            }
        }

        if(hideTimer > 0f)
        {
            hideTimer -= Time.deltaTime;
        }
        else if(!isHiding)
        {
            isHiding = true;
            buildHideCoroutine = StartCoroutine(FlipFade(buildModeImage, 0f));
            playHideCoroutine = StartCoroutine(FlipFade(playModeImage, 0f));
        }

        cursor.SetActive(isActivated);
        fluidCursor.SetActive(isActivated);
        grid.SetActive(isActivated);
    }

    public void DoInput()
    {
        Quaternion cameraQuat = Quaternion.LookRotation(Vector3.Scale(playerMovement.playerCamera.transform.forward, new Vector3(1f, 0f, 1f)).normalized, Vector3.up);

        Vector3 finalInput = (cameraQuat * inputRecieved);
        finalInput = new Vector3(MathHelper.Sign(finalInput.x), MathHelper.Sign(finalInput.y), MathHelper.Sign(finalInput.z));

        Vector3 finalPos = actualCursorPos + finalInput;

        if (worldStateManager.IsInsideWorldChunk(finalPos) != null)
        {
            actualCursorPos = finalPos;
        }
    }

    public void ChangeSelectionFromButton(int _value)
    {
        ChangeSelection(_value);
    }

    public void ChangeSelection(int _value)
    {
        currentSelection = _value;

        Color highlightedColour = Color.white;
        ColorUtility.TryParseHtmlString("#F5F5F5FF", out highlightedColour);

        foreach (ItemHide itemHide in FindObjectsOfType<ItemHide>())
        {
            Button button = itemHide.GetComponent<Button>();
            ColorBlock colorBlock = button.colors;

            if (itemHide.uniqueID != _value)
            {
                colorBlock.normalColor = Color.white;
                colorBlock.highlightedColor = highlightedColour;
            }
            else
            {
                colorBlock.normalColor = Color.cyan;
                colorBlock.highlightedColor = Color.cyan;
            }

            button.colors = colorBlock;
        }

        //Setup cursorObject
        SetupCursorObject();
    }

    public void StartBuildMode()
    {
        if(!isActivated)
        {
            isActivated = true;
            isAnimating = true;

            //Turn off Player
            playerMovement.lockMovements = true;
            playerMovement.isThinking = true;

            //Set Cursor as Target
            IsoCam isoCam = playerMovement.playerCamera.GetComponent<IsoCam>();
            OrbitCam orbitCam = playerMovement.playerCamera.GetComponent<OrbitCam>();

            if (isoCam != null)
            {
                isoCam.target = cameraTarget.transform;
            }
            else if(orbitCam != null)
            {
                orbitCam.target = cameraTarget.transform;
            }


            Vector3 playerPos = player.transform.position + Vector3.up;
            WorldChunk localChunk = worldStateManager.IsInsideWorldChunk(playerPos);
            if (localChunk != null)
            {
                Vector3 newPos = SnapToGrid(playerPos, localChunk);
                newPos.y = Mathf.Clamp(newPos.y, 0.5f, 10.5f);

                cursor.transform.position = newPos;
                actualCursorPos = cursor.transform.position;

                fluidCursor.transform.position = newPos;
                cameraTarget.transform.position = newPos;
            }

            StartCoroutine(DoSwapAnimation());

            itemsPanel.SetActive(true);

            foreach(ItemHide itemHide in FindObjectsOfType<ItemHide>())
            {
                itemHide.UpdateAvailablilty();
            }

            ChangeSelection(currentSelection);

            foreach(PhysicsPrefab prefab in FindObjectsOfType<PhysicsPrefab>())
            {
                prefab.Reset();
                prefab.GetComponent<Rigidbody>().isKinematic = true;
            }

            SetupCursorObject();
        }
    }

    public Vector3 SnapToGrid(Vector3 _position, WorldChunk _chunk)
    {
        Vector3 chunkPos = worldStateManager.WorldToChunk(_chunk, _position);
        return worldStateManager.ChunkToWorld(_chunk, chunkPos);
    }

    public void EndBuildMode()
    {
        if (isActivated)
        {
            isActivated = false;
            isAnimating = true;

            //Turn off Player
            playerMovement.lockMovements = false;
            playerMovement.isThinking = false;

            //Set Cursor as Target
            IsoCam isoCam = playerMovement.playerCamera.GetComponent<IsoCam>();
            OrbitCam orbitCam = playerMovement.playerCamera.GetComponent<OrbitCam>();

            if (isoCam != null)
            {
                isoCam.target = playerMovement.transform;
            }
            else if(orbitCam != null)
            {
                orbitCam.target = playerMovement.transform;
            }

            StartCoroutine(DoSwapAnimation());

            itemsPanel.SetActive(false);

            foreach (PhysicsPrefab prefab in FindObjectsOfType<PhysicsPrefab>())
            {
                prefab.GetComponent<Rigidbody>().isKinematic = false;
            }

            if (cursorObject != null)
            {
                Destroy(cursorObject);
            }
        }
    }

    private IEnumerator DoSwapAnimation()
    {
        if (isActivated)
        {
            StartCoroutine(FlipFade(buildModeImage, 1f));
            StartCoroutine(FlipFade(playModeImage, fadedValue));

            StartCoroutine(TravelRoundCircle(playModeImage, spinPoint, 420f, 270f));
            StartCoroutine(TravelRoundCircle(buildModeImage, spinPoint, 270f, 90f));
        }
        else
        {
            StartCoroutine(FlipFade(buildModeImage, fadedValue));
            StartCoroutine(FlipFade(playModeImage, 1f));

            StartCoroutine(TravelRoundCircle(buildModeImage, spinPoint, 420f, 270f));
            StartCoroutine(TravelRoundCircle(playModeImage, spinPoint, 270f, 90f));
        }

        yield return new WaitForSeconds(0.25f);

        if (isActivated)
        {
            buildModeImage.transform.SetAsLastSibling();
            playModeImage.transform.SetAsFirstSibling();
        }
        else
        {
            buildModeImage.transform.SetAsFirstSibling();
            playModeImage.transform.SetAsLastSibling();
        }

        yield return new WaitForSeconds(0.25f);

        isAnimating = false;
    }

    private IEnumerator FlipFade(Image _image, float _endVal)
    {
        float startTime = Time.time, startVal = _image.color.a;
        while(Time.time - startTime < travelTime)
        {
            _image.color = Color.Lerp(new Color(1f, 1f, 1f, startVal), new Color(1f, 1f, 1f, _endVal), (Time.time-startTime) / travelTime);
            yield return null;
        }

        _image.color = new Color(1f, 1f, 1f, _endVal);
    }

    private IEnumerator TravelRoundCircle(Image _image, Vector2 offset, float _startAngle, float _endAngle)
    {
        float travelTime = 0.5f, degreePerSecond = (_startAngle-_endAngle)/travelTime;

        float currentAngle = _startAngle;

        while (currentAngle > _endAngle)
        {
            Vector2 imagePos = new Vector2(radius * Mathf.Cos(currentAngle * Mathf.Deg2Rad), radius * Mathf.Sin(currentAngle * Mathf.Deg2Rad)) + offset;
            _image.rectTransform.anchoredPosition = imagePos;
            currentAngle -= degreePerSecond * Time.deltaTime;
            yield return null;
        }

        Vector2 finalImagePos = new Vector2(radius * Mathf.Cos(_endAngle * Mathf.Deg2Rad), radius * Mathf.Sin(_endAngle * Mathf.Deg2Rad)) + offset;
        _image.rectTransform.anchoredPosition = finalImagePos;
    }

    private void SetupCursorObject()
    {
        if (cursorObject != null)
        {
            Destroy(cursorObject);
        }

        BuildingPart buildingPart = FindObjectOfType<BuildingPartDatabaseManager>().GetBuildingPart(currentSelection);

        cursorObject = Instantiate(buildingPart.prefab);
        cursorObject.name = "lalala";
        cursorObject.transform.parent = cursor.transform;
        cursorObject.transform.localPosition = new Vector3(0f, buildingPart.placementYOffset, 0f);
        cursorObject.transform.localRotation = Quaternion.identity;

        Joint[] joints = cursorObject.GetComponentsInChildren<Joint>();
        WaterBody[] waterBodys = cursorObject.GetComponentsInChildren<WaterBody>();
        PhysicsPrefab[] physicsPrefabs = cursorObject.GetComponentsInChildren<PhysicsPrefab>();
        Rigidbody[] cursorBodys = cursorObject.GetComponentsInChildren<Rigidbody>();
        Collider[] cursorColliders = cursorObject.GetComponentsInChildren<Collider>();
        ItemPlacement[] itemPlacements = cursorObject.GetComponentsInChildren<ItemPlacement>();

        foreach (Joint joint in joints)
            Destroy(joint);

        foreach (PhysicsPrefab physics in physicsPrefabs)
            Destroy(physics);

        foreach (WaterBody water in waterBodys)
            Destroy(water);

        foreach (Rigidbody rbody in cursorBodys)
            Destroy(rbody);

        foreach (Collider collider in cursorColliders)
            Destroy(collider);

        foreach (ItemPlacement itemPlacement in itemPlacements)
        {
            if(itemPlacement.actualObject != null)
            {
                Destroy(itemPlacement.localObject);
            }
            Destroy(itemPlacement);
        }

        foreach (MeshRenderer meshRenderer in cursorObject.GetComponentsInChildren<MeshRenderer>())
        {
            currentCursorMaterial = new Material(Resources.Load<Material>("Materials/GhostMaterial"));
            meshRenderer.material = currentCursorMaterial;
            meshRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
            meshRenderer.receiveShadows = false;
        }
    }
}
