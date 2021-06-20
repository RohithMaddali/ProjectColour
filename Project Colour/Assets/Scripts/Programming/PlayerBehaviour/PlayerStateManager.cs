using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RileyMcGowan
{
    public class PlayerStateManager : MonoBehaviour
    {
        //Initialise the state manager
        private DelegateStateManager stateManager = new DelegateStateManager();

        //Initialise states
        private DelegateState respawn = new DelegateState();

        private void Start()
        {
            //Declare functions to run
            respawn.Enter = StartRespawn;
            respawn.Update = UpdateRespawn;
            respawn.Exit = ExitRespawn;

            //Change the initial state
            stateManager.ChangeState(respawn);
        }

        private void FixedUpdate()
        {
            //Make the update... update.
            stateManager.UpdateCurrentState();
        }

        /// <summary>
        /// Respawn State
        /// </summary>
        private void StartRespawn()
        {
        }

        private void UpdateRespawn()
        {
        }

        private void ExitRespawn()
        {
        }
    }
}