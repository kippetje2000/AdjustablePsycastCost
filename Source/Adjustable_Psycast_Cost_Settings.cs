using HarmonyLib;
using RimWorld;
using SettingsHelper;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;

namespace Adjustable_Psycast_Cost
{
    public class Adjustable_Psycast_Cost_Settings : ModSettings
    {
        public Dictionary<string, float> psycastCostAll = new Dictionary<string, float>();
        public Dictionary<string, float> psycastCostSkip = new Dictionary<string, float>();
        public Dictionary<string, float> psycastCostPsychic = new Dictionary<string, float>();
        public Dictionary<string, float> psycastCostWordOf = new Dictionary<string, float>();
        public float Painblock = 2;
        public float Stun = 1;
        public float Burden = 1;
        public float BlindingPulse = 1;
        public float Beckon = 1;
        public float VertigoPulse = 2;
        public float ChaosSkip = 2;
        public float Skip = 2;
        public float Wallraise = 2;
        public float Smokepop = 2;
        public float Focus = 3;
        public float Berserk = 4;
        public float Invisibility = 3;
        public float BerserkPulse = 6;
        public float ManhunterPulse = 4;
        public float MassChaosSkip = 3;
        public float Waterskip = 1.5f;
        public float Flashstorm = 4;
        public float BulletShield = 4;
        public float SolarPinhole = 8;
        public float Farskip = 70;
        public float Neuroquake = 50;
        public float Chunkskip = 40;
        public float WordOfTrust = 60;
        public float WordOfJoy = 40;
        public float WordOfLove = 50;
        //public float WordOfSerenity = 30;
        public float WordOfInspiration = 80;
        public override void ExposeData()
        {

            //Psycast
            Scribe_Values.Look(ref Painblock, "Painblock", 2);
            Scribe_Values.Look(ref Stun, "Stun", 1);
            Scribe_Values.Look(ref Burden, "Burden", 1);
            Scribe_Values.Look(ref BlindingPulse, "BlindingPulse", 1);
            Scribe_Values.Look(ref Beckon, "Beckon", 1);
            Scribe_Values.Look(ref VertigoPulse, "VertigoPulse", 2);
            Scribe_Values.Look(ref ChaosSkip, "ChaosSkip", 2);
            Scribe_Values.Look(ref Skip, "Skip", 2);
            Scribe_Values.Look(ref Wallraise, "Wallraise", 2);
            Scribe_Values.Look(ref Smokepop, "Smokepop", 2);
            Scribe_Values.Look(ref Focus, "Focus", 3);
            Scribe_Values.Look(ref Berserk, "Berserk", 4);
            Scribe_Values.Look(ref Invisibility, "Invisibility", 3);
            Scribe_Values.Look(ref BerserkPulse, "BerserkPulse", 6);
            Scribe_Values.Look(ref ManhunterPulse, "ManhunterPulse", 4);
            Scribe_Values.Look(ref MassChaosSkip, "MassChaosSkip", 3);
            Scribe_Values.Look(ref MassChaosSkip, "MassChaosSkip", 3);
            Scribe_Values.Look(ref Waterskip, "Waterskip", 1.5f);
            Scribe_Values.Look(ref Flashstorm, "Flashstorm", 4);
            Scribe_Values.Look(ref BulletShield, "BulletShield", 4);
            Scribe_Values.Look(ref SolarPinhole, "SolarPinhole", 8);
            Scribe_Values.Look(ref Farskip, "Farskip", 70);
            Scribe_Values.Look(ref Neuroquake, "Neuroquake", 50);
            Scribe_Values.Look(ref Chunkskip, "Chunkskip", 40);
            Scribe_Values.Look(ref WordOfTrust, "WordOfTrust", 60);
            Scribe_Values.Look(ref WordOfJoy, "WordOfJoy", 40);
            Scribe_Values.Look(ref WordOfLove, "WordOfLove", 50);
            //Scribe_Values.Look(ref WordOfSerenity, "WordOfSerenity", 30);
            Scribe_Values.Look(ref WordOfInspiration, "WordOfInspiration", 80);
            base.ExposeData();
        }
    }

    public class Adjustable_Psycast_Cost : Mod
    {

