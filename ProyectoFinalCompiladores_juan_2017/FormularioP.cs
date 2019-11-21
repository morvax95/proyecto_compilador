using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using PrjtF.Negocio;


namespace ProyectoFinalCompiladores_juan_2017
{
    public partial class FormularioP : Form
    {

        AnalizadorLexico analizadorLexico = new AnalizadorLexico();
        public FormularioP()
        {
            InitializeComponent();
            ini();
        }
        
        void ini()
        {
            ListaError listaE = new ListaError();
            ListaPR listaPR = new ListaPR();
            analizadorLexico.iniciarRPR(this.tbEntrada, listaE, listaPR);
        }
      void limpiar()
        {
            dgv1.Rows.Clear();
                dgv2.Rows.Clear();
                tbSalida.Clear();
        }
   

        private void tbEntrada_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (analizadorLexico.cargar)
            {
                ListaError listaErrores = new ListaError();
                ListaPR listaPR = new ListaPR();
                analizadorLexico.AnalizarCodigo(tbEntrada.Text, listaErrores, listaPR);
                limpiar();
                listaErrores.mostrar(dgv1);
                listaPR.mostrar(dgv2);
                analizadorLexico.textoSalida(dgv2, tbSalida);

            }
        }

      
    }
}
