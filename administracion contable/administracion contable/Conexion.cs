using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace administracion_contable
{
    class Conexion
    {

        SqliteConnection Connection { get; set; }


        public void conectar()
        {
            if (File.Exists("datos contables"))
            {
                try
                {
                    this.Connection = new SqliteConnection("Data Source = datos contables");
                    this.Connection.Open();
                }
                catch (Exception ex)
                {
                    throw new Exception("no pudo realizarse la conexion" + ex);
                }
            }
            else
            {
                SQLiteConnection.CreateFile("datos contables");
                conectar();
            }
        }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Security", "CA2100:Revisar consultas SQL para comprobar si tienen vulnerabilidades de seguridad")]

        public void guardarElementoContable(string numeroIdentificador, string nombre)
        {

            try
            {
                conectar();

                string sql = "insert into elementosDelPlanContables (numeroIdentificador,nombre) values ('" + numeroIdentificador + "','" + nombre + "')";
                SqliteCommand command = new SqliteCommand(sql, this.Connection);


                try
                {
                    command.ExecuteNonQuery();//cargamos los datos
                }
                catch (Exception)
                {
                    //si el archivo y la tabla no existen los creamos
                    String crearTabla = "create table elementosDelPlanContables(" +
                                        "numeroIdentificador varchar," +
                                        "nombre varchar)";

                    SqliteCommand comman2 = new SqliteCommand(crearTabla, this.Connection);

                    //cada objeto SqliteCommand realiza una sentencia diferente
                    comman2.ExecuteNonQuery();//creamos la tabla 
                    command.ExecuteNonQuery(); //cargamos los datos
                }
                finally
                {
                    close();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("no se ha podido realizar la conexion" + ex);
            }
            finally
            {
                close();
            }

        }

        public void cargarElementoContable(List<ElementoPlanContable> lista)
        {

            conectar();

            String consulta = "select * from elementosDelPlanContables";
            SqliteCommand command = new SqliteCommand(consulta, Connection);

            try
            {
                SqliteDataReader lector = command.ExecuteReader();


                while (lector.Read())
                {
                    string id = lector.GetString(0);
                    string nombre = lector.GetString(1);
                    ElementoPlanContable elementoPlanContable = new ElementoPlanContable(id,nombre);
                    lista.Add(elementoPlanContable);
                }


            }
            catch (Exception)
            {
                String crearTabla = "create table elementosDelPlanContables(" +
                                    "numeroIdentificador varchar," +
                                    "nombre varchar)";

                SqliteCommand comman2 = new SqliteCommand(crearTabla, this.Connection);
                comman2.ExecuteNonQuery(); //crea la tabla

                //el siguiente bloque lee la tabla y carga los datos a la lista que le pasamos
                SqliteDataReader lector = command.ExecuteReader();//crea el objeto encargado de leer
                while (lector.Read())
                {
                    string id = lector.GetString(0);
                    string nombre = lector.GetString(1);
                    ElementoPlanContable elementoPlanContable = new ElementoPlanContable(id, nombre);
                    lista.Add(elementoPlanContable);
                }
            }
            finally
            {
                close();
            }

        }

        public void close()
        {            
            try
            {
                this.Connection.Close();
            }
            catch (Exception)
            {
                throw new Exception("no puso realizarse la desconexion");
            }
        }



    }
}
