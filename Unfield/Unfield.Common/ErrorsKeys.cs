namespace Unfield.Common;

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

    public const string IncorrectPhoneNumber = "accounts:incorrect_phone_number";
    public const string PasswordsNotEqual = "accounts:password_not_equal";
    public const string PasswordDoesntMatchConditions = "accounts:password_doesnt_match_conditions";

    public const string ModifyPermissionsCurrentRole = "accounts:modify_permissions_current_role";
    public const string ModifyStadiumsCurrentUser = "accounts:modify_stadiums_current_user";
    public const string CantDeleteSuperuser = "accounts:cant_delete_superuser";
    public const string ModifyCurrentRole = "accounts:modify_current_role";
    public const string ModifyCurrentUser = "accounts:modify_current_user";
    public const string LastUserStadiumUnbind = "accounts:last_user_stadium_unbind";
    public const string DeleteRoleHasBindings = "accounts:delete_role_has_bindings";

    #endregion

    #region offers

    public const string LockerRoomNotFound = "offers:locker_room_not_found";
    public const string FieldNotFound = "offers:field_not_found";
    public const string FieldHasChildrenFields = "offers:field_has_children_fields";
    public const string InventoryNotFound = "offers:inventory_not_found";

    #endregion

    #region rates

    public const string PriceGroupNotFound = "rates:price_group_not_found";
    public const string TariffNotFound = "rates:tariff_not_found";

    public const string PromoCodeMinLength = "rates:promocode_min_length";
    public const string PromoCodeMinValue = "rates:promocode_min_value";
    public const string PromoCodeMaxValue = "rates:promocode_max_value";
    public const string PromoCodeSameCode = "rates:promocode_same_codes";
    public const string CrossIntervals = "rates:cross_intervals";

    #endregion

    #region settings

    public const string MainSettingsInvalidOpenClosePeriod = "settings:main_invalid_open_close";
    public const string BreakNotFound = "settings:break_not_found";
    public const string InvalidBreakPeriod = "settings:invalid_break_period";

    #endregion

    #region booking

    public const string BookingNotFound = "booking:booking_not_found";
    public const string BookingError = "booking:booking_error";
    public const string InvalidAccessCode = "booking:invalid_access_code";
    public const string BookingIntersection = "booking:booking_intersection";
    
    #endregion

    #region dashboards

    public const string StadiumDashboardNotFound = "dashboards:stadium_dashboard_not_found";

    #endregion

    #region customers

    public const string RedirectTokenNotFound = "auth:redirect_token_not_found";

    #endregion
}