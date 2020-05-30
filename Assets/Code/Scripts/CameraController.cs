using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour 
{
	public float horizontalRotationSpeed = 90.0f;
    public bool invertHorizontalAxis = false;
	public float verticalRotationSpeed = 1.0f;
    public bool invertVerticalAxis = true;
    [Range(0, 1)]public float verticalRotationLerper = 0.35f;
	public Vector2 verticalRotationLimits = new Vector2( -15, 90);
	public float zoomSpeed = 3.0f;
	public Vector2 zoomBoundaries = new Vector2(1, 5);
	[Range(0, 1)]public float zoomLerper = 0.5f;
	public float height = 3.0f;
	public float positionFilter = 0.1f;
	public float rotationFilter = 0.1f;
    public Transform targetTransform;
    
	private Quaternion _localRotationQuaternion;
	private Vector3 _lastMousePosition;
	private float _screenRatio;
	private Transform _cameraTransform;
    private float _initialVerticalRotationLerper;
    private float _initialZoomLerper;


    void Awake()
	{
		this.gameObject.transform.position = targetTransform.position + Vector3.up * height;
		this.gameObject.transform.rotation = targetTransform.rotation;
		_localRotationQuaternion = Quaternion.identity;
		_lastMousePosition = Input.mousePosition;
		_screenRatio = Screen.width / Screen.height;
		_cameraTransform = this.gameObject.transform.GetComponentInChildren<Camera>().gameObject.transform;
		_cameraTransform.localPosition = Vector3.zero;
        _initialVerticalRotationLerper = verticalRotationLerper;
        _initialZoomLerper = zoomLerper;
    }
	
	void LateUpdate()
	{
		this.gameObject.transform.position = Helpers.ChaseValue<Vector3>(this.gameObject.transform.position, targetTransform.position + Vector3.up * height, positionFilter, Time.unscaledDeltaTime);

		Vector2 inputValues = Vector2.zero;
		inputValues.x += JoysticksHelper.FilterAndEase(Input.GetAxis("RightStickHorizontal") * (invertHorizontalAxis ? -1 : 1));
		inputValues.y += JoysticksHelper.FilterAndEase(Input.GetAxis("RightStickVertical") * (invertVerticalAxis ? -1 : 1));
		if (Input.GetMouseButton(0))
		{
			Vector2 mouseDelta = (Vector2)(Input.mousePosition - _lastMousePosition);
			mouseDelta.y *= _screenRatio;
			mouseDelta.y *= -1.0f;
			inputValues += mouseDelta;
		}
		_lastMousePosition = Input.mousePosition;

		verticalRotationLerper += inputValues.y * verticalRotationSpeed * Time.unscaledDeltaTime;
		verticalRotationLerper = Mathf.Clamp01(verticalRotationLerper);

		Vector3 eulerRotation = _localRotationQuaternion.eulerAngles;
		eulerRotation.x = Mathf.Lerp(verticalRotationLimits.x, verticalRotationLimits.y, verticalRotationLerper);
		eulerRotation.y += inputValues.x * horizontalRotationSpeed * Time.unscaledDeltaTime;
		_localRotationQuaternion.eulerAngles = eulerRotation;

		zoomLerper += zoomSpeed * (JoysticksHelper.FilterAndEase(Input.GetAxis("LeftTrigger") - Input.GetAxis("RightTrigger")) - Input.mouseScrollDelta.y) * Time.unscaledDeltaTime;
		zoomLerper = Mathf.Clamp01(zoomLerper);
		float zoomValue = -Mathf.Lerp(zoomBoundaries.x, zoomBoundaries.y, zoomLerper);
		Vector3 newZoomPos = new Vector3(0, 0, zoomValue);

		if ( Input.GetButtonUp("RightStickClick") || Input.GetMouseButton(1))
		{
			ResetView();
		}

		this.gameObject.transform.rotation = Helpers.ChaseValue<Quaternion>(this.gameObject.transform.rotation, targetTransform.rotation * _localRotationQuaternion, rotationFilter, Time.unscaledDeltaTime);
		_cameraTransform.localPosition = newZoomPos;
	}

	public void ResetView()
	{
		verticalRotationLerper = _initialVerticalRotationLerper;
		Vector3 eulerRotation = _localRotationQuaternion.eulerAngles;
		eulerRotation.y = 0;
		_localRotationQuaternion.eulerAngles = eulerRotation;

		zoomLerper = _initialZoomLerper;
	}
}
