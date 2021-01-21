﻿using DbManagerDark.Attributes;
using DbManagerDark.DbManager;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DbManagerDark.Managers
{
    public class ActionsMode
    {
        /// <summary>
        /// nombre columna en DB
        /// </summary>
        public string Columnname { get; set; }
        /// <summary>
        /// Nombre variable para evitar injecciones SQL
        /// </summary>
        public string ColumPR { get; set; }
        /// <summary>
        /// valor de la variable
        /// </summary>
        public string Value { get; set; }
    }
    public class DarkManagerMySQL<T> where T : new()
    {
        private DarkConnectionMySQL dBConnection { get; set; }
        private string Nametable { get; set; }

        public T Element { get; set; }

        public DarkManagerMySQL()
        {
            Nametable = GetRealNameClass();
        }

        public DarkManagerMySQL(DarkConnectionMySQL dBConnection)
        {
            this.dBConnection = dBConnection;
            Nametable = GetRealNameClass();
        }
        private string GetRealNameClass()
        {
            string Nombre = "";
            DarkTable tableDefinifiton = GetClassAttribute();
            if (tableDefinifiton.IsMappedByLabels)
            {
                Nombre = tableDefinifiton.Name;
            }
            else
            {
                Nombre = typeof(T).Name;
            }
            return Nombre;
        }

        public bool Add()
        {
            DarkTable tableDefinifiton = GetClassAttribute();
            if (tableDefinifiton.IsStoreProcedure)
            {
                return ActionsObject(DbManagerTypes.Add);
            }
            else
            {
                return ActionsObjectCode(DbManagerTypes.Add, tableDefinifiton);
            }

        }

        public bool Update()
        {
            DarkTable tableDefinifiton = GetClassAttribute();
            if (tableDefinifiton.IsStoreProcedure)
            {
                return ActionsObject(DbManagerTypes.Update);
            }
            else
            {
                return ActionsObjectCode(DbManagerTypes.Update, tableDefinifiton);
            }
        }
        public bool Update(List<ActionsMode> valores, string where)
        {

            string sentencia = "";
            List<ProcedureModel> procedureModels = new List<ProcedureModel>();
            valores.ForEach(a => {
                sentencia = a.Columnname + " = @" + a.ColumPR + ",";
                procedureModels.Add(new ProcedureModel { Namefield = ""+ a.ColumPR, value = "" + a.Value });
            });
            string Statement = string.Format("UPDATE {0} SET {1} WHERE {2} ", Nametable, sentencia.Substring(0, sentencia.Length - 1), where);
            dBConnection.StartUpdate(Statement, procedureModels);
            bool result = true;

            return result;
        }
        /// <summary>
        /// ejecutar update
        /// </summary>
        /// <param name="tablename">Nombre de la tabla</param>
        /// <param name="valores">Valores nuevos</param>
        /// <param name="where">Sentencia where</param>
        /// <returns></returns>
        public bool Update(string tablename, List<ActionsMode> valores, string where)
        {

            string sentencia = "";
            List<ProcedureModel> procedureModels = new List<ProcedureModel>();
            valores.ForEach(a => {
                sentencia += a.Columnname + " = @" + a.ColumPR + ",";
                procedureModels.Add(new ProcedureModel { Namefield = "" + a.ColumPR, value = "" + a.Value });
            });
            string Statement = string.Format("UPDATE {0} SET {1} WHERE {2} ", tablename, sentencia.Substring(0, sentencia.Length - 1), where);
            dBConnection.StartUpdate(Statement, procedureModels);
            bool result = true;

            return result;
        }

        /// <summary>
        /// ejecutar insert
        /// </summary>
        /// <param name="tablename">Nombre de la tabla</param>
        /// <param name="valores">Valores nuevos</param>
        /// <param name="where">Sentencia where</param>
        /// <returns></returns>
        public bool Insert(string tablename, List<ActionsMode> valores)
        {

            string sentencia = "";
            string sentenciaVariables = "";
            List<ProcedureModel> procedureModels = new List<ProcedureModel>();
            valores.ForEach(a => {
                sentencia += a.Columnname + ",";
                sentenciaVariables += "@"+a.ColumPR + ",";
                procedureModels.Add(new ProcedureModel { Namefield = "" + a.ColumPR, value = "" + a.Value });
            });
            string Statement = string.Format("INSERT INTO {0}({1}) VALUES({2})", tablename, sentencia.Substring(0, sentencia.Length - 1), sentenciaVariables.Substring(0, sentenciaVariables.Length - 1));
            dBConnection.StartInsert(Statement, procedureModels);
            bool result = true;

            return result;
        }

        public bool Delete()
        {
            DarkTable tableDefinifiton = GetClassAttribute();
            if (tableDefinifiton.IsStoreProcedure)
            {
                return ActionsObject(DbManagerTypes.Delete);
            }
            else
            {
                return ActionsObjectCode(DbManagerTypes.Delete, tableDefinifiton);
            }
        }
        public int GetLastId(string column)
        {
            return dBConnection.GetIntegerValue(string.Format("select max({1}) from {0}", Nametable, column));
        }
        public int GetLastId()
        {
            return dBConnection.GetIntegerValue(string.Format("select max(Id{0}) from {0}", Nametable));
        }
        public int Count(string where)
        {
            return dBConnection.GetIntegerValue(string.Format("select count(*) from {0} {1}", Nametable, where));
        }
        public int GetMaxOpen(string SentenceMax)
        {
            return dBConnection.GetIntegerValue(SentenceMax);
        }
        public string GetStringValueGen(string SentenceMax)
        {
            return dBConnection.GetStringValue(SentenceMax);
        }

        public T Get(int? id)
        {
            List<T> Lista = DataReader(string.Format("select * from {0} where {1} = '{2}'", Nametable, KeyCol(), id));
            if (Lista.Count == 0)
            {
                return default(T);
            }
            return Lista.ElementAt(0);
        }

        public T GetString(string id)
        {
            List<T> Lista = DataReader(string.Format("select * from {0} where {1} = '{2}'", Nametable, KeyCol(), id));
            if (Lista.Count == 0)
            {
                return default(T);
            }
            return Lista.ElementAt(0);
        }

        public T GetByColumn(string id, string nameCol)
        {
            List<T> Lista = DataReader(string.Format("select * from {0} where {1} = '{2}'", Nametable, nameCol, id));
            if (Lista.Count == 0)
            {
                return default(T);
            }
            return Lista.ElementAt(0);
        }

        public T Get(string Columna1, string Columna1Val, string Columna2, string Columna1Val2)
        {
            List<T> Lista = DataReader(string.Format("select * from {0} where {1} = '{2}' and  {3} = '{4}'", Nametable, Columna1, Columna1Val, Columna2, Columna1Val2));
            if (Lista.Count == 0)
            {
                return default(T);
            }
            return Lista.ElementAt(0);
        }

        public List<T> Get(Predicate<T> match)
        {
            return DataReader(string.Format("select * from {0}", Nametable)).FindAll(match);
        }

        public List<T> Get(string id, string nameCol)
        {
            return DataReader(string.Format("select * from {0} where {1} = '{2}'", Nametable, nameCol, id));
        }
        public List<T> GetIn(int[] keys, string nameCol)
        {

            return DataReader(string.Format("select * from {0} where {1} in ({2})", Nametable, nameCol, string.Join(", ", keys)));
        }

        public List<T> Get()
        {
            return DataReader(string.Format("select * from {0}", Nametable));
        }
        /// <summary>
        /// obtener lista en base sentencia where y order by
        /// </summary>
        /// <param name="where"></param>
        /// <param name="Order"></param>
        /// <returns></returns>
        public List<T> GetOpenquery(string where = "", string Order = "")
        {
            return DataReader(string.Format("select * from {0} {1} {2}", Nametable, where, Order));
        }
        /// <summary>
        /// obtener objeto de acuerdo a sentencia where
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T GetOpenquery(string where = "")
        {
            List<T> Lista = DataReader(string.Format("select * from {0} {1} ", Nametable, where));
            if (Lista.Count == 0)
            {
                return default(T);
            }
            return Lista.ElementAt(0);
        }
        private string KeyCol()
        {
            string columna = "";
            if (GetClassAttribute().IsMappedByLabels)
            {
                object exFormAsObj = Activator.CreateInstance(typeof(T));
                foreach (var prop in typeof(T).GetProperties())
                {
                    PropertyInfo propertyInfo = exFormAsObj.GetType().GetProperty(prop.Name);
                    DarkColumn hiddenAttribute = (DarkColumn)propertyInfo.GetCustomAttribute(typeof(DarkColumn));

                    if (hiddenAttribute.IsKey)
                    {
                        columna = hiddenAttribute.Name;
                    }
                }
            }
            else
            {
                object exFormAsObj = Activator.CreateInstance(typeof(T));
                foreach (var prop in typeof(T).GetProperties())
                {
                    PropertyInfo propertyInfo = exFormAsObj.GetType().GetProperty(prop.Name);
                    DarkColumn hiddenAttribute = (DarkColumn)propertyInfo.GetCustomAttribute(typeof(DarkColumn));

                    if (hiddenAttribute.IsKey)
                    {
                        columna = prop.Name;
                    }
                }
            }

            return columna;
        }

        public string ColumName(string Name)
        {
            string columna = "";
            if (GetClassAttribute().IsMappedByLabels)
            {
                object exFormAsObj = Activator.CreateInstance(typeof(T));
                foreach (var prop in typeof(T).GetProperties())
                {
                    PropertyInfo propertyInfo = exFormAsObj.GetType().GetProperty(prop.Name);
                    DarkColumn hiddenAttribute = (DarkColumn)propertyInfo.GetCustomAttribute(typeof(DarkColumn));

                    if (prop.Name == Name)
                    {
                        columna = hiddenAttribute.Name;
                    }
                }
            }
            else
            {
                columna = Name;
            }

            return columna;
        }

        private List<T> DataReader(string SqlStatements)
        {
            DarkTable tableDefinifiton = GetClassAttribute();

            MySqlDataReader Data = dBConnection.GetDataReader(SqlStatements);
            List<T> Response = new List<T>();
            while (Data.Read())
            {
                object exFormAsObj = Activator.CreateInstance(typeof(T));
                foreach (var prop in typeof(T).GetProperties())
                {
                    PropertyInfo propertyInfo = exFormAsObj.GetType().GetProperty(prop.Name);
                    DarkColumn hiddenAttribute = (DarkColumn)propertyInfo.GetCustomAttribute(typeof(DarkColumn));

                    if (hiddenAttribute == null)
                    {
                        throw new Exceptions.DarkExceptionSystem(string.Format("The attribute was not found in the attribute '{0}', if you don´t want to use mapTable, please set IsMappedByLabels = false", prop.Name));
                    }

                    if (hiddenAttribute.IsMapped)
                    {
                        string NombrePropiedad = "";
                        if (tableDefinifiton.IsMappedByLabels)
                        {
                            NombrePropiedad = hiddenAttribute.Name.Trim();
                        }
                        else
                        {
                            NombrePropiedad = prop.Name;
                        }
                        try
                        {
                            if (prop.PropertyType.Equals(typeof(DateTime)))
                            {
                                var value = Data.GetValue(Data.GetOrdinal(NombrePropiedad)) is System.DBNull ? DateTime.Now : Data.GetValue(Data.GetOrdinal(NombrePropiedad));
                                propertyInfo.SetValue(exFormAsObj, Convert.ChangeType(value, TypeCode.DateTime), null);
                            }
                            if (prop.PropertyType.Equals(typeof(DateTime?)))
                            {
                                var value = Data.GetValue(Data.GetOrdinal(NombrePropiedad)) is System.DBNull ? null : Data.GetValue(Data.GetOrdinal(NombrePropiedad));
                                propertyInfo.SetValue(exFormAsObj, value, null);
                            }
                            if (prop.PropertyType.Equals(typeof(TimeSpan)))
                            {
                                var value = Data.GetValue(Data.GetOrdinal(NombrePropiedad)) is System.DBNull ? null : Data.GetValue(Data.GetOrdinal(NombrePropiedad));
                                propertyInfo.SetValue(exFormAsObj, value, null);
                            }
                            if (prop.PropertyType.Equals(typeof(double)))
                            {
                                var value = Data.GetValue(Data.GetOrdinal(NombrePropiedad)) is System.DBNull ? 0 : Data.GetValue(Data.GetOrdinal(NombrePropiedad));
                                propertyInfo.SetValue(exFormAsObj, Convert.ChangeType(value, TypeCode.Double), null);
                            }
                            if (prop.PropertyType.Equals(typeof(float)))
                            {
                                var value = Data.GetValue(Data.GetOrdinal(NombrePropiedad)) is System.DBNull ? 0 : Data.GetValue(Data.GetOrdinal(NombrePropiedad));
                                propertyInfo.SetValue(exFormAsObj, Convert.ToSingle(value), null);
                            }
                            if (prop.PropertyType.Equals(typeof(Decimal)))
                            {
                                var value = Data.GetValue(Data.GetOrdinal(NombrePropiedad)) is System.DBNull ? 0 : Data.GetValue(Data.GetOrdinal(NombrePropiedad));
                                propertyInfo.SetValue(exFormAsObj, Convert.ChangeType(value, TypeCode.Double), null);
                            }
                            if (prop.PropertyType.Equals(typeof(string)))
                            {
                                var value = Data.GetValue(Data.GetOrdinal(NombrePropiedad)) is System.DBNull ? "" : Data.GetValue(Data.GetOrdinal(NombrePropiedad));
                                propertyInfo.SetValue(exFormAsObj, Convert.ChangeType(value, TypeCode.String), null);
                            }
                            if (prop.PropertyType.Equals(typeof(bool)))
                            {
                                var value = Data.GetValue(Data.GetOrdinal(NombrePropiedad)) is System.DBNull ? false : Data.GetValue(Data.GetOrdinal(NombrePropiedad));
                                propertyInfo.SetValue(exFormAsObj, Convert.ChangeType(value, TypeCode.Boolean), null);
                            }
                            if (prop.PropertyType.Equals(typeof(int)))
                            {
                                var value = Data.GetValue(Data.GetOrdinal(NombrePropiedad)) is System.DBNull ? 0 : Data.GetValue(Data.GetOrdinal(NombrePropiedad));
                                propertyInfo.SetValue(exFormAsObj, Convert.ChangeType(value, propertyInfo.PropertyType), null);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exceptions.DarkExceptionSystem(string.Format("The attribute'{0}' has an error, {1}", prop.Name, ex.Message));
                        }
                    }
                }
                Response.Add((T)exFormAsObj);
            }
            Data.Close();

            return Response;
        }

        private bool ActionsObject(DbManagerTypes dbManagerTypes)
        {
            List<ProcedureModel> procedureModels = new List<ProcedureModel>();
            foreach (var prop in typeof(T).GetProperties())
            {
                if (prop.PropertyType.Equals(typeof(int)) || prop.PropertyType.Equals(typeof(string)) || prop.PropertyType.Equals(typeof(double)) || prop.PropertyType.Equals(typeof(TimeSpan)) || prop.PropertyType.Equals(typeof(DateTime)))
                {
                    PropertyInfo propertyInfo = Element.GetType().GetProperty(prop.Name);
                    procedureModels.Add(new ProcedureModel { Namefield = prop.Name, value = propertyInfo.GetValue(Element) });
                }
                if (prop.PropertyType.Equals(typeof(DateTime?)))
                {
                    PropertyInfo propertyInfo = Element.GetType().GetProperty(prop.Name);
                    procedureModels.Add(new ProcedureModel { Namefield = prop.Name, value = propertyInfo.GetValue(Element) });
                }
            }
            procedureModels.Add(new ProcedureModel { Namefield = "ModeProcedure", value = dbManagerTypes });
            dBConnection.StartProcedure(string.Format("Gps_{0}", Nametable), procedureModels);
            if (dBConnection.ErrorCode == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ActionsObjectCode(DbManagerTypes dbManagerTypes, DarkTable tableDefinifiton)
        {
            //DarkTable tableDefinifiton = GetClassAttribute();
            bool result = false;
            //mapeo de tabla con los nombres de los campos ya existentes
            string sentencia = "";
            string sentenciaVariables = "";
            foreach (var prop in typeof(T).GetProperties())
            {
                PropertyInfo propertyInfo = Element.GetType().GetProperty(prop.Name);
                DarkColumn hiddenAttribute = (DarkColumn)propertyInfo.GetCustomAttribute(typeof(DarkColumn));

                if (hiddenAttribute == null)
                {
                    throw new Exceptions.DarkExceptionSystem(string.Format("The attribute was not found in the attribute '{0}', if you don´t want to use mapTable, please set IsMappedByLabels = false", prop.Name));
                }
                if (tableDefinifiton.IsMappedByLabels)
                {
                    if (string.IsNullOrEmpty(hiddenAttribute.Name))
                    {
                        throw new Exceptions.DarkExceptionSystem(string.Format("The attribute {0} was setting like mapping column, the name is missing", prop.Name));
                    }
                }
                else
                {
                    hiddenAttribute.Name = prop.Name;
                }


                if (dbManagerTypes == DbManagerTypes.Add)
                {
                    if (!hiddenAttribute.IsKey && hiddenAttribute.IsMapped)
                    {
                        sentencia += hiddenAttribute.Name + ",";
                        sentenciaVariables += "@" + hiddenAttribute.Name + ",";
                    }
                }
                else if (dbManagerTypes == DbManagerTypes.Update)
                {
                    if (!hiddenAttribute.IsKey && hiddenAttribute.IsMapped)
                    {
                        sentencia += hiddenAttribute.Name + " = @" + hiddenAttribute.Name + ",";
                    }
                    else if (hiddenAttribute.IsKey && hiddenAttribute.IsMapped)
                    {
                        sentenciaVariables = hiddenAttribute.Name + " = @" + hiddenAttribute.Name + "";
                    }
                    else
                    {

                    }
                }
                else if (dbManagerTypes == DbManagerTypes.Delete)
                {
                    if (hiddenAttribute.IsKey)
                    {
                        sentenciaVariables = hiddenAttribute.Name + " = @" + hiddenAttribute.Name + "";
                    }
                }
                else
                {
                    throw new Exceptions.DarkExceptionSystem(string.Format("Delete action is not active"));
                }
            }

            if (dbManagerTypes == DbManagerTypes.Add)
            {
                string Statement = string.Format("INSERT INTO {0}({1}) VALUES({2})", Nametable, sentencia.Substring(0, sentencia.Length - 1), sentenciaVariables.Substring(0, sentenciaVariables.Length - 1));
                List<ProcedureModel> procedureModels = new List<ProcedureModel>();
                foreach (var prop in typeof(T).GetProperties())
                {
                    PropertyInfo propertyInfo = Element.GetType().GetProperty(prop.Name);
                    DarkColumn hiddenAttribute = (DarkColumn)propertyInfo.GetCustomAttribute(typeof(DarkColumn));

                    if (!hiddenAttribute.IsKey && hiddenAttribute.IsMapped)
                    {
                        procedureModels.Add(new ProcedureModel { Namefield = tableDefinifiton.IsMappedByLabels ? hiddenAttribute.Name : prop.Name, value = propertyInfo.GetValue(Element) });
                    }
                }

                dBConnection.StartInsert(Statement, procedureModels);
                result = true;
            }
            else if (dbManagerTypes == DbManagerTypes.Update)
            {
                string Statement = string.Format("UPDATE {0} SET {1} WHERE {2} ", Nametable, sentencia.Substring(0, sentencia.Length - 1), sentenciaVariables);
                List<ProcedureModel> procedureModels = new List<ProcedureModel>();
                foreach (var prop in typeof(T).GetProperties())
                {
                    PropertyInfo propertyInfo = Element.GetType().GetProperty(prop.Name);
                    DarkColumn hiddenAttribute = (DarkColumn)propertyInfo.GetCustomAttribute(typeof(DarkColumn));

                    if (hiddenAttribute.IsMapped)
                    {
                        procedureModels.Add(new ProcedureModel { Namefield = tableDefinifiton.IsMappedByLabels ? hiddenAttribute.Name : prop.Name, value = propertyInfo.GetValue(Element) });
                    }
                }

                dBConnection.StartUpdate(Statement, procedureModels);
                result = true;
            }
            else if (dbManagerTypes == DbManagerTypes.Delete)
            {
                string Statement = string.Format("DELETE FROM  {0} WHERE {1} ", Nametable, sentenciaVariables);
                List<ProcedureModel> procedureModels = new List<ProcedureModel>();
                foreach (var prop in typeof(T).GetProperties())
                {
                    PropertyInfo propertyInfo = Element.GetType().GetProperty(prop.Name);
                    DarkColumn hiddenAttribute = (DarkColumn)propertyInfo.GetCustomAttribute(typeof(DarkColumn));

                    if (hiddenAttribute.IsMapped && hiddenAttribute.IsKey)
                    {
                        procedureModels.Add(new ProcedureModel { Namefield = tableDefinifiton.IsMappedByLabels ? hiddenAttribute.Name : prop.Name, value = propertyInfo.GetValue(Element) });
                    }
                }
                dBConnection.StartDelete(Statement, procedureModels);
                result = true;
            }
            else
            {
                throw new Exceptions.DarkExceptionSystem(string.Format("Delete action is not active"));
            }

            return result;
        }

        private DarkTable GetClassAttribute()
        {
            DarkTable tableDefinifiton = (DarkTable)Attribute.GetCustomAttribute(typeof(T), typeof(DarkTable));

            if (tableDefinifiton == null)
            {
                throw new Exceptions.DarkExceptionSystem(string.Format("The attribute was not found in the class '{0}'.", typeof(T).Name));
            }
            return tableDefinifiton;
        }
        public string GetLastMessage()
        {
            return dBConnection.mensaje;
        }
    }
}
