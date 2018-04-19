﻿using ff14bot;
using ff14bot.Directors;
using ff14bot.Managers;
using ff14bot.RemoteAgents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Deep.Helpers
{
    /// <summary>
    /// helper class for wrapping director calls
    /// </summary>
    public static class DeepDungeonManager
    {
        public static InstanceContentDirector Director => DirectorManager.ActiveDirector as InstanceContentDirector;

        public static bool BossFloor => Director != null ? (Director.DeepDungeonLevel % 10 == 0) : false;
        public static DDInventoryItem GetInventoryItem(Pomander pom)
        {
            return Director.DeepDungeonInventory[(byte)pom - 1];
        }

        public static bool IsCasting => Core.Me.IsCasting;

        public static void UsePomander(Pomander pom)
        {
            AgentModule.GetAgentInterfaceByType<AgentDeepDungeonInformation>().UsePomander(pom);
        }

        public static int PortalStatus => Director.DeepDungeonPortalStatus;
        public static int Level => Director.DeepDungeonLevel;

        public static bool PortalActive => Director.DeepDungeonPortalStatus == 11;
        public static bool ReturnActive => Director.DeepDungeonReturnStatus == 11;

        
    }
}