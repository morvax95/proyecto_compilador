using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrjtF.Negocio
{
    public class ListaPR
    {
        public NodoPR lisPR;
        public int cnnPR = 0;
        public ListaPR()
        {
            lisPR = null;
        }
        public void insertar(string token, string lexema)
        {
            if (lisPR == null)
                lisPR = new NodoPR(lexema, token);
            else
            {
                NodoPR n = new NodoPR(lexema, token);
                NodoPR aux = lisPR;
                while (aux.enlace != null)
                {
                    aux = aux.enlace;
                }
                aux.enlace = n;
            }
            cnnPR++;
        }












        public void mostrar(DataGridView dgv2)
        {
            if (lisPR != null)
            {
                dgv2.RowCount = cnnPR;
                int pos = 0;
                NodoPR aux = lisPR;
                while (aux != null)
                {
                    dgv2.Rows[pos].Cells[0].Value = aux.Lexema;
                    dgv2.Rows[pos].Cells[1].Value = aux.Token;
                    aux = aux.enlace;
                    pos++;
                }
            }
            //else
            //MessageBox.Show("La lista esta vacia");  //not necesary?
        }
    }
}
