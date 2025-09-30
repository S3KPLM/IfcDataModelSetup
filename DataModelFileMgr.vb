Imports System.IO

Public Class DataModelFileMgr
    Private ErrorReadingFile As String
    Private Function ReadFile(fi As FileInfo, Optional ByVal maxLines As Integer = 0) As List(Of String)
        '
        ' Remigio Cassalino 7/7/2022
        '
        ' Funzione che verifica l'esistenza di un file e se esiste lo legge e ne restituisce il contenuto sotto forma di lista di stringhe
        ' Se il file non esiste valorizza ErrorReadingFile e restituisce Nothing
        ' Se avviene un errorre durante la lettura valorizza ErrorReadingFile e restituisce Nothing
        '
        ' Se il parametro maxLines è diverso da 0 indica quante linee leggere
        '
        ' ISO 8859-16
        ' Per getire correttamente le lettere accentate occorre salvare i files con L'unicode corretto.
        ' Vedi comando Notepad++ "Formato\Set di caratteri"
        '
        ErrorReadingFile = ""
        '
        Try ' Questo try serve per gestire eventuali errori inaspettati che impediscano di leggere il file
            If Not fi.Exists Then
                ErrorReadingFile = "File " & fi.FullName & " not found"
                Return Nothing
            End If
            '
            Dim fileContent As New List(Of String)
            'Dim ISO8859 As System.Text.Encoding = System.Text.Encoding.GetEncoding(1252)
            Dim sReader As StreamReader
            '
            Try
                'sReader = New StreamReader(fi.FullName, ISO8859) ' Tento la lettura
                sReader = New StreamReader(fi.FullName) ' Tento la lettura
                Dim i As Long = 0
                While sReader.EndOfStream = False                   ' Leggo il file una riga per volta (questo metodo, l'ho testato, è più performante di altri)
                    Try
                        Dim line As String = sReader.ReadLine.Trim
                        If maxLines > 0 And i > maxLines Then Exit While
                        i += 1
                        If line <> vbNullString Then
                            Try
                                fileContent.Add(line) ' Le righe vuote vengono ignorate
                            Catch ex As Exception
                                ErrorReadingFile = "Problems adding contents in multifile string list " & Err.Description
                                Return Nothing
                            End Try
                        End If
                    Catch ex As Exception
                        ErrorReadingFile = "Problems during reading line " & i & " file " & fi.FullName & " " & Err.Description
                        Return Nothing
                    End Try
                End While
                sReader.Close()
                sReader.Dispose()
                Return fileContent
            Catch ex As Exception
                ErrorReadingFile = "Problems during reading file " & fi.FullName & " " & Err.Description
                Return Nothing
            End Try

        Catch ex As Exception
            ErrorReadingFile = Err.Description
        End Try
        '
        Return Nothing
        '
    End Function


    Public Function ReadDataModel() As Dictionary(Of String, List(Of String))
        '
        ' Remigio Cassalino 8/7/2022
        '
        ' Lettura del data model proveniente dal PLM
        '
        Dim dataModel As New Dictionary(Of String, List(Of String))
        Dim rootDir As New DirectoryInfo(Application.StartupPath)
        Dim dataModelDir As New DirectoryInfo(Path.Combine(rootDir.Parent.Parent.Parent.FullName, "PlmData", "DataModel"))
        For Each fi As FileInfo In dataModelDir.GetFiles()
            dataModel.Add(fi.Name, ReadFile(fi))
        Next
        Return dataModel
    End Function
End Class
