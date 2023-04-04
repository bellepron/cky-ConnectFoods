using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace cky.Reuseables.Extension
{
    public static class Extensions
    {
        public static Vector3 WorldUnitDirectionX(this Transform actorTr, Vector3 targetPos)
        {
            var direction = targetPos - actorTr.position;
            direction.y = 0.0f;
            direction.z = 0.0f;
            direction.Normalize();

            return direction;
        }
        public static Vector3 LocalUnitDirectionX(this Transform actorTr, Vector3 targetPos)
        {
            var direction = targetPos - actorTr.localPosition;
            direction.y = 0.0f;
            direction.z = 0.0f;
            direction.Normalize();

            return direction;
        }
        public static void TurnToThis(this Transform actorTr, Vector3 targetPos, float rotationSpeed)
        {
            Vector3 direction = targetPos - actorTr.position;
            direction.y = 0;

            float angle = Vector3.Angle(direction, actorTr.forward);
            actorTr.rotation = Quaternion.Slerp(actorTr.rotation,
                                                Quaternion.LookRotation(direction),
                                                Time.deltaTime * rotationSpeed);
        }

        public static void TurnToDirection(this Transform actorTr, Vector3 targetForward, float rotationSpeed)
        {
            //Vector3 direction = targetForward - stateMachineAI.transform.position;
            Vector3 direction = targetForward;
            direction.y = 0;

            float angle = Vector3.Angle(direction, actorTr.forward);
            actorTr.rotation = Quaternion.Slerp(actorTr.rotation,
                                                Quaternion.LookRotation(direction),
                                                Time.deltaTime * rotationSpeed);
        }

        public static void TurnToAngle(this Transform actorTr, Vector3 targetAngle, float rotationSpeed)
        {
            actorTr.rotation = Quaternion.Slerp(actorTr.rotation,
                                                Quaternion.LookRotation(targetAngle),
                                                Time.deltaTime * rotationSpeed);
        }

        public static float Angle(this Transform actorTr, Vector3 targetForward)
        {
            return Vector3.Angle(actorTr.forward, targetForward);
        }

        public static void MoveWithVelocity(this Rigidbody rb, Vector3 targetPos, float moveSpeed, float rotationSpeed)
        {
            var actorTr = rb.transform;

            actorTr.TurnToThis(targetPos, rotationSpeed);

            rb.velocity = actorTr.forward * moveSpeed;
        }

        public static bool CloseToThisXZ(this Transform actorTr, Vector3 targetPos, float targetDistance)
        {
            var actorPos = actorTr.position;
            targetPos.y = actorPos.y;

            var distance = Vector3.Distance(actorPos, (Vector3)targetPos);

            if (distance < targetDistance)
                return true;

            return false;
        }

        private static readonly System.Random rng = new System.Random();

        public static void ShuffleMatrix<T>(this T[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);

            // Flatten matrix
            T[] flattenedMatrix = new T[rows * cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    flattenedMatrix[i * cols + j] = matrix[i, j];
                }
            }

            // Shuffle flattened matrix
            int n = flattenedMatrix.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                T temp = flattenedMatrix[n];
                flattenedMatrix[n] = flattenedMatrix[k];
                flattenedMatrix[k] = temp;
            }

            // Unflatten shuffled matrix
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    matrix[i, j] = flattenedMatrix[i * cols + j];
                }
            }
        }

        public static T RandomFromArray<T>(this IEnumerable<T> array)
        {
            var random = UnityEngine.Random.Range(0, array.Count());
            return array.ElementAt(random);
        }

        public static Tuple<int, int> CoordinatesOfItemInGrid<T>(this T[,] matrix, T value)
        {
            int w = matrix.GetLength(0); // width
            int h = matrix.GetLength(1); // height

            for (int x = 0; x < w; ++x)
            {
                for (int y = 0; y < h; ++y)
                {
                    if (matrix[x, y].Equals(value))
                        return Tuple.Create(x, y);
                }
            }

            return Tuple.Create(-1, -1);
        }

        public static List<T> GetNeighboursInGrid<T>(this T[,] grid, T cell)
        {
            var neighbours = new List<T>();
            var rowNum = grid.GetLength(0);
            var colNum = grid.GetLength(1);
            var c = grid.CoordinatesOfItemInGrid(cell);
            var row = c.Item1;
            var col = c.Item2;

            for (int i = row - 1; i <= row + 1; i++)
            {
                for (int j = col - 1; j <= col + 1; j++)
                {
                    if (i == row && j == col) continue;
                    if (i < 0 || i >= rowNum || j < 0 || j >= colNum) continue;

                    neighbours.Add(grid[i, j]);
                }
            }

            return neighbours;
        }


        #region Camera

        public static GameObject GetGameObjectFromMousePosition(this Camera camera, LayerMask layerMask, float maxDistance = Mathf.Infinity)
        {
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
            {
                return hit.transform.gameObject;
            }

            return null;
        }

        public static GameObject GetGameObjectFromMousePosition2D(this Camera camera, LayerMask layerMask)
        {
            var hit = Physics2D.OverlapPoint(camera.ScreenToWorldPoint(Input.mousePosition));

            if (hit != null)
            {
                if (hit.gameObject.layer == layerMask)
                {
                    return null;
                }

                return hit.gameObject;
            }

            return null;
        }

        #endregion

        public static Vector3 ClampedPositionRelativeToTheCursorInWorldSpace(this Vector3 referencePos, Camera camera, float minLength, float maxLength)
        {
            var mousePosToWorld = camera.ScreenToWorldPoint(Input.mousePosition);
            mousePosToWorld.z = 0.0f;
            var lastObjectPosition = referencePos;
            var direction = mousePosToWorld - lastObjectPosition;
            var dist = Mathf.Clamp((direction).magnitude, minLength, maxLength);
            var targetPos = lastObjectPosition + dist * direction.normalized;

            return targetPos;
        }
    }
}