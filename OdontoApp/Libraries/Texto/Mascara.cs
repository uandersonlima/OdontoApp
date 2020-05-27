namespace OdontoApp.Libraries.Texto
{
    public static class Mascara
    {
        public static string FormatarReal(decimal valor)
        {
            return $"R$ {valor}";
        }

        public static float ConverterIntToDecimal(int valor)
        {
            //10000 -> "10000" -> "100.00" -> 100.00
            string valorPagarMeString = valor.ToString();
            string valorDecimalString = valorPagarMeString.Substring(0, valorPagarMeString.Length - 2) + "," + valorPagarMeString.Substring(valorPagarMeString.Length - 2);

            var dec = float.Parse(valorDecimalString);

            return dec;
        }
        public static string PrimeiroNome(string nomeCompleto)
        {
            return nomeCompleto.Split(' ')[0];
        }
    }
}
