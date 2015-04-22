using ColossalFramework;
using ColossalFramework.Globalization;
using ColossalFramework.UI;
using ICities;
using System.Reflection;
using UnityEngine;

namespace AdvancedBuilding
{
    class UIX
    {
        public static Texture2D LoadTextureFromAssembly(string path, bool readOnly = true)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            System.IO.Stream textureStream = assembly.GetManifestResourceStream(path);

            var buf = new byte[textureStream.Length];  //declare arraysize
            textureStream.Read(buf, 0, buf.Length); // read from stream to byte array
            var tex = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            tex.LoadImage(buf);
            tex.Apply(false, readOnly);
            return tex;
        }

        public static UITextureAtlas CreateTextureAtlas(string atlasName, string[] spriteNames, string assemblyPath)
        {

            Material baseMaterial = UIView.GetAView().defaultAtlas.material;
            const int size = 1024;
            var atlasTex = new Texture2D(size, size, TextureFormat.ARGB32, false);

            var textures = new Texture2D[spriteNames.Length];
            var rects = new Rect[spriteNames.Length];

            for (int i = 0; i < spriteNames.Length; i++)
            {
                textures[i] = LoadTextureFromAssembly(assemblyPath + spriteNames[i] + ".png", false);
            }

            rects = atlasTex.PackTextures(textures, 2, size);


            UITextureAtlas atlas = ScriptableObject.CreateInstance<UITextureAtlas>();

            // Setup atlas
            Material material = UnityEngine.Object.Instantiate(baseMaterial);
            material.mainTexture = atlasTex;
            atlas.material = material;
            atlas.name = atlasName;

            // Add SpriteInfo
            for (int i = 0; i < spriteNames.Length; i++)
            {
                var spriteInfo = new UITextureAtlas.SpriteInfo
                {
                    name = spriteNames[i],
                    texture = atlasTex,
                    region = rects[i]
                };
                atlas.AddSprite(spriteInfo);
            }
            return atlas;
        }

        public static void Panel(UIPanel panel, string title)
        {

            //Header panel
            var headerPanel = panel.AddUIComponent(typeof(UIPanel)) as UIPanel;
            headerPanel.width = panel.width;
            headerPanel.height = 40;
            headerPanel.relativePosition = Vector3.zero;

            var nameLabel = headerPanel.AddUIComponent(typeof(UILabel)) as UILabel;
            nameLabel.autoSize = false;
            nameLabel.width = panel.width;
            nameLabel.height = 40;
            nameLabel.relativePosition = Vector3.zero;
            nameLabel.textAlignment = UIHorizontalAlignment.Center;
            nameLabel.verticalAlignment = UIVerticalAlignment.Middle;
            nameLabel.textScale = 1.3125f;
            nameLabel.text = title;

            var closeButton = headerPanel.AddUIComponent(typeof(UIButton)) as UIButton;
            closeButton.width = 32;
            closeButton.height = 32;
            closeButton.normalFgSprite = "buttonclose";
            closeButton.hoveredFgSprite = "buttonclosehover";
            closeButton.pressedFgSprite = "buttonclosepressed";
            closeButton.relativePosition = new Vector3(panel.width - 40, 4);
            closeButton.eventClick += closeButton_eventClick;

            var dragHandle = headerPanel.AddUIComponent(typeof(UIDragHandle)) as UIDragHandle;
            dragHandle.width = panel.width - 40;
            dragHandle.height = 40;
            dragHandle.relativePosition = Vector3.zero;
            dragHandle.target = panel;
        }

        static void closeButton_eventClick(UIComponent component, UIMouseEventParameter eventParam)
        {
            component.parent.parent.isVisible = false;
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

        public static UILabel Label(UIComponent parent, string text, Vector3 position)
        {
            var label = Label(parent, text);
            label.relativePosition = position;
            return label;
        }

        public static UILabel Label(UIComponent parent, string text)
        {
            var label = parent.AddUIComponent<UILabel>();
            label.textAlignment = UIHorizontalAlignment.Center;
            label.verticalAlignment = UIVerticalAlignment.Middle;
            label.text = text;
            return label;
        }
    }
}
