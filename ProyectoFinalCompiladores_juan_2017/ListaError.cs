using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrjtF.Negocio
{
    public class ListaError
    {
        public NodoError listaE;
        public int cnnE = 0;
        public ListaError()
        {
            listaE = null;
        }
        public void insertar(string token, string error)
        {
            if (listaE == null)
                listaE = new NodoError(error, token);
            else
            {
                NodoError n = new NodoError(error, token);
                NodoError aux = listaE;
                while (aux.enlace != null)
                {
                    aux = aux.enlace;
                }
                aux.enlace = n;
            }
            cnnE++;
        }

























        public void mostrar(DataGridView dgv1)
        {
            if (listaE != null)
            {
                dgv1.RowCount = cnnE;
                int pos = 0;
                NodoError aux = listaE;
                while (aux != null)
                {
                    dgv1.Rows[pos].Cells[0].Value = aux.Error;
                    dgv1.Rows[pos].Cells[1].Value = aux.Token;
                    aux = aux.enlace;
                    pos++;
                }
            }
            //else
            //MessageBox.Show("La lista esta vacia");  //not necesary?
        }
    }
}
