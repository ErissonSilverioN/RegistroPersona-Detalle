using DetalleTelefono.DAL;
using DetalleTelefono.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DetalleTelefono.BLL
{
    public class PersonaBLL
    {
        public static bool Guardar(Personas personas)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                if (db.personas.Add(personas) != null)
                    paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;


        }


        public static bool Modificar(Personas personas)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                
                var Anterior = PersonaBLL.Buscar(personas.PersonaId);
                foreach (var item in Anterior.Detalles)
                {
                    if (!personas.Detalles.Exists(d => d.Id == item.Id))
                        db.Entry(item).State = EntityState.Deleted;

                }
                
                foreach (var item in personas.Detalles)
                {
                    
                    var estado = item.Id > 0 ? EntityState.Modified : EntityState.Added;
                    db.Entry(item).State = estado;
                }
                
                db.Entry(personas).State = EntityState.Modified;
                paso = (db.SaveChanges() > 0);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static bool Eliminar(int id)
        {
            bool paso = false;
            Contexto db = new Contexto();

            try
            {
                var eliminar = db.personas.Find(id);
                db.Entry(eliminar).State = EntityState.Deleted;
                paso = db.SaveChanges() > 0;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return paso;
        }

        public static Personas Buscar(int id)
        {

            Contexto db = new Contexto();
            Personas personas = new Personas();

            try
            {
                personas = db.personas.Where(o => o.PersonaId == id).Include(o => o.Detalles).SingleOrDefault();
                //personas.Telefonos.Count();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }
            return personas;
        }

        public static List<Personas> GetList(Expression<Func<Personas, bool>> persona)
        {
            List<Personas> Lista = new List<Personas>();
            Contexto db = new Contexto();

            try
            {

                Lista = db.personas.Where(persona).ToList();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                db.Dispose();
            }

            return Lista;
        }
    }
}
