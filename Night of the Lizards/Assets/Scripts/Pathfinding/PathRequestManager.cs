﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace LizardNight
{

    public class PathRequestManager : MonoBehaviour
    {

        Queue<PathRequest> pathRequestQueue = new Queue<PathRequest>();
        PathRequest currentPathRequest;

        static PathRequestManager instance;
        [SerializeField]
        Pathfinding pathfinding;

        bool isProcessingPath;

        private void Awake()
        {
            instance = this;
            pathfinding = GetComponent<Pathfinding>();
        }

        public static void RequestPath (Vector3 pathStart, Vector3 pathEnd, Action<Vector3, bool, bool> callback)
        {
            PathRequest newRequest = new PathRequest(pathStart, pathEnd, callback);
            instance.pathRequestQueue.Enqueue(newRequest);
            instance.TryProcessNext();

        }

        void TryProcessNext ()
        {
            if (!isProcessingPath && pathRequestQueue.Count > 0)
            {
                currentPathRequest = pathRequestQueue.Dequeue();
                isProcessingPath = true;
                pathfinding.StartFindPath(currentPathRequest.pathStart, currentPathRequest.pathEnd);
            }

        }

        public void FinishedProcessingPath(Vector3 path, bool success, bool isTarget)
        {
            currentPathRequest.callback(path, success, isTarget);
            isProcessingPath = false;
            TryProcessNext();

        }

        struct PathRequest
        {
            public Vector3 pathStart;
            public Vector3 pathEnd;
            public Action<Vector3, bool, bool> callback;

            public PathRequest(Vector3 _start, Vector3 _end, Action<Vector3, bool, bool> _callback)
            {
                pathStart = _start;
                pathEnd = _end;
                callback = _callback;
            }
        }
    }
}
