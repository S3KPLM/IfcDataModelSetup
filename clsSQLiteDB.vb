Imports System.Data.SQLite

Public Class clsSQLiteDB

#Region " Variables "

    Dim conn As SQLiteConnection

#End Region

#Region " Methods "

    ''' <summary>
    ''' Apre la connessione con il database
    ''' </summary>
    ''' <param name="cPathDB">Percorso database</param>
    ''' <remarks>Fabio Dellarole - 2012</remarks>
    Public Sub OpenDB(cPathDB As String, Optional ByVal bReadOnly As Boolean = True, Optional ByVal cPassword As String = """""")

        Dim connString As String = "Data Source=" & cPathDB.Replace("\\", "\\\\") & ";Version=3;Pooling=true;FailIfMissing=false;Read Only=" & Convert.ToString(bReadOnly) ' & ";Password=" & cPassword & ";"

        Try
            If IO.File.Exists(cPathDB) = False Then Throw New Exception("File DB does not exist: " & cPathDB)

            ' Istanzia la connessione col database
            If conn Is Nothing Then
                conn = New SQLiteConnection(connString)
                conn.Open()
            ElseIf conn.State = ConnectionState.Closed Then
                conn.ConnectionString = connString
                conn.Open()
            End If
        Catch exc As Exception
            Throw exc
        End Try

        'Debug.WriteLine(conn.MemoryHighwater)
        'Debug.WriteLine(conn.MemoryUsed)

    End Sub

    ''' <summary>
    ''' Chiude la connessione con il database
    ''' </summary>
    ''' <remarks>Fabio Dellarole - 2012</remarks>
    Public Sub CloseDB()

        Try
            If conn IsNot Nothing AndAlso conn.State = ConnectionState.Open Then conn.Close()
            conn = Nothing
        Catch exc As Exception
            Throw exc
        End Try

    End Sub

    ''' <summary>
    ''' Ritorna se il database è aperto
    ''' </summary>
    ''' <returns>Connection state</returns>
    ''' <remarks>Fabio Dellarole - 2012</remarks>
    Public Function IsOpen() As ConnectionState
        Return conn.State
    End Function

    Public Function GetConnection() As SQLiteConnection
        Return conn
    End Function

#End Region

#Region " Inner Methods "

    Private Function GetParametersList(ByVal dicParameters As Dictionary(Of String, Object)) As List(Of SQLiteParameter)
        Dim lst As List(Of SQLiteParameter) = New List(Of SQLiteParameter)()

        If dicParameters IsNot Nothing Then

            For Each kv As KeyValuePair(Of String, Object) In dicParameters
                lst.Add(New SQLiteParameter(kv.Key, kv.Value))
            Next
        End If

        Return lst
    End Function

    Private Shared Function AdjustTableName(sTableName As String) As String

        ' Aggiorna il nome della tabella con caratteri "[" e "]"
        Dim sTable = sTableName
        If sTableName.Trim.StartsWith("[") = False Then sTable = "[" + sTableName.Trim
        If sTableName.Trim.EndsWith("]") = False Then sTable += "]"

        Return sTable

    End Function

#End Region

