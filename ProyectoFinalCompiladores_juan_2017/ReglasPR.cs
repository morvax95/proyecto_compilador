using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PrjtF.Negocio
{
    public class ReglasPR
    {
        Regex rex;
        StringBuilder patron;
        bool requiereCompilar;
        List<string> TNombres;
        int[] TCant;

        public ReglasPR()
        {
            requiereCompilar = true;
            TNombres = new List<string>();
        }

        public void guardar(string NvoPatron, string NombToken, bool ignore = false)
        {
            if (string.IsNullOrWhiteSpace(NombToken))
                throw new ArgumentException(string.Format("{0} no es un nombre válido.", NombToken));
            if (string.IsNullOrEmpty(NvoPatron))
                throw new ArgumentException(string.Format("El patrón {0} no es válido.", NvoPatron));
            if (patron == null)
                patron = new StringBuilder(string.Format("(?<{0}>{1})", NombToken, NvoPatron));
            else
                patron.Append(string.Format("|(?<{0}>{1})", NombToken, NvoPatron));
            if (!ignore)
                TNombres.Add(NombToken);
            requiereCompilar = true;
        }
        public void Reiniciar()
        {
            rex = null;
            patron = null;
            requiereCompilar = true;
            TNombres.Clear();
            TCant = null;
        }

        public IEnumerable<NodoPR> ObtenerTokens(string text)
        {
            if (requiereCompilar) throw new Exception("Compilación Requerida, llame al método Compile(options).");
            Match coin = rex.Match(text);
            if (!coin.Success) yield break;
            int linea = 1, inicio = 0, indice = 0;
            while (coin.Success)
            {
                if (coin.Index > indice)
                {
                    string token = text.Substring(indice, coin.Index - indice);
                    yield return new NodoPR("ERROR", token);
                    linea += ContarLineas(token, indice, ref inicio);
                }
                for (int i = 0; i < TCant.Length; i++)
                {
                    if (coin.Groups[TCant[i]].Success)
                    {
                        string name = rex.GroupNameFromNumber(TCant[i]);
                        yield return new NodoPR(name, coin.Value);
                        break;
                    }
                }
                linea += ContarLineas(coin.Value, coin.Index, ref inicio);
                indice = coin.Index + coin.Length;
                coin = coin.NextMatch();
            }
            if (text.Length > indice)
                yield return new NodoPR("ERROR", text.Substring(indice));
        }

        public void Compilar(RegexOptions options)
        {
            if (patron == null) throw new Exception("Agrege uno o más patrones, llame al método AddTokenRule(pattern, token_name).");
            if (requiereCompilar)
            {
                try
                {
                    rex = new Regex(patron.ToString(), options);
                    TCant = new int[TNombres.Count];
                    string[] gn = rex.GetGroupNames();
                    for (int i = 0, idx = 0; i < gn.Length; i++)
                        if (TNombres.Contains(gn[i]))
                            TCant[idx++] = rex.GroupNumberFromName(gn[i]);
                    requiereCompilar = false;
                }
                catch (Exception ex) { throw ex; }
            }
        }

        private int ContarLineas(string token, int indice, ref int inicioLinea)
        {
            int linea = 0;
            for (int i = 0; i < token.Length; i++)
                if (token[i] == '\n')
                {
                    linea++;
                    inicioLinea = indice + i + 1;
                }
            return linea;
        }
    }
}
