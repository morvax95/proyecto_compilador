using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrjtF.Negocio
{
    public class NodoError
    {
        NodoError NodoEnl;
        string error;
        string token;
        public NodoError(string tkn, string err)
        {
            error = err;
            token = tkn;
            NodoEnl = null;
        }

        public string Error
        {
            set { error = value; }
            get { return error; }
        }
        public string Token
        {
            set { token = value; }
            get { return token; }
        }

        public NodoError enlace
        {
            set { NodoEnl = value; }
            get { return NodoEnl; }
        }
    }
}