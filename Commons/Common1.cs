using Microsoft.CSharp;
using System;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Commons
{
    public static class Converts
    {
        /// <summary> 
        /// Generates a C# class code file from a Database given an SQL connection string and table name.
        /// </summary>
        public static string SQLToCSharp(string ConnectionString, string TableName)
        {
            DataTable table = Query.QueryToDataTable(ConnectionString, "SELECT TOP 1 * FROM [{0}]", TableName);
            return Code.DatatableToCSharp(table);
        }

        /// <summary>
        /// Creates an SQL table from a class object.
        /// </summary>
        public static bool ClassToSQL<T>(string ConnectionString, params T[] ClassCollection) where T : class
        {
            string createTableScript = Script.CreateTable<T>(ClassCollection);
            return (Query.ExecuteNonQuery(ConnectionString, createTableScript) == -1);
        }
    }

    /// <summary>
    /// DataTable/Class Mapping Class
    /// </summary>
    public static class Map
    {
        /// <summary>
        /// Fills properties of a class from a row of a DataTable where the name of the property matches the column name from that DataTable.
        /// It does this for each row in the DataTable, returning a List of classes.
        /// </summary>
        /// <typeparam name="T">The class type that is to be returned.</typeparam>
        /// <param name="Table">DataTable to fill from.</param>
        /// <returns>A list of ClassType with its properties set to the data from the matching columns from the DataTable.</returns>
        public static IList<T> DatatableToClass<T>(DataTable Table) where T : class, new()
        {
            if (!Helper.IsValidDatatable(Table))
                return new List<T>();

            Type classType = typeof(T);
            IList<PropertyInfo> propertyList = classType.GetProperties();

            // Parameter class has no public properties.
            if (propertyList.Count == 0)
                return new List<T>();

            List<string> columnNames = Table.Columns.Cast<DataColumn>().Select(column => column.ColumnName).ToList();

            List<T> result = new List<T>();
            try
            {
                foreach (DataRow row in Table.Rows)
                {
                    T classObject = new T();
                    foreach (PropertyInfo property in propertyList)
                    {
                        if (property != null && property.CanWrite)   // Make sure property isn't read only
                        {
                            if (columnNames.Contains(property.Name))  // If property is a column name
                            {
                                if (row[property.Name] != System.DBNull.Value)   // Don't copy over DBNull
                                {
                                    object propertyValue = System.Convert.ChangeType(
                                            row[property.Name],
                                            property.PropertyType
                                        );
                                    property.SetValue(classObject, propertyValue, null);
                                }
                            }
                        }
                    }
                    result.Add(classObject);
                }
                return result;
            }
            catch
            {
                return new List<T>();
            }
        }

        /// <summary>
        /// Creates a DataTable from a class type's public properties and adds a new DataRow to the table for each class passed as a parameter.
        /// The DataColumns of the table will match the name and type of the public properties.
        /// </summary>
        /// <param name="ClassCollection">A class or array of class to fill the DataTable with.</param>
        /// <returns>A DataTable who's DataColumns match the name and type of each class T's public properties.</returns>
        public static DataTable ClassToDatatable<T>(params T[] ClassCollection) where T : class
        {
            DataTable result = ClassToDatatable<T>();

            if (Helper.IsValidDatatable(result, IgnoreRows: true))
                return new DataTable();
            if (Helper.IsCollectionEmpty(ClassCollection))
                return result;   // Returns and empty DataTable with columns defined (table schema)

            foreach (T classObject in ClassCollection)
            {
                ClassToDataRow(ref result, classObject);
            }

            return result;
        }

        /// <summary>
        /// Creates a DataTable from a class type's public properties. The DataColumns of the table will match the name and type of the public properties.
        /// </summary>
        /// <typeparam name="T">The type of the class to create a DataTable from.</typeparam>
        /// <returns>A DataTable who's DataColumns match the name and type of each class T's public properties.</returns>
        public static DataTable ClassToDatatable<T>() where T : class
        {
            Type classType = typeof(T);
            DataTable result = new DataTable(classType.UnderlyingSystemType.Name);

            foreach (PropertyInfo property in classType.GetProperties())
            {
                DataColumn column = new DataColumn();
                column.ColumnName = property.Name;
                column.DataType = property.PropertyType;

                if (Helper.IsNullableType(column.DataType) && column.DataType.IsGenericType)
                {   // If Nullable<>, this is how we get the underlying Type...
                    column.DataType = column.DataType.GenericTypeArguments.FirstOrDefault();
                }
                else
                {   // True by default, so set it false
                    column.AllowDBNull = false;
                }

                // Add column
                result.Columns.Add(column);
            }
            return result;
        }

        /// <summary>
        /// Adds a DataRow to a DataTable from the public properties of a class.
        /// </summary>
        /// <param name="Table">A reference to the DataTable to insert the DataRow into.</param>
        /// <param name="ClassObject">The class containing the data to fill the DataRow from.</param>
        private static void ClassToDataRow<T>(ref DataTable Table, T ClassObject) where T : class
        {
            DataRow row = Table.NewRow();
            foreach (PropertyInfo property in typeof(T).GetProperties())
            {
                if (Table.Columns.Contains(property.Name))
                {
                    if (Table.Columns[property.Name] != null)
                    {
                        row[property.Name] = property.GetValue(ClassObject, null);
                    }
                }
            }
            Table.Rows.Add(row);
        }
    }

    /// <summary>
    /// SQL Query Helper Class
    /// </summary>
    public static class Query
    {
        /// <summary>
        /// Runs a SQL query and returns the results as a List of the specified class
        /// </summary>
        /// <typeparam name="T">The type the result will be returned as.</typeparam>
        /// <param name="ConnectionString">The SQL connection string.</param>
        /// <param name="FormatString_Query">A SQL command that will be passed to string.Format().</param>
        /// <param name="FormatString_Parameters">The parameters for string.Format().</param>
        /// <returns>A List of classes that represent the records returned.</returns>
        public static IList<T> QueryToClass<T>(string ConnectionString, string FormatString_Query, params object[] FormatString_Parameters) where T : class, new()
        {
            IList<T> result = new List<T>();
            DataTable tableQueryResult = QueryToDataTable(ConnectionString, string.Format(FormatString_Query, FormatString_Parameters));
            if (Helper.IsValidDatatable(tableQueryResult))
            {
                result = Map.DatatableToClass<T>(tableQueryResult);
            }
            return result;
        }

        /// <summary>
        /// Executes an SQL query and returns the results as a DataTable.
        /// </summary>
        /// <param name="ConnectionString">The SQL connection string.</param>
        /// <param name="FormatString_Query">A SQL command that will be passed to string.Format().</param>
        /// <param name="FormatString_Parameters">The parameters for string.Format().</param>
        /// <returns>The results of the query as a DataTable.</returns>
        public static DataTable QueryToDataTable(string ConnectionString, string FormatString_Query, params object[] FormatString_Parameters)
        {
            try
            {
                DataTable result = new DataTable();

                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = string.Format(FormatString_Query, FormatString_Parameters);
                        sqlCommand.CommandType = CommandType.Text;

                        SqlDataAdapter sqlAdapter = new SqlDataAdapter(sqlCommand);
                        sqlAdapter.Fill(result);
                    }
                }
                return result;
            }
            catch
            {
                return new DataTable();
            }
        }

        /// <summary>
        /// Executes a query, and returns the first column of the first row in the result set returned by the query.
        /// </summary>
        /// <typeparam name="T">The type the result will be returned as.</typeparam>
        /// <param name="ConnectionString">>The SQL connection string.</param>
        /// <param name="FormatString_Query">The SQL query as string.Format string.</param>
        /// <param name="FormatString_Parameters">The string.Format parameters.</param>
        /// <returns>The  first column of the first row in the result, converted and casted to type T.</returns>
        public static T QueryToScalarType<T>(string ConnectionString, string FormatString_Query, params object[] FormatString_Parameters)
        {
            try
            {
                object result = new object();
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    sqlConnection.Open();

                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = string.Format(FormatString_Query, FormatString_Parameters);
                        sqlCommand.CommandType = CommandType.Text;

                        result = System.Convert.ChangeType(sqlCommand.ExecuteScalar(), typeof(T));
                    }
                }
                return (T)result;
            }
            catch
            {
                return (T)new object();
            }
        }

        /// <summary>
        /// Executes a non-query SQL command, such as INSERT or DELETE
        /// </summary>
        /// <param name="ConnectionString">The connection string.</param>
        /// <param name="FormatString_Command">The SQL command, as a format string.</param>
        /// <param name="FormatString_Parameters">The parameters for the format string.</param>
        /// <returns>The number of rows affected, or -1 on errors.</returns>
        public static int ExecuteNonQuery(string ConnectionString, string FormatString_Command, params object[] FormatString_Parameters)
        {
            try
            {
                int rowsAffected = 0;

                using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = sqlConnection.CreateCommand())
                    {
                        string commandText = string.Format(FormatString_Command, FormatString_Parameters);

                        sqlCommand.CommandText = commandText;
                        sqlCommand.CommandType = CommandType.Text;
                        rowsAffected = sqlCommand.ExecuteNonQuery();
                    }
                }

                return rowsAffected;
            }
            catch
            {
                return 0;
            }
        }
    }

    /// <summary>
    /// SQL Script Generation Class
    /// </summary>
    public static class Script
    {
        /// <summary>
        /// Creates a SQL script that inserts the values of the specified classes' public properties into a table.
        /// </summary>
        public static string InsertInto<T>(params T[] ClassObjects) where T : class
        {
            DataTable table = Map.ClassToDatatable<T>(ClassObjects);
            return InsertInto(table);   // We don't need to check IsValidDatatable() because InsertInto does
        }

        /// <summary>
        /// Creates a SQL script that inserts the cell values of a DataTable's DataRows into a table.
        /// </summary>
        public static string InsertInto(DataTable Table)
        {
            if (!Helper.IsValidDatatable(Table))
                return string.Empty;

            StringBuilder result = new StringBuilder();
            foreach (DataRow row in Table.Rows)
            {
                if (row == null || row.Table.Columns.Count < 1 || row.ItemArray.Length < 1)
                    return string.Empty;

                string columns = Helper.RowToColumnString(row);
                string values = Helper.RowToValueString(row);

                if (string.IsNullOrWhiteSpace(columns) || string.IsNullOrWhiteSpace(values))
                    return string.Empty;

                result.AppendFormat("INSERT INTO [{0}] {1} VALUES {2}", Table.TableName, columns, values);
            }

            return result.ToString();
        }

        /// <summary>
        /// Creates a SQL script that creates a table where the column names match the specified class's public properties.
        /// </summary>
        public static string CreateTable<T>(params T[] ClassObjects) where T : class
        {
            DataTable table = Map.ClassToDatatable<T>(ClassObjects);
            return Script.CreateTable(table);
        }

        /// <summary>
        /// Creates a SQL script that creates a table where the columns matches that of the specified DataTable.
        /// </summary>
        public static string CreateTable(DataTable Table)
        {
            if (Helper.IsValidDatatable(Table, IgnoreRows: true))
                return string.Empty;

            StringBuilder result = new StringBuilder();
            result.AppendFormat("CREATE TABLE [{0}] ({1}", Table.TableName, Environment.NewLine);

            bool FirstTime = true;
            foreach (DataColumn column in Table.Columns.OfType<DataColumn>())
            {
                if (FirstTime) FirstTime = false;
                else
                    result.Append(",");

                result.AppendFormat("[{0}] {1} {2}NULL{3}",
                    column.ColumnName,
                    GetDataTypeString(column.DataType),
                    column.AllowDBNull ? "" : "NOT ",
                    Environment.NewLine
                );
            }
            result.AppendFormat(") ON [PRIMARY]{0}GO", Environment.NewLine);

            return result.ToString();
        }

        /// <summary>
        /// Returns the SQL data type equivalent, as a string for use in SQL script generation methods.
        /// </summary>
        private static string GetDataTypeString(Type DataType)
        {
            switch (DataType.Name)
            {
                case "Boolean": return "[bit]";
                case "Char": return "[char]";
                case "SByte": return "[tinyint]";
                case "Int16": return "[smallint]";
                case "Int32": return "[int]";
                case "Int64": return "[bigint]";
                case "Byte": return "[tinyint] UNSIGNED";
                case "UInt16": return "[smallint] UNSIGNED";
                case "UInt32": return "[int] UNSIGNED";
                case "UInt64": return "[bigint] UNSIGNED";
                case "Single": return "[float]";
                case "Double": return "[double]";
                case "Decimal": return "[decimal]";
                case "DateTime": return "[datetime]";
                case "Guid": return "[uniqueidentifier]";
                case "Object": return "[variant]";
                case "String": return "[nvarchar](250)";
                default: return "[nvarchar](MAX)";
            }
        }
    }

    /// <summary>
    /// Helper Functions. Conversion, Validation
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Indicates whether a specified DataTable is null, has zero columns, or (optionally) zero rows.
        /// </summary>
        /// <param name="Table">DataTable to check.</param>
        /// <param name="IgnoreRows">When set to true, the function will return true even if the table's row count is equal to zero.</param>
        /// <returns>False if the specified DataTable null, has zero columns, or zero rows, otherwise true.</returns>
        public static bool IsValidDatatable(DataTable Table, bool IgnoreRows = false)
        {
            if (Table == null) return false;
            if (Table.Columns.Count == 0) return false;
            if (!IgnoreRows && Table.Rows.Count == 0) return false;
            return true;
        }

        /// <summary>
        /// Indicates whether a specified Enumerable collection is null or an empty collection.
        /// </summary>
        /// <typeparam name="T">The specified type contained in the collection.</typeparam>
        /// <param name="Input">An Enumerator to the collection to check.</param>
        /// <returns>True if the specified Enumerable collection is null or empty, otherwise false.</returns>
        public static bool IsCollectionEmpty<T>(IEnumerable<T> Input)
        {
            return (Input == null || Input.Count() < 1) ? true : false;
        }

        /// <summary>
        ///  Indicates whether a specified Type can be assigned null.
        /// </summary>
        /// <param name="Input">The Type to check for nullable property.</param>
        /// <returns>True if the specified Type can be assigned null, otherwise false.</returns>
        public static bool IsNullableType(Type Input)
        {
            if (!Input.IsValueType) return true; // Reference Type
            if (Nullable.GetUnderlyingType(Input) != null) return true; // Nullable<T>
            return false;   // Value Type
        }

        /// <summary>
        /// Returns all the column names of the specified DataRow in a string delimited like and SQL INSERT INTO statement.
        /// Example: ([FullName], [Gender], [BirthDate])
        /// </summary>
        /// <returns>A string formatted like the columns specified in an SQL 'INSERT INTO' statement.</returns>
        public static string RowToColumnString(DataRow Row)
        {
            IEnumerable<string> Collection = Row.ItemArray.Select(item => item as String);
            return ListToDelimitedString(Collection, "([", "], [", "])");
        }

        /// <summary>
        /// Returns all the values the specified DataRow in as a string delimited like and SQL INSERT INTO statement.
        /// Example: ('John Doe', 'M', '10/3/1981'')
        /// </summary>
        /// <returns>A string formatted like the values specified in an SQL 'INSERT INTO' statement.</returns>
        public static string RowToValueString(DataRow Row)
        {
            IEnumerable<string> Collection = GetDatatableColumns(Row.Table).Select(c => c.ColumnName);
            return ListToDelimitedString(Collection, "('", "', '", "')");
        }

        /// <summary>
        /// Enumerates a collection as delimited collection of strings.
        /// </summary>
        /// <typeparam name="T">The Type of the collection.</typeparam>
        /// <param name="Collection">An Enumerator to a collection to populate the string.</param>
        /// <param name="Prefix">The string to prefix the result.</param>
        /// <param name="Delimiter">The string that will appear between each item in the specified collection.</param>
        /// <param name="Postfix">The string to postfix the result.</param>         
        public static string ListToDelimitedString<T>(IEnumerable<T> Collection, string Prefix, string Delimiter, string Postfix)
        {
            if (IsCollectionEmpty<T>(Collection)) return string.Empty;

            StringBuilder result = new StringBuilder();
            foreach (T item in Collection)
            {
                if (result.Length != 0)
                    result.Append(Delimiter);   // Add comma

                result.Append(EscapeSingleQuotes(item as String));
            }
            if (result.Length < 1) return string.Empty;

            result.Insert(0, Prefix);
            result.Append(Postfix);

            return result.ToString();
        }

        /// <summary>
        /// Returns an enumerator, which supports a simple iteration over a collection of all the DataColumns in a specified DataTable.
        /// </summary>
        public static IEnumerable<DataColumn> GetDatatableColumns(DataTable Input)
        {
            if (Input == null || Input.Columns.Count < 1) return new List<DataColumn>();
            return Input.Columns.OfType<DataColumn>().ToList();
        }

        /// <summary>
        /// Returns an enumerator, which supports a simple iteration over a collection of all the DataRows in a specified DataTable.
        /// </summary>
        public static IEnumerable<DataRow> GetDatatableRows(DataTable Input)
        {
            if (!IsValidDatatable(Input)) return new List<DataRow>();
            return Input.Rows.OfType<DataRow>().ToList();
        }

        /// <summary>
        /// Returns a new string in which all occurrences of the single quote character in the current instance are replaced with a back-tick character.
        /// </summary>
        public static string EscapeSingleQuotes(string Input)
        {
            return Input.Replace('\'', '`'); // Replace with back-tick
        }
    }

    /// <summary>
    /// C# Code Generation Class
    /// </summary>
    public static class Code
    {
        /// <summary> 
        /// Generates a C# class code file from a DataTable.
        /// </summary>
        public static string DatatableToCSharp(DataTable Table)
        {
            string className = Table.TableName;

            if (string.IsNullOrWhiteSpace(className))
            {
                return "// Class cannot be created: DataTable.TableName must have a value to use as the name of the class";
            }

            // Create the class
            CodeTypeDeclaration classDeclaration = CreateClass(className);

            // Add public properties
            foreach (DataColumn column in Table.Columns)
            {
                classDeclaration.Members.Add(CreateProperty(column.ColumnName, column.DataType));
            }

            // Add Class to Namespace
            string namespaceName = new StackFrame(2).GetMethod().DeclaringType.Namespace;// "EntityJustWorks.AutoGeneratedClassObject";
            CodeNamespace codeNamespace = new CodeNamespace(namespaceName);
            codeNamespace.Types.Add(classDeclaration);

            // Generate code
            string filename = string.Format("{0}.{1}.cs", namespaceName, className);
            CreateCodeFile(filename, codeNamespace);

            // Return filename
            return filename;
        }

        #region Private Members
        private static CodeTypeDeclaration CreateClass(string name)
        {
            CodeTypeDeclaration result = new CodeTypeDeclaration(name);
            result.Attributes = MemberAttributes.Public;
            result.Members.Add(CreateConstructor(name)); // Add class constructor
            return result;
        }

        private static CodeConstructor CreateConstructor(string className)
        {
            CodeConstructor result = new CodeConstructor();
            result.Attributes = MemberAttributes.Public;
            result.Name = className;
            return result;
        }

        private static CodeMemberField CreateProperty(string name, Type type)
        {
            // This is a little hack. Since you cant create auto properties in CodeDOM,
            //  we make the getter and setter part of the member name.
            // This leaves behind a trailing semicolon that we comment out.
            //  Later, we remove the commented out semicolons.
            string memberName = name + "\t{ get; set; }//";

            CodeMemberField result = new CodeMemberField(type, memberName);
            result.Attributes = MemberAttributes.Public | MemberAttributes.Final;
            return result;
        }

        private static void CreateCodeFile(string filename, CodeNamespace codeNamespace)
        {
            // CodeGeneratorOptions so the output is clean and easy to read
            CodeGeneratorOptions codeOptions = new CodeGeneratorOptions();
            codeOptions.BlankLinesBetweenMembers = false;
            codeOptions.VerbatimOrder = true;
            codeOptions.BracingStyle = "C";
            codeOptions.IndentString = "\t";

            // Create the code file
            using (TextWriter textWriter = new StreamWriter(filename))
            {
                CSharpCodeProvider codeProvider = new CSharpCodeProvider();
                codeProvider.GenerateCodeFromNamespace(codeNamespace, textWriter, codeOptions);
            }

            // Correct our little auto-property 'hack'
            File.WriteAllText(filename, File.ReadAllText(filename).Replace("//;", ""));
        }
        #endregion
    }

}
