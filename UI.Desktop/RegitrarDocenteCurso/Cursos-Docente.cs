﻿using Business.Entities;
using Business.Logic.EntidadesLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UI.Desktop.RegistrarNotas;

namespace UI.Desktop.RegitrarDocenteCurso
{
    public partial class Cursos_Docente : Form
    {
        Usuario docente = new Usuario();
        public Usuario Docente { get => docente; set => docente = value; }

        public Cursos_Docente(Usuario doc)
        {
            InitializeComponent();
            Docente = doc;
            this.dgvCursosDocente.AutoGenerateColumns = false;
            this.dgvCursosDocente.ReadOnly = true;
        }

        public void Listar()
        {
            DocenteCursoLogic dcl = new DocenteCursoLogic();
            try
            {
                this.dgvCursosDocente.DataSource = dcl.getCursosDocente(docente);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Error detected: ", "Ha habido un error interno.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnActualizar_Click(object sender, EventArgs e)
        {
            this.Listar();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            DICDesktop docente_alumnos = new DICDesktop(ApplicationForm.ModoForm.Alta);
            try
            {
                docente_alumnos.ShowDialog();
                this.Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Error detected: ", "Ha habido un error interno.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (this.dgvCursosDocente.SelectedRows.Count == 0)
            {
                MessageBox.Show("Accion Invalida", "Seleccione una fila", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int ID = ((Business.Entities.Entidades.DocenteCurso)this.dgvCursosDocente.SelectedRows[0].DataBoundItem).ID;
            DICDesktop docente_alumnos = new DICDesktop(ID, ApplicationForm.ModoForm.Modificacion);
            try
            {
                docente_alumnos.ShowDialog();
                this.Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Error detected: ", "Ha habido un error interno.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tsbEliminar_Click(object sender, EventArgs e)
        {
            if (this.dgvCursosDocente.SelectedRows.Count == 0)
            {
                MessageBox.Show("Accion Invalida", "Seleccione una fila", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int ID = ((Business.Entities.Entidades.DocenteCurso)this.dgvCursosDocente.SelectedRows[0].DataBoundItem).ID;
            DICDesktop docente_alumnos = new DICDesktop(ID, ApplicationForm.ModoForm.Baja);
            try
            {
                docente_alumnos.ShowDialog();
                this.Listar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "Error detected: ", "Ha habido un error interno.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cursos_Docente_Load(object sender, EventArgs e)
        {
            this.Listar();
        }
    }
}