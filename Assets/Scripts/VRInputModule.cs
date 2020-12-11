using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;

public class VRInputModule : BaseInputModule
{
    public Pointer pointer = null;

    public PointerEventData Data { get; private set; } = null;

    public SteamVR_Input_Sources m_Source = SteamVR_Input_Sources.RightHand;
    public SteamVR_Action_Boolean m_Click;
    //public string m_Click = "InteractUI";
    
    protected override void Start()
    {
        //UnityEngine.Debug.Log("Start Ran");
        //this.inputOverride = this;
        
        //this.GetComponent<EventSystem>().
    }

    public void initializePointer()
    {
        Data = new PointerEventData(eventSystem);
        Data.position = new Vector2(pointer.Camera.pixelWidth / 2, pointer.Camera.pixelHeight / 2);
    }

    public override void Process()
    {
        if(pointer != null)
        {
            //UnityEngine.Debug.Log("Processing");
            eventSystem.RaycastAll(Data, m_RaycastResultCache);
            Data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);

            HandlePointerExitAndEnter(Data, Data.pointerCurrentRaycast.gameObject);

            ExecuteEvents.Execute(Data.pointerDrag, Data, ExecuteEvents.dragHandler);

            // Press
            if (m_Click.GetStateDown(m_Source))
                Press();
            // Release
            if (m_Click.GetStateUp(m_Source))
                Release();
        }
    }

    public void Press()
    {
        UnityEngine.Debug.Log("Down Press");
        Data.pointerPressRaycast = Data.pointerCurrentRaycast;

        Data.pointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(Data.pointerPressRaycast.gameObject);
        Data.pointerDrag = ExecuteEvents.GetEventHandler<IDragHandler>(Data.pointerPressRaycast.gameObject);
        
        ExecuteEvents.Execute(Data.pointerPress, Data, ExecuteEvents.pointerDownHandler);
        ExecuteEvents.Execute(Data.pointerDrag, Data, ExecuteEvents.beginDragHandler);
    }

    public void Release()
    {
        UnityEngine.Debug.Log("Release");
        GameObject pointerRelease = ExecuteEvents.GetEventHandler<IPointerClickHandler>(Data.pointerCurrentRaycast.gameObject);

        if (Data.pointerPress == pointerRelease)
            ExecuteEvents.Execute(Data.pointerPress, Data, ExecuteEvents.pointerClickHandler);

        ExecuteEvents.Execute(Data.pointerPress, Data, ExecuteEvents.pointerUpHandler);
        ExecuteEvents.Execute(Data.pointerDrag, Data, ExecuteEvents.endDragHandler);

        Data.pointerPress = null;
        Data.pointerDrag = null;

        Data.pointerCurrentRaycast.Clear();
    }
}