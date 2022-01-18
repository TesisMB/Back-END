namespace Entities.Helpers
{
    public class WithoutSpace_CamelCase
    {

        public static string GetCamelCase(string camelCase)
        {
            System.Globalization.TextInfo textInfo = new System.Globalization.CultureInfo("en-US", false).TextInfo;

            camelCase = camelCase.Trim();
            camelCase = textInfo.ToTitleCase(camelCase);

            return camelCase;
        }

        public static string GetWithoutSpace(string space)
        {
            var espacio = space.Trim();

            return espacio;
        }
    }
}
