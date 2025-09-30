Imports System.IO
Imports Newtonsoft.Json

Public Class clsConfigs

    <JsonProperty("log_path")>
    Public Property LogPath As String

    <JsonProperty("db_file_path")>
    Public Property DBFilePath As String

    Public Sub New(configFilePath As String)

        If File.Exists(configFilePath) = False Then
            Throw New Exception("File config not found: " & configFilePath)
        End If

        Dim meTemp As clsConfigs = JsonConvert.DeserializeObject(Of clsConfigs)(File.ReadAllText(configFilePath))
        LogPath = meTemp.LogPath
        DBFilePath = meTemp.DBFilePath
    End Sub

End Class
