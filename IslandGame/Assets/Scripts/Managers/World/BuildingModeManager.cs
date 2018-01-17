using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuildingModeManager : MonoBehaviour
{
    public bool isActivated = false;
    private bool isAnimating = false, isHiding = false;

    public Image buildModeImage, playModeImage;
    private const float fadedValue = 0.2f, travelTime = 0.5f, radius = 20f;
    private readonly Vector2 spinPoint = new Vector2(775f, -475f);

    private float hideTimer;
    Coroutine buildHideCoroutine, playHideCoroutine;

    public GameObject cursor, player, itemsPanel;
    private PlayerMovement playerMovement;
    private WorldStateManager worldStateManager;

    private Vector3 actualCursorPos;
    private float rotationAmount = 0f, lerpingRotationAmount = 0f;

    public float lerpAmount = 10f;

    private Vector3 inputRecieved;

    private const float maxInputLockTimer = 0.03f;
    private float inputLockTimer = maxInputLockTimer;

    private int currentSelection = 1;

    private void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        worldStateManager = FindObjectOfType<WorldStateManager>();

        itemsPanel.SetActive(false);
    }

    private void Update()
    {
        //User Inputs
        if (InputManager.controllers.Count > 0 && !PauseMenu.isPaused)
        {
            InputDevice inputDevice = InputManager.controllers[0];

            if(!isAnimating && inputDevice.GetButtonWithLock("BuildMode"))
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

            if(isActivated)
            {             
                //Get Inputs
                float hori = inputDevice.GetInput("MovementHorizontal");
                float verti = inputDevice.GetInput("MovementVertical");
                float height = inputDevice.GetInput("MovementHeight");

                bool build = inputDevice.GetButtonWithLock("Create");
                bool delete = inputDevice.GetButtonWithLock("Delete");

                int rotate = inputDevice.GetIntInputWithLock("Rotate");

                //Debug Draw
                Quaternion cameraQuat = Quaternion.LookRotation(Vector3.Scale(playerMovement.playerCamera.transform.forward, new Vector3(1f, 0f, 1f)).normalized, Vector3.up);
                Vector3 input = new Vector3(MathHelper.Sign(hori), MathHelper.Sign(-height), MathHelper.Sign(-verti)).normalized;
                float squareSize = (((hori != 0 && verti == 0) || (verti != 0 && hori == 0)) ? 1.5f : 1f);
                Vector3 finalInput = cameraQuat * input * squareSize;
                Vector3 finalPos = actualCursorPos + finalInput;

                Debug.DrawLine(actualCursorPos, finalPos, Color.red);

                if (hori != 0f || verti != 0f || height != 0f)
                {
                    if (inputLockTimer > 0f)
                    {
                        inputRecieved.x = (hori != 0f)      ? Mathf.Sign(hori)      : inputRecieved.x;
                        inputRecieved.y = (height != 0f)    ? Mathf.Sign(-height)    : inputRecieved.y;
                        inputRecieved.z = (verti != 0f)     ? Mathf.Sign(-verti)     : inputRecieved.z;
                    }
                }
                else
                {
                    inputLockTimer = maxInputLockTimer;
                }

                if(inputRecieved.sqrMagnitude != 0)
                {
                    if (inputLockTimer < 0)
                    {
                        DoInput();
                        inputRecieved = Vector3.zero;
                    }
                    else
                    {
                        inputLockTimer -= Time.deltaTime;
                    }        
                }

                if(rotate != 0)
                {
                    rotationAmount -= rotate * 90f;
                }

                if(build)
                {
                    worldStateManager.CreateItem(currentSelection, actualCursorPos, rotationAmount);
                }

                if (delete)
                {
                    worldStateManager.DeleteItem(actualCursorPos);
                }

                cursor.transform.position = Vector3.Lerp(cursor.transform.position, actualCursorPos, Time.deltaTime * lerpAmount);

                lerpingRotationAmount = Mathf.Lerp(lerpingRotationAmount, rotationAmount, Time.deltaTime * lerpAmount);
                cursor.transform.rotation = Quaternion.identity * Quaternion.AngleAxis(lerpingRotationAmount, Vector3.up);

                //Item Swapping
                if(inputDevice.inputType == InputType.Keyboard)
                {
                    for(int i = 1; i < 9; i++)
                    {
                        if(inputDevice.GetButtonWithLock("Item" + i))
                        {
                            ChangeSelection(i);
                        }
                    }
                }
                else
                {
                    int itemSwitch = inputDevice.GetIntInputWithLock("ItemSwitch");

                    if(itemSwitch != 0)
                    {
                        int max = FindObjectOfType<BuildingPartDatabaseManager>().GetBuildingPartCount() + 1;
                        ChangeSelection(MathHelper.NumClamp(currentSelection + itemSwitch, 1, Mathf.Min(max, 9)));
                    }
                }
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
    }

    public void StartBuildMode()
    {
        if(!isActivated)
        {
            isActivated = true;
            isAnimating = true;

            //Turn off Player
            playerMovement.lockMovements = true;

            //Set Cursor as Target
            IsoCam isoCam = playerMovement.playerCamera.GetComponent<IsoCam>();
            OrbitCam orbitCam = playerMovement.playerCamera.GetComponent<OrbitCam>();

            if (isoCam != null)
            {
                isoCam.target = cursor.transform;
            }
            else if(orbitCam != null)
            {
                orbitCam.target = cursor.transform;
            }

            Vector3 playerPos = player.transform.position - Vector3.up;
            WorldChunk localChunk = worldStateManager.IsInsideWorldChunk(playerPos);
            if (localChunk != null)
            {
                Vector3 newPos = SnapToGrid(playerPos, localChunk);
                newPos.y = Mathf.Clamp(newPos.y, 0.5f, 10.5f);

                cursor.transform.position = newPos;
                actualCursorPos = cursor.transform.position;
            }

            StartCoroutine(DoSwapAnimation());

            itemsPanel.SetActive(true);

            foreach(ItemHide itemHide in FindObjectsOfType<ItemHide>())
            {
                itemHide.UpdateAvailablilty();
            }

            ChangeSelection(currentSelection);
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
}
