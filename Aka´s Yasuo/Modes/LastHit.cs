﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using SharpDX;

namespace AkaYasuo.Modes
{
    partial class LastHit
    {
        public static void LastHitmode()
        {
            foreach (Obj_AI_Base minion in EntityManager.MinionsAndMonsters.GetLaneMinions(EntityManager.UnitTeam.Enemy, Variables._Player.ServerPosition, Program.Q.Range, true).OrderByDescending(m => m.Health))
            {
                if (minion == null)
                {
                    return;
                }

                if (!minion.IsDead && MenuManager.LastHitMenu["Q"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady() && minion.IsValidTarget() && !Variables.Q3READY(Variables._Player))
                {
                    var predHealth = Prediction.Health.GetPrediction(minion, (int)(Variables._Player.Distance(minion.Position) * 1000 / 2000));
                    if (predHealth <= Variables._Player.GetSpellDamage(minion, SpellSlot.Q))
                    {
                        Program.Q.Cast(minion.ServerPosition);
                    }
                }
                if (!minion.IsDead && MenuManager.LastHitMenu["Q3"].Cast<CheckBox>().CurrentValue && Program.Q.IsReady() && minion.IsValidTarget() && Variables.Q3READY(Variables._Player))
                {
                    var predHealth = Prediction.Health.GetPrediction(minion, (int)(Variables._Player.Distance(minion.Position) * 1000 / 2000));
                    if (predHealth <= Variables._Player.GetSpellDamage(minion, SpellSlot.Q))
                    {
                        Program.Q.Cast(minion.ServerPosition);
                    }
                }
                if (MenuManager.LastHitMenu["E"].Cast<CheckBox>().CurrentValue && Program.E.IsReady() && minion.IsValidTarget())
                {
                    if (!Variables.UnderTower((Vector3) Variables.PosAfterE(minion)))
                    {
                        var predHealth = Prediction.Health.GetPrediction(minion, (int)(Variables._Player.Distance(minion.Position) * 1000 / 2000));
                        if (predHealth <= Variables._Player.GetSpellDamage(minion, SpellSlot.E))
                        {
                            Program.E.Cast(minion);
                        }
                    }
                }
            }
        }
    }
}
