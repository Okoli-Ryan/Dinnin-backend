using System.Collections;

namespace OrderUp_API.Constants {

    public class Permissions {
        private PermissionName PermissionName { get; set; }
        private string PermissionCategory { get; set; }

        public Permissions(PermissionName permissionName) {
            string[] permissionParts = permissionName.ToString().Split("__");
            if (permissionParts.Length != 2) {
                throw new ArgumentException("Invalid PermissionName format. Expected format: {category}__{name}");
            }

            PermissionCategory = permissionParts[0];
            PermissionName = permissionName;
        }


        public static List<Permissions> GetPermissions() {
            List<Permissions> permissions = new();

            for (int i = 0; i < Enum.GetNames(typeof(PermissionName)).Length; i++) {
                permissions.Add(new Permissions((PermissionName)i));
            }

            return permissions;
        }


    }

    public enum PermissionName {
        // Analytics Permissions
        ANALYTICS__BREAKDOWN,
        ANALYTICS__ORDER_AMOUNT,
        ANALYTICS__ORDER_COUNT,
        ANALYTICS__ORDER_ITEM_COUNT,

        // Orders Permissions
        ORDERS__VIEW_ORDERS,
        ORDERS__UPDATE_ORDERS,

        // Menu Permissions (replace "MenuCategory" with "Menu")
        MENU__UPDATE_MENU,
        MENU__DELETE_MENU,
        MENU__CREATE_MENU,

        // MenuItem Permissions
        MENU_ITEM__CREATE_MENU_ITEM,
        MENU_ITEM__UPDATE_MENU_ITEM,
        MENU_ITEM__DELETE_MENU_ITEM,

        // Table Permissions
        TABLE__CREATE_TABLE,
        TABLE__UPDATE_TABLE,
        TABLE__DELETE_TABLE,

        // Staff Permissions
        STAFF__CREATE_STAFF,
        STAFF__UPDATE_STAFF,
        STAFF__DELETE_STAFF,

        // Restaurant Permissions
        RESTAURANT__UPDATE_RESTAURANT,
    }

}
