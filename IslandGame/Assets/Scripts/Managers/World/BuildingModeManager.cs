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
    }

    public void StartBuildMode()
    {
        if(!isActivated)
        {
            isActivated = true;
            isAnimating = true;

            //Turn off Player
            FindObjectOfType<PlayerMovement>().lockMovements = true;

            StartCoroutine(DoSwapAnimation());
        }
    }

    public void EndBuildMode()
    {
        if (isActivated)
        {
            isActivated = false;
            isAnimating = true;

            //Turn off Player
            FindObjectOfType<PlayerMovement>().lockMovements = false;

            StartCoroutine(DoSwapAnimation());
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
