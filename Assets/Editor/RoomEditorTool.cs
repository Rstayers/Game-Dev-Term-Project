using UnityEngine;
using UnityEditor;

public class RoomEditorTool : EditorWindow
{
    [MenuItem("Tools/Room Editor")]
    public static void ShowWindow()
    {
        GetWindow<RoomEditorTool>("Room Editor");
    }

    private void OnGUI()
    {
        GUILayout.Label("Room Creation Tool", EditorStyles.boldLabel);

        if (GUILayout.Button("Make Room from Selection"))
        {
            CreateRoomFromSelection();
        }
    }

    private void CreateRoomFromSelection()
    {
        if (Selection.activeGameObject != null)
        {
            GameObject selectedObject = Selection.activeGameObject;
            CreateRoom(selectedObject);
        }
        else
        {
            Debug.LogWarning("No object selected! Please select a mesh to convert into a room.");
        }
    }

    private void CreateRoom(GameObject meshObject)
    {
        // Create a new GameObject to hold the room components
        GameObject room = new GameObject(meshObject.name + "_Room");

        // Set the room's position to the mesh's position
        room.transform.position = meshObject.transform.position;

        // Move the selected mesh under the new room GameObject
        meshObject.transform.SetParent(room.transform);

        // Automatically add a BoxCollider that covers the room
        BoxCollider roomCollider = room.AddComponent<BoxCollider>();
        roomCollider.center = meshObject.GetComponent<MeshRenderer>().bounds.center - room.transform.position;
        roomCollider.size = meshObject.GetComponent<MeshRenderer>().bounds.size - new Vector3(1.5f, 0, 1.5f) ;
        roomCollider.isTrigger = true;

        // Add the RoomManager script to the room
        RoomManager roomManager = room.AddComponent<RoomManager>();

        // Add the mask logic here (placeholder)
        AddHallwayMasks(room);

        Debug.Log("Room created successfully from selected object!");
    }

    private void AddHallwayMasks(GameObject room)
    {
        // Placeholder for adding hallway mask logic
        // You would need to define the mask logic for hallways and how to display/hide them based on room entry/exit.
    }
}
