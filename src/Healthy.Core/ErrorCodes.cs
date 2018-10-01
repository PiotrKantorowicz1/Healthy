namespace Healthy.Core
{
    public static class ErrorCodes
    {
        public static string Success => "success";
        public static string UserNotFound => "user_not_found";
        public static string ProductNotFound => "product_not_found";
        public static string SlotNotFound => "slot_not_found";
        public static string DailyWorkoutNotFound => "daily_workout_not_found";
        public static string MealNotFound => "meal_not_found";
        public static string MealItemNotFound => "meal_item_not_found";
        public static string DailySupplementationNotFound => "daily_supplementation_not_found";
        public static string InvalidCategory => "invalid_category";
        public static string InvalidDay => "invalid_day";
        public static string NameNotProvided => "name_not_provided";
        public static string InvalidProductName => "invalid_product_name";
        public static string IntervalNotProvided => "interval_not-provided";
        public static string DescriptionNotProvided => "description_not_provided";
        public static string InvalidDescription => "invalid_description";
        public static string CategoryNotProvided => "category_not_provided";
        public static string InvalidMealNumber => "invalid_meal_number";
        public static string NutritionValuesNotProvided => "nutrition_value_not_provided";
        public static string InvalidQuantity => "invalidQuantity";
        public static string ToManySlots => "to_many_slots";
        public static string UserIdInUse => "user_id_in_use";
        public static string InactiveUser => "inactive_user";
        public static string SessionNotFound => "session_not_found";
        public static string InvalidSessionKey => "invalid_session_key";
        public static string RefreshTokenAlreadyRevoked => "refresh_token_already_revoked";
        public static string RefreshTokenNotFound => "refresh_token_not_found";
        public static string SessionExpired => "session_expired";
        public static string SessionRefreshed => "session_refreshed";
        public static string SessionDestroyed => "session_destroyed";
        public static string InvalidCredentials => "invalid_credentials";
        public static string InvalidRole => "invalid_role";
        public static string EmailInUse => "email_in_use";
        public static string NameInUse => "name_in_use";
        public static string NameAlreadySet => "name_already_set";
        public static string InvalidAccountType => "invalid_account_type";
        public static string InvalidName => "invalid_name";
        public static string InvalidEmail => "invalid_email";
        public static string InvalidPassword => "invalid_password";
        public static string InvalidFile => "invalid_file";
        public static string InvalidCurrentPassword => "invalid_current_password";
        public static string EmailNotFound => "email_not_found";
        public static string OwnerAlreadyExists => "owner_already_exists";
        public static string OwnerCannotBeLocked => "owner_cannot_be_locked";
        public static string InvalidPasswordResetToken => "invalid_password_reset_token";
        public static string FileTooBig => "file_too_big";
        public static string TextTooLong => "text_too_long";
        public static string OperationNotFound => "operation_not_found";
        public static string InvalidSecuredOperation => "invalid_secured_operation";
        public static string Error => "error";
        public static string InvalidAvatar => "invalid_avatar";
        public static string InvalidUser => "invalid_user";
        public static string UserAlreadyLocked => "user_already_locked";
        public static string UserNotLocked => "user_not_locked";
        public static string UserAlreadyActive => "user_already_active";
        public static string UserAlreadyDeleted => "user_already_deleted";
        public static string UserAlreadyUnconfirmed => "user_already_unconfirmed";
    }
}