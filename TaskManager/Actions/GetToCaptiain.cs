﻿using Buddy.Coroutines;
using Deep.Logging;
using ff14bot;
using ff14bot.Behavior;
using ff14bot.Managers;
using ff14bot.Pathing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deep.TaskManager.Actions
{
    class GetToCaptiain : ITask
    {
        public string Name => "GetToCaptain";

        public async Task<bool> Run()
        {
            //we are inside POTD
            if (Constants.InDeepDungeon || Constants.InExitLevel) return false;

            if (WorldManager.ZoneId != Constants.SouthShroudZoneId ||
               GameObjectManager.GetObjectByNPCId(Constants.CaptainNpcId) == null ||
               GameObjectManager.GetObjectByNPCId(Constants.CaptainNpcId).Distance2D(Core.Me.Location) > 35)
            {
                if (Core.Me.IsCasting)
                {
                    await Coroutine.Sleep(500);
                    return true;
                }

                if (!WorldManager.TeleportById(5))
                {
                    Logger.Error("We can't get to Quarrymill. something is very wrong...");
                    TreeRoot.Stop();
                    return false;
                }
                await Coroutine.Sleep(1000);
                return true;

            }
            if (GameObjectManager.GetObjectByNPCId(Constants.CaptainNpcId) == null || GameObjectManager.GetObjectByNPCId(Constants.CaptainNpcId).Distance2D(Core.Me.Location) > 6f)
            {
                return await CommonTasks.MoveAndStop(new MoveToParameters(Constants.CaptainNpcPosition, "Moving toward NPC"), 6f, true);
            }
            return false;
        }

        public void Tick()
        {
            
        }
    }
}