        public static Adjustable_Psycast_Cost_Settings settings;
        private Vector2 scrollPosition;
        private static float totalContentHeight = 1150f;
        private const float ScrollBarWidthMargin = 18f;

        public Adjustable_Psycast_Cost(ModContentPack content) : base(content)
        {
            settings = GetSettings<Adjustable_Psycast_Cost_Settings>();
            if (settings.psycastCostAll.Count == 0)
            {
                AddPsycastCostToDictionary();
            }
        }
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Rect rectWeCanSee = inRect.ContractedBy(10f);
            rectWeCanSee.height -= 50f; // "close" button
            bool scrollBarVisible = totalContentHeight > rectWeCanSee.height;
            Rect rectThatHasEverything = new Rect(0f, 0f, rectWeCanSee.width - (scrollBarVisible ? ScrollBarWidthMargin : 0), totalContentHeight);

            //Menu
            Widgets.BeginScrollView(rectWeCanSee, ref scrollPosition, rectThatHasEverything);
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(rectThatHasEverything);

            //Psycast
            listingStandard.Label("After changing values of a psycast the cost will update and work ingame but: \nThe cost % in the info will not be updated. For this to update a restart is needed");
            listingStandard.AddHorizontalLine();

            //Skip
            listingStandard.Label("\nPsycast Category: Skip");
            foreach (KeyValuePair<string, float> dictionary in settings.psycastCostSkip.ToList())
            {
                string name = dictionary.Key.ToString();
                float value = settings.psycastCostAll.GetValueSafe(name);
                listingStandard.AddLabeledSlider("Psyfocuscost for the psycast: " + name + "(" + value + ") %", ref value, 0, 100, "0", "100", 0.5f);
                settings.psycastCostAll[name] = value;
            }
            listingStandard.AddHorizontalLine();

            //Psychic
            listingStandard.Label("\nPsycast Category: Psychic");
            foreach (KeyValuePair<string, float> dictionary in settings.psycastCostPsychic.ToList())
            {
                string name = dictionary.Key.ToString();
                float value = settings.psycastCostAll.GetValueSafe(name);
                listingStandard.AddLabeledSlider("Psyfocuscost for the psycast: " + name + "(" + value + ") %", ref value, 0, 100, "0", "100", 0.5f);
                settings.psycastCostAll[name] = value;
            }
            listingStandard.AddHorizontalLine();

            //WordOf
            listingStandard.Label("\nPsycast Category: WordOf");
            foreach (KeyValuePair<string, float> dictionary in settings.psycastCostWordOf.ToList())
            {
                string name = dictionary.Key.ToString();
                float value = settings.psycastCostAll.GetValueSafe(name);
                listingStandard.AddLabeledSlider("Psyfocuscost for the psycast: " + name + "(" + value + ") %", ref value, 0, 100, "0", "100", 0.5f);
                settings.psycastCostAll[name] = value;
            }
            listingStandard.AddHorizontalLine();

            listingStandard.End();
            Widgets.EndScrollView();

            //Save settings
            Rect applyButton = inRect.BottomPart(0.1f).LeftPart(0.1f);
            bool apply = Widgets.ButtonText(applyButton, "Apply Settings", true, true, true);
            if (apply)
            {
                Adjustable_Psycast_Cost.ApplySettings();
            }

            //Reset settings
            Rect resetButton = inRect.BottomPart(0.1f).RightPart(0.1f);
            bool reset = Widgets.ButtonText(resetButton, "Reset Settings", true, true, true);
            if (reset)
            {
                Adjustable_Psycast_Cost.ResetSettings();
            }

            base.DoSettingsWindowContents(inRect);
        }

