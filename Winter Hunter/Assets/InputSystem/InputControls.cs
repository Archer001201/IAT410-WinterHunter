//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.6.3
//     from Assets/InputSystem/InputControls.inputactions
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

public partial class @InputControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""24412f92-3d2a-4ca1-82d0-94e07639c74f"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""14f54739-3085-40c5-972d-a9141533dd13"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""a6e42a92-fde3-413a-917a-eef13a062ee1"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ThrowSnowball"",
                    ""type"": ""Button"",
                    ""id"": ""79cd5955-fac8-4a32-9e79-663332fe3e5b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RollSnowball"",
                    ""type"": ""Button"",
                    ""id"": ""1ebdcf70-bdfd-47f7-a6a5-49066a8281ac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwitchSnowmanLeft"",
                    ""type"": ""Button"",
                    ""id"": ""a49def1a-7fbc-460a-9ad5-c335438362b0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SummonSnowman"",
                    ""type"": ""Button"",
                    ""id"": ""44c7d15b-bd2c-4dc7-be5a-801a84436f2b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SwitchSnowmanRight"",
                    ""type"": ""Button"",
                    ""id"": ""16be959f-c4de-48be-9358-5253fd553882"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""91ee7add-a519-407c-bbfa-773cff030b0d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Rush"",
                    ""type"": ""Button"",
                    ""id"": ""13644ed1-b528-4877-a0ab-6ae285f2fd1f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Skip"",
                    ""type"": ""Button"",
                    ""id"": ""85182c09-d64d-4d3b-b37f-14918ca9ece1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ThrowSnowballPress"",
                    ""type"": ""Button"",
                    ""id"": ""03ddd4e5-8337-4738-aaa3-91d406787be6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Map"",
                    ""type"": ""Button"",
                    ""id"": ""fcb58ac5-03c1-4e89-9ecc-1190811968fd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""fbfa09b1-6878-4bdc-8968-93a58dca1b8b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""9c177506-4fc3-409f-af95-28034576f0ff"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""b287afee-9726-4590-b7b4-38a25afa1b74"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""235514e2-7716-463d-8bde-502dafe7b42d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""fbd7cc7d-865d-4592-bec8-aaa37ef4ab50"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""5999ef11-da0c-4ceb-8a46-730b44e2c187"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7ff13d09-4099-4e6b-a655-53b7f5058d44"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrowSnowball"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6af0e74c-cd85-495e-8193-d73f369f9f46"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RollSnowball"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""69170510-53a2-48d1-bf6a-d6f6aae14c4e"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchSnowmanLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6ff523f1-46d4-459f-8dad-1d7d52078661"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SummonSnowman"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""381e3132-c0cc-4fed-ad89-4656fa8a2707"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SwitchSnowmanRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5fdb3310-b685-4bcc-8caf-aee923d692b1"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2697c6a1-9e72-4b07-bacc-f1b19d97e9b4"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rush"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8fb20ab5-066a-43ac-bc54-19d25d3bca90"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Skip"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b9fe8db2-ebb5-4318-a5a6-b2d76e0c9696"",
                    ""path"": ""<Mouse>/press"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ThrowSnowballPress"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6c41f27e-5e67-4c7c-b35b-b0de145399d8"",
                    ""path"": ""<Keyboard>/m"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Map"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""3cb348d6-d862-48a4-8bbe-4e4ae8be97c8"",
            ""actions"": [
                {
                    ""name"": ""Up"",
                    ""type"": ""Button"",
                    ""id"": ""1a4063b6-c58c-442a-9ffc-85a1b793097a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Down"",
                    ""type"": ""Button"",
                    ""id"": ""742bd21b-dca8-492b-b090-c5607cc94bb5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""79b105bc-cd6e-499f-8875-194f0044e540"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e91c57e-c30e-4a64-9fa1-861268f265d6"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Global"",
            ""id"": ""bcbd5083-6fe2-453e-b5c0-bf0b3d1eec38"",
            ""actions"": [
                {
                    ""name"": ""OptionButton"",
                    ""type"": ""Button"",
                    ""id"": ""62595445-2875-4e25-a40a-ff77e9950c63"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""InventoryButton"",
                    ""type"": ""Button"",
                    ""id"": ""73f71972-9765-4f7f-ac5a-c95eb73ba1df"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""154bdd61-df4d-4d2a-afcd-6f5da545b3e9"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OptionButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fb5b91bf-7af7-4938-a334-4ced05377cfb"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""InventoryButton"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Move = m_Gameplay.FindAction("Move", throwIfNotFound: true);
        m_Gameplay_MousePosition = m_Gameplay.FindAction("MousePosition", throwIfNotFound: true);
        m_Gameplay_ThrowSnowball = m_Gameplay.FindAction("ThrowSnowball", throwIfNotFound: true);
        m_Gameplay_RollSnowball = m_Gameplay.FindAction("RollSnowball", throwIfNotFound: true);
        m_Gameplay_SwitchSnowmanLeft = m_Gameplay.FindAction("SwitchSnowmanLeft", throwIfNotFound: true);
        m_Gameplay_SummonSnowman = m_Gameplay.FindAction("SummonSnowman", throwIfNotFound: true);
        m_Gameplay_SwitchSnowmanRight = m_Gameplay.FindAction("SwitchSnowmanRight", throwIfNotFound: true);
        m_Gameplay_Interact = m_Gameplay.FindAction("Interact", throwIfNotFound: true);
        m_Gameplay_Rush = m_Gameplay.FindAction("Rush", throwIfNotFound: true);
        m_Gameplay_Skip = m_Gameplay.FindAction("Skip", throwIfNotFound: true);
        m_Gameplay_ThrowSnowballPress = m_Gameplay.FindAction("ThrowSnowballPress", throwIfNotFound: true);
        m_Gameplay_Map = m_Gameplay.FindAction("Map", throwIfNotFound: true);
        // UI
        m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
        m_UI_Up = m_UI.FindAction("Up", throwIfNotFound: true);
        m_UI_Down = m_UI.FindAction("Down", throwIfNotFound: true);
        // Global
        m_Global = asset.FindActionMap("Global", throwIfNotFound: true);
        m_Global_OptionButton = m_Global.FindAction("OptionButton", throwIfNotFound: true);
        m_Global_InventoryButton = m_Global.FindAction("InventoryButton", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private List<IGameplayActions> m_GameplayActionsCallbackInterfaces = new List<IGameplayActions>();
    private readonly InputAction m_Gameplay_Move;
    private readonly InputAction m_Gameplay_MousePosition;
    private readonly InputAction m_Gameplay_ThrowSnowball;
    private readonly InputAction m_Gameplay_RollSnowball;
    private readonly InputAction m_Gameplay_SwitchSnowmanLeft;
    private readonly InputAction m_Gameplay_SummonSnowman;
    private readonly InputAction m_Gameplay_SwitchSnowmanRight;
    private readonly InputAction m_Gameplay_Interact;
    private readonly InputAction m_Gameplay_Rush;
    private readonly InputAction m_Gameplay_Skip;
    private readonly InputAction m_Gameplay_ThrowSnowballPress;
    private readonly InputAction m_Gameplay_Map;
    public struct GameplayActions
    {
        private @InputControls m_Wrapper;
        public GameplayActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Gameplay_Move;
        public InputAction @MousePosition => m_Wrapper.m_Gameplay_MousePosition;
        public InputAction @ThrowSnowball => m_Wrapper.m_Gameplay_ThrowSnowball;
        public InputAction @RollSnowball => m_Wrapper.m_Gameplay_RollSnowball;
        public InputAction @SwitchSnowmanLeft => m_Wrapper.m_Gameplay_SwitchSnowmanLeft;
        public InputAction @SummonSnowman => m_Wrapper.m_Gameplay_SummonSnowman;
        public InputAction @SwitchSnowmanRight => m_Wrapper.m_Gameplay_SwitchSnowmanRight;
        public InputAction @Interact => m_Wrapper.m_Gameplay_Interact;
        public InputAction @Rush => m_Wrapper.m_Gameplay_Rush;
        public InputAction @Skip => m_Wrapper.m_Gameplay_Skip;
        public InputAction @ThrowSnowballPress => m_Wrapper.m_Gameplay_ThrowSnowballPress;
        public InputAction @Map => m_Wrapper.m_Gameplay_Map;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void AddCallbacks(IGameplayActions instance)
        {
            if (instance == null || m_Wrapper.m_GameplayActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameplayActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @MousePosition.started += instance.OnMousePosition;
            @MousePosition.performed += instance.OnMousePosition;
            @MousePosition.canceled += instance.OnMousePosition;
            @ThrowSnowball.started += instance.OnThrowSnowball;
            @ThrowSnowball.performed += instance.OnThrowSnowball;
            @ThrowSnowball.canceled += instance.OnThrowSnowball;
            @RollSnowball.started += instance.OnRollSnowball;
            @RollSnowball.performed += instance.OnRollSnowball;
            @RollSnowball.canceled += instance.OnRollSnowball;
            @SwitchSnowmanLeft.started += instance.OnSwitchSnowmanLeft;
            @SwitchSnowmanLeft.performed += instance.OnSwitchSnowmanLeft;
            @SwitchSnowmanLeft.canceled += instance.OnSwitchSnowmanLeft;
            @SummonSnowman.started += instance.OnSummonSnowman;
            @SummonSnowman.performed += instance.OnSummonSnowman;
            @SummonSnowman.canceled += instance.OnSummonSnowman;
            @SwitchSnowmanRight.started += instance.OnSwitchSnowmanRight;
            @SwitchSnowmanRight.performed += instance.OnSwitchSnowmanRight;
            @SwitchSnowmanRight.canceled += instance.OnSwitchSnowmanRight;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
            @Rush.started += instance.OnRush;
            @Rush.performed += instance.OnRush;
            @Rush.canceled += instance.OnRush;
            @Skip.started += instance.OnSkip;
            @Skip.performed += instance.OnSkip;
            @Skip.canceled += instance.OnSkip;
            @ThrowSnowballPress.started += instance.OnThrowSnowballPress;
            @ThrowSnowballPress.performed += instance.OnThrowSnowballPress;
            @ThrowSnowballPress.canceled += instance.OnThrowSnowballPress;
            @Map.started += instance.OnMap;
            @Map.performed += instance.OnMap;
            @Map.canceled += instance.OnMap;
        }

        private void UnregisterCallbacks(IGameplayActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @MousePosition.started -= instance.OnMousePosition;
            @MousePosition.performed -= instance.OnMousePosition;
            @MousePosition.canceled -= instance.OnMousePosition;
            @ThrowSnowball.started -= instance.OnThrowSnowball;
            @ThrowSnowball.performed -= instance.OnThrowSnowball;
            @ThrowSnowball.canceled -= instance.OnThrowSnowball;
            @RollSnowball.started -= instance.OnRollSnowball;
            @RollSnowball.performed -= instance.OnRollSnowball;
            @RollSnowball.canceled -= instance.OnRollSnowball;
            @SwitchSnowmanLeft.started -= instance.OnSwitchSnowmanLeft;
            @SwitchSnowmanLeft.performed -= instance.OnSwitchSnowmanLeft;
            @SwitchSnowmanLeft.canceled -= instance.OnSwitchSnowmanLeft;
            @SummonSnowman.started -= instance.OnSummonSnowman;
            @SummonSnowman.performed -= instance.OnSummonSnowman;
            @SummonSnowman.canceled -= instance.OnSummonSnowman;
            @SwitchSnowmanRight.started -= instance.OnSwitchSnowmanRight;
            @SwitchSnowmanRight.performed -= instance.OnSwitchSnowmanRight;
            @SwitchSnowmanRight.canceled -= instance.OnSwitchSnowmanRight;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
            @Rush.started -= instance.OnRush;
            @Rush.performed -= instance.OnRush;
            @Rush.canceled -= instance.OnRush;
            @Skip.started -= instance.OnSkip;
            @Skip.performed -= instance.OnSkip;
            @Skip.canceled -= instance.OnSkip;
            @ThrowSnowballPress.started -= instance.OnThrowSnowballPress;
            @ThrowSnowballPress.performed -= instance.OnThrowSnowballPress;
            @ThrowSnowballPress.canceled -= instance.OnThrowSnowballPress;
            @Map.started -= instance.OnMap;
            @Map.performed -= instance.OnMap;
            @Map.canceled -= instance.OnMap;
        }

        public void RemoveCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameplayActions instance)
        {
            foreach (var item in m_Wrapper.m_GameplayActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameplayActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);

    // UI
    private readonly InputActionMap m_UI;
    private List<IUIActions> m_UIActionsCallbackInterfaces = new List<IUIActions>();
    private readonly InputAction m_UI_Up;
    private readonly InputAction m_UI_Down;
    public struct UIActions
    {
        private @InputControls m_Wrapper;
        public UIActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Up => m_Wrapper.m_UI_Up;
        public InputAction @Down => m_Wrapper.m_UI_Down;
        public InputActionMap Get() { return m_Wrapper.m_UI; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
        public void AddCallbacks(IUIActions instance)
        {
            if (instance == null || m_Wrapper.m_UIActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_UIActionsCallbackInterfaces.Add(instance);
            @Up.started += instance.OnUp;
            @Up.performed += instance.OnUp;
            @Up.canceled += instance.OnUp;
            @Down.started += instance.OnDown;
            @Down.performed += instance.OnDown;
            @Down.canceled += instance.OnDown;
        }

        private void UnregisterCallbacks(IUIActions instance)
        {
            @Up.started -= instance.OnUp;
            @Up.performed -= instance.OnUp;
            @Up.canceled -= instance.OnUp;
            @Down.started -= instance.OnDown;
            @Down.performed -= instance.OnDown;
            @Down.canceled -= instance.OnDown;
        }

        public void RemoveCallbacks(IUIActions instance)
        {
            if (m_Wrapper.m_UIActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IUIActions instance)
        {
            foreach (var item in m_Wrapper.m_UIActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_UIActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public UIActions @UI => new UIActions(this);

    // Global
    private readonly InputActionMap m_Global;
    private List<IGlobalActions> m_GlobalActionsCallbackInterfaces = new List<IGlobalActions>();
    private readonly InputAction m_Global_OptionButton;
    private readonly InputAction m_Global_InventoryButton;
    public struct GlobalActions
    {
        private @InputControls m_Wrapper;
        public GlobalActions(@InputControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @OptionButton => m_Wrapper.m_Global_OptionButton;
        public InputAction @InventoryButton => m_Wrapper.m_Global_InventoryButton;
        public InputActionMap Get() { return m_Wrapper.m_Global; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GlobalActions set) { return set.Get(); }
        public void AddCallbacks(IGlobalActions instance)
        {
            if (instance == null || m_Wrapper.m_GlobalActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GlobalActionsCallbackInterfaces.Add(instance);
            @OptionButton.started += instance.OnOptionButton;
            @OptionButton.performed += instance.OnOptionButton;
            @OptionButton.canceled += instance.OnOptionButton;
            @InventoryButton.started += instance.OnInventoryButton;
            @InventoryButton.performed += instance.OnInventoryButton;
            @InventoryButton.canceled += instance.OnInventoryButton;
        }

        private void UnregisterCallbacks(IGlobalActions instance)
        {
            @OptionButton.started -= instance.OnOptionButton;
            @OptionButton.performed -= instance.OnOptionButton;
            @OptionButton.canceled -= instance.OnOptionButton;
            @InventoryButton.started -= instance.OnInventoryButton;
            @InventoryButton.performed -= instance.OnInventoryButton;
            @InventoryButton.canceled -= instance.OnInventoryButton;
        }

        public void RemoveCallbacks(IGlobalActions instance)
        {
            if (m_Wrapper.m_GlobalActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGlobalActions instance)
        {
            foreach (var item in m_Wrapper.m_GlobalActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GlobalActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GlobalActions @Global => new GlobalActions(this);
    public interface IGameplayActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnThrowSnowball(InputAction.CallbackContext context);
        void OnRollSnowball(InputAction.CallbackContext context);
        void OnSwitchSnowmanLeft(InputAction.CallbackContext context);
        void OnSummonSnowman(InputAction.CallbackContext context);
        void OnSwitchSnowmanRight(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnRush(InputAction.CallbackContext context);
        void OnSkip(InputAction.CallbackContext context);
        void OnThrowSnowballPress(InputAction.CallbackContext context);
        void OnMap(InputAction.CallbackContext context);
    }
    public interface IUIActions
    {
        void OnUp(InputAction.CallbackContext context);
        void OnDown(InputAction.CallbackContext context);
    }
    public interface IGlobalActions
    {
        void OnOptionButton(InputAction.CallbackContext context);
        void OnInventoryButton(InputAction.CallbackContext context);
    }
}
