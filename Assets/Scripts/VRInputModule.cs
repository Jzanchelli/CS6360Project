using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRInputModule : BaseInputModule
{
    [SerializeField] private Pointer pointer = null;

    public PointerEventData Data { get; private set; } = null;

    public SteamVR_Input_Sources m_Source = SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Boolean m_Click;

    protected override void Start()
    {
        Data = new PointerEventData(eventSystem);
        Data.position = new Vector2(pointer.Camera.pixelWidth / 2, pointer.Camera.pixelHeight / 2);
    }

    public override void Process()
    {
        eventSystem.RaycastAll(Data, m_RaycastResultCache);
        Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);

        HandlePointerExitAndEnter(Data, Data.pointerCurrentRaycast.gameObject);

        // Press
        if (m_Click[m_Source].stateDown)
            Press();
        // Release
        if (m_Click[m_Source].stateUp)
            Release();
    }

    public void Press()
    {
        Data.pointerPressRaycast = Data.pointerCurrentRaycast;

        Data.pointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(Data.pointerPressRaycast.gameObject);

        ExecuteEvents.Execute(Data.pointerPress, Data, ExecuteEvents.pointerDownHandler);
    }

    public void Release()
    {
        GameObject pointerRelease = ExecuteEvents.GetEventHandler<IPointerClickHandler>(Data.pointerCurrentRaycast.gameObject);

        if (Data.pointerPress == pointerRelease)
            ExecuteEvents.Execute(Data.pointerPress, Data, ExecuteEvents.pointerClickHandler);

        ExecuteEvents.Execute(Data.pointerPress, Data, ExecuteEvents.pointerUpHandler);

        Data.pointerPress = null;

        Data.pointerCurrentRaycast.Clear();
    }
}