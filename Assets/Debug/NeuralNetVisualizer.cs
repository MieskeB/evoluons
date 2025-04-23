using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeuralNetVisualizer : MonoBehaviour
{
//     public Agent agent;
//     public RectTransform canvasTransform;
//     public GameObject nodePrefab;
//     public GameObject connectionPrefab;
//
//     private List<Image> inputNodes = new List<Image>();
//     private List<Image> hiddenNodes = new List<Image>();
//     private List<Image> outputNodes = new List<Image>();
//     private List<Image> connections = new List<Image>();
//
//     public float xSpacing = 150f;
//     public float ySpacing = 60f;
//
//     private void Start()
//     {
//         DrawNetwork();
//     }
//
//     private void Update()
//     {
//         if (agent != null && agent.brain != null)
//         {
//             UpdateVisuals(agent.brain);
//         }
//     }
//
//     void DrawNetwork()
// {
//     // int[] layerSizes = new int[] { agent.brain.GetAmountOfInputNodes(), agent.brain.GetAmountOfHiddenNodes(), agent.brain.GetAmountOfOutputNodes() };
//     int[] layerSizes = new int[] { 5, 7, 3 };
//     RectTransform panel = canvasTransform;
//     Vector2 panelSize = panel.rect.size;
//
//     // Auto-calculate spacing based on panel size
//     float xPadding = 40f; // Padding around the edges
//     float yPadding = 40f;
//
//     float availableWidth = panelSize.x - 2 * xPadding;
//     float availableHeight = panelSize.y - 2 * yPadding;
//
//     int numLayers = layerSizes.Length;
//     xSpacing = availableWidth / (numLayers - 1);
//
//     int maxNeuronsInLayer = 0;
//     foreach (int size in layerSizes)
//         if (size > maxNeuronsInLayer) maxNeuronsInLayer = size;
//
//     ySpacing = maxNeuronsInLayer > 1
//         ? availableHeight / (maxNeuronsInLayer - 1)
//         : availableHeight;
//
//     Vector2 startPos = new Vector2(-panelSize.x / 2f + xPadding, 0);
//
//     List<Vector2> previousLayerPositions = new List<Vector2>();
//
//     for (int l = 0; l < layerSizes.Length; l++)
//     {
//         List<Vector2> currentLayerPositions = new List<Vector2>();
//         List<Image> currentLayerNodes = new List<Image>();
//
//         int neuronsInThisLayer = layerSizes[l];
//         float totalHeight = (neuronsInThisLayer - 1) * ySpacing;
//         float yOffset = totalHeight / 2f;
//
//         for (int n = 0; n < neuronsInThisLayer; n++)
//         {
//             Vector2 pos = startPos + new Vector2(l * xSpacing, -n * ySpacing + yOffset);
//             GameObject node = Instantiate(nodePrefab, canvasTransform);
//             node.GetComponent<RectTransform>().anchoredPosition = pos;
//             Image img = node.GetComponent<Image>();
//             currentLayerNodes.Add(img);
//
//             if (l == 0) inputNodes.Add(img);
//             else if (l == 1) hiddenNodes.Add(img);
//             else outputNodes.Add(img);
//
//             currentLayerPositions.Add(pos);
//
//             // Draw connections
//             if (l > 0)
//             {
//                 foreach (Vector2 prevPos in previousLayerPositions)
//                 {
//                     GameObject conn = Instantiate(connectionPrefab, canvasTransform);
//                     RectTransform connRect = conn.GetComponent<RectTransform>();
//
//                     Vector2 start = prevPos;
//                     Vector2 end = pos;
//                     float distance = Vector2.Distance(start, end);
//
//                     connRect.sizeDelta = new Vector2(distance, 1f);
//                     connRect.anchoredPosition = (start + end) / 2f;
//
//                     float angle = Mathf.Atan2(end.y - start.y, end.x - start.x) * Mathf.Rad2Deg;
//                     connRect.rotation = Quaternion.Euler(0, 0, angle);
//
//                     connections.Add(conn.GetComponent<Image>());
//                 }
//             }
//         }
//
//         previousLayerPositions = currentLayerPositions;
//     }
// }
//
//
//     void UpdateVisuals(Brain brain)
//     {
//         float[] inputs = agent.GetInputs();
//         float[] hidden = brain.GetLatestHidden();
//         float[] outputs = brain.GetLatestOutputs();
//
//         UpdateNodeColors(inputNodes, inputs);
//         UpdateNodeColors(hiddenNodes, hidden);
//         UpdateNodeColors(outputNodes, outputs);
//     }
//
//     void UpdateNodeColors(List<Image> nodes, float[] activations)
//     {
//         for (int i = 0; i < nodes.Count; i++)
//         {
//             float val = Mathf.Clamp01((activations[i] + 1f) / 2f); // Normalize -1 to 1 => 0 to 1
//             nodes[i].color = new Color(1f - val, val, 0f); // Red to Green
//         }
//     }
}
