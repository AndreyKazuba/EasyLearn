using EasyLearn.Infrastructure.Exceptions;
using EasyLearn.Infrastructure.UIConstants;
using System;
using System.Windows.Media;

namespace EasyLearn.Infrastructure.Helpers
{
    public static class ColorHelper
    {
        public static SolidColorBrush GetBrushByHex(this string hex) => new BrushConverter().ConvertFrom(hex) as SolidColorBrush ?? throw new Exception(ExceptionMessagesHelper.HexColorConvertionIsUnsuccessful);
        public static SolidColorBrush GetForegroundColorForRating(this int rating)
        {
            if (rating < 0 || rating > 100)
                throw new ArgumentOutOfRangeException(nameof(rating));

            if (rating < 10)
                return ColorCodes.RelationViewRating_0_10_Color.GetBrushByHex();
            else if (rating < 20)
                return ColorCodes.RelationViewRating_10_20_Color.GetBrushByHex();
            else if (rating < 30)
                return ColorCodes.RelationViewRating_20_30_Color.GetBrushByHex();
            else if (rating < 40)
                return ColorCodes.RelationViewRating_30_40_Color.GetBrushByHex();
            else if (rating < 50)
                return ColorCodes.RelationViewRating_40_50_Color.GetBrushByHex();
            else if (rating < 60)
                return ColorCodes.RelationViewRating_50_60_Color.GetBrushByHex();
            else if (rating < 70)
                return ColorCodes.RelationViewRating_60_70_Color.GetBrushByHex();
            else if (rating < 80)
                return ColorCodes.RelationViewRating_70_80_Color.GetBrushByHex();
            else if (rating < 90)
                return ColorCodes.RelationViewRating_80_90_Color.GetBrushByHex();
            else
                return ColorCodes.RelationViewRating_90_100_Color.GetBrushByHex();
        }

        public static SolidColorBrush GetBackgroundColorForRating(this int rating)
        {
            if (rating < 0 || rating > 100)
                throw new ArgumentOutOfRangeException(nameof(rating));

            if (rating < 10)
                return ColorCodes.RelationViewRating_0_10_BackgroundColor.GetBrushByHex();
            else if (rating < 20)
                return ColorCodes.RelationViewRating_10_20_BackgroundColor.GetBrushByHex();
            else if (rating < 30)
                return ColorCodes.RelationViewRating_20_30_BackgroundColor.GetBrushByHex();
            else if (rating < 40)
                return ColorCodes.RelationViewRating_30_40_BackgroundColor.GetBrushByHex();
            else if (rating < 50)
                return ColorCodes.RelationViewRating_40_50_BackgroundColor.GetBrushByHex();
            else if (rating < 60)
                return ColorCodes.RelationViewRating_50_60_BackgroundColor.GetBrushByHex();
            else if (rating < 70)
                return ColorCodes.RelationViewRating_60_70_BackgroundColor.GetBrushByHex();
            else if (rating < 80)
                return ColorCodes.RelationViewRating_70_80_BackgroundColor.GetBrushByHex();
            else if (rating < 90)
                return ColorCodes.RelationViewRating_80_90_BackgroundColor.GetBrushByHex();
            else
                return ColorCodes.RelationViewRating_90_100_BackgroundColor.GetBrushByHex();
        }

        public static SolidColorBrush GetColorForGrade(this int grade)
        {
            if (grade < 0 || grade > 100)
                throw new ArgumentOutOfRangeException(nameof(grade));

            if (grade < 10)
                return ColorCodes.Grade_0_10_Color.GetBrushByHex();
            else if (grade < 20)  
                return ColorCodes.Grade_10_20_Color.GetBrushByHex();
            else if (grade < 30)  
                return ColorCodes.Grade_20_30_Color.GetBrushByHex();
            else if (grade < 40)  
                return ColorCodes.Grade_30_40_Color.GetBrushByHex();
            else if (grade < 50)  
                return ColorCodes.Grade_40_50_Color.GetBrushByHex();
            else if (grade < 60)  
                return ColorCodes.Grade_50_60_Color.GetBrushByHex();
            else if (grade < 70)  
                return ColorCodes.Grade_60_70_Color.GetBrushByHex();
            else if (grade < 80)  
                return ColorCodes.Grade_70_80_Color.GetBrushByHex();
            else if (grade < 90)  
                return ColorCodes.Grade_80_90_Color.GetBrushByHex();
            else
                return ColorCodes.Grade_90_100_Color.GetBrushByHex();
        }
    }
}
