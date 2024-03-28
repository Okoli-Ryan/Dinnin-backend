using System.Collections;

namespace OrderUp_API.Constants {

    public class Permission {
        private Guid PermissionID { get; set; }
        private string PermissionName { get; set; }
        private PermissionCategory PermissionCategory { get; set; }

        public Permission(PermissionCategory permissionCategory, PermissionNameType permissionName) {
            PermissionID = Guid.NewGuid();
            PermissionName = $"{permissionCategory}.{permissionName}";
            PermissionCategory = permissionCategory;
        }

        public string GetPermissionName(PermissionCategory permissionCategory, PermissionNameType permissionName) {
            return $"{permissionCategory}.{permissionName}";
        }

        public static List<Permission> GetPermissions() {
            List<Permission> permissions = new() {

                new Permission(PermissionCategory.Analytics, PermissionNameType.BREAKDOWN),
                new Permission(PermissionCategory.Analytics, PermissionNameType.ORDER_AMOUNT),
                new Permission(PermissionCategory.Analytics, PermissionNameType.ORDER_COUNT),
                new Permission(PermissionCategory.Analytics, PermissionNameType.ORDER_ITEM_COUNT),
                new Permission(PermissionCategory.Orders, PermissionNameType.VIEW_ORDERS),
                new Permission(PermissionCategory.Orders, PermissionNameType.UPDATE_ORDERS),
                new Permission(PermissionCategory.MenuCategory, PermissionNameType.UPDATE_MENU),
                new Permission(PermissionCategory.MenuCategory, PermissionNameType.DELETE_MENU),
                new Permission(PermissionCategory.MenuCategory, PermissionNameType.CREATE_MENU),
                new Permission(PermissionCategory.MenuItem, PermissionNameType.CREATE_MENU_ITEM),
                new Permission(PermissionCategory.MenuItem, PermissionNameType.UPDATE_MENU_ITEM),
                new Permission(PermissionCategory.MenuItem, PermissionNameType.DELETE_MENU_ITEM),
                new Permission(PermissionCategory.Table, PermissionNameType.CREATE_TABLE),
                new Permission(PermissionCategory.Table, PermissionNameType.UPDATE_TABLE),
                new Permission(PermissionCategory.Table, PermissionNameType.DELETE_TABLE),
                new Permission(PermissionCategory.Staff, PermissionNameType.CREATE_STAFF),
                new Permission(PermissionCategory.Staff, PermissionNameType.UPDATE_STAFF),
                new Permission(PermissionCategory.Staff, PermissionNameType.DELETE_STAFF),
                new Permission(PermissionCategory.Restaurant, PermissionNameType.UPDATE_RESTAURANT),
            };

            return permissions;
        }


    }

    public enum PermissionCategory {
        Analytics,
        Orders,
        MenuCategory,
        MenuItem,
        Table,
        Staff,
        Restaurant
    }

    public enum PermissionNameType {
        BREAKDOWN,
        ORDER_AMOUNT,
        ORDER_COUNT,
        ORDER_ITEM_COUNT,
        VIEW_ORDERS,
        UPDATE_ORDERS,
        UPDATE_MENU,
        DELETE_MENU,
        CREATE_MENU,
        CREATE_MENU_ITEM,
        UPDATE_MENU_ITEM,
        DELETE_MENU_ITEM,
        CREATE_TABLE,
        UPDATE_TABLE,
        DELETE_TABLE,
        CREATE_STAFF,
        UPDATE_STAFF,
        DELETE_STAFF,
        UPDATE_RESTAURANT
    }

}
