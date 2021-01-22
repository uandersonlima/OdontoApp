using System;
using System.Linq;

namespace OdontoApp.Libraries.Texto
{
    public static class Mask
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

        public static string PrimeirasDuasLetrasNome(string nomeCompleto)
        {
            if (!string.IsNullOrEmpty(nomeCompleto))
            {
                string[] ArrayNomes = nomeCompleto.Split(" ");
                string resultado = ArrayNomes[0].Substring(0,1);
                if (ArrayNomes.Count() > 1) resultado += ArrayNomes[^1].Substring(0,1);
                return resultado;
            }
            return "";
        }
   	    public static string FormattedTime(DateTime msg)
        {
            var time = DateTime.Now.Subtract(msg);
            if (time.TotalSeconds <= 60)
            {
                if (time.TotalSeconds == 1)
                    return string.Format(Convert.ToInt32(time.TotalSeconds) + " segundo atrás");
                else
                    return string.Format(Convert.ToInt32(time.TotalSeconds) + " segundos atrás");
            }
            else if (time.TotalMinutes <= 60)
            {
                if (time.TotalMinutes == 1)
                    return string.Format(Convert.ToInt32(time.TotalMinutes) + " minuto atrás");
                else
                    return string.Format(Convert.ToInt32(time.TotalMinutes) + " minutos atrás");
            }
            else if (time.TotalHours <= 24)
            {
                if (time.TotalHours == 1)
                    return string.Format(Convert.ToInt32(time.TotalHours) + " hora atrás");
                else
                    return string.Format(Convert.ToInt32(time.TotalHours) + " horas atrás");
            }
            else if (time.TotalDays == 1)
                return string.Format(Convert.ToInt32(time.TotalDays) + " dia atrás");

            return string.Format(Convert.ToInt32(time.TotalDays) + " dias atrás");
        }
    }
}
