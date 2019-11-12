using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TownAttackRPG.Models.Actors;
using TownAttackRPG.Models.Actors.ActorProperties;
using TownAttackRPG.Models.Actors.Characters;
using TownAttackRPG.Models.Actors.SkillCollections;
using TownAttackRPG.Models.Items;
using TownAttackRPG.Models.Items.Equipment;
using TownAttackRPG.Models.Professions;
using TownAttackRPG.Models.Professions.DefaultProfessions;
using TownAttackRPG.Models.Scenarios;
using TownAttackRPG.Models.Scenarios.Campaign.Tutorial;

namespace TownAttackRPG.Models.ViewModels.Game
{
    public class GameData
    {
        public int UserID { get; set; }
        public int SaveGameSlot { get; set; }
        public Scenario ActiveScenario { get; set; }

        #region Atomization and De-Atomization
        public void Atomize()
        {
            ScenarioName = ActiveScenario.Name;
            ModuleName = ActiveScenario.ActiveModule.Name;
            ModuleTurnNumber = ActiveScenario.ActiveModule.TurnNumber;
            int i = 0;
            foreach (Actor actor in ActiveScenario.PlayerParty)
            {
                PlayerPartyNames.Add(i, actor.Name);
                PlayerPartyGenders.Add(i, actor.Gender);
                if (actor is Character)
                {
                    Character actorChar = actor as Character;
                    PlayerPartyIsCharacter.Add(i, true);
                    PlayerPartyProfessions.Add(i, actorChar.Profession.Title);
                    PlayerPartyInventoryItems.Add(i, actorChar.Inventory.Items);
                    PlayerPartyEquipmentSlots.Add(i, actorChar.Equipment.Slot);
                    PlayerPartyAttributes.Add(i, actorChar.Attributes.BaseValue);
                    PlayerPartyTalents.Add(i, actorChar.Talents.BaseValue);
                    List<int> XpLvl = new List<int>() { actorChar.XP.Current, actorChar.XP.Level };
                    PlayerPartyXpLvl.Add(i, XpLvl);
                }
                else
                {
                    PlayerPartyIsCharacter.Add(i, false);
                }
                PlayerPartySkillbars.Add(i, actor.Skillbar);
                PlayerPartyActiveEffects.Add(i, actor.ActiveEffects);
                List<int> HpSp = new List<int>() { actor.HP.Current, actor.SP.Current };
                PlayerPartyHpSp.Add(i, HpSp);
                i++;
            }
            i = 0;
            foreach (Actor actor in ActiveScenario.ActiveModule.Allies)
            {
                AllyPartyNames.Add(i, actor.Name);
                AllyPartyGenders.Add(i, actor.Gender);
                if (actor is Character)
                {
                    Character actorChar = (Character)actor;
                    AllyPartyIsCharacter.Add(i, true);
                    AllyPartyProfessions.Add(i, actorChar.Profession.Title);
                    AllyPartyInventoryItems.Add(i, actorChar.Inventory.Items);
                    AllyPartyEquipmentSlots.Add(i, actorChar.Equipment.Slot);
                    AllyPartyAttributes.Add(i, actorChar.Attributes.BaseValue);
                    AllyPartyTalents.Add(i, actorChar.Talents.BaseValue);
                }
                else
                {
                    PlayerPartyIsCharacter.Add(i, false);
                }
                AllyPartySkillbars.Add(i, actor.Skillbar);
                AllyPartyActiveEffects.Add(i, actor.ActiveEffects);
                List<int> HpSp = new List<int>() { actor.HP.Current, actor.SP.Current };
                AllyPartyHpSp.Add(i, HpSp);
                i++;
            }
            i = 0;
            // TODO: if actor is enemy, serialize as enemy
            foreach (Actor actor in ActiveScenario.ActiveModule.Enemies)
            {
                EnemyPartyNames.Add(i, actor.Name);
                EnemyPartyGenders.Add(i, actor.Gender);
                if (actor is Character)
                {
                    Character actorChar = (Character)actor;
                    EnemyPartyIsCharacter.Add(i, true);
                    EnemyPartyProfessions.Add(i, actorChar.Profession.Title);
                    EnemyPartyInventoryItems.Add(i, actorChar.Inventory.Items);
                    EnemyPartyEquipmentSlots.Add(i, actorChar.Equipment.Slot);
                    EnemyPartyAttributes.Add(i, actorChar.Attributes.BaseValue);
                    EnemyPartyTalents.Add(i, actorChar.Talents.BaseValue);
                }
                else
                {
                    PlayerPartyIsCharacter.Add(i, false);
                }
                EnemyPartySkillbars.Add(i, actor.Skillbar);
                EnemyPartyActiveEffects.Add(i, actor.ActiveEffects);
                List<int> HpSp = new List<int>() { actor.HP.Current, actor.SP.Current };
                EnemyPartyHpSp.Add(i, HpSp);
                i++;
            }

            ActiveScenario = null;
        }
        public void DeAtomize()
        {
            List<Actor> PlayerParty = new List<Actor>();
            for (int i=0; i<PlayerPartyNames.Count(); i++)
            {
                if (PlayerPartyIsCharacter[i])
                {
                    PlayerParty.Add(new Character(PlayerPartyNames[i],
                        ProfessionMaker(PlayerPartyProfessions[i], PlayerPartyGenders[i])
                        ));
                    Character character = PlayerParty[i] as Character;
                    character.XP.Level = PlayerPartyXpLvl[i][1];
                    character.XP.Current = PlayerPartyXpLvl[i][0];
                }
                else
                {
                    // must be an enemy! add behavior here
                }
                PlayerParty[i].HP.Current = PlayerPartyHpSp[i][0];
                PlayerParty[i].SP.Current = PlayerPartyHpSp[i][1];
            }
            List<Actor> AllyParty = new List<Actor>();
            for (int i = 0; i < AllyPartyNames.Count(); i++)
            {
                if (AllyPartyIsCharacter[i])
                {
                    AllyParty.Add(new Character(AllyPartyNames[i],
                        ProfessionMaker(AllyPartyProfessions[i], AllyPartyGenders[i])
                        ));
                    Character character = AllyParty[i] as Character;
                    character.XP.Level = AllyPartyXpLvl[i][1];
                    character.XP.Current = AllyPartyXpLvl[i][0];
                }
                else
                {
                    // must be an enemy! add behavior here
                }
                AllyParty[i].HP.Current = AllyPartyHpSp[i][0];
                AllyParty[i].SP.Current = AllyPartyHpSp[i][1];
            }
            List<Actor> EnemyParty = new List<Actor>();
            for (int i = 0; i < EnemyPartyNames.Count(); i++)
            {
                if (EnemyPartyIsCharacter[i])
                {
                    EnemyParty.Add(new Character(EnemyPartyNames[i],
                        ProfessionMaker(EnemyPartyProfessions[i], EnemyPartyGenders[i])
                        ));
                    Character character = EnemyParty[i] as Character;
                    character.XP.Level = EnemyPartyXpLvl[i][1];
                    character.XP.Current = EnemyPartyXpLvl[i][0];
                }
                else
                {
                    // must be an enemy! add behavior here
                }
                EnemyParty[i].HP.Current = EnemyPartyHpSp[i][0];
                EnemyParty[i].SP.Current = EnemyPartyHpSp[i][1];
            }

            ActiveScenario = ScenarioMaker(ScenarioName, ModuleName);
            ActiveScenario.PlayerParty = PlayerParty;
            ActiveScenario.ActiveModule.Allies = AllyParty;
            ActiveScenario.ActiveModule.Enemies = EnemyParty;
            ActiveScenario.UpdateModuleParty();
        }

