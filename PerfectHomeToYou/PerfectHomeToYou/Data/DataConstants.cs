namespace PerfectHomeToYou.Data
{
    public class DataConstants
    {
        public class Apartment
        {
            public const int FloorMaxLength = 15;
        }

        public class Neighborhood
        {
            public const int NameMaxLength = 40;
        }

        public class City
        {
            public const int NameMaxLength = 40;
            public const int PostcodeMaxLength = 4;
        }

        public class Client
        {
            public const int FirstNameMinLength = 3;
            public const int FirstNameMaxLength = 30;
            public const int LastNameMinLength = 5;
            public const int LastNameMaxLength = 30;
            public const int PhoneNumberMinLength = 10;
            public const int PhoneNumberMaxLength = 15;
        }

        public class Question
        {
            public const int MessageMaxLength = 500;
        }
    }
}
