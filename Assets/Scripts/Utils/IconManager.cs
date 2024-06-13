using System.Collections.Generic;
using UnityEngine;

public class IconManager : MonoBehaviour {

    public enum LabelIcon {
        Gray = 0,
        Blue,
        Teal,
        Green,
        Yellow,
        Orange,
        Red,
        Purple
    }

    public enum Icon {
        CircleGray = 0,
        CircleBlue,
        CircleTeal,
        CircleGreen,
        CircleYellow,
        CircleOrange,
        CircleRed,
        CirclePurple,
        DiamondGray,
        DiamondBlue,
        DiamondTeal,
        DiamondGreen,
        DiamondYellow,
        DiamondOrange,
        DiamondRed,
        DiamondPurple
    }

    private static Dictionary<LabelIcon, Color> labelIconColors;
    private static Dictionary<Icon, Color> iconColors;

    private void Start() {
        InitializeIconColors();
    }

    private void InitializeIconColors() {
        labelIconColors = new Dictionary<LabelIcon, Color> {
            { LabelIcon.Gray, Color.gray },
            { LabelIcon.Blue, Color.blue },
            { LabelIcon.Teal, Color.cyan },
            { LabelIcon.Green, Color.green },
            { LabelIcon.Yellow, Color.yellow },
            { LabelIcon.Orange, new Color(1.0f, 0.5f, 0.0f) }, // Orange
            { LabelIcon.Red, Color.red },
            { LabelIcon.Purple, new Color(0.5f, 0.0f, 0.5f) } // Purple
        };

        iconColors = new Dictionary<Icon, Color> {
            { Icon.CircleGray, Color.gray },
            { Icon.CircleBlue, Color.blue },
            { Icon.CircleTeal, Color.cyan },
            { Icon.CircleGreen, Color.green },
            { Icon.CircleYellow, Color.yellow },
            { Icon.CircleOrange, new Color(1.0f, 0.5f, 0.0f) }, // Orange
            { Icon.CircleRed, Color.red },
            { Icon.CirclePurple, new Color(0.5f, 0.0f, 0.5f) }, // Purple
            { Icon.DiamondGray, Color.gray },
            { Icon.DiamondBlue, Color.blue },
            { Icon.DiamondTeal, Color.cyan },
            { Icon.DiamondGreen, Color.green },
            { Icon.DiamondYellow, Color.yellow },
            { Icon.DiamondOrange, new Color(1.0f, 0.5f, 0.0f) }, // Orange
            { Icon.DiamondRed, Color.red },
            { Icon.DiamondPurple, new Color(0.5f, 0.0f, 0.5f) } // Purple
        };
    }

    public static void SetLabelIcon(GameObject gObj, LabelIcon icon) {
        if (labelIconColors == null) {
            Debug.LogWarning("Icon colors not initialized. Call InitializeIconColors in Start.");
            return;
        }

        Color color;
        if (labelIconColors.TryGetValue(icon, out color)) {
            ApplyIcon(gObj, color);
        } else {
            Debug.LogWarning("Icon color not found.");
        }
    }

    public static void SetIcon(GameObject gObj, Icon icon) {
        if (iconColors == null) {
            Debug.LogWarning("Icon colors not initialized. Call InitializeIconColors in Start.");
            return;
        }

        Color color;
        if (iconColors.TryGetValue(icon, out color)) {
            ApplyIcon(gObj, color);
        } else {
            Debug.LogWarning("Icon color not found.");
        }
    }

    private static void ApplyIcon(GameObject gObj, Color color) {
        // Adding a custom component to handle drawing the icon
        var iconDrawer = gObj.GetComponent<IconDrawer>();
        if (iconDrawer == null) {
            iconDrawer = gObj.AddComponent<IconDrawer>();
        }
        iconDrawer.SetColor(color);
    }

    // This component is responsible for drawing the icon using Gizmos
    public class IconDrawer : MonoBehaviour {
        private Color iconColor;

        public void SetColor(Color color) {
            iconColor = color;
        }

        private void OnDrawGizmos() {
            Gizmos.color = iconColor;
            Gizmos.DrawSphere(transform.position, 0.5f); // Draw a sphere at the GameObject's position
        }
    }
}