#Region " Classe che gestisce la tabella aperta (SQLiteDataReader)"

    Public Class DBTable

        Private objDB As clsSQLiteDB
        Private reader As SQLiteDataReader

        Public Property TableName As String
        Public Property TableFields As New List(Of Field)

        Public Sub New(objDB As clsSQLiteDB, tableName As String, Optional getFieldNames As Boolean = False)
            Me.objDB = objDB
            Me.TableName = tableName

            If getFieldNames = True Then
                GetTableFieldNames()
            End If
        End Sub

        Public Sub GetTableFieldNames()

            TableFields.Clear()

            OpenTableWithQuery("PRAGMA table_info(" & TableName & ")")

            While (reader.Read())
                Dim field As New Field With {
                    .Name = reader("name").ToString,
                    .Type = reader("type").ToString,
                    .NotNull = Convert.ToBoolean(reader("notnull")),
                    .Pk = Convert.ToBoolean(reader("pk"))
                }

                TableFields.Add(field)
            End While

        End Sub

        ''' <summary>
        ''' Apre una tabella del database
        ''' </summary>
        ''' <param name="cQuery">Query di oggetti</param>
        ''' <remarks>Fabio Dellarole - 2012</remarks>
        Public Sub OpenTableWithQuery(cQuery As String)

            ' Check query string
            If String.IsNullOrEmpty(cQuery) = True Then Return

            Try
                ' Istanzia il comando SQL
                Dim cmd As New SQLiteCommand(objDB.GetConnection)
                cmd.CommandText = cQuery
                ' Istanzia il reader SQL
                reader = cmd.ExecuteReader()
            Catch exc As Exception
                Throw exc
            End Try

        End Sub

        Public Sub OpenTableWithQuery(cQuery As String, ByRef reader As SQLiteDataReader)

            ' Check query string
            If String.IsNullOrEmpty(cQuery) = True Then Return

            Try
                ' Istanzia il comando SQL
                Dim cmd As New SQLiteCommand(objDB.GetConnection)
                cmd.CommandText = cQuery
                ' Istanzia il reader SQL
                reader = cmd.ExecuteReader()
            Catch exc As Exception
                Throw exc
            End Try

        End Sub

        Public Sub OpenTable(keyName As String, keyValue As Object)

            ' Aggiorna il nome della tabella con caratteri "[" e "]"
            Dim sTable = AdjustTableName(TableName)

            Dim dicCond As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()
            If String.IsNullOrEmpty(keyName) = False Then
                ' Compose condition
                dicCond(keyName) = keyValue
            End If

            ' SQL
            Dim cmd As New SQLiteCommand(objDB.GetConnection)
            SelectFrom(sTable, dicCond, cmd)

            ' Istanzia il reader SQL
            reader = cmd.ExecuteReader()

        End Sub

        Public Sub OpenTable(Optional params As Dictionary(Of String, Object) = Nothing)

            ' Aggiorna il nome della tabella con caratteri "[" e "]"
            Dim sTable = AdjustTableName(TableName)
            If params Is Nothing Then params = New Dictionary(Of String, Object)()

            ' SQL
            Dim cmd As New SQLiteCommand(objDB.GetConnection)
            SelectFrom(sTable, params, cmd)

            ' Istanzia il reader SQL
            reader = cmd.ExecuteReader()

        End Sub

        ''' <summary>
        ''' Chiude la tabella
        ''' </summary>
        ''' <remarks>Fabio Dellarole - 2012</remarks>
        Public Sub CloseTable()

            Try
                If reader IsNot Nothing AndAlso reader.IsClosed = False Then reader.Close()
                reader = Nothing
            Catch exc As Exception
                Throw exc
            End Try

        End Sub

        Public Function GetAllRecordsAsListMap() As List(Of Dictionary(Of String, Object))

            Dim recValues As New List(Of Dictionary(Of String, Object))

            While (reader.Read())
                Dim dictRow As New Dictionary(Of String, Object)
                For Each field As Field In TableFields
                    dictRow.Add(field.Name, reader(field.Name))
                Next

                recValues.Add(dictRow)
            End While

            Return recValues
        End Function

        Private Sub Insert(ByVal dic As Dictionary(Of String, Object))

            If dic Is Nothing Then dic = New Dictionary(Of String, Object)()
            If dic.Count = 0 Then Throw New Exception("Query 'Insert' failed. Data dictionary is empty.")
            Dim sbCol As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim sbVal As System.Text.StringBuilder = New System.Text.StringBuilder()

            For Each kv As KeyValuePair(Of String, Object) In dic

                If sbCol.Length = 0 Then
                    sbCol.Append("insert into ")
                    sbCol.Append(TableName)
                    sbCol.Append("(")
                Else
                    sbCol.Append(",")
                End If

                sbCol.Append("`")
                sbCol.Append(kv.Key)
                sbCol.Append("`")

                If sbVal.Length = 0 Then
                    sbVal.Append(" values(")
                Else
                    sbVal.Append(", ")
                End If

                sbVal.Append("@v")
                sbVal.Append(kv.Key)
            Next

            sbCol.Append(") ")
            sbVal.Append(");")

            Dim cmd As New SQLiteCommand(objDB.GetConnection)
            cmd.CommandText = sbCol.ToString() + sbVal.ToString()
            cmd.Parameters.Clear()

            For Each kv As KeyValuePair(Of String, Object) In dic
                cmd.Parameters.AddWithValue("@v" & kv.Key, kv.Value)
            Next

            cmd.ExecuteNonQuery()

        End Sub

        Private Sub Update(ByVal dicData As Dictionary(Of String, Object), ByVal dicCond As Dictionary(Of String, Object))

            Dim sbData As System.Text.StringBuilder = New System.Text.StringBuilder()
            Dim _dicTypeSource As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()
            If dicCond Is Nothing Then dicCond = New Dictionary(Of String, Object)()
            If dicData Is Nothing Then dicData = New Dictionary(Of String, Object)()
            If dicData.Count = 0 Then Throw New Exception("Query 'Update' failed. Data dictionary is empty.")

            For Each kv1 As KeyValuePair(Of String, Object) In dicData
                _dicTypeSource(kv1.Key) = Nothing
            Next

            For Each kv2 As KeyValuePair(Of String, Object) In dicCond
                If Not _dicTypeSource.ContainsKey(kv2.Key) Then _dicTypeSource(kv2.Key) = Nothing
            Next

            sbData.Append("update ")
            sbData.Append(TableName)
            sbData.Append(" set ")
            Dim firstRecord As Boolean = True

            For Each kv As KeyValuePair(Of String, Object) In dicData

                If firstRecord Then
                    firstRecord = False
                Else
                    sbData.Append(",")
                End If

                sbData.Append("`")
                sbData.Append(kv.Key)
                sbData.Append("` = ")
                sbData.Append("@v")
                sbData.Append(kv.Key)
            Next

            firstRecord = True

            For Each kv As KeyValuePair(Of String, Object) In dicCond

                If firstRecord Then
                    sbData.Append(" where ")
                    firstRecord = False
                Else
                    sbData.Append(" and ")
                End If

                sbData.Append("`")
                sbData.Append(kv.Key)
                sbData.Append("` = ")
                sbData.Append("@c")
                sbData.Append(kv.Key)
            Next

            sbData.Append(";")

            Dim cmd As New SQLiteCommand(objDB.GetConnection)
            cmd.CommandText = sbData.ToString()
            cmd.Parameters.Clear()

            For Each kv As KeyValuePair(Of String, Object) In dicData

                If kv.Value Is Nothing Then
                    cmd.Parameters.AddWithValue("@v" & kv.Key, CObj(DBNull.Value))
                Else
                    cmd.Parameters.AddWithValue("@v" & kv.Key, kv.Value)
                End If
            Next

            For Each kv As KeyValuePair(Of String, Object) In dicCond

                If kv.Value Is Nothing Then
                    cmd.Parameters.AddWithValue("@c" & kv.Key, CObj(DBNull.Value))
                Else
                    cmd.Parameters.AddWithValue("@c" & kv.Key, kv.Value)
                End If
            Next

            cmd.ExecuteNonQuery()

        End Sub

        Private Sub InsertOrUpdate(dicData As Dictionary(Of String, Object), dicCond As Dictionary(Of String, Object))

            If dicCond Is Nothing Then dicCond = New Dictionary(Of String, Object)
            If dicData Is Nothing Then dicData = New Dictionary(Of String, Object)
            If dicData.Count = 0 Then Throw New Exception("Query 'Insert or Update' failed. Data dictionary is empty.")
            Dim dt As DataTable = SelectFrom(TableName, dicCond)

            If dt.Rows.Count > 0 Then
                Update(dicData, dicCond)
            Else
                Insert(dicData)
            End If

        End Sub

        ''' <summary>
        ''' Inserisce un nuovo record nella tabella
        ''' </summary>
        ''' <param name="dictRec">Mappa Campo/Valore</param>
        ''' <remarks>Fabio Dellarole - 2014</remarks>
        Public Sub InsertRecord(dictRec As Dictionary(Of String, Object))

            Try
                CloseTable()

                ' Aggiorna il nome della tabella con caratteri "[" e "]"
                Dim sTable = AdjustTableName(TableName)

                ' Insert record
                Insert(dictRec)

            Catch exc As Exception
                Throw exc
            End Try

        End Sub

        ''' <summary>
        ''' Modifica il record corrente nella tabella
        ''' </summary>
        ''' <param name="sTableName">Nome tabella da aggiornare</param>
        ''' <param name="dictRec">Mappa Campo/Valore</param>
        ''' <param name="id">Indice del record da aggiornare</param>
        ''' <remarks>Fabio Dellarole - 2014</remarks>
        Public Sub UpdateRecord(sTableName As String, dictRec As Dictionary(Of String, Object), id As String)

            Try
                CloseTable()

                ' Aggiorna il nome della tabella con caratteri "[" e "]"
                Dim sTable = AdjustTableName(sTableName)

                ' Compose condition
                Dim dicCond As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()
                dicCond("ID") = id

                ' Update record
                Update(dictRec, dicCond)

            Catch exc As Exception
                Throw exc
            End Try

        End Sub

        ''' <summary>
        ''' Inserisce o Modifica a seconda della necessità il record con indice univoco
        ''' </summary>
        ''' <param name="sTableName">Nome tabella da aggiornare</param>
        ''' <param name="dictRec">Mappa Campo/Valore</param>
        ''' <param name="keyName">Nome chiave di riferimento</param>
        ''' <param name="keyValue">Valore chiave di riferimento</param>
        ''' <remarks>Fabio Dellarole - 2015</remarks>
        Public Sub InsertUpdateUniqueRecord(sTableName As String, dictRec As Dictionary(Of String, Object), keyName As String, keyValue As String)

            Try
                CloseTable()

                ' Aggiorna il nome della tabella con caratteri "[" e "]"
                Dim sTable = AdjustTableName(sTableName)

                ' Compose condition
                Dim dicCond As Dictionary(Of String, Object) = New Dictionary(Of String, Object)()
                dicCond(keyName) = keyValue

                ' Insert or Update record
                InsertOrUpdate(dictRec, dicCond)

            Catch exc As Exception
                Throw exc

            End Try

        End Sub

        ''' <summary>
        ''' Ritorna il primo record corrente di una query
        ''' </summary>
        ''' <returns>Dictionary [nome campo]-[valore]</returns>
        ''' <remarks>Fabio Dellarole - 2014</remarks>
        Public Function GetCurrentRecord() As Dictionary(Of String, Object)

            Dim objDict As New Dictionary(Of String, Object)

            If reader.HasRows = True AndAlso reader.Read() = True Then
                For i As Integer = 0 To reader.FieldCount - 1
                    Dim value As Object = reader(i).ToString
                    objDict.Add(reader.GetName(i), value)
                Next i
            Else
                Return Nothing
            End If

            Return objDict
        End Function

        ''' <summary>
        ''' Itera i record della tabella sfruttando la variabile statica al suo interno
        ''' </summary>
        ''' <returns>Dictionary [nome campo]-[valore]</returns>
        ''' <remarks>Fabio Dellarole - 2014</remarks>
        Public Function GetNextRecord() As Dictionary(Of String, Object)

            Static s_reader As SQLiteDataReader = reader
            Dim objDict As New Dictionary(Of String, Object)

            If s_reader.Read() = True Then
                For i As Integer = 0 To s_reader.FieldCount - 1
                    Dim value As Object = s_reader(i).ToString
                    objDict.Add(s_reader.GetName(i), value)
                Next i
            Else
                Return Nothing
            End If

            Return objDict
        End Function

        Private Function SelectFrom(ByVal tableName As String, ByVal dicCond As Dictionary(Of String, Object)) As DataTable

            Dim cmd As New SQLiteCommand(objDB.GetConnection)
            SelectFrom(tableName, dicCond, cmd)

            Dim da As New SQLiteDataAdapter(cmd)
            Dim dt As New DataTable
            da.Fill(dt)

            Return dt
        End Function

        Private Function SelectFrom(ByVal tableName As String, ByVal dicCond As Dictionary(Of String, Object), ByRef sqliteCmd As SQLiteCommand) As Boolean

            Try
                If sqliteCmd Is Nothing Then Return False

                Dim sbData As New System.Text.StringBuilder
                If dicCond Is Nothing Then dicCond = New Dictionary(Of String, Object)
                Dim _dicTypeSource As New Dictionary(Of String, Object)

                For Each kv2 As KeyValuePair(Of String, Object) In dicCond
                    If _dicTypeSource.ContainsKey(kv2.Key) = False Then
                        _dicTypeSource(kv2.Key) = Nothing
                    End If
                Next

                sbData.Append("select * from ")
                sbData.Append(tableName)
                Dim firstRecord As Boolean = True

                For Each kv As KeyValuePair(Of String, Object) In dicCond

                    If firstRecord Then
                        sbData.Append(" where ")
                        firstRecord = False
                    Else
                        sbData.Append(" and ")
                    End If

                    sbData.Append("`")
                    sbData.Append(kv.Key)
                    sbData.Append("` = ")
                    sbData.Append("@c")
                    sbData.Append(kv.Key)
                Next

                sbData.Append(";")
                sqliteCmd.CommandText = sbData.ToString()
                sqliteCmd.Parameters.Clear()

                For Each kv As KeyValuePair(Of String, Object) In dicCond
                    If kv.Value Is Nothing Then
                        sqliteCmd.Parameters.AddWithValue("@c" & kv.Key, CObj(DBNull.Value))
                    Else
                        sqliteCmd.Parameters.AddWithValue("@c" & kv.Key, kv.Value)
                    End If
                Next

            Catch ex As Exception
                Return False
            End Try

            Return True
        End Function

#Region " Classe che gestisce la colonna della tabella "

        Public Class Field

            Public Property Name As String
            Public Property Type As String
            Public Property NotNull As Boolean
            Public Property Pk As Boolean

        End Class

#End Region

    End Class

#End Region

End Class
