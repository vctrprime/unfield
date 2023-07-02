namespace StadiumEngine.Common;

public class SmsTemplates
{
    public const string SendPasswordRu = "Ваш пароль для входа: {0}";
    public const string SendPasswordEn = "Your login password: {0}";

    public const string SendAccessCodeRu = "Бронирование № {0}\n{1}\nКод подтверждения: {2}";
    public const string SendAccessCodeEn = "Booking № {0}\n{1}\nAccess code: {2}";
    
    public const string SendConfirmationRu = "Бронирование № {0} подтверждено!{1}";
    public const string SendConfirmationEn = "Booking № {0} confirmed!{1}";
    
    public const string SendConfirmationLockerRoomRu = "\nНазначена раздевалка: {0}";
    public const string SendConfirmationLockerRoomEn = "\nLocker room: {0}";
}