        private static void ApplySettings()
        {
            //Skip
            settings.Skip = settings.psycastCostAll.GetValueSafe("Skip");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Skip").statBases, StatDefOf.Ability_PsyfocusCost, settings.Skip / 100);
            settings.ChaosSkip = settings.psycastCostAll.GetValueSafe("ChaosSkip");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("ChaosSkip").statBases, StatDefOf.Ability_PsyfocusCost, settings.ChaosSkip / 100);
            settings.Chunkskip = settings.psycastCostAll.GetValueSafe("Chunkskip");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Chunkskip").statBases, StatDefOf.Ability_PsyfocusCost, settings.Chunkskip / 100);
            settings.MassChaosSkip = settings.psycastCostAll.GetValueSafe("MassChaosSkip");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("MassChaosSkip").statBases, StatDefOf.Ability_PsyfocusCost, settings.MassChaosSkip / 100);
            settings.Farskip = settings.psycastCostAll.GetValueSafe("Farskip");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Farskip").statBases, StatDefOf.Ability_PsyfocusCost, settings.Farskip / 100);
            settings.Wallraise = settings.psycastCostAll.GetValueSafe("Wallraise");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Wallraise").statBases, StatDefOf.Ability_PsyfocusCost, settings.Wallraise / 100);
            settings.Smokepop = settings.psycastCostAll.GetValueSafe("Smokepop");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Smokepop").statBases, StatDefOf.Ability_PsyfocusCost, settings.Smokepop / 100);
            settings.Waterskip = settings.psycastCostAll.GetValueSafe("Waterskip");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Waterskip").statBases, StatDefOf.Ability_PsyfocusCost, settings.Waterskip / 100);
            settings.Flashstorm = settings.psycastCostAll.GetValueSafe("Flashstorm");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Flashstorm").statBases, StatDefOf.Ability_PsyfocusCost, settings.Flashstorm / 100);
            settings.BulletShield = settings.psycastCostAll.GetValueSafe("BulletShield");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("BulletShield").statBases, StatDefOf.Ability_PsyfocusCost, settings.BulletShield / 100);
            settings.SolarPinhole = settings.psycastCostAll.GetValueSafe("SolarPinhole");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("SolarPinhole").statBases, StatDefOf.Ability_PsyfocusCost, settings.SolarPinhole / 100);

            //Psychic
            settings.Painblock = settings.psycastCostAll.GetValueSafe("Painblock");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Painblock").statBases, StatDefOf.Ability_PsyfocusCost, settings.Painblock / 100);
            settings.Stun = settings.psycastCostAll.GetValueSafe("Stun");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Stun").statBases, StatDefOf.Ability_PsyfocusCost, settings.Stun / 100);
            settings.Burden = settings.psycastCostAll.GetValueSafe("Burden");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Burden").statBases, StatDefOf.Ability_PsyfocusCost, settings.Burden / 100);
            settings.BlindingPulse = settings.psycastCostAll.GetValueSafe("BlindingPulse");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("BlindingPulse").statBases, StatDefOf.Ability_PsyfocusCost, settings.BlindingPulse / 100);
            settings.Beckon = settings.psycastCostAll.GetValueSafe("Beckon");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Beckon").statBases, StatDefOf.Ability_PsyfocusCost, settings.Beckon / 100);
            settings.VertigoPulse = settings.psycastCostAll.GetValueSafe("VertigoPulse");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("VertigoPulse").statBases, StatDefOf.Ability_PsyfocusCost, settings.VertigoPulse / 100);
            settings.Focus = settings.psycastCostAll.GetValueSafe("Focus");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Focus").statBases, StatDefOf.Ability_PsyfocusCost, settings.Focus / 100);
            settings.Berserk = settings.psycastCostAll.GetValueSafe("Berserk");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Berserk").statBases, StatDefOf.Ability_PsyfocusCost, settings.Berserk / 100);
            settings.Invisibility = settings.psycastCostAll.GetValueSafe("Invisibility");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Invisibility").statBases, StatDefOf.Ability_PsyfocusCost, settings.Invisibility / 100);
            settings.BerserkPulse = settings.psycastCostAll.GetValueSafe("BerserkPulse");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("BerserkPulse").statBases, StatDefOf.Ability_PsyfocusCost, settings.BerserkPulse / 100);
            settings.ManhunterPulse = settings.psycastCostAll.GetValueSafe("ManhunterPulse");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("ManhunterPulse").statBases, StatDefOf.Ability_PsyfocusCost, settings.ManhunterPulse / 100);
            settings.Neuroquake = settings.psycastCostAll.GetValueSafe("Neuroquake");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Neuroquake").statBases, StatDefOf.Ability_PsyfocusCost, settings.Neuroquake / 100);

            //WordOf
            settings.WordOfTrust = settings.psycastCostAll.GetValueSafe("WordOfTrust");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("WordOfTrust").statBases, StatDefOf.Ability_PsyfocusCost, settings.WordOfTrust / 100);
            settings.WordOfJoy = settings.psycastCostAll.GetValueSafe("WordOfJoy");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("WordOfJoy").statBases, StatDefOf.Ability_PsyfocusCost, settings.WordOfJoy / 100);
            settings.WordOfLove = settings.psycastCostAll.GetValueSafe("WordOfLove");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("WordOfLove").statBases, StatDefOf.Ability_PsyfocusCost, settings.WordOfLove / 100);
            //settings.WordOfSerenity = settings.psycastCostAll.GetValueSafe("WordOfSerenity");
            //StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("WordOfSerenity").statBases, StatDefOf.Ability_PsyfocusCost, settings.WordOfSerenity / 100);
            settings.WordOfInspiration = settings.psycastCostAll.GetValueSafe("WordOfInspiration");
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("WordOfInspiration").statBases, StatDefOf.Ability_PsyfocusCost, settings.WordOfInspiration / 100);
        }

        private static void ResetSettings()
        {
            //Skip
            settings.Skip = 2;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Skip").statBases, StatDefOf.Ability_PsyfocusCost, settings.Skip / 100);
            settings.psycastCostAll.SetOrAdd("Skip", 2);
            settings.ChaosSkip = 2;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("ChaosSkip").statBases, StatDefOf.Ability_PsyfocusCost, settings.ChaosSkip / 100);
            settings.psycastCostAll.SetOrAdd("ChaosSkip", 2);
            settings.Chunkskip = 40;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Chunkskip").statBases, StatDefOf.Ability_PsyfocusCost, settings.Chunkskip / 100);
            settings.psycastCostAll.SetOrAdd("Chunkskip", 40);
            settings.MassChaosSkip = 3;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("MassChaosSkip").statBases, StatDefOf.Ability_PsyfocusCost, settings.MassChaosSkip / 100);
            settings.psycastCostAll.SetOrAdd("MassChaosSkip", 3);
            settings.Farskip = 70;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Farskip").statBases, StatDefOf.Ability_PsyfocusCost, settings.Farskip / 100);
            settings.psycastCostAll.SetOrAdd("Farskip", 70);
            settings.Wallraise = 2;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Wallraise").statBases, StatDefOf.Ability_PsyfocusCost, settings.Wallraise / 100);
            settings.psycastCostAll.SetOrAdd("Wallraise", 2);
            settings.Smokepop = 2;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Smokepop").statBases, StatDefOf.Ability_PsyfocusCost, settings.Smokepop / 100);
            settings.psycastCostAll.SetOrAdd("Smokepop", 2);
            settings.Waterskip = 1.5f;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Waterskip").statBases, StatDefOf.Ability_PsyfocusCost, settings.Waterskip / 100);
            settings.psycastCostAll.SetOrAdd("Waterskip", 1.5f);
            settings.Flashstorm = 4;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Flashstorm").statBases, StatDefOf.Ability_PsyfocusCost, settings.Flashstorm / 100);
            settings.psycastCostAll.SetOrAdd("Flashstorm", 4);
            settings.BulletShield = 4;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("BulletShield").statBases, StatDefOf.Ability_PsyfocusCost, settings.BulletShield / 100);
            settings.psycastCostAll.SetOrAdd("BulletShield", 4);
            settings.SolarPinhole = 8;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("SolarPinhole").statBases, StatDefOf.Ability_PsyfocusCost, settings.SolarPinhole / 100);
            settings.psycastCostAll.SetOrAdd("SolarPinhole", 8);

            //Psychic
            settings.Painblock = 2;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Painblock").statBases, StatDefOf.Ability_PsyfocusCost, settings.Painblock / 100);
            settings.psycastCostAll.SetOrAdd("Painblock", 2);
            settings.Stun = 1;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Stun").statBases, StatDefOf.Ability_PsyfocusCost, settings.Stun / 100);
            settings.psycastCostAll.SetOrAdd("Stun", 1);
            settings.Burden = 1;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Burden").statBases, StatDefOf.Ability_PsyfocusCost, settings.Burden / 100);
            settings.psycastCostAll.SetOrAdd("Burden", 1);
            settings.BlindingPulse = 1;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("BlindingPulse").statBases, StatDefOf.Ability_PsyfocusCost, settings.BlindingPulse / 100);
            settings.psycastCostAll.SetOrAdd("BlindingPulse", 1);
            settings.Beckon = 1;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Beckon").statBases, StatDefOf.Ability_PsyfocusCost, settings.Beckon / 100);
            settings.psycastCostAll.SetOrAdd("Beckon", 1);
            settings.VertigoPulse = 2;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("VertigoPulse").statBases, StatDefOf.Ability_PsyfocusCost, settings.VertigoPulse / 100);
            settings.psycastCostAll.SetOrAdd("VertigoPulse", 2);
            settings.Focus = 3;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Focus").statBases, StatDefOf.Ability_PsyfocusCost, settings.Focus / 100);
            settings.psycastCostAll.SetOrAdd("Focus", 3); ;
            settings.Berserk = 4;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Berserk").statBases, StatDefOf.Ability_PsyfocusCost, settings.Berserk / 100);
            settings.psycastCostAll.SetOrAdd("Berserk", 4);
            settings.Invisibility = 3;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Invisibility").statBases, StatDefOf.Ability_PsyfocusCost, settings.Invisibility / 100);
            settings.psycastCostAll.SetOrAdd("Invisibility", 3);
            settings.BerserkPulse = 6;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("BerserkPulse").statBases, StatDefOf.Ability_PsyfocusCost, settings.BerserkPulse / 100);
            settings.psycastCostAll.SetOrAdd("BerserkPulse", 6);
            settings.ManhunterPulse = 4;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("ManhunterPulse").statBases, StatDefOf.Ability_PsyfocusCost, settings.ManhunterPulse / 100);
            settings.psycastCostAll.SetOrAdd("ManhunterPulse", 4);
            settings.Neuroquake = 50;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("Neuroquake").statBases, StatDefOf.Ability_PsyfocusCost, settings.Neuroquake / 100);
            settings.psycastCostAll.SetOrAdd("Neuroquake", 50);

            //WordOf
            settings.WordOfTrust = 60;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("WordOfTrust").statBases, StatDefOf.Ability_PsyfocusCost, settings.WordOfTrust / 100);
            settings.psycastCostAll.SetOrAdd("WordOfTrust", 50);
            settings.WordOfJoy = 40;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("WordOfJoy").statBases, StatDefOf.Ability_PsyfocusCost, settings.WordOfJoy / 100);
            settings.psycastCostAll.SetOrAdd("WordOfJoy", 40);
            settings.WordOfLove = 50;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("WordOfLove").statBases, StatDefOf.Ability_PsyfocusCost, settings.WordOfLove / 100);
            settings.psycastCostAll.SetOrAdd("WordOfLove", 50);
            //settings.WordOfSerenity = 30;
            //StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("WordOfSerenity").statBases, StatDefOf.Ability_PsyfocusCost, settings.WordOfSerenity / 100);
            //settings.psycastCostAll.SetOrAdd("WordOfSerenity", 30);
            settings.WordOfInspiration = 80;
            StatUtility.SetStatValueInList(ref DefDatabase<AbilityDef>.GetNamed("WordOfInspiration").statBases, StatDefOf.Ability_PsyfocusCost, settings.WordOfInspiration / 100);
            settings.psycastCostAll.SetOrAdd("WordOfInspiration", 80);
        }

        public void AddPsycastCostToDictionary()
        {
            //Skip
            settings.psycastCostAll.Add("Skip", settings.Skip);
            settings.psycastCostAll.Add("ChaosSkip", settings.ChaosSkip);
            settings.psycastCostAll.Add("Chunkskip", settings.Chunkskip);
            settings.psycastCostAll.Add("MassChaosSkip", settings.MassChaosSkip);
            settings.psycastCostAll.Add("Farskip", settings.Farskip);
            settings.psycastCostAll.Add("Wallraise", settings.Wallraise);
            settings.psycastCostAll.Add("Smokepop", settings.Smokepop);
            settings.psycastCostAll.Add("Waterskip", settings.Waterskip);
            settings.psycastCostAll.Add("Flashstorm", settings.Flashstorm);
            settings.psycastCostAll.Add("BulletShield", settings.BulletShield);
            settings.psycastCostAll.Add("SolarPinhole", settings.SolarPinhole);

            //Psychic
            settings.psycastCostAll.Add("Painblock", settings.Painblock);
            settings.psycastCostAll.Add("Stun", settings.Stun);
            settings.psycastCostAll.Add("Burden", settings.Burden);
            settings.psycastCostAll.Add("BlindingPulse", settings.BlindingPulse);
            settings.psycastCostAll.Add("Beckon", settings.Beckon);
            settings.psycastCostAll.Add("VertigoPulse", settings.VertigoPulse);
            settings.psycastCostAll.Add("Focus", settings.Focus);
            settings.psycastCostAll.Add("Berserk", settings.Berserk);
            settings.psycastCostAll.Add("Invisibility", settings.Invisibility);
            settings.psycastCostAll.Add("BerserkPulse", settings.BerserkPulse);
            settings.psycastCostAll.Add("ManhunterPulse", settings.ManhunterPulse);
            settings.psycastCostAll.Add("Neuroquake", settings.Neuroquake);

            //WordOf
            settings.psycastCostAll.Add("WordOfTrust", settings.WordOfTrust);
            settings.psycastCostAll.Add("WordOfJoy", settings.WordOfJoy);
            settings.psycastCostAll.Add("WordOfLove", settings.WordOfLove);
            //settings.psycastCostAll.Add("WordOfSerenity", settings.WordOfSerenity);
            settings.psycastCostAll.Add("WordOfInspiration", settings.WordOfInspiration);


            //This part is only useful for displaying the settings menu
            //Skip
            settings.psycastCostSkip.Add("Skip", 2f);
            settings.psycastCostSkip.Add("ChaosSkip", 2f);
            settings.psycastCostSkip.Add("Chunkskip", 40f);
            settings.psycastCostSkip.Add("MassChaosSkip", 3f);
            settings.psycastCostSkip.Add("Farskip", 70f);
            settings.psycastCostSkip.Add("Wallraise", 2f);
            settings.psycastCostSkip.Add("Smokepop", 2f);
            settings.psycastCostSkip.Add("Waterskip", 1.5f);
            settings.psycastCostSkip.Add("Flashstorm", 4f);
            settings.psycastCostSkip.Add("BulletShield", 4f);
            settings.psycastCostSkip.Add("SolarPinhole", 8f);

            //Psychic
            settings.psycastCostPsychic.Add("Painblock", 2f);
            settings.psycastCostPsychic.Add("Stun", 1f);
            settings.psycastCostPsychic.Add("Burden", 1f);
            settings.psycastCostPsychic.Add("BlindingPulse", 1f);
            settings.psycastCostPsychic.Add("Beckon", 1f);
            settings.psycastCostPsychic.Add("VertigoPulse", 2f);
            settings.psycastCostPsychic.Add("Focus", 3f);
            settings.psycastCostPsychic.Add("Berserk", 4f);
            settings.psycastCostPsychic.Add("Invisibility", 3f);
            settings.psycastCostPsychic.Add("BerserkPulse", 6f);
            settings.psycastCostPsychic.Add("ManhunterPulse", 4f);
            settings.psycastCostPsychic.Add("Neuroquake", 50f);

            //WordOf
            settings.psycastCostWordOf.Add("WordOfTrust", 60f);
            settings.psycastCostWordOf.Add("WordOfJoy", 40f);
            settings.psycastCostWordOf.Add("WordOfLove", 50f);
            //settings.psycastCostWordOf.Add("WordOfSerenity", 30f);
            settings.psycastCostWordOf.Add("WordOfInspiration", 80f);
        }
        public override string SettingsCategory()
        {
            return "Adjustable_Psycast_Cost".Translate();
        }

        [StaticConstructorOnStartup]
        internal static class StartupAdjustable_Psycast_Cost
        {
            static StartupAdjustable_Psycast_Cost()
            {
                Harmony harmony = new Harmony("KipsMods.AdjustablePsycastCost");
                harmony.PatchAll();
            }
        }
    }

}
