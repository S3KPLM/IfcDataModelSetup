Imports System.IO
Imports System.Windows.Forms

Public Class frmMain

#Region " Eventi "

    Private Sub btnClose_Click(sender As Object, e As EventArgs) 
        Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs)

        ' Effettua un controllo sui percorsi inseriti
        '

        ' Salva le impostazioni nei Settings
        '''My.Settings.LogPath = txtLogPath.Text
        '''My.Settings.DBFilePath = txtDatabaseFilePath.Text
        '''My.Settings.Save()

        Close()
    End Sub

    Private Sub btnDatabaseFilePathBrowse_Click(sender As Object, e As EventArgs)

        Dim dlg As New OpenFileDialog
        With dlg
            .Title = "Select SQLite Database file"
            .Filter = "SQLite DB (*.db3; *.db)|*.db3;*.db"
            .RestoreDirectory = True
            .CheckFileExists = True
            .CheckPathExists = True
            .ValidateNames = True
        End With

        Dim result As DialogResult = dlg.ShowDialog()

    End Sub

    Private Sub btnLogPathBrowse_Click(sender As Object, e As EventArgs)

        Dim dlg As New FolderBrowserDialog
        With dlg
            .Description = "Select Log Path"
            .ShowNewFolderButton = False
        End With

        Dim result As DialogResult = dlg.ShowDialog()

    End Sub
    '
    Private Sub cmdIfcSelection_Click(sender As Object, e As EventArgs) Handles cmdIfcSelection.Click
        OpenIfcDialog.ShowDialog()
    End Sub

    Private Sub OpenIfcDialog_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenIfcDialog.FileOk

        ' RECA Provvisorio
        ' Quando sistemo il problema del My.Settings lo tolgo
        OpenIfcDialog.InitialDirectory = "C:\01 WORK\Sorgenti\Snam\IFCPreprocessor (versione rivista in VS 2022 a partire dal 4 5 2022)\Samples"

        txtIfcFullPath.Text = OpenIfcDialog.FileName


        ' RECA ho un problema con il My.Settings????
        'My.Settings.IfcLastFileName = txtIfcFullPath.Text
        'Debug.Print(My.Settings.IfcLastFileName)
    End Sub



    Private Sub txtIfcFullPath_TextChanged(sender As Object, e As EventArgs) Handles txtIfcFullPath.Click


    End Sub


    Private Sub txtOutput_Click(sender As Object, e As EventArgs) Handles txtOutput.Click
        'showDataOnTextBox(txtOutput)
    End Sub

    Private Sub cmdAnalyze_Click(sender As Object, e As EventArgs) Handles cmdAnalyze.Click
        Dim ifcProd As IFCProductExt = AnalyzeIFC(txtIfcFullPath.Text)




        'For Each pe1 As IFCProductExt In ifcProd.Items
        '    txtOutput.Text = txtOutput.Text & pe1.ToString() & vbCrLf
        '    For Each pe2 As IFCProductExt In pe1.Items
        '        txtOutput.Text = txtOutput.Text & pe2.ToString() & vbCrLf
        '    Next
        'Next



    End Sub

    Private Sub txtIfcFullPath_TextChanged_1(sender As Object, e As EventArgs) Handles txtIfcFullPath.TextChanged
        cmdAnalyze.Enabled = Trim(txtIfcFullPath.Text) <> ""
    End Sub



#End Region

End Class