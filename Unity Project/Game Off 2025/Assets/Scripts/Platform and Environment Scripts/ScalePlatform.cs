using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScalePlatform : MonoBehaviour
{
    [SerializeField] private WeightCheck side1;
    [SerializeField] private WeightCheck side2;

    [SerializeField] private float maxHeight;
    [SerializeField] private float minHeight;
    [SerializeField] private float middlePoint;
    [SerializeField] private float moveSpeed = 1;

    [SerializeField] private int blockCountRequirement;
    [SerializeField] private bool canPlayerWeighDownScale;
    [SerializeField] private bool startBalanced;
    [SerializeField] private bool stayBalanced;
    [SerializeField] private bool side1StartRaised;

    private void Start()
    {
        middlePoint = ((maxHeight + minHeight) / 2);
        if (!startBalanced)
        {
            if (side1StartRaised)
            {
                side1.transform.localPosition = new Vector3(side1.transform.localPosition.x, minHeight, 0);
                side2.transform.localPosition = new Vector3(side2.transform.localPosition.x, maxHeight, 0);
            }
            else
            {
                side1.transform.localPosition = new Vector3(side1.transform.localPosition.x, maxHeight, 0);
                side2.transform.localPosition = new Vector3(side2.transform.localPosition.x, minHeight, 0);
            }
        }
        else
        {
            side1.transform.localPosition = new Vector3(side1.transform.localPosition.x, middlePoint, 0);
            side2.transform.localPosition = new Vector3(side2.transform.localPosition.x, middlePoint, 0);
        }
    }

    void Update()
    {
        // When sides are balanced
        if (side1.blockCount > 0 && side1.blockCount == side2.blockCount)
        {
            side1.transform.localPosition = Vector3.MoveTowards(side1.transform.localPosition, new Vector3(side1.transform.localPosition.x, middlePoint, 0), moveSpeed * Time.deltaTime);
            side2.transform.localPosition = Vector3.MoveTowards(side2.transform.localPosition, new Vector3(side2.transform.localPosition.x, middlePoint, 0), moveSpeed * Time.deltaTime);

        }
        else if (stayBalanced && side1.blockCount == side2.blockCount)
        {
            side1.transform.localPosition = Vector3.MoveTowards(side1.transform.localPosition, new Vector3(side1.transform.localPosition.x, middlePoint, 0), moveSpeed * Time.deltaTime);
            side2.transform.localPosition = Vector3.MoveTowards(side2.transform.localPosition, new Vector3(side2.transform.localPosition.x, middlePoint, 0), moveSpeed * Time.deltaTime);

        }

        // Weighed down by blocks
        switch (blockCountRequirement)
        {
            case 1:
                if (side1.blockCount > side2.blockCount)
                {
                    side1.transform.localPosition = Vector3.MoveTowards(side1.transform.localPosition, new Vector3(side1.transform.localPosition.x, minHeight, 0), moveSpeed * Time.deltaTime);
                    side2.transform.localPosition = Vector3.MoveTowards(side2.transform.localPosition, new Vector3(side2.transform.localPosition.x, maxHeight, 0), moveSpeed * Time.deltaTime);
                }

                else if (side1.blockCount < side2.blockCount)
                {
                    side1.transform.localPosition = Vector3.MoveTowards(side1.transform.localPosition, new Vector3(side1.transform.localPosition.x, maxHeight, 0), moveSpeed * Time.deltaTime);
                    side2.transform.localPosition = Vector3.MoveTowards(side2.transform.localPosition, new Vector3(side2.transform.localPosition.x, minHeight, 0), moveSpeed * Time.deltaTime);
                }
                break;

            case 2:
                if (side1.blockCount > side2.blockCount && side1.blockCount > 1)
                {
                    side1.transform.localPosition = Vector3.MoveTowards(side1.transform.localPosition, new Vector3(side1.transform.localPosition.x, minHeight, 0), moveSpeed * Time.deltaTime);
                    side2.transform.localPosition = Vector3.MoveTowards(side2.transform.localPosition, new Vector3(side2.transform.localPosition.x, maxHeight, 0), moveSpeed * Time.deltaTime);
                }

                else if (side1.blockCount < side2.blockCount && side2.blockCount > 1)
                {
                    side1.transform.localPosition = Vector3.MoveTowards(side1.transform.localPosition, new Vector3(side1.transform.localPosition.x, maxHeight, 0), moveSpeed * Time.deltaTime);
                    side2.transform.localPosition = Vector3.MoveTowards(side2.transform.localPosition, new Vector3(side2.transform.localPosition.x, minHeight, 0), moveSpeed * Time.deltaTime);
                }
                else if (side1.blockCount > side2.blockCount && side1.blockCount == 1)
                {
                    side1.transform.localPosition = Vector3.MoveTowards(side1.transform.localPosition, new Vector3(side1.transform.localPosition.x, (minHeight + middlePoint) / 2, 0), moveSpeed * Time.deltaTime);
                    side2.transform.localPosition = Vector3.MoveTowards(side2.transform.localPosition, new Vector3(side2.transform.localPosition.x, (maxHeight + middlePoint) / 2, 0), moveSpeed * Time.deltaTime);
                }
                else if (side1.blockCount < side2.blockCount && side2.blockCount == 1)
                {
                    side1.transform.localPosition = Vector3.MoveTowards(side1.transform.localPosition, new Vector3(side1.transform.localPosition.x, (maxHeight + middlePoint) / 2, 0), moveSpeed * Time.deltaTime);
                    side2.transform.localPosition = Vector3.MoveTowards(side2.transform.localPosition, new Vector3(side2.transform.localPosition.x, (minHeight + middlePoint) / 2, 0), moveSpeed * Time.deltaTime);
                }

                break;

            case 3:

                // one side has 3 blocks
                if (side1.blockCount > side2.blockCount && side1.blockCount > 2)
                {
                    side1.transform.localPosition = Vector3.MoveTowards(side1.transform.localPosition, new Vector3(side1.transform.localPosition.x, minHeight, 0), moveSpeed * Time.deltaTime);
                    side2.transform.localPosition = Vector3.MoveTowards(side2.transform.localPosition, new Vector3(side2.transform.localPosition.x, maxHeight, 0), moveSpeed * Time.deltaTime);
                }

                else if (side1.blockCount < side2.blockCount && side2.blockCount > 2)
                {
                    side1.transform.localPosition = Vector3.MoveTowards(side1.transform.localPosition, new Vector3(side1.transform.localPosition.x, maxHeight, 0), moveSpeed * Time.deltaTime);
                    side2.transform.localPosition = Vector3.MoveTowards(side2.transform.localPosition, new Vector3(side2.transform.localPosition.x, minHeight, 0), moveSpeed * Time.deltaTime);
                }
                // one side has 1 block
                else if (side1.blockCount > side2.blockCount && side1.blockCount == 1)
                {
                    side1.transform.localPosition = Vector3.MoveTowards(side1.transform.localPosition, new Vector3(side1.transform.localPosition.x, minHeight - (2 * middlePoint / 3), 0), moveSpeed * Time.deltaTime);
                    side2.transform.localPosition = Vector3.MoveTowards(side2.transform.localPosition, new Vector3(side2.transform.localPosition.x, maxHeight + (2 * middlePoint / 3), 0), moveSpeed * Time.deltaTime);
                }
                else if (side1.blockCount < side2.blockCount && side2.blockCount == 1)
                {
                    side1.transform.localPosition = Vector3.MoveTowards(side1.transform.localPosition, new Vector3(side1.transform.localPosition.x, maxHeight + (2 * middlePoint / 3), 0), moveSpeed * Time.deltaTime);
                    side2.transform.localPosition = Vector3.MoveTowards(side2.transform.localPosition, new Vector3(side2.transform.localPosition.x, minHeight - (2 * middlePoint / 3), 0), moveSpeed * Time.deltaTime);
                }
                // one side has 2 blocks
                else if (side1.blockCount > side2.blockCount && side1.blockCount == 2)
                {
                    side1.transform.localPosition = Vector3.MoveTowards(side1.transform.localPosition, new Vector3(side1.transform.localPosition.x, minHeight - (middlePoint / 3), 0), moveSpeed * Time.deltaTime);
                    side2.transform.localPosition = Vector3.MoveTowards(side2.transform.localPosition, new Vector3(side2.transform.localPosition.x, maxHeight + (middlePoint / 3), 0), moveSpeed * Time.deltaTime);
                }
                else if (side1.blockCount < side2.blockCount && side2.blockCount == 2)
                {
                    side1.transform.localPosition = Vector3.MoveTowards(side1.transform.localPosition, new Vector3(side1.transform.localPosition.x, maxHeight + (middlePoint / 3), 0), moveSpeed * Time.deltaTime);
                    side2.transform.localPosition = Vector3.MoveTowards(side2.transform.localPosition, new Vector3(side2.transform.localPosition.x, minHeight - (middlePoint / 3), 0), moveSpeed * Time.deltaTime);
                }
                break;

            default:
                break;
        }


        // Player weighing down scale
        if (side1.blockCount == 0 && side2.blockCount == 0)
        {
            if (canPlayerWeighDownScale)
            {
                if (side1.playerIsWeighingDown)
                {
                    side1.transform.localPosition = Vector3.MoveTowards(side1.transform.localPosition, new Vector3(side1.transform.localPosition.x, minHeight, 0), moveSpeed * Time.deltaTime);
                    side2.transform.localPosition = Vector3.MoveTowards(side2.transform.localPosition, new Vector3(side2.transform.localPosition.x, maxHeight, 0), moveSpeed * Time.deltaTime);
                }
                else if (side2.playerIsWeighingDown)
                {
                    side1.transform.localPosition = Vector3.MoveTowards(side1.transform.localPosition, new Vector3(side1.transform.localPosition.x, maxHeight, 0), moveSpeed * Time.deltaTime);
                    side2.transform.localPosition = Vector3.MoveTowards(side2.transform.localPosition, new Vector3(side2.transform.localPosition.x, minHeight, 0), moveSpeed * Time.deltaTime);
                }
            }
        }
    }
}
