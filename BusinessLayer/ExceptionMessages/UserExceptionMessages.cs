namespace BusinessLayer.ExceptionMessages
{
    public static class UserExceptionMessages
    {

        public static string NotFoundUserById = "no user with this id";

        public static string NotFoundUserByEmail = "no user with this email";

        public static string EmailAlreadyExsist = "this email is already exist";

        public static string InvalidCode = "invalid code";

        public static string IncorrectPassword = "password is not correct";

        public static string MustVerifyEmail = "you must verify your email";

        public static string ExpiredCode = "expired code, we sent another one";

        public static string CanNotFollowYourSelf = "you can't follow your self";

        public static string AlreadyFollowUser = "you already followed this user";

        public static string NotFollowedUser = "you did't follow this user";

        public static string UserIsAdmin = "this is admin user";

        public static string UserAlreadyBlocked = "the user already blocked";

        public static string UserAlreadyUnblocked = "the user already unblocked";

        public static string CanNotBlock = "can't block admins and experts";

        public static string BlocedUserFromPosting = "the user blocked from posting";

        public static string UserAlreadyHaveRole = "the user already have this role";

        public static string DontHavePermission = "you don't have permisstion for that";

        public static string AlreadyHaveProfilePicture = "user already have profile picture";

        public static string DoNotHaveProfilePicture = "user don't have profile picture";
    }
}
