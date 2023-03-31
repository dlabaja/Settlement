//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/KeystrokesController/Keystrokes.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Keystrokes : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Keystrokes()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Keystrokes"",
    ""maps"": [
        {
            ""name"": ""Camera"",
            ""id"": ""3e1dedeb-1549-445a-9f2b-693cd220923a"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""9ad6004b-4d2d-44c0-abe4-f94db8f7898a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Drag"",
                    ""type"": ""Button"",
                    ""id"": ""dd50f12c-3a03-4c85-96d6-9797cac85bb1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Zoom"",
                    ""type"": ""Button"",
                    ""id"": ""09d75570-1e42-4991-bd65-7ac466704047"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""bac18a6c-ee88-4945-9eb3-80aab491fca4"",
                    ""path"": ""3DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""0d80182a-12c7-4bf0-a097-c0937d26d05e"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""26a8b24e-6fd2-4752-8d53-ac402dadb78a"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""ffdf839b-bd22-402a-8f58-9aa458d7d0e0"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""d608cfeb-a6c9-488f-8624-add3f22e8235"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Forward"",
                    ""id"": ""17538f1f-4220-4ec3-b88e-87105628a91d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Backward"",
                    ""id"": ""902b1f96-9654-40d3-9c86-96d5d2edac4f"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""21297607-3b0c-494d-b3d4-e55aaf28f549"",
                    ""path"": ""<Mouse>/middleButton"",
                    ""interactions"": ""Hold(duration=0.02)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5455d22-2140-49ec-814a-6ddcdf96da1d"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Zoom"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Mouse"",
            ""id"": ""98657829-e2ea-4c3a-9257-e7600dab0eab"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""2b6e0f9b-495c-49f8-b20a-ba5c7d542a21"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Hold"",
                    ""type"": ""Button"",
                    ""id"": ""44e283aa-2aa4-4811-96c9-63549256e625"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""3244df90-53e6-4931-bdaf-c306ca01a2e8"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6f9a2c08-a166-4544-b09d-5382801f46b0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Hold"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hold"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Keybinds"",
            ""id"": ""884df629-4697-49ed-9864-694a0aae2cd0"",
            ""actions"": [
                {
                    ""name"": ""CloseStats"",
                    ""type"": ""Value"",
                    ""id"": ""7785541c-1104-4c20-93fd-fb2b990c0b1c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Esc"",
                    ""type"": ""Button"",
                    ""id"": ""ae5d4831-09a1-414d-85c1-2058102be51b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c66b6a33-0fd8-4c8e-a812-627bda9831f2"",
                    ""path"": ""<Keyboard>/delete"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CloseStats"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""082e779d-f8c6-4512-bf97-eb751a75f41d"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Esc"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_Movement = m_Camera.FindAction("Movement", throwIfNotFound: true);
        m_Camera_Drag = m_Camera.FindAction("Drag", throwIfNotFound: true);
        m_Camera_Zoom = m_Camera.FindAction("Zoom", throwIfNotFound: true);
        // Mouse
        m_Mouse = asset.FindActionMap("Mouse", throwIfNotFound: true);
        m_Mouse_Click = m_Mouse.FindAction("Click", throwIfNotFound: true);
        m_Mouse_Hold = m_Mouse.FindAction("Hold", throwIfNotFound: true);
        // Keybinds
        m_Keybinds = asset.FindActionMap("Keybinds", throwIfNotFound: true);
        m_Keybinds_CloseStats = m_Keybinds.FindAction("CloseStats", throwIfNotFound: true);
        m_Keybinds_Esc = m_Keybinds.FindAction("Esc", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_Movement;
    private readonly InputAction m_Camera_Drag;
    private readonly InputAction m_Camera_Zoom;
    public struct CameraActions
    {
        private @Keystrokes m_Wrapper;
        public CameraActions(@Keystrokes wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Camera_Movement;
        public InputAction @Drag => m_Wrapper.m_Camera_Drag;
        public InputAction @Zoom => m_Wrapper.m_Camera_Zoom;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnMovement;
                @Drag.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnDrag;
                @Drag.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnDrag;
                @Drag.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnDrag;
                @Zoom.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
                @Zoom.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
                @Zoom.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnZoom;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Drag.started += instance.OnDrag;
                @Drag.performed += instance.OnDrag;
                @Drag.canceled += instance.OnDrag;
                @Zoom.started += instance.OnZoom;
                @Zoom.performed += instance.OnZoom;
                @Zoom.canceled += instance.OnZoom;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);

    // Mouse
    private readonly InputActionMap m_Mouse;
    private IMouseActions m_MouseActionsCallbackInterface;
    private readonly InputAction m_Mouse_Click;
    private readonly InputAction m_Mouse_Hold;
    public struct MouseActions
    {
        private @Keystrokes m_Wrapper;
        public MouseActions(@Keystrokes wrapper) { m_Wrapper = wrapper; }
        public InputAction @Click => m_Wrapper.m_Mouse_Click;
        public InputAction @Hold => m_Wrapper.m_Mouse_Hold;
        public InputActionMap Get() { return m_Wrapper.m_Mouse; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MouseActions set) { return set.Get(); }
        public void SetCallbacks(IMouseActions instance)
        {
            if (m_Wrapper.m_MouseActionsCallbackInterface != null)
            {
                @Click.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnClick;
                @Click.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnClick;
                @Click.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnClick;
                @Hold.started -= m_Wrapper.m_MouseActionsCallbackInterface.OnHold;
                @Hold.performed -= m_Wrapper.m_MouseActionsCallbackInterface.OnHold;
                @Hold.canceled -= m_Wrapper.m_MouseActionsCallbackInterface.OnHold;
            }
            m_Wrapper.m_MouseActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Click.started += instance.OnClick;
                @Click.performed += instance.OnClick;
                @Click.canceled += instance.OnClick;
                @Hold.started += instance.OnHold;
                @Hold.performed += instance.OnHold;
                @Hold.canceled += instance.OnHold;
            }
        }
    }
    public MouseActions @Mouse => new MouseActions(this);

    // Keybinds
    private readonly InputActionMap m_Keybinds;
    private IKeybindsActions m_KeybindsActionsCallbackInterface;
    private readonly InputAction m_Keybinds_CloseStats;
    private readonly InputAction m_Keybinds_Esc;
    public struct KeybindsActions
    {
        private @Keystrokes m_Wrapper;
        public KeybindsActions(@Keystrokes wrapper) { m_Wrapper = wrapper; }
        public InputAction @CloseStats => m_Wrapper.m_Keybinds_CloseStats;
        public InputAction @Esc => m_Wrapper.m_Keybinds_Esc;
        public InputActionMap Get() { return m_Wrapper.m_Keybinds; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(KeybindsActions set) { return set.Get(); }
        public void SetCallbacks(IKeybindsActions instance)
        {
            if (m_Wrapper.m_KeybindsActionsCallbackInterface != null)
            {
                @CloseStats.started -= m_Wrapper.m_KeybindsActionsCallbackInterface.OnCloseStats;
                @CloseStats.performed -= m_Wrapper.m_KeybindsActionsCallbackInterface.OnCloseStats;
                @CloseStats.canceled -= m_Wrapper.m_KeybindsActionsCallbackInterface.OnCloseStats;
                @Esc.started -= m_Wrapper.m_KeybindsActionsCallbackInterface.OnEsc;
                @Esc.performed -= m_Wrapper.m_KeybindsActionsCallbackInterface.OnEsc;
                @Esc.canceled -= m_Wrapper.m_KeybindsActionsCallbackInterface.OnEsc;
            }
            m_Wrapper.m_KeybindsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @CloseStats.started += instance.OnCloseStats;
                @CloseStats.performed += instance.OnCloseStats;
                @CloseStats.canceled += instance.OnCloseStats;
                @Esc.started += instance.OnEsc;
                @Esc.performed += instance.OnEsc;
                @Esc.canceled += instance.OnEsc;
            }
        }
    }
    public KeybindsActions @Keybinds => new KeybindsActions(this);
    public interface ICameraActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnDrag(InputAction.CallbackContext context);
        void OnZoom(InputAction.CallbackContext context);
    }
    public interface IMouseActions
    {
        void OnClick(InputAction.CallbackContext context);
        void OnHold(InputAction.CallbackContext context);
    }
    public interface IKeybindsActions
    {
        void OnCloseStats(InputAction.CallbackContext context);
        void OnEsc(InputAction.CallbackContext context);
    }
}
