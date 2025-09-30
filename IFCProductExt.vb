Imports Xbim.Ifc4.Interfaces

Public Class IFCProductExt

#Region " Proprietà "

    Public Property Type As String
    Public Property Name As String
    Public Property Element As IIfcObjectDefinition
    Public Property Level As Integer
    Public Property Validation As New Check
    '
    ' Lista di proprietà piatte
    Public Property Properties As New List(Of PropertyItem)
    '
    ' Lista di proprietà raggruppate per set
    Public Property PropertyGroup As New Dictionary(Of String, List(Of PropertyItem))
    Public Property Items As New List(Of IFCProductExt)

#End Region

#Region " Costruttori "

    Public Sub New(element As IIfcObjectDefinition)
        Me.New(element, 0)
    End Sub

    Public Sub New(element As IIfcObjectDefinition, level As Integer)
        Me.New(element, level, True)
    End Sub

    Public Sub New(element As IIfcObjectDefinition, level As Integer, getProperies As Boolean)

        Me.Element = element
        Me.Level = level

        Type = element.GetType().Name
        Name = element.Name.ToString

        If getProperies = True Then
            ' Ricava le proprietà dell'elemento
            GetPropertyData()
        End If
    End Sub

#End Region

#Region " Metodi "

    Public Sub GetPropertyData()
        If Properties.Any() Then Return

        If TypeOf Element Is IIfcObject Then
            Dim asIfcObject As IIfcObject = CType(Element, IIfcObject)

            For Each pSet In asIfcObject.IsDefinedBy.Select(Function(relDef) TryCast(relDef.RelatingPropertyDefinition, IIfcPropertySet))
                AddPropertySet(pSet)
            Next

        ElseIf TypeOf Element Is IIfcTypeObject Then
            Dim asIfcTypeObject = TryCast(Element, IIfcTypeObject)
            If asIfcTypeObject.HasPropertySets Is Nothing Then Return

            For Each pSet In asIfcTypeObject.HasPropertySets.OfType(Of IIfcPropertySet)()
                AddPropertySet(pSet)
            Next
        End If
    End Sub

    Private Sub AddPropertySet(ByVal pSet As IIfcPropertySet)
        If pSet Is Nothing Then Return

        For Each item In pSet.HasProperties.OfType(Of IIfcPropertySingleValue)
            AddProperty(item, pSet.Name.ToString)
        Next

        For Each item In pSet.HasProperties.OfType(Of IIfcComplexProperty)

            For Each composingProperty In item.HasProperties.OfType(Of IIfcPropertySingleValue)
                AddProperty(composingProperty, pSet.Name.ToString & " / " & item.Name.ToString)
            Next
        Next

        For Each item In pSet.HasProperties.OfType(Of IIfcPropertyEnumeratedValue)
            AddProperty(item, pSet.Name.ToString)
        Next
    End Sub

    Private Sub AddProperty(ByVal item As IIfcPropertyEnumeratedValue, ByVal groupName As String)

        Dim val As String = ""
        Dim nomVals = item.EnumerationValues

        For Each nomVal As IIfcValue In nomVals
            If nomVal IsNot Nothing Then val = nomVal.ToString

            AddToPropertyList(item.EntityLabel, item.Name, val, groupName)
        Next
    End Sub

    Private Sub AddProperty(item As IIfcPropertySingleValue, groupName As String)

        Dim val As String = ""
        Dim nomVal As IIfcValue = item.NominalValue

        If nomVal IsNot Nothing Then val = nomVal.ToString

        AddToPropertyList(item.EntityLabel, item.Name, val, groupName)
    End Sub

    Private Sub AddToPropertyList(entityLabel As Integer, name As String, value As String, groupName As String)

        ' Istanzia l'elemento
        Dim item As New PropertyItem With {
            .IfcLabel = entityLabel,
            .PropertySetName = groupName,
            .Name = name,
            .Value = value
        }

        ' Popola le proprietà raggruppandole nel set
        Dim propItemList As New List(Of PropertyItem)
        If PropertyGroup.ContainsKey(groupName) = True Then
            ' Il gruppo esiste già del dictionary
            PropertyGroup.TryGetValue(groupName, propItemList)
            propItemList.Add(item)
        Else
            ' Il gruppo non esiste nel dictionary
            propItemList.Add(item)
            PropertyGroup.Add(groupName, propItemList)
        End If

        ' Popola le proprietà senza raggruppamento
        Properties.Add(New PropertyItem With {
            .IfcLabel = entityLabel,
            .PropertySetName = groupName,
            .Name = name,
            .Value = value
        })
    End Sub

#End Region

#Region " Classe che gestisce la property "

    Public Class PropertyItem

        Public Property Units As String
        Public Property PropertySetName As String
        Public Property Name As String
        Public Property IfcLabel As Integer

        Public ReadOnly Property IfcUri As String
            Get
                Return "xbim://EntityLabel/" & IfcLabel
            End Get
        End Property

        Public ReadOnly Property IsLabel As Boolean
            Get
                Return IfcLabel > 0
            End Get
        End Property

        Public Property Value As String
        Private ReadOnly _schemas As String() = {"file", "ftp", "http", "https"}

        Public ReadOnly Property IsLink As Boolean
            Get
                Dim uri As Uri = Nothing
                If Not Uri.TryCreate(Value, UriKind.Absolute, uri) Then Return False
                Dim schema = uri.Scheme
                Return _schemas.Contains(schema)
            End Get
        End Property
    End Class

#End Region

#Region " Classe che gestisce la validazione del dato "

    Public Class Check

        Public Enum ECheck As Byte
            NONE = 0
            OK = 1
            FAIL = 2
        End Enum

        Public Property Check As ECheck = ECheck.NONE
        Public Property Description As String

    End Class

#End Region

End Class