        public Profession ProfessionMaker(string profName, string gender)
        {
            switch (profName.ToLower())
            {
                case "knight":
                    return new Knight(gender);
                case "alchemist":
                    return new Alchemist(gender);
                case "barmaid":
                    return new Barmaid(gender);
                case "blacksmith":
                    return new Blacksmith(gender);
                case "constable":
                    return new Constable(gender);
                case "convict":
                    return new Convict(gender);
                case "footman":
                    return new Footman(gender);
                case "huntress":
                    return new Huntress(gender);
                case "noble":
                    return new Noble(gender);
                case "plague doctor":
                    return new PlagueDoctor(gender);
                case "scholar":
                    return new Scholar(gender);
                case "squire":
                    return new Squire(gender);
                case "pony":
                    return new SecretProfession(gender);
                default:
                    throw new ArgumentException("Invalid profession chosen");
            }
        }
        public Scenario ScenarioMaker(string scenarioName, string moduleName)
        {
            Scenario scenario;
            switch (scenarioName.ToLower())
            {
                
                case "tutorial":
                    scenario = new Tutorial();
                    break;
                case "townattackclassic":
                    throw new NotImplementedException("Town Attack Classic is not yet implemented.");
                default:
                    throw new ArgumentException("Error: invalid scenario name given!");
            }
            Module module = ( from i in scenario.Modules
                              where moduleName == i.Name
                              select i)
                              .ToList<Module>()[0];

            scenario.ActiveModule = module;
            module.TurnNumber = ModuleTurnNumber;
            return scenario;
        }
        #endregion

