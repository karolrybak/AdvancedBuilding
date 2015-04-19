using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.UI;
using ICities;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace AdvancedBuilding
{
    public class Mod : IUserMod
    {
        public string Description
        {
            get { return "Allows building zoned buildings."; }
        }

        public string Name
        {
            get { return "Advanced building tool"; }
        }
    }

    public class LoadingExtension : LoadingExtensionBase
    {
        UIPanel panel;
        UIButton mainButton;
        UIDropDown serviceDropdown;
        UIDropDown itemsDropdown;
        UIDropDown modesDropdown;
        UIDropDown levelDropdown;
        UIDropDown netToolDropdown;
        UIDropDown propToolDropdown;
        List<UIButton> buttons;

        void buttonClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            if(component == mainButton)
            {
                panel.isVisible = !panel.isVisible;
            }
            else if ((BuildingInfo)component.objectUserData != null)
            {
                GameObject.FindObjectOfType<BuildingTool>().m_prefab = (BuildingInfo)component.objectUserData;
                GameObject.FindObjectOfType<BuildingTool>().enabled = true;
            }
        }

        public void ServiceDropdown()
        {
            serviceDropdown = panel.AddUIComponent<UIDropDown>();
            UIX.DropDown(serviceDropdown);
            var values = Enum.GetValues(typeof(ItemClass.Service));

            foreach (var itemClass in values)
            {
                var name = Enum.GetName(typeof(ItemClass.Service), itemClass);
                serviceDropdown.AddItem(name);
            }

            
            serviceDropdown.relativePosition = new Vector3(10.0f, 10.0f);
            serviceDropdown.selectedIndex = 0;
            serviceDropdown.eventSelectedIndexChanged += serviceDropdown_eventSelectedIndexChanged;
        }

        void serviceDropdown_eventSelectedIndexChanged(UIComponent component, int value)
        {
            RefreshButtons();
        }

        public void LevelDropdown()
        {
            levelDropdown = panel.AddUIComponent<UIDropDown>();
            UIX.DropDown(levelDropdown);
            var values = Enum.GetValues(typeof(ItemClass.Level));

            levelDropdown.AddItem("All");

            foreach (var itemClass in values)
            {
                var name = Enum.GetName(typeof(ItemClass.Level), itemClass);
                levelDropdown.AddItem(name);
            }


            levelDropdown.relativePosition = new Vector3(10.0f, 50.0f);
            levelDropdown.selectedIndex = 0;
            levelDropdown.eventSelectedIndexChanged += levelDropdown_eventSelectedIndexChanged; 
        }

        void levelDropdown_eventSelectedIndexChanged(UIComponent component, int value)
        {
            RefreshButtons();
        }

        void ItemsDropdown()
        {
            itemsDropdown = panel.AddUIComponent<UIDropDown>();
            UIX.DropDown(itemsDropdown);
            itemsDropdown.relativePosition = new Vector3(10.0f, 90.0f);
            itemsDropdown.selectedIndex = 0;
            itemsDropdown.eventSelectedIndexChanged += itemsDropdown_eventSelectedIndexChanged;
            itemsDropdown.eventClick += itemsDropdown_eventClick;
        }

        void itemsDropdown_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            itemsDropdown_eventSelectedIndexChanged(component, 0);
        }

        void itemsDropdown_eventSelectedIndexChanged(UIComponent component, int value)
        {
            
            for (uint i = 0; i <= PrefabCollection<BuildingInfo>.PrefabCount(); i++)
            {
                var prefab = PrefabCollection<BuildingInfo>.GetPrefab(i);
                
                if (prefab.name == itemsDropdown.selectedValue)
                {
                    GameObject.FindObjectOfType<BuildingTool>().m_prefab = prefab;
                    GameObject.FindObjectOfType<BuildingTool>().enabled = true;
                    modesDropdown.selectedValue = prefab.m_placementMode.ToString();
                }
            }

        }

        void ModesDropdown()
        {
            modesDropdown = panel.AddUIComponent<UIDropDown>();
            UIX.DropDown(modesDropdown);
            modesDropdown.relativePosition = new Vector3(10.0f, 130.0f);
            var values = Enum.GetValues(typeof(BuildingInfo.PlacementMode));

            
            foreach (var itemClass in values)
            {
                var name = Enum.GetName(typeof(BuildingInfo.PlacementMode), itemClass);
                modesDropdown.AddItem(name);
            }

            modesDropdown.selectedIndex = 0;
            modesDropdown.eventSelectedIndexChanged += modesDropdown_eventSelectedIndexChanged;
        }

        void modesDropdown_eventSelectedIndexChanged(UIComponent component, int value)
        {
            var values = Enum.GetValues(typeof(BuildingInfo.PlacementMode));
            var targetMode = BuildingInfo.PlacementMode.Roadside;
            foreach (var itemClass in values)
            {
                var name = Enum.GetName(typeof(BuildingInfo.PlacementMode), itemClass);
                if(name == modesDropdown.selectedValue)
                {
                    targetMode = (BuildingInfo.PlacementMode)itemClass;
                }
            }

            GameObject.FindObjectOfType<BuildingTool>().m_prefab.m_placementMode = targetMode;
        }

        void NetToolDropdown()
        {
            netToolDropdown = panel.AddUIComponent<UIDropDown>();
            UIX.DropDown(netToolDropdown);
            netToolDropdown.relativePosition = new Vector3(10.0f, 190.0f);
            

            for (uint i = 0; i < PrefabCollection<NetInfo>.PrefabCount(); i++ )
            {
                var prefab = PrefabCollection<NetInfo>.GetPrefab(i);
                if(prefab != null)
                    netToolDropdown.AddItem(prefab.name);
            }
            netToolDropdown.selectedIndex = 0;
            netToolDropdown.eventSelectedIndexChanged += netToolDropdown_eventSelectedIndexChanged;
            netToolDropdown.eventClick += netToolDropdown_eventClick;
        }

        void netToolDropdown_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            netToolDropdown_eventSelectedIndexChanged(component, 0);
        }

        void netToolDropdown_eventSelectedIndexChanged(UIComponent component, int value)
        {
            for (uint i = 0; i < PrefabCollection<NetInfo>.PrefabCount(); i++)
            {
                var prefab = PrefabCollection<NetInfo>.GetPrefab(i);
                if(prefab.name == netToolDropdown.selectedValue)
                {
                    var netTool = GameObject.FindObjectOfType<NetTool>();
                    netTool.m_prefab = prefab;
                    netTool.enabled = true;
                }
            }
        }

        void PropToolDropdown()
        {
            propToolDropdown = panel.AddUIComponent<UIDropDown>();
            UIX.DropDown(propToolDropdown);
            propToolDropdown.relativePosition = new Vector3(10.0f, 230.0f);
            

            for (uint i = 0; i < PrefabCollection<PropInfo>.PrefabCount(); i++)
            {
                var prefab = PrefabCollection<PropInfo>.GetPrefab(i);
                if (prefab != null)
                    propToolDropdown.AddItem(prefab.name);
            }
            propToolDropdown.selectedIndex = 0;
            propToolDropdown.eventSelectedIndexChanged += propToolDropdown_eventSelectedIndexChanged;
        }

        void propToolDropdown_eventSelectedIndexChanged(UIComponent component, int value)
        {            
            for (uint i = 0; i < PrefabCollection<PropInfo>.PrefabCount(); i++)
            {
                var prefab = PrefabCollection<PropInfo>.GetPrefab(i);
                if ((prefab != null) && prefab.name == propToolDropdown.selectedValue)
                {
                    var netTool = GameObject.FindObjectOfType<PropTool>();
                    netTool.m_prefab = prefab;
                    netTool.enabled = true;
                }
            }
        }
            
        

        public void RefreshButtons()
        {
            itemsDropdown.items = new string[0];
            
            for (uint i = 0; i < PrefabCollection<BuildingInfo>.PrefabCount(); i++)
            {
                var prefab = PrefabCollection<BuildingInfo>.GetPrefab(i);

                if(prefab.GetService().ToString() == serviceDropdown.selectedValue)
                {
                    if(prefab.m_class.m_level.ToString() == levelDropdown.selectedValue || levelDropdown.selectedValue == "All")
                        itemsDropdown.AddItem(prefab.name);
                }
            }
        }

        public void CreateButtons()
        {
            buttons = new List<UIButton>();
            UIView uiView = UIView.GetAView();
            
            UIComponent tsBar = uiView.FindUIComponent("TSBar");
            if (mainButton == null)
            {
                panel = tsBar.AddUIComponent<UIPanel>();
                panel.backgroundSprite = "SubcategoriesPanel";
                panel.name = "BuildingPanel";
                panel.isVisible = false;
                panel.width = 200;
                panel.height = 400;
                panel.absolutePosition = new Vector2(40, 80);


                var bulldozeButton = UIView.GetAView().FindUIComponent<UIMultiStateButton>("BulldozerButton");
                mainButton = bulldozeButton.parent.AddUIComponent<UIButton>();

                UIX.Button(mainButton, "buttonclose", new Vector2(43, 49));

                mainButton.relativePosition = new Vector2
                (
                    bulldozeButton.relativePosition.x + bulldozeButton.width / 2.0f - (mainButton.width + bulldozeButton.width) * 2,
                    bulldozeButton.relativePosition.y + bulldozeButton.height / 2.0f - mainButton.height / 2.0f
                );

                mainButton.eventClick += buttonClick;
                mainButton.name = "AdvancedBuildButton";

                ServiceDropdown();
                LevelDropdown();
                ItemsDropdown();
                ModesDropdown();
                NetToolDropdown();
                PropToolDropdown();
                RefreshButtons();
            }
        }

        public override void OnLevelLoaded(LoadMode mode)
        {            
            CreateButtons();    
        }
    }
}
