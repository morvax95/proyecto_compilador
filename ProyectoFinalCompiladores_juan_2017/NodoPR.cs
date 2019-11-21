using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjtF.Negocio
{
    public class NodoPR
    {
        NodoPR NodoEnl;
        string lexema;
        string token;
        public NodoPR(string tkn, string lex)
        {
            lexema = lex;
            token = tkn;
            NodoEnl = null;
        }

        public string Lexema
        {
            set { lexema = value; }
            get { return lexema; }
        }
        public string Token
        {
            set { token = value; }
            get { return token; }
        }

        public NodoPR enlace
        {
            set { NodoEnl = value; }
            get { return NodoEnl; }
        }
    }
}
