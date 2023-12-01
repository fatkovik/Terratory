using UnityEngine;
using System.Collections;

public class TaskRoutine
{
    /// <summary>
    /// Returns true if and only if the coroutine is running.  
    /// Paused tasks are considered to be running.
    /// </summary>
    public bool IsRunning
    {
        get
        {
            return taskRoutine.Running;
        }
    }

    /// <summary>
    /// Returns True if and only if the coroutine is currently paused.
    /// </summary>
    public bool IsPaused
    {
        get
        {
            return taskRoutine.Paused;
        }
    }

    /// <summary>
    /// Delegate for termination subscribers.  manual is true if and only if
    /// the coroutine was stopped with an explicit call to Stop().
    /// </summary>
    /// <param name="manual"></param>
    public delegate void FinishedHandler(bool manual);

    /// <summary>
    /// Termination event. Triggered when the coroutine completes execution.
    /// </summary>
    public event FinishedHandler Finished;

    /// <summary>
    /// Creates a new Task object for the given coroutine.
    /// If autoStart is true (default) the task is automatically started
    /// upon construction.
    /// </summary>
    /// <param name="c"></param>
    /// <param name="autoStart"></param>
    public TaskRoutine(IEnumerator c, bool autoStart = true)
    {
        taskRoutine = TaskRoutineManager.CreateTask(c);
        taskRoutine.Finished += TaskFinished;
        if (autoStart)
            Start();
    }

    /// <summary>
    /// Begins execution of the coroutine
    /// </summary>
    public void Start()
    {
        taskRoutine.Start();
    }

    /// <summary>
    /// Discontinues execution of the coroutine at its next yield.
    /// </summary>
    public void Stop()
    {
        taskRoutine.Stop();
    }

    /// <summary>
    /// Pauses the coroutine. Has no effect if the coroutine is already paused.
    /// </summary>
    public void Pause()
    {
        taskRoutine.Pause();
    }

    /// <summary>
    /// Unpauses the coroutine. Has no effect if the coroutine is not paused.
    /// </summary>
    public void Unpause()
    {
        taskRoutine.Unpause();
    }

    void TaskFinished(bool manual)
    {
        FinishedHandler handler = Finished;
        if (handler != null)
            handler(manual);
    }

    TaskRoutineManager.TaskRoutineState taskRoutine;
}

class TaskRoutineManager : MonoBehaviour
{
    public class TaskRoutineState
    {
        public bool Running
        {
            get
            {
                return running;
            }
        }

        public bool Paused
        {
            get
            {
                return paused;
            }
        }

        public delegate void FinishedHandler(bool manual);
        public event FinishedHandler Finished;

        IEnumerator coroutine;
        bool running;
        bool paused;
        bool stopped;

        public TaskRoutineState(IEnumerator c)
        {
            coroutine = c;
        }

        public void Pause()
        {
            paused = true;
        }

        public void Unpause()
        {
            paused = false;
        }

        public void Start()
        {
            running = true;
            singleton.StartCoroutine(CallWrapper());
        }

        public void Stop()
        {
            stopped = true;
            running = false;
        }

        IEnumerator CallWrapper()
        {
            yield return null;
            IEnumerator e = coroutine;
            while (running)
            {
                if (paused)
                    yield return null;
                else
                {
                    if (e != null && e.MoveNext())
                    {
                        yield return e.Current;
                    }
                    else
                    {
                        running = false;
                    }
                }
            }

            FinishedHandler handler = Finished;
            if (handler != null)
                handler(stopped);
        }
    }

    static TaskRoutineManager singleton;

    public static TaskRoutineState CreateTask(IEnumerator coroutine)
    {
        if (singleton == null)
        {
            GameObject go = new GameObject("TaskManager");
            singleton = go.AddComponent<TaskRoutineManager>();
        }
        return new TaskRoutineState(coroutine);
    }
}