        #region Scenario
        public string ScenarioName { get; set; }
        public string ModuleName { get; set; }
        public int ModuleTurnNumber { get; set; }
        #endregion

        #region Player Party
        public Dictionary<int, bool> PlayerPartyIsCharacter { get; set; } = new Dictionary<int, bool>();
        public Dictionary<int, string> PlayerPartyNames { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> PlayerPartyProfessions { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> PlayerPartyGenders { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, Dictionary<string, int>> PlayerPartyAttributes { get; set; } = new Dictionary<int, Dictionary<string, int>>();
        public Dictionary<int, Dictionary<string, int>> PlayerPartyTalents { get; set; } = new Dictionary<int, Dictionary<string, int>>();
        public Dictionary<int, Skillbar> PlayerPartySkillbars { get; set; } = new Dictionary<int, Skillbar>();
        public Dictionary<int, List<Item>> PlayerPartyInventoryItems { get; set; } = new Dictionary<int, List<Item>>();
        public Dictionary<int, Dictionary<string, EquipmentItem>> PlayerPartyEquipmentSlots {get; set;} = new Dictionary<int, Dictionary<string, EquipmentItem>>();
        public Dictionary<int, ActiveEffects> PlayerPartyActiveEffects { get; set; } = new Dictionary<int, ActiveEffects>();
        public Dictionary<int, List<int>> PlayerPartyHpSp { get; set; } = new Dictionary<int, List<int>>();
        public Dictionary<int, List<int>> PlayerPartyXpLvl { get; set; } = new Dictionary<int, List<int>>();
        #endregion

        #region Allies
        public Dictionary<int, bool> AllyPartyIsCharacter { get; set; } = new Dictionary<int, bool>();
        public Dictionary<int, string> AllyPartyNames { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> AllyPartyProfessions { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> AllyPartyGenders { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, Dictionary<string, int>> AllyPartyAttributes { get; set; } = new Dictionary<int, Dictionary<string, int>>();
        public Dictionary<int, Dictionary<string, int>> AllyPartyTalents { get; set; } = new Dictionary<int, Dictionary<string, int>>();
        public Dictionary<int, Skillbar> AllyPartySkillbars { get; set; } = new Dictionary<int, Skillbar>();
        public Dictionary<int, List<Item>> AllyPartyInventoryItems { get; set; } = new Dictionary<int, List<Item>>();
        public Dictionary<int, Dictionary<string, EquipmentItem>> AllyPartyEquipmentSlots { get; set; } = new Dictionary<int, Dictionary<string, EquipmentItem>>();
        public Dictionary<int, ActiveEffects> AllyPartyActiveEffects { get; set; } = new Dictionary<int, ActiveEffects>();
        public Dictionary<int, List<int>> AllyPartyHpSp { get; set; } = new Dictionary<int, List<int>>();
        public Dictionary<int, List<int>> AllyPartyXpLvl { get; set; } = new Dictionary<int, List<int>>();
        #endregion

        #region Enemies
        public Dictionary<int, bool> EnemyPartyIsCharacter { get; set; } = new Dictionary<int, bool>();
        public Dictionary<int, string> EnemyPartyNames { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> EnemyPartyProfessions { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, string> EnemyPartyGenders { get; set; } = new Dictionary<int, string>();
        public Dictionary<int, Dictionary<string, int>> EnemyPartyAttributes { get; set; } = new Dictionary<int, Dictionary<string, int>>();
        public Dictionary<int, Dictionary<string, int>> EnemyPartyTalents { get; set; } = new Dictionary<int, Dictionary<string, int>>();
        public Dictionary<int, Skillbar> EnemyPartySkillbars { get; set; } = new Dictionary<int, Skillbar>();
        public Dictionary<int, List<Item>> EnemyPartyInventoryItems { get; set; } = new Dictionary<int, List<Item>>();
        public Dictionary<int, Dictionary<string, EquipmentItem>> EnemyPartyEquipmentSlots { get; set; } = new Dictionary<int, Dictionary<string, EquipmentItem>>();
        public Dictionary<int, ActiveEffects> EnemyPartyActiveEffects { get; set; } = new Dictionary<int, ActiveEffects>();
        public Dictionary<int, List<int>> EnemyPartyHpSp { get; set; } = new Dictionary<int, List<int>>();
        public Dictionary<int, List<int>> EnemyPartyXpLvl { get; set; } = new Dictionary<int, List<int>>();
        #endregion
    }
}
