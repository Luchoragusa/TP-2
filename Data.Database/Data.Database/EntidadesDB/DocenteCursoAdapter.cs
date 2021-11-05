﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Entities.Entidades;
using System.Data;
using System.Data.SqlClient;
using Business.Entities;

namespace Data.Database.EntidadesDB
{
    public class DocenteCursoAdapter : Adapter
    {
        public DocenteCursoAdapter() { }

        public List<DocenteCurso> GetAll()
        {
            List<DocenteCurso> DocenteCursoes = new List<DocenteCurso>();
            try
            {
                this.OpenConnection();

                SqlCommand cmdDocenteCursoes = new SqlCommand("SELECT * FROM docentes_cursos", sqlConn);

                SqlDataReader drDocenteCursos = cmdDocenteCursoes.ExecuteReader();
                if (drDocenteCursos != null)
                {
                    while (drDocenteCursos.Read())
                    {
                        DocenteCurso DocenteCurso = new DocenteCurso();
                        DocenteCurso.ID = (int)drDocenteCursos["id_dictado"];
                        DocenteCurso.IDCurso = (int)drDocenteCursos["id_curso"];
                        DocenteCurso.IDDocente = (int)drDocenteCursos["id_docente"];
                        DocenteCurso.Cargo = (DocenteCurso.TipoCargos)drDocenteCursos["cargo"];

                        DocenteCursoes.Add(DocenteCurso);
                    }
                }
                drDocenteCursos.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar la lista de Docente cursos", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                this.CloseConnection();
            }
            return DocenteCursoes;
        }

        public List<Materia> GetAllMateriaslDelDocente(Usuario docente)
        {
            List<Materia> materiasDelDocente = new List<Materia>();
            try
            {
                this.OpenConnection();

                SqlCommand cmdmateriasDocente = new SqlCommand("select mat.id_materia, mat.desc_materia, mat.hs_semanales, mat.hs_totales, mat.id_plan from materias mat inner join cursos c on c.id_materia = mat.id_materia inner join docentes_cursos doc on doc.id_curso = c.id_curso inner join comisiones com on com.id_comision = c.id_comision inner join personas p on p.id_persona = doc.id_docente inner join usuarios u on u.id_persona = p.id_persona where u.nombre_usuario = @nombre_usuario and u.clave = @clave", sqlConn);

                cmdmateriasDocente.Parameters.Add("@nombre_usuario", SqlDbType.VarChar, 50).Value = docente.NombreUsuario;
                cmdmateriasDocente.Parameters.Add("@clave", SqlDbType.VarChar, 50).Value = docente.Clave;

                SqlDataReader drMateriasDocente = cmdmateriasDocente.ExecuteReader();

                while (drMateriasDocente.Read())
                {
                    Materia mat = new Materia();

                    mat.ID = (int)drMateriasDocente["id_materia"];
                    mat.Descripcion = (string)drMateriasDocente["desc_materia"];
                    mat.HSSSemanales = (int)drMateriasDocente["hs_semanales"];
                    mat.HSTotales = (int)drMateriasDocente["hs_totales"];
                    mat.IDPlan = (int)drMateriasDocente["id_plan"];

                    materiasDelDocente.Add(mat);
                }
                drMateriasDocente.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar la lista de cursos", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                CloseConnection();
            }
            return materiasDelDocente;
        }

