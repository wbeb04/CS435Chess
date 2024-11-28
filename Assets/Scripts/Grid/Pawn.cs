using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class Pawn : MonoBehaviour
{
    [SerializeField] private int _playerNumber; // Player number for the pawn
    [SerializeField] public CharacterController controller; // Reference to the CharacterController component
    [SerializeField] public Grid _grid;
    [SerializeField] private float playerSpeed = 2.0f;

    [SerializeField] private GameObject _wpawn;
    [SerializeField] private GameObject _wrook;
    [SerializeField] private GameObject _wknight;
    [SerializeField] private GameObject _wbishop;
    [SerializeField] private GameObject _wqueen;
    [SerializeField] private GameObject _wking;

    [SerializeField] private GameObject _bpawn;
    [SerializeField] private GameObject _brook;
    [SerializeField] private GameObject _bknight;
    [SerializeField] private GameObject _bbishop;
    [SerializeField] private GameObject _bqueen;
    [SerializeField] private GameObject _bking;

    [SerializeField] private GameObject _position;
    [SerializeField] private float rotationSpeed = 5.0f;
    [SerializeField] private InputReader inputReader;
    private GameObject selectedPawn;
    private List<GameObject> pawns = new List<GameObject>(); // List to store pawns
    private List<GameObject> rooks = new List<GameObject>(); // List to store rooks
    private List<GameObject> knights = new List<GameObject>();
    private List<GameObject> bishops = new List<GameObject>();
    private List<GameObject> queen = new List<GameObject>();
    private List<GameObject> king = new List<GameObject>();
    private List<GameObject> pawns2 = new List<GameObject>(); // List to store pawns for player 2
    private List<GameObject> rooks2 = new List<GameObject>(); // List to store rooks for player 2
    private List<GameObject> knights2 = new List<GameObject>();
    private List<GameObject> bishops2 = new List<GameObject>();
    private List<GameObject> queen2 = new List<GameObject>();
    private List<GameObject> king2 = new List<GameObject>();
    private Vector3 playerVelocity;
    private Transform cameraMain;
    Camera Camera;
    // Start is called before the first frame update
    void Start()
    {
        cameraMain = Camera.main.transform;
        //Cursor.visible = false;
        //Vector3 worldPosition = _grid.GetCellCenterWorld(new Vector3Int(0, 0, 0)); // Get the world position of the cell at (0, 0)
       // Debug.Log("World Position: " + worldPosition);
        // worldPosition.y = 0;
        // transform.position = worldPosition; // Set the position of the pawn to the world position
        // Instantiate(_pawn, worldPosition, Quaternion.identity);

        //player 1 pawns
        for (int i = 0; i < 8; i++) 
        {
            Vector3 worldPosition2 = _grid.GetCellCenterWorld(new Vector3Int(0, i, 0));
            Vector3Int gridPosition = _grid.WorldToCell(worldPosition2);
            worldPosition2.y = 0;
            transform.position = worldPosition2; // Set the position of the pawn to the world position
            GameObject pawnInstance = Instantiate(_wpawn, worldPosition2, Quaternion.identity);
            BoxCollider boxCollider = pawnInstance.AddComponent<BoxCollider>();
            Targeter targeter = pawnInstance.AddComponent<Targeter>();
            targeter.renderer = pawnInstance.GetComponent<Renderer>();
            PieceIdentity id = pawnInstance.AddComponent<PieceIdentity>();
            id.pieceType = ChessPieceType.Player1Pawn;
            //pawnInstance.tag = "Pawn";

            pawnInstance.transform.Rotate(Vector3.right, -90f);
            pawnInstance.transform.localScale = new Vector3(4f, 4f, 4f);
            pawnInstance.layer = LayerMask.NameToLayer("Chessboard"); //for the raycast
            pawns.Add(pawnInstance);
            
        }
        //player 1 rooks
        for (int i = 0; i < 9; i += 7)
        {
            Vector3 worldPosition2 = _grid.GetCellCenterWorld(new Vector3Int(1, i, 0));
            Vector3Int gridPosition = _grid.WorldToCell(worldPosition2);
            worldPosition2.y = 0;
            transform.position = worldPosition2; // Set the position of the rook to the world position
            GameObject rookInstance = Instantiate(_wrook, worldPosition2, Quaternion.identity);
            BoxCollider boxCollider = rookInstance.AddComponent<BoxCollider>();
            Targeter targeter = rookInstance.AddComponent<Targeter>();
            targeter.renderer = rookInstance.GetComponent<Renderer>();
            PieceIdentity id = rookInstance.AddComponent<PieceIdentity>();
            rookInstance.transform.Rotate(Vector3.right, -90f);
            rookInstance.transform.localScale = new Vector3(4f, 4f, 4f);
            id.pieceType = ChessPieceType.Player1Rook;
            rookInstance.layer = LayerMask.NameToLayer("Chessboard"); //for the raycast
            rooks.Add(rookInstance);
        }
        //player 1 knights
        for (int i = 1; i < 9; i += 5)
        {
            Vector3 worldPosition2 = _grid.GetCellCenterWorld(new Vector3Int(1, i, 0));
            Vector3Int gridPosition = _grid.WorldToCell(worldPosition2);
            worldPosition2.y = 0;
            transform.position = worldPosition2; // Set the position of the rook to the world position
            GameObject knightInstance = Instantiate(_wknight, worldPosition2, Quaternion.identity);
            BoxCollider boxCollider = knightInstance.AddComponent<BoxCollider>();
            Targeter targeter = knightInstance.AddComponent<Targeter>();
            targeter.renderer = knightInstance.GetComponent<Renderer>();
            PieceIdentity id = knightInstance.AddComponent<PieceIdentity>();
            knightInstance.transform.Rotate(Vector3.right, -90f);
            knightInstance.transform.localScale = new Vector3(4f, 4f, 4f);
            id.pieceType = ChessPieceType.Player1Knight;
            knightInstance.layer = LayerMask.NameToLayer("Chessboard"); //for the raycast
            knights.Add(knightInstance);
        }
        //player 1 bishops
        for (int i = 2; i < 6; i += 3)
        {
            Vector3 worldPosition2 = _grid.GetCellCenterWorld(new Vector3Int(1, i, 0));
            Vector3Int gridPosition = _grid.WorldToCell(worldPosition2);
            worldPosition2.y = 0;
            worldPosition2.y += 0.43f;
            transform.position = worldPosition2; // Set the position of the rook to the world position
            GameObject bishopInstance = Instantiate(_wbishop, worldPosition2, Quaternion.identity);
            BoxCollider boxCollider = bishopInstance.AddComponent<BoxCollider>();
            Targeter targeter = bishopInstance.AddComponent<Targeter>();
            targeter.renderer = bishopInstance.GetComponent<Renderer>();
            bishopInstance.transform.localScale = new Vector3(5f, 5f, 5f);
            PieceIdentity id = bishopInstance.AddComponent<PieceIdentity>();

            id.pieceType = ChessPieceType.Player1Bishop;
            bishopInstance.layer = LayerMask.NameToLayer("Chessboard"); //for the raycast
            bishops.Add(bishopInstance);
        }
        //player 1 queen
        for (int i = 0; i < 1; i += 1)
        {
            Vector3 worldPosition2 = _grid.GetCellCenterWorld(new Vector3Int(1, 3, 0));
            Vector3Int gridPosition = _grid.WorldToCell(worldPosition2);
            worldPosition2.y = 0;
            transform.position = worldPosition2; // Set the position of the rook to the world position
            GameObject queenInstance = Instantiate(_wqueen, worldPosition2, Quaternion.identity);
            BoxCollider boxCollider = queenInstance.AddComponent<BoxCollider>();
            Targeter targeter = queenInstance.AddComponent<Targeter>();
            targeter.renderer = queenInstance.GetComponent<Renderer>();
            PieceIdentity id = queenInstance.AddComponent<PieceIdentity>();
            queenInstance.transform.Rotate(Vector3.right, -90f);
            queenInstance.transform.localScale = new Vector3(4f, 4f, 4f);
            id.pieceType = ChessPieceType.Player1Queen;
            queenInstance.layer = LayerMask.NameToLayer("Chessboard"); //for the raycast
            queen.Add(queenInstance);
        }
        //player 1 king
        for (int i = 0; i < 1; i += 1)
        {
            Vector3 worldPosition2 = _grid.GetCellCenterWorld(new Vector3Int(1, 4, 0));
            Vector3Int gridPosition = _grid.WorldToCell(worldPosition2);
            worldPosition2.y = 0;
            transform.position = worldPosition2; // Set the position of the rook to the world position
            GameObject kingInstance = Instantiate(_wking, worldPosition2, Quaternion.identity);
            BoxCollider boxCollider = kingInstance.AddComponent<BoxCollider>();
            Targeter targeter = kingInstance.AddComponent<Targeter>();
            targeter.renderer = kingInstance.GetComponent<Renderer>();
            PieceIdentity id = kingInstance.AddComponent<PieceIdentity>();
            kingInstance.transform.Rotate(Vector3.right, -90f);
            kingInstance.transform.localScale = new Vector3(4f, 4f, 4f);
            id.pieceType = ChessPieceType.Player1King;
            kingInstance.layer = LayerMask.NameToLayer("Chessboard"); //for the raycast
            king.Add(kingInstance);
        }

        //player 2 pawns
        for (int i = 0; i < 8; i++)
        {
            Vector3 worldPosition2 = _grid.GetCellCenterWorld(new Vector3Int(-5, i, 0));
            Vector3Int gridPosition = _grid.WorldToCell(worldPosition2);
            worldPosition2.y = 0;
            transform.position = worldPosition2; // Set the position of the pawn to the world position
            GameObject pawnInstance = Instantiate(_bpawn, worldPosition2, Quaternion.identity);
            BoxCollider boxCollider = pawnInstance.AddComponent<BoxCollider>();
            Targeter targeter = pawnInstance.AddComponent<Targeter>();
            targeter.renderer = pawnInstance.GetComponent<Renderer>();

            pawnInstance.transform.Rotate(Vector3.right, -90f);
            pawnInstance.transform.localScale = new Vector3(4f, 4f, 4f);
            // pawnInstance.tag = "Pawn";
            //pawnInstance.tag = "Player2";   
            pawnInstance.layer = LayerMask.NameToLayer("Chessboard"); //for the raycast
            pawns2.Add(pawnInstance);
        }
        //player 2 rooks
        for (int i = 0; i < 9; i += 7)
        {
            Vector3 worldPosition2 = _grid.GetCellCenterWorld(new Vector3Int(-6, i, 0));
            Vector3Int gridPosition = _grid.WorldToCell(worldPosition2);
            worldPosition2.y = 0;
            transform.position = worldPosition2; // Set the position of the rook to the world position
            GameObject rookInstance = Instantiate(_brook, worldPosition2, Quaternion.identity);
            BoxCollider boxCollider = rookInstance.AddComponent<BoxCollider>();
            Targeter targeter = rookInstance.AddComponent<Targeter>();
            targeter.renderer = rookInstance.GetComponent<Renderer>();
            PieceIdentity id = rookInstance.AddComponent<PieceIdentity>();
            rookInstance.transform.Rotate(Vector3.right, -90f);
            rookInstance.transform.localScale = new Vector3(4f, 4f, 4f);
            id.pieceType = ChessPieceType.Player2Rook;
            rookInstance.layer = LayerMask.NameToLayer("Chessboard"); //for the raycast
            rooks2.Add(rookInstance);
        }
        //player 2 knights
        for (int i = 1; i < 9; i += 5)
        {
            Vector3 worldPosition2 = _grid.GetCellCenterWorld(new Vector3Int(-6, i, 0));
            Vector3Int gridPosition = _grid.WorldToCell(worldPosition2);
            worldPosition2.y = 0;
            transform.position = worldPosition2; // Set the position of the rook to the world position
            GameObject knightInstance = Instantiate(_bknight, worldPosition2, Quaternion.identity);
            BoxCollider boxCollider = knightInstance.AddComponent<BoxCollider>();
            Targeter targeter = knightInstance.AddComponent<Targeter>();
            targeter.renderer = knightInstance.GetComponent<Renderer>();
            PieceIdentity id = knightInstance.AddComponent<PieceIdentity>();
            knightInstance.transform.Rotate(Vector3.right, -90f);
            knightInstance.transform.localScale = new Vector3(4f, 4f, 4f);
            id.pieceType = ChessPieceType.Player2Knight;
            knightInstance.layer = LayerMask.NameToLayer("Chessboard"); //for the raycast
            knights2.Add(knightInstance);
        }
        //player 2 bishops
        for (int i = 2; i < 6; i += 3)
        {
            Vector3 worldPosition2 = _grid.GetCellCenterWorld(new Vector3Int(-6, i, 0));
            Vector3Int gridPosition = _grid.WorldToCell(worldPosition2);
            worldPosition2.y = 0;
            worldPosition2.y += 0.43f;
            transform.position = worldPosition2; // Set the position of the rook to the world position
            GameObject bishopInstance = Instantiate(_bbishop, worldPosition2, Quaternion.identity);
            BoxCollider boxCollider = bishopInstance.AddComponent<BoxCollider>();
            Targeter targeter = bishopInstance.AddComponent<Targeter>();
            targeter.renderer = bishopInstance.GetComponent<Renderer>();
            bishopInstance.transform.localScale = new Vector3(5f, 5f, 5f);
            PieceIdentity id = bishopInstance.AddComponent<PieceIdentity>();

            id.pieceType = ChessPieceType.Player2Bishop;
            bishopInstance.layer = LayerMask.NameToLayer("Chessboard"); //for the raycast
            bishops2.Add(bishopInstance);
        }
        //player 2 queen
        for (int i = 0; i < 1; i += 1)
        {
            Vector3 worldPosition2 = _grid.GetCellCenterWorld(new Vector3Int(-6, 4, 0));
            Vector3Int gridPosition = _grid.WorldToCell(worldPosition2);
            worldPosition2.y = 0;
            transform.position = worldPosition2; // Set the position of the rook to the world position
            GameObject queenInstance = Instantiate(_bqueen, worldPosition2, Quaternion.identity);
            BoxCollider boxCollider = queenInstance.AddComponent<BoxCollider>();
            Targeter targeter = queenInstance.AddComponent<Targeter>();
            targeter.renderer = queenInstance.GetComponent<Renderer>();
            PieceIdentity id = queenInstance.AddComponent<PieceIdentity>();
            queenInstance.transform.Rotate(Vector3.right, -90f);
            queenInstance.transform.localScale = new Vector3(4f, 4f, 4f);
            id.pieceType = ChessPieceType.Player2Queen;
            queenInstance.layer = LayerMask.NameToLayer("Chessboard"); //for the raycast
            queen2.Add(queenInstance);
        }
        //player 2 king
        for (int i = 0; i < 1; i += 1)
        {
            Vector3 worldPosition2 = _grid.GetCellCenterWorld(new Vector3Int(-6, 3, 0));
            Vector3Int gridPosition = _grid.WorldToCell(worldPosition2);
            worldPosition2.y = 0;
            transform.position = worldPosition2; // Set the position of the rook to the world position
            GameObject kingInstance = Instantiate(_bking, worldPosition2, Quaternion.identity);
            BoxCollider boxCollider = kingInstance.AddComponent<BoxCollider>();
            Targeter targeter = kingInstance.AddComponent<Targeter>();
            targeter.renderer = kingInstance.GetComponent<Renderer>();
            PieceIdentity id = kingInstance.AddComponent<PieceIdentity>();
            kingInstance.transform.Rotate(Vector3.right, -90f);
            kingInstance.transform.localScale = new Vector3(4f, 4f, 4f);
            id.pieceType = ChessPieceType.Player2King;
            kingInstance.layer = LayerMask.NameToLayer("Chessboard"); //for the raycast
            king2.Add(kingInstance);
        }
    }

    // Update is called once per frame
    void Update()
    {
        setPosition();
    }
    private void setPosition()
    {
        //this code will actively track the mouse position and convert it to a grid position
        //screen position function did not work had to use raycast to get the world space position

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        int layerMask = LayerMask.GetMask("Chessboard");
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
        {
            Vector3 worldMousePosition = hit.point; //get the world position of the mouse
            Vector3Int gridPosition = _grid.WorldToCell(worldMousePosition); //convert the world position to a grid position(this works)
            Vector3 worldPosition2 = _grid.GetCellCenterWorld(gridPosition); // Get the center of the cell at the grid position and move the peice to that position
            float posy = gridPosition.y;
            if (Input.GetMouseButtonDown(0))
            {
                // this if statement is to select the piece 
                if (selectedPawn == null)
                {
                    // see if we got a hit and the tag is "pawn"
                    PieceIdentity piece = hit.collider.gameObject.GetComponent<PieceIdentity>();
                    if (hit.collider != null && (piece.pieceType == ChessPieceType.Player1Pawn || piece.pieceType == ChessPieceType.Player2Pawn))
                    {
                        selectedPawn = hit.collider.gameObject; // Select the pawn
                        Debug.Log("Selected Pawn Grid Position: " + gridPosition);
                  
                    }
                    else 
                    {
          
                    }
                }
                else
                {
                        // move the selected pawn to the new position, if the position is valid
                    Vector3Int grid = getGridPosition(selectedPawn);
                    if (isValidPosition(grid,gridPosition))
                        {
                        selectedPawn.transform.position = _grid.GetCellCenterWorld(gridPosition);
                        //Vector3Int worldPositionGrid = _grid.WorldToCell(worldPosition2);
                        //Debug.Log("Move Valid:" + worldPositionGrid);
                        //selectedPawn.transform.position = worldPosition2;
                        selectedPawn = null; // deselect the pawn after moving
                        }
                    selectedPawn = null;
                }
            }
        }

    }
    private bool isValidPosition(Vector3Int gridPosition, Vector3Int hitPosition) 
    {
        bool isValid = false;
        PieceIdentity piece = selectedPawn.GetComponent<PieceIdentity>();
        if (piece.pieceType == ChessPieceType.Player1Pawn || piece.pieceType == ChessPieceType.Player2Pawn)
        {
            Vector3Int selectedPawnGridPos = getGridPosition(selectedPawn);
            // Check if target is adjacent on X and remains on the same Y
            if ((hitPosition.x == selectedPawnGridPos.x + 1 ||
                 hitPosition.x == selectedPawnGridPos.x - 1) &&
                hitPosition.y == selectedPawnGridPos.y)
            {
               Debug.Log("Valid Position: " + selectedPawnGridPos);
                return isValid = true;
            }
            else {Debug.Log("Invalid Position: " + selectedPawnGridPos); }

            return isValid;
        }
        if (selectedPawn.CompareTag("Rook"))
        {
            Vector3Int selectedRookGridPos = getGridPosition(selectedPawn);
            // Check if target is adjacent on X and remains on the same Y
            if ((hitPosition.x == selectedRookGridPos.x + 1 ||
                 hitPosition.x == selectedRookGridPos.x - 1))
            {
                Debug.Log("Valid Position: " + selectedRookGridPos);
                return isValid = true;
            }
            else { Debug.Log("Invalid Position: " + selectedRookGridPos); }

            return isValid;
        }
        //will add more logic for other pieces but should be just as simple as this

        return isValid;
    }

    Vector3Int getGridPosition (GameObject pawn)
    {
        Vector3Int gridPosition = _grid.WorldToCell(pawn.transform.position);
        Debug.Log("getGridPosition(): " + gridPosition);
        return gridPosition;
    }
    private void GetCharacterPosition()
    {
        Vector3 characterPosition = transform.position;
        Debug.Log("Character Position: " + characterPosition);
    }
}
