//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Input/PlayerControls.inputactions
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

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Base Movement"",
            ""id"": ""a3686852-4133-4114-b6d6-78eb963376d5"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""70087e58-94f6-4956-824a-7d0765603a1a"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""d5abfc75-fbf8-4345-b065-85bb244c2644"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Melee Attack"",
                    ""type"": ""Button"",
                    ""id"": ""7a767f58-0f1f-4827-bebd-566a6d958d71"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""7a733861-0721-4c00-9475-7679898a1e72"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold(duration=0.6)"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Select Spell"",
                    ""type"": ""Value"",
                    ""id"": ""2b329d93-a0ef-4c1e-9480-c01aeddfaaae"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Activate Shooting Mode"",
                    ""type"": ""Button"",
                    ""id"": ""5d83f3ee-5739-4ea3-9c79-4e5a8a35d5aa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Focus"",
                    ""type"": ""Value"",
                    ""id"": ""a49bb896-31f1-44f6-b4f6-6a7480cd0ea8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Dodge Roll"",
                    ""type"": ""Button"",
                    ""id"": ""9531699d-6943-42ff-98d2-ebe218057308"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Lock On"",
                    ""type"": ""Button"",
                    ""id"": ""93d998f5-74f0-4477-9c11-ca5eb4d886f7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Lock On Selection"",
                    ""type"": ""Value"",
                    ""id"": ""f74b8750-b8df-43cc-93df-91183603d19c"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""404c0572-17ca-4b2b-9c35-c3a7f07e0d73"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d3f1948d-93fc-4b29-9e05-2e0182566622"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8fbbb20-a34f-42eb-b2a3-5513beafd78e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Melee Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""69bd61d9-86a5-4c9c-b80f-6364a2683b63"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Melee Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d362143-83b8-45b6-8143-4f578287447d"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""88a2029b-501f-4bb2-a8fe-093f6aea113d"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9376cc18-6408-4898-bd3f-4bcb5dd34b31"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Activate Shooting Mode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""557e5a9c-7d76-4166-bd9c-9977eb9cd7a7"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": ""Hold(duration=1E+30)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Activate Shooting Mode"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""21be5c54-2f15-401a-9840-a86e062bb026"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select Spell"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""bbd932a1-03c5-4e93-85f3-613858061272"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select Spell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""79800099-6c29-4c91-a7eb-6ae5cfffa27d"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select Spell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""d10373f4-b6f2-4a23-9484-4916c8b36a28"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select Spell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""1853d738-35e8-4245-a4e3-74ce2e6de942"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Select Spell"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""94a0ae27-45bc-4341-9ad8-825209532821"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Hold(duration=1E+30)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Focus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""548491aa-94df-400e-96a1-3eb8e2f7eedd"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Hold(duration=1E+30)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Focus"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38e91a3e-f89c-4858-895e-570726b6e82d"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d6f041d6-2cf0-4868-ae32-c0ed0aec4e3f"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dodge Roll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2c03e7d6-d239-4d5a-9960-7593fe758ad5"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lock On"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""082fdb39-21e1-453e-8281-7c2f453ef8b7"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lock On"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""b3b1e803-0a0f-4610-a489-601e0cc3c424"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""20363102-c43e-4d1b-af46-dbf514172f99"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""ad2aaf2f-1a5f-4476-93a5-22b3f4bd594f"",
                    ""path"": ""<Gamepad>/leftStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""42ceef27-12bf-4556-bbc6-416536d8b7b6"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""9bce1d7d-15f9-47f0-8dc8-9cc448f0e913"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""594acab7-d036-4a52-b6da-8d47ccb77143"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""3fe875fd-8693-49cc-8c66-b7632ca6bfaa"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""7d888e0a-c681-41d2-8b91-c2772fa33913"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""2c16f116-4f48-4fb6-8369-09063d58523e"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""f4e3b1d5-b199-406a-9251-94112eb7dcfc"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""8c474d6d-ee44-4328-bf19-bd55f5f41bca"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lock On Selection"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""8909c5fb-70af-49bb-a7d2-d9aa71464579"",
                    ""path"": ""<Gamepad>/rightStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lock On Selection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""196407df-07e5-490f-a19f-f0008fc1b96b"",
                    ""path"": ""<Gamepad>/rightStick/down"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lock On Selection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""9f083c56-8194-45aa-ada4-830e97da06d3"",
                    ""path"": ""<Gamepad>/rightStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lock On Selection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""6f5a36b1-3e96-4c35-9103-fb05c8e26fff"",
                    ""path"": ""<Gamepad>/rightStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lock On Selection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""5c5c4c54-14bb-4871-bf53-f45cb846c3cf"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lock On Selection"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""fcd5c321-36fc-4673-9fd0-61581d9bdffb"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lock On Selection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""925735c6-3df7-4449-af34-6a05f5ff9baa"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lock On Selection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left"",
                    ""id"": ""96594dc8-86c4-42bf-aefa-04ad50e93132"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lock On Selection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Right"",
                    ""id"": ""0edc72ef-a595-496b-9338-047d7220f296"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Lock On Selection"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Base Movement
        m_BaseMovement = asset.FindActionMap("Base Movement", throwIfNotFound: true);
        m_BaseMovement_Move = m_BaseMovement.FindAction("Move", throwIfNotFound: true);
        m_BaseMovement_Interact = m_BaseMovement.FindAction("Interact", throwIfNotFound: true);
        m_BaseMovement_MeleeAttack = m_BaseMovement.FindAction("Melee Attack", throwIfNotFound: true);
        m_BaseMovement_Sprint = m_BaseMovement.FindAction("Sprint", throwIfNotFound: true);
        m_BaseMovement_SelectSpell = m_BaseMovement.FindAction("Select Spell", throwIfNotFound: true);
        m_BaseMovement_ActivateShootingMode = m_BaseMovement.FindAction("Activate Shooting Mode", throwIfNotFound: true);
        m_BaseMovement_Focus = m_BaseMovement.FindAction("Focus", throwIfNotFound: true);
        m_BaseMovement_DodgeRoll = m_BaseMovement.FindAction("Dodge Roll", throwIfNotFound: true);
        m_BaseMovement_LockOn = m_BaseMovement.FindAction("Lock On", throwIfNotFound: true);
        m_BaseMovement_LockOnSelection = m_BaseMovement.FindAction("Lock On Selection", throwIfNotFound: true);
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

    // Base Movement
    private readonly InputActionMap m_BaseMovement;
    private List<IBaseMovementActions> m_BaseMovementActionsCallbackInterfaces = new List<IBaseMovementActions>();
    private readonly InputAction m_BaseMovement_Move;
    private readonly InputAction m_BaseMovement_Interact;
    private readonly InputAction m_BaseMovement_MeleeAttack;
    private readonly InputAction m_BaseMovement_Sprint;
    private readonly InputAction m_BaseMovement_SelectSpell;
    private readonly InputAction m_BaseMovement_ActivateShootingMode;
    private readonly InputAction m_BaseMovement_Focus;
    private readonly InputAction m_BaseMovement_DodgeRoll;
    private readonly InputAction m_BaseMovement_LockOn;
    private readonly InputAction m_BaseMovement_LockOnSelection;
    public struct BaseMovementActions
    {
        private @PlayerControls m_Wrapper;
        public BaseMovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_BaseMovement_Move;
        public InputAction @Interact => m_Wrapper.m_BaseMovement_Interact;
        public InputAction @MeleeAttack => m_Wrapper.m_BaseMovement_MeleeAttack;
        public InputAction @Sprint => m_Wrapper.m_BaseMovement_Sprint;
        public InputAction @SelectSpell => m_Wrapper.m_BaseMovement_SelectSpell;
        public InputAction @ActivateShootingMode => m_Wrapper.m_BaseMovement_ActivateShootingMode;
        public InputAction @Focus => m_Wrapper.m_BaseMovement_Focus;
        public InputAction @DodgeRoll => m_Wrapper.m_BaseMovement_DodgeRoll;
        public InputAction @LockOn => m_Wrapper.m_BaseMovement_LockOn;
        public InputAction @LockOnSelection => m_Wrapper.m_BaseMovement_LockOnSelection;
        public InputActionMap Get() { return m_Wrapper.m_BaseMovement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(BaseMovementActions set) { return set.Get(); }
        public void AddCallbacks(IBaseMovementActions instance)
        {
            if (instance == null || m_Wrapper.m_BaseMovementActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_BaseMovementActionsCallbackInterfaces.Add(instance);
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Interact.started += instance.OnInteract;
            @Interact.performed += instance.OnInteract;
            @Interact.canceled += instance.OnInteract;
            @MeleeAttack.started += instance.OnMeleeAttack;
            @MeleeAttack.performed += instance.OnMeleeAttack;
            @MeleeAttack.canceled += instance.OnMeleeAttack;
            @Sprint.started += instance.OnSprint;
            @Sprint.performed += instance.OnSprint;
            @Sprint.canceled += instance.OnSprint;
            @SelectSpell.started += instance.OnSelectSpell;
            @SelectSpell.performed += instance.OnSelectSpell;
            @SelectSpell.canceled += instance.OnSelectSpell;
            @ActivateShootingMode.started += instance.OnActivateShootingMode;
            @ActivateShootingMode.performed += instance.OnActivateShootingMode;
            @ActivateShootingMode.canceled += instance.OnActivateShootingMode;
            @Focus.started += instance.OnFocus;
            @Focus.performed += instance.OnFocus;
            @Focus.canceled += instance.OnFocus;
            @DodgeRoll.started += instance.OnDodgeRoll;
            @DodgeRoll.performed += instance.OnDodgeRoll;
            @DodgeRoll.canceled += instance.OnDodgeRoll;
            @LockOn.started += instance.OnLockOn;
            @LockOn.performed += instance.OnLockOn;
            @LockOn.canceled += instance.OnLockOn;
            @LockOnSelection.started += instance.OnLockOnSelection;
            @LockOnSelection.performed += instance.OnLockOnSelection;
            @LockOnSelection.canceled += instance.OnLockOnSelection;
        }

        private void UnregisterCallbacks(IBaseMovementActions instance)
        {
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Interact.started -= instance.OnInteract;
            @Interact.performed -= instance.OnInteract;
            @Interact.canceled -= instance.OnInteract;
            @MeleeAttack.started -= instance.OnMeleeAttack;
            @MeleeAttack.performed -= instance.OnMeleeAttack;
            @MeleeAttack.canceled -= instance.OnMeleeAttack;
            @Sprint.started -= instance.OnSprint;
            @Sprint.performed -= instance.OnSprint;
            @Sprint.canceled -= instance.OnSprint;
            @SelectSpell.started -= instance.OnSelectSpell;
            @SelectSpell.performed -= instance.OnSelectSpell;
            @SelectSpell.canceled -= instance.OnSelectSpell;
            @ActivateShootingMode.started -= instance.OnActivateShootingMode;
            @ActivateShootingMode.performed -= instance.OnActivateShootingMode;
            @ActivateShootingMode.canceled -= instance.OnActivateShootingMode;
            @Focus.started -= instance.OnFocus;
            @Focus.performed -= instance.OnFocus;
            @Focus.canceled -= instance.OnFocus;
            @DodgeRoll.started -= instance.OnDodgeRoll;
            @DodgeRoll.performed -= instance.OnDodgeRoll;
            @DodgeRoll.canceled -= instance.OnDodgeRoll;
            @LockOn.started -= instance.OnLockOn;
            @LockOn.performed -= instance.OnLockOn;
            @LockOn.canceled -= instance.OnLockOn;
            @LockOnSelection.started -= instance.OnLockOnSelection;
            @LockOnSelection.performed -= instance.OnLockOnSelection;
            @LockOnSelection.canceled -= instance.OnLockOnSelection;
        }

        public void RemoveCallbacks(IBaseMovementActions instance)
        {
            if (m_Wrapper.m_BaseMovementActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IBaseMovementActions instance)
        {
            foreach (var item in m_Wrapper.m_BaseMovementActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_BaseMovementActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public BaseMovementActions @BaseMovement => new BaseMovementActions(this);
    public interface IBaseMovementActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnInteract(InputAction.CallbackContext context);
        void OnMeleeAttack(InputAction.CallbackContext context);
        void OnSprint(InputAction.CallbackContext context);
        void OnSelectSpell(InputAction.CallbackContext context);
        void OnActivateShootingMode(InputAction.CallbackContext context);
        void OnFocus(InputAction.CallbackContext context);
        void OnDodgeRoll(InputAction.CallbackContext context);
        void OnLockOn(InputAction.CallbackContext context);
        void OnLockOnSelection(InputAction.CallbackContext context);
    }
}