        public DocenteCurso GetOne(DocenteCurso DocenteCurso)
        {
            try
            {
                OpenConnection();
                SqlCommand cmdDocenteCursos = new SqlCommand("SELECT * FROM docentes_cursos WHERE id_dictado = @id", sqlConn);
                cmdDocenteCursos.Parameters.Add("@id", SqlDbType.Int).Value = DocenteCurso.ID;
                SqlDataReader drDocenteCursos = cmdDocenteCursos.ExecuteReader();
                if (drDocenteCursos != null)
                {

                    while (drDocenteCursos.Read())
                    {
                        DocenteCurso.ID = (int)drDocenteCursos["id_dictado"];
                        DocenteCurso.IDCurso = (int)drDocenteCursos["id_curso"];
                        DocenteCurso.IDDocente = (int)drDocenteCursos["id_docente"];
                        int cargo = (int)drDocenteCursos["cargo"];

                        if (cargo == (int)Business.Entities.Entidades.DocenteCurso.TipoCargos.Titular)
                            DocenteCurso.Cargo = Business.Entities.Entidades.DocenteCurso.TipoCargos.Titular;

                        else if (cargo == (int)Business.Entities.Entidades.DocenteCurso.TipoCargos.Auxiliar)
                            DocenteCurso.Cargo = Business.Entities.Entidades.DocenteCurso.TipoCargos.Auxiliar;

                        else if (cargo == (int)Business.Entities.Entidades.DocenteCurso.TipoCargos.JefeCatedra)
                            DocenteCurso.Cargo = Business.Entities.Entidades.DocenteCurso.TipoCargos.JefeCatedra;
                    }
                    
                    drDocenteCursos.Read();
                    DocenteCurso.IDCurso = (int)drDocenteCursos["id_curso"];
                    DocenteCurso.IDDocente = (int)drDocenteCursos["id_docente"];
                    DocenteCurso.Cargo = (DocenteCurso.TipoCargos)drDocenteCursos["cargo"];

                }
                drDocenteCursos.Close();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManejada = new Exception("Error al recuperar datos del DocenteCurso", Ex);
                throw ExcepcionManejada;
            }
            finally
            {
                CloseConnection();
            }
            return DocenteCurso;
        }

        public void Delete(int ID)
        {
            try
            {
                OpenConnection();
                SqlCommand cmdDelete = new SqlCommand("delete docentes_cursos where id_dictado = @id", sqlConn);
                cmdDelete.Parameters.Add("@id", SqlDbType.Int).Value = ID;
                cmdDelete.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExcepcionManjeada = new Exception("Error al eliminar el docente del curso", Ex);
                throw ExcepcionManjeada;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Insert(Business.Entities.Entidades.DocenteCurso DocenteCurso)
        {
            try
            {
                OpenConnection();
                SqlCommand cmdSave = new SqlCommand("INSERT INTO docentes_cursos (id_dictado,id_curso, id_docente, cargo) values(@id_dictado,@id_curso,@id_docente,@cargo) select @@identity ", sqlConn);

                cmdSave.Parameters.Add("@id_curso", SqlDbType.VarChar, 50).Value = DocenteCurso.IDCurso;
                cmdSave.Parameters.Add("@id_docente", SqlDbType.VarChar, 50).Value = DocenteCurso.IDDocente;
                cmdSave.Parameters.Add("@cargo", SqlDbType.VarChar, 50).Value = DocenteCurso.Cargo;

                DocenteCurso.ID = Decimal.ToInt32((decimal)cmdSave.ExecuteScalar());
            }
            catch (Exception Ex)
            {
                Exception ExceptionManejada = new Exception("Error al crear el docente del curso", Ex);
                throw ExceptionManejada;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Update(Business.Entities.Entidades.DocenteCurso DocenteCurso)
        {
            try
            {
                OpenConnection();
                SqlCommand cmdUpd = new SqlCommand("UPDATE docentes_cursos SET id_curso = @id_curso, id_docente = @id_docente, cargo = @cargo HERE id_dictado = @id ", sqlConn);

                cmdUpd.Parameters.Add("@id_curso", SqlDbType.VarChar, 50).Value = DocenteCurso.IDCurso;
                cmdUpd.Parameters.Add("@id_docente", SqlDbType.VarChar, 50).Value = DocenteCurso.IDDocente;
                cmdUpd.Parameters.Add("@cargo", SqlDbType.VarChar, 50).Value = DocenteCurso.Cargo;

                cmdUpd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                Exception ExceptionManejada = new Exception("Error al modificar datos del docente del curso", Ex);
                throw ExceptionManejada;
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Save(Business.Entities.Entidades.DocenteCurso DocenteCurso)
        {
            if (DocenteCurso.State == BusinessEntity.States.Deleted)
            {
                Delete(DocenteCurso.ID);
            }

            else if (DocenteCurso.State == BusinessEntity.States.New)
            {
                this.Insert(DocenteCurso);
            }
            else if (DocenteCurso.State == BusinessEntity.States.Modified)
            {
                Update(DocenteCurso);
            }
            DocenteCurso.State = BusinessEntity.States.Unmodified;
        }
    }
}
