namespace StadiumEngine.Common;

public static class ErrorsKeys
{
    #region accounts

    public const string Forbidden = "accounts:forbidden";
    public const string InvalidLogin = "accounts:invalid_login";
    public const string InvalidPassword = "accounts:invalid_password";
    public const string LoginAlreadyExist = "accounts:login_already_exist";
    
    public const string UserNotFound = "accounts:user_not_found";
    public const string StadiumNotFound = "accounts:stadium_not_found";
    public const string RoleNotFound = "accounts:role_not_found";
    public const string UserRolesDoesntHaveStadiums = "accounts:user_role_doesnt_have_stadium";

    public const string IncorrectPhoneNumber = "accounts:incorrect_phone_number";
    public const string PasswordsNotEqual = "accounts:password_not_equal";
    public const string PasswordDoesntMatchConditions = "accounts:password_doesnt_match_conditions";

    public const string ModifyPermissionsCurrentRole = "accounts:modify_permissions_current_role";
    public const string ModifyStadiumsCurrentRole = "accounts:modify_stadiums_current_role";
    public const string CantDeleteSuperuser = "accounts:cant_delete_superuser";
    public const string ModifyCurrentRole = "accounts:modify_current_role";
    public const string ModifyCurrentUser = "accounts:modify_current_user"; 
    public const string LastRoleStadiumUnbind = "accounts:last_role_stadium_unbind"; 
    public const string DeleteRoleHasBindings = "accounts:delete_role_has_bindings"; 

    #endregion

    #region offers
    public const string LockerRoomNotFound = "offers:locker_room_not_found";
    public const string FieldNotFound = "offers:field_not_found";
    public const string FieldHasChildrenFields = "offers:field_has_children_fields";
    public const string InventoryNotFound = "offers:inventory_not_found";

    #endregion

}