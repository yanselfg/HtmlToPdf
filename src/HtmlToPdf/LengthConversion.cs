namespace HtmlToPdf
{
    public static class LengthConversion
    {
        public static Length Millimeters(this double length) => Length.Millimeters(length);

        public static Length Centimeters(this double length) => Length.Centimeters(length);

        public static Length Inches(this double length) => Length.Inches(length);

        public static Length Millimeters(this float length) => Length.Millimeters(length);

        public static Length Centimeters(this float length) => Length.Centimeters(length);

        public static Length Inches(this float length) => Length.Inches(length);

        public static Length Millimeters(this int length) => Length.Millimeters(length);

        public static Length Centimeters(this int length) => Length.Centimeters(length);

        public static Length Inches(this int length) => Length.Inches(length);
    }
}
