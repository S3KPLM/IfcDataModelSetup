Imports System.IO
Imports Xbim.Ifc
Imports Xbim.Ifc4.Interfaces
Imports Xbim.Ifc4.Kernel
Imports Xbim.Ifc4.MeasureResource
Imports Xbim.Ifc4.ProductExtension
Imports Xbim.Ifc4.PropertyResource
Imports Xbim.Ifc4.SharedBldgElements
Imports Xbim.IO
Module mdlMain

    Public Structure STables
        Shared GENERAL As String = "General"
    End Structure

    ' Costanti  
    Public Structure SDataFields
        Shared CODICE_APP As String = "CodiceApp"
        Shared DESCRIZIONE As String = "Descrizione"
        Shared IFC As String = "IFC"
        Shared PROPERTY_SET As String = "PropertySet"
        Shared [PROPERTY] As String = "Property"
    End Structure

    Public Sub Main(args() As String)

        Try
            Dim MyIfcWriter As New MyIfcUtils
            'Dim myIfcStorey As IfcBuildingStorey
            '
            'Impostazione della struttura di base di un file Ifc
            'Project
            ' Model
            '  Storey
            Dim myModel As IfcStore = MyIfcWriter.CreateAndIntModel("modelStart")
            Dim DataModelData As Dictionary(Of String, List(Of String))
            Dim myDMFileMgr As New DataModelFileMgr
            Dim myIfcObj As IfcObject
            Dim myPset As IfcPropertySet
            Dim fileContent As List(Of String)
            Dim ifcObj As String = ""
            Dim ifcObjTyp As String = ""
            Dim ifcObjTypEnum As String = ""
            Dim psetName As String = ""
            Dim addProperty As Boolean = False
            DataModelData = myDMFileMgr.ReadDataModel()
            If (Not myModel Is Nothing) Then
                Dim mySite As IfcSite = MyIfcWriter.CreateSite(myModel, "Default Site")
                If (DataModelData.ContainsKey("IfcSite_SRG.txt")) Then
                    fileContent = DataModelData("IfcSite_SRG.txt")
                    For Each myRec In fileContent
                        'Debug.WriteLine("rec : " & myRec)
                        If (myRec.Contains("ifcObj    : ")) Then
                            ifcObj = myRec.Replace("ifcObj    : ", "")
                            Debug.WriteLine("-----ifcObj : " & ifcObj)
                            addProperty = False
                        End If
                        If (myRec.Contains("ifcObj typ: ") Or myRec.Contains("ifcObj typ:")) Then
                            ifcObjTyp = myRec.Replace("ifcObj typ:", "")
                            ifcObjTyp = myRec.Replace(" ", "")
                            Debug.WriteLine("-----ifcObjTyp :" & ifcObjTyp & "<")
                            'If (ifcObj.Length() > 0) Then
                            '    myIfcObj = MyIfcWriter.CreateIfcObject(myModel, ifcObj, ifcObjTyp, "default" & ifcObj)
                            '    addProperty = False
                            'End If
                        End If
                        If (myRec.Contains("PSet: ")) Then
                            psetName = myRec.Replace("PSet: ", "")
                            myPset = MyIfcWriter.AddPropertySet(myModel, mySite, psetName)
                            addProperty = True
                        End If
                        If (myRec.Length() <= 0) Then
                            addProperty = False
                        End If
                        If (addProperty And Not myRec.Contains("PSet: ")) Then
                            ' 01/05/2023
                            ' modifico logica creazione attributi per introdurre gestione DataType
                            ' come da aggiornamento modello dati del 30/03/2023
                            '
                            'nuovo tracciato record:
                            '<nome attributo>#|#<valori ammissibili>#|#<unità di misura>#|#<tipo di dato (grandezza)>
                            'grandezza <=> Length;MassMeasure;Pressure;Integer;Real;CalendarDate;PlaneAngle;ElectricCurrent;
                            '              MassPerLength;MassDensity;Power;VolumetricFlowRate;ThermodynamicTemperature;
                            '              Ratio;ElectricResistance;Text
                            MyIfcWriter.AddProperty(myModel, myPset, myRec)
                        End If
                    Next
                End If
                '
                Dim myBuilding As IfcBuilding = MyIfcWriter.CreateBuilding(myModel, "Default building")
                If (Not myBuilding Is Nothing) Then
                    Dim myObj As IfcObject = MyIfcWriter.CreateIfcObject(myModel, "IfcBuildingStorey", "", "", "DefaultStorey")
                    If (Not myObj Is Nothing) Then
                        '
                        ' Lettura dei file di data model e per ognuno
                        '    aggiunta det tipo IFC
                        '       ciclo sui Pset definiti a livello di modello dati
                        '           aggiunta Pset al tipo
                        '               aggiunta proprietà...
                        '
                        For Each key In DataModelData.Keys
                            Debug.WriteLine("Obj : " & key)
                            If (Not key.Equals("IfcSite_SRG.txt")) Then
                                fileContent = DataModelData(key)
                                'Dim fileContent As List(Of String)
                                'Dim ifcObj As String = ""
                                'Dim ifcObjTyp As String = ""
                                'Dim psetName As String = ""
                                'Dim addProperty As Boolean = False
                                For Each myRec In fileContent
                                    Debug.WriteLine("rec : " & myRec)
                                    If (myRec.Contains("ifcObj    : ")) Then
                                        ifcObj = myRec.Replace("ifcObj    : ", "")
                                        Debug.WriteLine("-----ifcObj : " & ifcObj)
                                        addProperty = False
                                    End If
                                    If (myRec.Contains("ifcObj typ: ") Or myRec.Contains("ifcObj typ:")) Then
                                        ifcObjTyp = myRec.Replace("ifcObj typ:", "")
                                        ifcObjTyp = ifcObjTyp.Replace(" ", "")
                                        Debug.WriteLine("-----ifcObjTyp :" & ifcObjTyp & "<")
                                    End If
                                    If (myRec.Contains("ifcObj typ enum: ") Or myRec.Contains("ifcObj typ enum:")) Then
                                        ifcObjTypEnum = myRec.Replace("ifcObj typ enum:", "")
                                        ifcObjTypEnum = ifcObjTypEnum.Replace(" ", "")
                                        Debug.WriteLine("-----ifcObjTypEnum :" & ifcObjTypEnum & "<")
                                        If (ifcObj.Length() > 0) Then
                                            myIfcObj = MyIfcWriter.CreateIfcObject(myModel, ifcObj, ifcObjTyp, ifcObjTypEnum, "default" & ifcObj)
                                            addProperty = False
                                        End If
                                    End If
                                    If (myRec.Contains("PSet: ")) Then
                                        psetName = myRec.Replace("PSet: ", "")
                                        myPset = MyIfcWriter.AddPropertySet(myModel, myIfcObj, psetName)
                                        addProperty = True
                                        'MyIfcWriter.AddPropertySetToObjType(myModel, myIfcObj, ifcObjTyp, myPset)
                                    End If
                                    If (myRec.Length() <= 0) Then
                                        addProperty = False
                                    End If
                                    If (addProperty And Not myRec.Contains("PSet: ")) Then
                                        ' 01/05/2023
                                        ' modifico logica creazione attributi per introdurre gestione DataType
                                        ' come da aggiornamento modello dati del 30/03/2023
                                        '
                                        If (myRec.Contains("#|#")) Then
                                            'nuovo tracciato record:
                                            '<nome attributo>#|#<valori ammissibili>#|#<unità di misura>#|#<tipo di dato (grandezza)>
                                            'grandezza <=> Length;MassMeasure;Pressure;Integer;Real;CalendarDate;PlaneAngle;ElectricCurrent;
                                            '              MassPerLength;MassDensity;Power;VolumetricFlowRate;ThermodynamicTemperature;
                                            '              Ratio;ElectricResistance;Text
                                            '
                                            'nuovo tracciato record:
                                            '<nome attributo>#|#<valori ammissibili>#|#<unità di misura>#|#<tipo di dato (grandezza)>
                                            'grandezza <=> Length;MassMeasure;Pressure;Integer;Real;CalendarDate;PlaneAngle;ElectricCurrent;
                                            '              MassPerLength;MassDensity;Power;VolumetricFlowRate;ThermodynamicTemperature;
                                            '              Ratio;ElectricResistance;Text
                                            MyIfcWriter.AddProperty(myModel, myPset, myRec)
                                            '
                                        Else
                                            MyIfcWriter.addSingleValueProperty(myModel, myPset, myRec, "")
                                        End If
                                    End If
                                Next
                            End If
                        Next
                    End If
                End If
                myModel.FileName = "C:\Temp\SNM_Rete_tst\IfcMapping\DummyIfcForDataModelExtension.ifc"
                myModel.Header.FileDescription.Description.Add("ViewDefinition [CoordinationView_V2.0]")
                myModel.SaveAs("C:\Temp\SNM_Rete_tst\IfcMapping\DummyIfcForDataModelExtension.ifc")
                Debug.WriteLine("-----elaborazione terminata" & "<")
            End If
        Catch exc As Exception
            'Riportare nel log l'errore
            Console.WriteLine(" Erroe : " & Err.Description & " - " & Err.Number)
            Debug.WriteLine(" Erroe : " & Err.Description & " - " & Err.Number)
            End
        End Try







    End Sub

    Public Function AnalyzeIFC(fileFullName As String) As IFCProductExt

        Dim xbim As New clsXBimUtils

        ' RECA
        Dim ifcProd As IFCProductExt = xbim.GetStructure(fileFullName)

        Return ifcProd

    End Function



    Public Sub showDataOnTextBox(tb As TextBox)

        ' Apre il DB
        Dim objDB As New clsSQLiteDB

        Dim di As New DirectoryInfo(Application.ExecutablePath)
        objDB.OpenDB(Path.Combine(di.Parent.Parent.Parent.Parent.ToString, "Resources", "info.db3"))

        'Dim cmd As New SQLiteCommand("SELECT * FROM General", objDB.GetConnection)
        'Dim da As New SQLiteDataAdapter(cmd)
        'Dim dt As New DataTable
        'da.Fill(dt)

        'Dim readerDataGrid As SQLiteDataReader = cmd.ExecuteReader()

        ' Istanzia l'oggetto tabella
        Dim tbl As New clsSQLiteDB.DBTable(objDB, STables.GENERAL, True)




        ' Effettua la query sulla tabella
        tbl.OpenTableWithQuery("SELECT * FROM General")

        ' Ricava i record dalla tabella
        Dim recList As List(Of Dictionary(Of String, Object)) = tbl.GetAllRecordsAsListMap


        For Each dict As Dictionary(Of String, Object) In recList

            For Each s As String In dict.Keys
                tb.Text = tb.Text & s & vbCrLf
            Next


        Next


        objDB.CloseDB()

    End Sub

End Module
