Imports Xbim.Ifc
Imports Xbim.Ifc4.Interfaces

Public Class clsXBimUtils

    Public Function GetStructure(filePathName As String) As IFCProductExt

        Dim ifcProd As IFCProductExt

        Using model = IfcStore.Open(filePathName)
            'MessageBox(model.SchemaVersion.ToString)
            Dim project = model.Instances.FirstOrDefault(Of IIfcProject)
            ifcProd = New IFCProductExt(project)

            GetStructure(ifcProd, 0)

            Return ifcProd
        End Using

    End Function

    Public Sub GetStructure(ByRef parent As IFCProductExt, level As Integer)

        Console.WriteLine(String.Format("{0}{1} [{2}]", GetIndent(level), parent.Element.Name, parent.Element.[GetType]().Name))
        Dim spatialElement = TryCast(parent.Element, IIfcSpatialStructureElement)

        If spatialElement IsNot Nothing Then
            Dim containedElements = spatialElement.ContainsElements.SelectMany(Function(rel) rel.RelatedElements)

            For Each element In containedElements
                Console.WriteLine(String.Format("{0}    ->{1} [{2}]", GetIndent(level), element.Name, element.[GetType]().Name))

                Dim ifcItem As New IFCProductExt(element, level)
                'GetProperties(ifcItem)

                parent.Items.Add(ifcItem)
            Next
        End If

        For Each item In parent.Element.IsDecomposedBy.SelectMany(Function(r) r.RelatedObjects)

            Dim ifcItem As New IFCProductExt(item, level + 1)
            parent.Items.Add(ifcItem)

            GetStructure(ifcItem, level + 1)
        Next

    End Sub

    'Private Sub GetProperties(ByRef ifcItem As IFCProductExt)

    '    Dim element As IIfcProduct = ifcItem.Element
    '    Dim properties = element.IsDefinedBy.Where(Function(r) TypeOf r.RelatingPropertyDefinition Is IIfcPropertySet).SelectMany(Function(r) (CType(r.RelatingPropertyDefinition, IIfcPropertySet)).HasProperties).OfType(Of IIfcPropertySingleValue)()

    '    For Each [property] In properties
    '        Console.WriteLine($"Property: {[property].Name}, Value: {[property].NominalValue}")
    '        ifcItem.Properties.Add([property].Name, [property].NominalValue)
    '    Next

    'End Sub

    Private Shared Function GetIndent(ByVal level As Integer) As String
        Dim indent = ""

        For i As Integer = 0 To level - 1
            indent += "  "
        Next

        Return indent
    End Function

End Class
