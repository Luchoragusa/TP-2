﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Desktop.DocenteCurso;

namespace UI.Desktop
{
    public partial class formMain : Form
    {
        private int tipo;

        public formMain()
        {
            InitializeComponent();
        }

        public formMain(int tipo)
        {
            this.tipo = tipo;
            //tipoPersona(tipo);
        }

        private void tipoPersona(int tipo)
        {
            if (tipo == 1)  //alumno
            {
                btnEspecialidad.Visible = false;
            }
            else if (tipo == 3)     //docente
            {

            }
            else if (tipo == 4)
            {

            }
        }
        private void mnuSalir_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void formMain_Shown(object sender, EventArgs e)
        {
           frmLogin appLogin = new frmLogin();
           if (appLogin.ShowDialog() != DialogResult.OK)
           {
                this.Dispose();
           }
        }


        private void btn_Usuario_Click(object sender, EventArgs e)
        {
            Usuarios us = new Usuarios();
            us.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Materia mat = new Materia();
            mat.ShowDialog();
        }

        private void btnComision_Click(object sender, EventArgs e)
        {
            Comisiones com = new Comisiones();
            com.ShowDialog();
        }

        private void btnPlan_Click(object sender, EventArgs e)
        {
            Plan plan = new Plan();
            plan.ShowDialog();
        }

        private void btnEspecialidad_Click(object sender, EventArgs e)
        {
            Especialidad esp = new Especialidad();
            esp.ShowDialog();
        }

        private void btnCurso_Click(object sender, EventArgs e)
        {
            Cursos curso = new UI.Desktop.Cursos();
            curso.ShowDialog();            
        }

        private void btnDC_Click(object sender, EventArgs e)
        {
            DocenteCurso.DocenteCurso dc = new UI.Desktop.DocenteCurso.DocenteCurso();
            dc.ShowDialog();
        }

        private void Alum_Inscrip_Click(object sender, EventArgs e)
        {
            Alum_Inscrip.Alum_Inscrip a_i = new Alum_Inscrip.Alum_Inscrip();
            a_i.ShowDialog();
        }
    }
}
