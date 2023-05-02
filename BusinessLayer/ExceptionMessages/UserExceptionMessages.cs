namespace BusinessLayer.ExceptionMessages
{
    public static class UserExceptionMessages
    {

        public static string NotFoundUserById = "no user with this id";

        public static string NotFoundUserByEmail = "no user with this email";

        public static string InvalidCode = "the code you entered is not valid";

        public static string IncorrectPassword = "password is not correct";

        public static string MustVerifyEmail = "you must verify your email";

        public static string ExpiredCode = "expired Code, we sent another one";

        public static string CanNotFollowYourSelf = "you can't follow your self";

        public static string AlreadyFollowUser = "you already followed this user";

        public static string NotFollowedUser = "you did't follow this user";

        public static string UserAlreadyExpert = "the user is already expert";

        public static string UserAlreadyAdmin = "the user is already expert";

        public static string UserIsAdmin = "this is admin user";

        public static string UserAlreadyBlocked = "the user already blocked";

        public static string UserAlreadyUnblocked = "the user already unblocked";

        public static string CanNotBlock = "can't block admins and experts";

        public static string BlocedUserFromPosting = "the user blocked from posting";
    }
}
