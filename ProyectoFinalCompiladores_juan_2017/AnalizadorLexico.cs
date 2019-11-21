using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

using System.Windows.Forms;
using PrjtF.Negocio;

namespace PrjtF.Negocio
{
    public class AnalizadorLexico
    {
        ReglasPR Reglas = new ReglasPR();
        public bool cargar;
        List<string> palabrasReservadas;

        public void iniciarRPR(TextBox tbxCode, ListaError lE, ListaPR lPR)
        {
            Reglas.guardar(@">>[^\r\n]*", "desplazamientoIzquiero");
            Reglas.guardar(@"<<[^\r\n]*", "desplazamientoD");
            Reglas.guardar(@"::[^\r\n]*", "alias");
            Reglas.guardar(@":[^\r\n]*", "condicional");
            Reglas.guardar(@"!=[^\r\n]*", "diferente");
            Reglas.guardar(@"¿¿[^\r\n]*", "null");
            Reglas.guardar(@"'[^\r\n]*", "comillas_simples");
            Reglas.guardar(@"&&[^\r\n]*", "and");
            Reglas.guardar(@"->[^\r\n]*", "desReferenciacion");
            Reglas.guardar(@"==[^\r\n]*", "exactamente_igual");
            Reglas.guardar(@"%[^\r\n]*", "resto");
            Reglas.guardar(@">=[^\r\n]*", "mayorQue");
            Reglas.guardar(@"<=[^\r\n]*", "menorQue");
            
            
            Reglas.guardar(@"\s+", "Espacio", true);
            Reglas.guardar(@"\b[_a-zA-Z][\w]*\b", "Identificador");

            Reglas.guardar("\".*?\"", "Cadena");
            Reglas.guardar(@"'\\.'|'[^\\]'", "Caracter");
            Reglas.guardar("//[^\r\n]*", "Comentario_simple");
            Reglas.guardar("/[*].*?[*]/", "Comentario_multiple");
            Reglas.guardar(@"\d*\.?\d+", "Nùmero");
            Reglas.guardar(@"[\(\)\{\}\[\];,]", "Delimitador");
            Reglas.guardar(@"[\.=\+\-/*%]", "Matematico");
            Reglas.guardar(@">|<|==|>=|<=|!", "Comparador");
            palabrasReservadas = new List<string>() { "abstract", "as","break", "case",
                "do", "else", "enum", "event", "explicit", "extern", "false", "finally",
                "new", "null", "operator","catch","private", "protected", "public",
                "ref", "return", "static","switch", "this",  "true", "try",  "namespace",
                "void", "while", "float", "int", "long", "object","get", "set", "new","select", "where",
                "equals", "using","bool", "byte", "char", "decimal", "double", 
                 "short", "string", "var", "class", "struct" , "Procedure", "for", "if"};
            Reglas.Compilar(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture);
            cargar = true;
            AnalizarCodigo(tbxCode.Text, lE, lPR);
            tbxCode.Focus();



        }
        public void AnalizarCodigo(string tbxCode, ListaError lE, ListaPR lPR)
        {
            foreach (var tk in Reglas.ObtenerTokens(tbxCode))
            {
                if (tk.Token == "ERROR")
                    lE.insertar(tk.Token, tk.Lexema);
                else
                    if (tk.Token == "IDENTIFICADOR")
                    if (palabrasReservadas.Contains(tk.Lexema))
                        tk.Token = "RESERVADO";
                lPR.insertar(tk.Token, tk.Lexema);
            }
        }





        public void textoSalida(DataGridView dg2, TextBox salida)
        {
            for (int i = 0; i < dg2.RowCount; i++)
                salida.Text += dg2.Rows[i].Cells[1].Value.ToString() + " -> " + dg2.Rows[i].Cells[0].Value.ToString() + "\r\n";
        }
    }
}
