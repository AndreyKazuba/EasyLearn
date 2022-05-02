namespace EasyLearn.Infrastructure.Enums
{
    public enum DictationDirection
    {
        /// <summary>
        /// Отображаются русские слова, необходимо указывать английский перевод.
        /// </summary>
        Directly = 0,

        /// <summary>
        /// Отображаются английские слова, необходимо указывать русский перевод.
        /// </summary>
        Opposite = 1,
    }
}
