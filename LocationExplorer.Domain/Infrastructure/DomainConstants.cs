namespace LocationExplorer.Domain.Infrastructure
{
    public static class DomainConstants
    {
        public const int MaxNamesLength = 100;
        public const int MinNamesLength = 2;

        public const int UserPasswordMaxLength = 100;
        public const int UserPasswordMinLength = 6;

        public const int PhoneNumberMaxLength = 20;
        public const int PhoneNumberMinLength = 7;

        public const int ArticelTitleMinLength = 2;
        public const int ArticelTitleMaxLength = 150;

        public const int ArticleContentMaxLength = 12000;

        public const int CountryNameMaxLength = 50;

        public const int DestinationNameMaxLength = 70;

        public const int GalleryNameMaxLength = 100;

        public const int PhotographerNameMaxLength = MaxNamesLength * 2;

        public const int PictureNameMaxLength = 50;
        public const int PictureLocationNameMaxLength = 50;
        public const int PictureDescriptionMaxLength = 200;

        public const int RegionNameMaxLength = 80;
        public const int RegionDescriptionMaxLength = 250;

        public const int TagNameMaxLength = 25;
    }
}
