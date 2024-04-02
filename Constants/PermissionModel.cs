using System.Collections;

namespace OrderUp_API.Constants {

    public class PermissionModel {

        public static readonly List<PermissionName> PermissionList = new();

        public static List<Permission> GetSeedData() {

            List<Permission> permissions = new();
            var count = 1;

            foreach (var permission in PermissionDictionary) {

                permissions.Add(new Permission() {
                    ID = count,
                    Name = permission.Key.ToString(),
                    Alias = permission.Value.Alias,
                    Category = permission.Key.ToString().Split("__")[0]
                });

                count++;
            }

            return permissions;
        }

        public static readonly Dictionary<PermissionName, Permission> PermissionDictionary = new() {
            [PermissionName.ANALYTICS__BREAKDOWN] = new Permission(PermissionName.ANALYTICS__BREAKDOWN, "Can View Analytics Breakdown"),
            [PermissionName.ANALYTICS__ORDER_AMOUNT] = new Permission(PermissionName.ANALYTICS__ORDER_AMOUNT, "Can View Analytics Order Amount"),
            [PermissionName.ANALYTICS__ORDER_COUNT] = new Permission(PermissionName.ANALYTICS__ORDER_COUNT, "Can View Analytics Order Count"),
            [PermissionName.ANALYTICS__ORDER_ITEM_COUNT] = new Permission(PermissionName.ANALYTICS__ORDER_ITEM_COUNT, "Can View Analytics Order Item Count"),

            [PermissionName.ORDERS__VIEW_ORDERS] = new Permission(PermissionName.ORDERS__VIEW_ORDERS, "Can View Orders"),
            [PermissionName.ORDERS__UPDATE_ORDERS] = new Permission(PermissionName.ORDERS__UPDATE_ORDERS, "Can Update Orders"),

            [PermissionName.MENU__UPDATE_MENU] = new Permission(PermissionName.MENU__UPDATE_MENU, "Can Update Menu"),
            [PermissionName.MENU__DELETE_MENU] = new Permission(PermissionName.MENU__DELETE_MENU, "Can Delete Menu"),
            [PermissionName.MENU__CREATE_MENU] = new Permission(PermissionName.MENU__CREATE_MENU, "Can Create Menu"),

            [PermissionName.MENU_ITEM__CREATE_MENU_ITEM] = new Permission(PermissionName.MENU_ITEM__CREATE_MENU_ITEM, "Can Create Menu Item"),
            [PermissionName.MENU_ITEM__UPDATE_MENU_ITEM] = new Permission(PermissionName.MENU_ITEM__UPDATE_MENU_ITEM, "Can Update Menu Item"),
            [PermissionName.MENU_ITEM__DELETE_MENU_ITEM] = new Permission(PermissionName.MENU_ITEM__DELETE_MENU_ITEM, "Can Delete Menu Item"),

            [PermissionName.TABLE__CREATE_TABLE] = new Permission(PermissionName.TABLE__CREATE_TABLE, "Can Create Table"),
            [PermissionName.TABLE__UPDATE_TABLE] = new Permission(PermissionName.TABLE__UPDATE_TABLE, "Can Update Table"),
            [PermissionName.TABLE__DELETE_TABLE] = new Permission(PermissionName.TABLE__DELETE_TABLE, "Can Delete Table"),

            [PermissionName.STAFF__CREATE_STAFF] = new Permission(PermissionName.STAFF__CREATE_STAFF, "Can Create Staff"),
            [PermissionName.STAFF__UPDATE_STAFF] = new Permission(PermissionName.STAFF__UPDATE_STAFF, "Can Update Staff"),
            [PermissionName.STAFF__DELETE_STAFF] = new Permission(PermissionName.STAFF__DELETE_STAFF, "Can Delete Staff"),

            [PermissionName.RESTAURANT__UPDATE_RESTAURANT] = new Permission(PermissionName.RESTAURANT__UPDATE_RESTAURANT, "Can Update Restaurant"),
        };
    };

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
