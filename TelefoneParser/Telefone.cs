using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TelefoneParser
{
    public class Telefone
    {
        private static HashSet<string> DDDsValidos = new HashSet<string>() { "11", "12", "13", "14", "15", "16", "17", "18", "19", "21", "22", "24", "27", "28", "31", "32", "33", "34", "35", "37", "38", "41", "42", "43", "44", "45", "46", "47", "48", "49", "51", "53", "54", "55", "61", "62", "63", "64", "65", "66", "67", "68", "69", "71", "73", "74", "75", "77", "79", "81", "82", "83", "84", "85", "86", "87", "88", "89", "91", "92", "93", "94", "95", "96", "97", "98", "99" };

        public bool NumeroValido { get; private set; }
        public bool CodigoEstadoValido { get; private set; }
        public bool TelefoneValido { get; private set; }

        public bool NumeroCelular { get; private set; }

        public string CodigoPais { get; private set; }
        public string CodigoEstado { get; private set; }
        public string Numero { get; private set; }
        
        private static IEnumerable<char> ApenasNumeros(string telefone)
        {
            foreach (var c in telefone)
                if (char.IsNumber(c))
                    yield return c;
        }

        public static Telefone Parse(string telefone)
        {
            if (telefone == null) return null;
            telefone = new string(ApenasNumeros(telefone).ToArray());

            var resultado = new Telefone();

            if(telefone.Length == 8 || telefone.Length == 9)
            {
                resultado.Numero = telefone;
            }
            else if(telefone.Length == 10 || telefone.Length == 11)
            {
                resultado.CodigoEstado = telefone.Substring(0, 2);
                resultado.Numero = telefone.Substring(2);
            }
            else if(telefone.Length == 12 || telefone.Length == 13)
            {
                resultado.CodigoPais = telefone.Substring(0, 2);
                resultado.CodigoEstado = telefone.Substring(2, 2);
                resultado.Numero = telefone.Substring(4);
            }

            var possuiNumero = !string.IsNullOrEmpty(resultado.Numero);
            resultado.NumeroCelular = possuiNumero && resultado.Numero.Length == 9 && resultado.Numero[0] == '9';
            resultado.NumeroValido = resultado.NumeroCelular || (possuiNumero && resultado.Numero.Length == 8);

            resultado.CodigoEstadoValido = string.IsNullOrEmpty(resultado.CodigoEstado) || DDDsValidos.Contains(resultado.CodigoEstado);
            resultado.TelefoneValido = resultado.NumeroValido && resultado.CodigoEstadoValido;
            
            return resultado;
        }
    }
}
