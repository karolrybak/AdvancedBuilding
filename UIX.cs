using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.UI;
using ICities;
using UnityEngine;

namespace AdvancedBuilding
{
    class UIX
    {
        public static void Panel()
        {

            ////Header panel
            //var headerPanel = panel.AddUIComponent(typeof(UIPanel)) as UIPanel;
            //headerPanel.width = 360;
            //headerPanel.height = 40;

            //var nameLabel = headerPanel.AddUIComponent(typeof(UILabel)) as UILabel;
            //nameLabel.autoSize = false;
            //nameLabel.width = 360;
            //nameLabel.height = 40;
            //nameLabel.relativePosition = Vector3.zero;
            //nameLabel.textAlignment = UIHorizontalAlignment.Center;
            //nameLabel.verticalAlignment = UIVerticalAlignment.Middle;
            //nameLabel.textScale = 1.3125f;
            //nameLabel.text = "Advanced building";

            ////var closeButton = headerPanel.AddUIComponent(typeof(UIButton)) as UIButton;
            ////closeButton.width = 32;
            ////closeButton.height = 32;
            ////closeButton.normalFgSprite = "buttonclose";
            ////closeButton.hoveredFgSprite = "buttonclosehover";
            ////closeButton.pressedFgSprite = "buttonclosepressed";
            ////closeButton.relativePosition = new Vector3(324, 4);
            ////closeButton.eventClick += CloseWindow;

            //var dragHandle = headerPanel.AddUIComponent(typeof(UIDragHandle)) as UIDragHandle;
            //dragHandle.width = 320;
            //dragHandle.height = 40;
            //dragHandle.relativePosition = Vector3.zero;
            //dragHandle.target = panel;
        }

        public static void Button(UIButton button, string texture, Vector2 size)
        {
            button.normalBgSprite = texture;
            button.disabledBgSprite = texture + "Disabled";
            button.hoveredBgSprite = texture + "Focused";
            button.focusedBgSprite = texture + "Focused";
            button.pressedBgSprite = texture + "Pressed";
            button.size = size;
        }

        public static void DropDown(UIDropDown dropdown)
        {
            dropdown.size = new Vector2(180.0f, 32.0f);
            dropdown.relativePosition = new Vector3(10.0f, 78.0f);
            dropdown.listBackground = "GenericPanelLight";
            dropdown.itemHeight = 32;
            dropdown.itemHover = "ListItemHover";
            dropdown.itemHighlight = "ListItemHighlight";
            dropdown.normalBgSprite = "ButtonMenu";
            dropdown.listWidth = 200;
            dropdown.listHeight = 500;
            dropdown.foregroundSpriteMode = UIForegroundSpriteMode.Stretch;
            dropdown.popupColor = new Color32(45, 52, 61, 255);
            dropdown.popupTextColor = new Color32(170, 170, 170, 255);
            dropdown.zOrder = 1;
            dropdown.textScale = 0.8f;
            dropdown.verticalAlignment = UIVerticalAlignment.Middle;
            dropdown.horizontalAlignment = UIHorizontalAlignment.Left;
            dropdown.selectedIndex = 0;
            dropdown.textFieldPadding = new RectOffset(8, 0, 8, 0);
            dropdown.itemPadding = new RectOffset(14, 0, 0, 0);

            var dropdownButton = dropdown.AddUIComponent<UIButton>();
            dropdown.triggerButton = dropdownButton;

            dropdownButton.text = "";
            dropdownButton.size = dropdown.size;
            dropdownButton.relativePosition = new Vector3(0.0f, 0.0f);
            dropdownButton.textVerticalAlignment = UIVerticalAlignment.Middle;
            dropdownButton.textHorizontalAlignment = UIHorizontalAlignment.Left;
            dropdownButton.normalFgSprite = "IconDownArrow";
            dropdownButton.hoveredFgSprite = "IconDownArrowHovered";
            dropdownButton.pressedFgSprite = "IconDownArrowPressed";
            dropdownButton.focusedFgSprite = "IconDownArrowFocused";
            dropdownButton.disabledFgSprite = "IconDownArrowDisabled";
            dropdownButton.foregroundSpriteMode = UIForegroundSpriteMode.Fill;
            dropdownButton.horizontalAlignment = UIHorizontalAlignment.Right;
            dropdownButton.verticalAlignment = UIVerticalAlignment.Middle;
            dropdownButton.zOrder = 0;
            dropdownButton.textScale = 0.8f;
        }
    }
}
