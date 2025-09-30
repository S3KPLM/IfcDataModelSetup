Imports System.IO
Imports Xbim.Ifc
Imports Xbim.Ifc4.GeometricConstraintResource
Imports Xbim.Ifc4.GeometryResource
Imports Xbim.Ifc4.Interfaces
Imports Xbim.Ifc4.Kernel
Imports Xbim.Ifc4.MeasureResource
Imports Xbim.Ifc4.ProductExtension
Imports Xbim.Ifc4.SharedBldgElements
Imports Xbim.Ifc4.SharedComponentElements
Imports Xbim.IO
Imports Xbim.Ifc4.StructuralElementsDomain
Imports Xbim.Ifc4.SharedBldgServiceElements
Imports Xbim.Ifc4.HvacDomain
Imports Xbim.Ifc4.ElectricalDomain
Imports Xbim.Ifc4.QuantityResource
'Imports Xbim.Ifc4.QuantityResource
Imports Xbim.Ifc4.DateTimeResource
'Imports Xbim.Ifc4.DateTimeResource
Imports Xbim.Ifc4.PropertyResource

Public Class MyIfcUtils
    Public Function CreateAndIntModel(modelName As String) As IfcStore
        Dim myCredentials As New XbimEditorCredentials
        With myCredentials
            .ApplicationDevelopersName = "xbim developer"
            .ApplicationFullName = "xbim toolkit"
            .ApplicationIdentifier = "xbim"
            .ApplicationVersion = "4"
            .EditorsFamilyName = "S3K"
            .EditorsGivenName = "S3K"
            .EditorsOrganisationName = "Independent Architecture"
        End With
        Dim myModel As IfcStore
        myModel = IfcStore.Create(myCredentials, Xbim.Common.Step21.XbimSchemaVersion.Ifc4, XbimStoreType.InMemoryModel)
        '
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Initialize Model ForDatamodelExtension")
        '
        Dim myPrj As IfcProject
        myPrj = myModel.Instances.[New](Of IfcProject)()
        myPrj.Name = "ExtendDataMOdel"
        myPrj.Initialize(Xbim.Common.ProjectUnits.SIUnitsUK)
        '
        myTransaction.Commit()
        '
        Return myModel
    End Function
    Public Function CreateIfcObject(myModel As IfcStore, myIfcType As String, myIfcObjType As String, myIfcObjTypeEnum As String, myIfcObjName As String) As IfcObject
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add IfcObject")
        '
        Dim myObj As IfcObject = Nothing
        Select Case myIfcType
            Case "IfcBeam"
                myObj = myModel.Instances.[New](Of IfcBeam)()
                myObj = SetupIfcObj(myObj, myModel, myIfcObjType, myIfcObjTypeEnum, myIfcObjName)
            Case "IfcBuildingElementProxy"
                myObj = myModel.Instances.[New](Of IfcBuildingElementProxy)()
                myObj = SetupIfcObj(myObj, myModel, myIfcObjType, myIfcObjTypeEnum, myIfcObjName)
            Case "IfcDiscreteAccessory"
                myObj = myModel.Instances.[New](Of IfcDiscreteAccessory)()
                myObj = SetupIfcObj(myObj, myModel, myIfcObjType, myIfcObjTypeEnum, myIfcObjName)
            Case "IfcDoor"
                myObj = myModel.Instances.[New](Of IfcDoor)()
                myObj = SetupIfcObj(myObj, myModel, myIfcObjType, myIfcObjTypeEnum, myIfcObjName)
            Case "IfcEnergyConversionDevice"
                myObj = myModel.Instances.[New](Of IfcEnergyConversionDevice)()
                myObj = SetupIfcObj(myObj, myModel, myIfcObjType, myIfcObjTypeEnum, myIfcObjName)
            Case "IfcFlowController"
                myObj = myModel.Instances.[New](Of IfcFlowController)()
                myObj = SetupIfcObj(myObj, myModel, myIfcObjType, myIfcObjTypeEnum, myIfcObjName)
            Case "IfcFlowFitting"
                myObj = myModel.Instances.[New](Of IfcFlowFitting)()
                myObj = SetupIfcObj(myObj, myModel, myIfcObjType, myIfcObjTypeEnum, myIfcObjName)
            Case "IfcFlowMovingDevice"
                myObj = myModel.Instances.[New](Of IfcFlowMovingDevice)()
                myObj = SetupIfcObj(myObj, myModel, myIfcObjType, myIfcObjTypeEnum, myIfcObjName)
            Case "IfcFlowSegment"
                myObj = myModel.Instances.[New](Of IfcFlowSegment)()
                myObj = SetupIfcObj(myObj, myModel, myIfcObjType, myIfcObjTypeEnum, myIfcObjName)
            Case "IfcFlowStorageDevice"
                myObj = myModel.Instances.[New](Of IfcFlowStorageDevice)()
                myObj = SetupIfcObj(myObj, myModel, myIfcObjType, myIfcObjTypeEnum, myIfcObjName)
            Case "IfcFlowTreatmentDevice"
                myObj = myModel.Instances.[New](Of IfcFlowTreatmentDevice)()
                myObj = SetupIfcObj(myObj, myModel, myIfcObjType, myIfcObjTypeEnum, myIfcObjName)
            Case "IfcFooting"
                myObj = myModel.Instances.[New](Of IfcFooting)()
                myObj = SetupIfcObj(myObj, myModel, myIfcObjType, myIfcObjTypeEnum, myIfcObjName)
            Case "IfcFurnishingElement"
                myObj = myModel.Instances.[New](Of IfcFurnishingElement)()
                myObj = SetupIfcObj(myObj, myModel, myIfcObjType, myIfcObjTypeEnum, myIfcObjName)
            Case "IfcSite"
                myObj = myModel.Instances.[New](Of IfcSite)()
                myObj = SetupIfcObj(myObj, myModel, myIfcObjType, myIfcObjTypeEnum, myIfcObjName)
            Case "IfcSlab"
                myObj = myModel.Instances.[New](Of IfcSlab)()
                myObj = SetupIfcObj(myObj, myModel, myIfcObjType, myIfcObjTypeEnum, myIfcObjName)
            Case "IfcReinforcingMesh"
                myObj = myModel.Instances.[New](Of IfcReinforcingMesh)()
                myObj = SetupIfcObj(myObj, myModel, myIfcObjType, myIfcObjTypeEnum, myIfcObjName)
            Case "IfcStair"
                myObj = myModel.Instances.[New](Of IfcStair)()
                myObj = SetupIfcObj(myObj, myModel, myIfcObjType, myIfcObjTypeEnum, myIfcObjName)
            Case "IfcBuildingStorey"
                myObj = CreateIfcStorey(myModel, myIfcObjType, myIfcObjName)
            Case "IfcWall"
                myObj = CreateIfcWall(myModel, myIfcObjType, myIfcObjName)
            Case "IfcDistributionControlElement"
                myObj = myModel.Instances.[New](Of IfcDistributionControlElement)()
                myObj = SetupIfcObj(myObj, myModel, myIfcObjType, myIfcObjTypeEnum, myIfcObjName)
        End Select
        '
        myTransaction.Commit()
        Return myObj
    End Function
    Public Sub AddPropertySetToObjType(myModel As IfcStore, myIfcObj As IfcObject, ifcObjTyp As String, myPset As IfcPropertySet)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset to Type")
        '
        Select Case ifcObjTyp
            Case "IfcTransformerType"
                Dim myTransfType As IfcTransformer = myModel.Instances.[New](Of IfcTransformer)()
                myTransfType.AddPropertySet(myPset)
            Case "IfcValveType"
                Dim myValveType As IfcValve = myModel.Instances.[New](Of IfcValve)()
                myValveType.AddPropertySet(myPset)
            Case "IfcPipeFittingType"
                Dim myFittingType As IfcPipeFitting = myModel.Instances.[New](Of IfcPipeFitting)()
                myFittingType.AddPropertySet(myPset)
            Case "IfcPumpType"
                Dim myPumpType As IfcPump = myModel.Instances.[New](Of IfcPump)()
                myPumpType.AddPropertySet(myPset)
            Case "IfcPipeSegmentType"
                Dim mySegmType As IfcPipeSegment = myModel.Instances.[New](Of IfcPipeSegment)()
                mySegmType.AddPropertySet(myPset)
            Case "IfcTankType"
                Dim myTankType As IfcTank = myModel.Instances.[New](Of IfcTank)()
                myTankType.AddPropertySet(myPset)
        End Select
        '
        myTransaction.Commit()

    End Sub
    Public Sub AddProperty(myModel As IfcStore, myPset As IfcPropertySet, myRec As String)
        If (myRec.Contains("#|#")) Then
            'nuovo tracciato record:
            '<nome attributo>#|#<valori ammissibili>#|#<unità di misura>#|#<tipo di dato (grandezza)>
            'grandezza <=> Length;MassMeasure;Pressure;Integer;Real;CalendarDate;PlaneAngle;ElectricCurrent;
            '              MassPerLength;MassDensity;Power;VolumetricFlowRate;ThermodynamicTemperature;
            '              Ratio;ElectricResistance;Text
            Dim campi() As String = myRec.Split("#|#")
            'campi(0) = nome attributo
            'campi(1) = valori ammissibili (separati da ",")
            'campi(2) = unità di misura
            'campi(3) = tipo di dato (grandezza)
            '
            Dim campi1() As String = campi(0).Split("_")
            myRec = campi(0)
            Select Case campi(3)
                Case "Area"
                    'If (campi(1).Equals("-")) Then
                    addSingleValueAreaProperty(myModel, myPset, myRec, "", campi(2))
                    'Else
                    'addMultipleValueAreaProperty(myModel, myPset, myRec, campi(1), campi(2))
                    'End If
                Case "Boolean"
                    'If (campi(1).Equals("-")) Then
                    addSingleValueBooleanProperty(myModel, myPset, myRec, "")
                    'Else
                    'addMultipleValueProperty(myModel, myPset, myRec, campi(1))
                    'End If
                Case "CalendarDate"
                    addSingleValueDateProperty(myModel, myPset, myRec, "")
                Case "ElectricCharge"
                    'If (campi(1).Equals("-")) Then
                    addSingleValueElChargProperty(myModel, myPset, myRec, "", campi(2))
                    'Else
                    'addMultipleValueElChargProperty(myModel, myPset, myRec, campi(1), campi(2))
                    'End If
                Case "ElectricCurrent"
                    addSingleValueElCurrentProperty(myModel, myPset, myRec, "", campi(2))
                Case "ElectricResistance"
                    addSingleValueElResitProperty(myModel, myPset, myRec, "", campi(2))
                Case "ElectricVoltage"
                    addSingleValueElVoltageProperty(myModel, myPset, myRec, "", campi(2))
                Case "Force"
                    addSingleValueForceProperty(myModel, myPset, myRec, "", campi(2))
                Case "Frequency"
                    addSingleValueFrequencyProperty(myModel, myPset, myRec, "", campi(2))
                Case "Integer"
                    addSingleValueIntegerProperty(myModel, myPset, myRec, "", campi(2))
                Case "Length"
                    addSingleValueLengthProperty(myModel, myPset, myRec, "", campi(2))
                Case "LinearVelocity"
                    addSingleValueLinearVelProperty(myModel, myPset, myRec, "", campi(2))
                Case "LuminousFlux"
                    addSingleValueLuminFlxProperty(myModel, myPset, myRec, "", campi(2))
                Case "Mass"
                    addSingleValueMassProperty(myModel, myPset, myRec, "", campi(2))
                Case "MassDensity"
                    addSingleValueMassDensProperty(myModel, myPset, myRec, "", campi(2))
                Case "MassMeasure"
                    addSingleValueMassProperty(myModel, myPset, myRec, "", campi(2))
                Case "MassPerLength"
                    addSingleValueMassPerLengthProperty(myModel, myPset, myRec, "", campi(2))
                Case "PlaneAngle"
                    addSingleValuePLanAngleProperty(myModel, myPset, myRec, "", campi(2))
                Case "Power"
                    addSingleValuePowerProperty(myModel, myPset, myRec, "", campi(2))
                Case "Pressure"
                    addSingleValuePressureProperty(myModel, myPset, myRec, "", campi(2))
                Case "Real"
                    addSingleValueRealProperty(myModel, myPset, myRec, "", campi(2))
                Case "SoundPower"
                    addSingleValueSoundPwProperty(myModel, myPset, myRec, "", campi(2))
                Case "Text"
                    'If (campi(1).Equals("-")) Then
                    'attributo di tipo testo senza elenco di valori ammissibili
                    addSingleValueProperty(myModel, myPset, myRec, "")
                    'Else
                    'addMultipleValueProperty(myModel, myPset, myRec, campi(1))
                    'End If
                Case "Time"
                    addSingleValueTimeProperty(myModel, myPset, myRec, "", campi(2))
                Case "Torque"
                    addSingleValueTorqueProperty(myModel, myPset, myRec, "", campi(2))
                Case "Volume"
                    addSingleValueVolumeProperty(myModel, myPset, myRec, "", campi(2))
                Case "VolumetricFlowRate"
                    addSingleValueVolumFlowProperty(myModel, myPset, myRec, "", campi(2))
            End Select
            '
        Else
            addSingleValueProperty(myModel, myPset, myRec, "")
        End If
    End Sub
    Public Sub addSingleValueVolumFlowProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcVolumetricFlowRateMeasure()
        Dim myUnit As IfcSIUnit = myModel.Instances.[New](Of IfcSIUnit)()
        myUnit.UnitType = IfcUnitEnum.VOLUMEUNIT
        myUnit.Name = IfcSIUnitName.CUBIC_METRE
        myProp.Unit = myUnit
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addSingleValueVolumeProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcVolumeMeasure()
        Dim myUnit As IfcSIUnit = myModel.Instances.[New](Of IfcSIUnit)()
        myUnit.UnitType = IfcUnitEnum.VOLUMEUNIT
        myUnit.Name = IfcSIUnitName.CUBIC_METRE
        myProp.Unit = myUnit
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addSingleValueTorqueProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcTorqueMeasure()
        'Dim myUnit As IfcSIUnit = myModel.Instances.[New](Of IfcSIUnit)()
        'myProp.Unit = myUnit
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addSingleValueTimeProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcTimeMeasure()
        Dim myUnit As IfcSIUnit = myModel.Instances.[New](Of IfcSIUnit)()
        myUnit.UnitType = IfcUnitEnum.TIMEUNIT
        myUnit.Name = IfcSIUnitName.SECOND
        myProp.Unit = myUnit
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addSingleValueSoundPwProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcSoundPowerMeasure()
        'Dim myUnit As IfcSIUnit = myModel.Instances.[New](Of IfcSIUnit)()
        'Select Case uom
        '    Case "db"
        '        myUnit.Prefix = IfcSIPrefix.CENTI
        '    Case "barg"
        '        myUnit.Prefix = IfcSIPrefix.CENTI
        'End Select
        'myProp.Unit = myUnit
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addSingleValueRealProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcReal()
        'Dim myUnit As IfcSIUnit = myModel.Instances.[New](Of IfcSIUnit)()
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addSingleValuePressureProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcPressureMeasure()
        Dim myUnit As IfcSIUnit = myModel.Instances.[New](Of IfcSIUnit)()
        myUnit.UnitType = IfcUnitEnum.PRESSUREUNIT
        myUnit.Name = IfcSIUnitName.PASCAL
        'Select Case uom
        '    Case "bar"
        '        myUnit.Prefix = IfcSIPrefix.CENTI
        '    Case "barg"
        '        myUnit.Prefix = IfcSIPrefix.CENTI
        'End Select
        myProp.Unit = myUnit
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addSingleValuePowerProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcPowerMeasure()
        Dim myUnit As IfcSIUnit = myModel.Instances.[New](Of IfcSIUnit)()
        myUnit.UnitType = IfcUnitEnum.POWERUNIT
        myUnit.Name = IfcSIUnitName.WATT
        Select Case uom
            Case "kW"
                myUnit.Prefix = IfcSIPrefix.KILO
            Case "kWh"
                myUnit.Prefix = IfcSIPrefix.KILO
            Case "kWA"
                myUnit.Prefix = IfcSIPrefix.KILO
        End Select
        myProp.Unit = myUnit
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addSingleValuePLanAngleProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcPlaneAngleMeasure()
        'Dim myUnit As IfcSIUnit = myModel.Instances.[New](Of IfcSIUnit)()
        'myUnit.UnitType = IfcUnitEnum.PLANEANGLEUNIT
        ''myUnit.Name = IfcSIUnitName.DEGREE_CELSIUS
        'myProp.Unit = myUnit
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addSingleValueMassPerLengthProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcMassPerLengthMeasure()
        Dim myUnit As IfcSIUnit = myModel.Instances.[New](Of IfcSIUnit)()
        myUnit.UnitType = IfcUnitEnum.MASSUNIT
        myUnit.Name = IfcSIUnitName.GRAM
        myProp.Unit = myUnit
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addSingleValueMassDensProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcMassDensityMeasure()
        Dim myUnit As IfcSIUnit = myModel.Instances.[New](Of IfcSIUnit)()
        myUnit.UnitType = IfcUnitEnum.MASSUNIT
        myUnit.Name = IfcSIUnitName.GRAM
        myProp.Unit = myUnit
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addSingleValueMassProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcMassMeasure()
        Dim myUnit As IfcSIUnit = myModel.Instances.[New](Of IfcSIUnit)()
        myUnit.UnitType = IfcUnitEnum.MASSUNIT
        myUnit.Name = IfcSIUnitName.GRAM
        Select Case uom
            Case "kg"
                myUnit.Prefix = IfcSIPrefix.KILO
            Case "mg"
                myUnit.Prefix = IfcSIPrefix.MILLI
            Case "t"
                myUnit.Prefix = IfcSIPrefix.MEGA
        End Select
        myProp.Unit = myUnit
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addSingleValueLuminFlxProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcLuminousFluxMeasure()
        'Dim myUnit As IfcSIUnit = myModel.Instances.[New](Of IfcSIUnit)()
        'myUnit.UnitType = IfcUnitEnum.LUMINOUSFLUXUNIT
        'myUnit.Name = IfcSIUnitName.LUX
        'myProp.Unit = myUnit
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addSingleValueLinearVelProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcLinearVelocityMeasure()
        'Dim myUnit As IfcSIUnit = myModel.Instances.[New](Of IfcSIUnit)()
        'myUnit.UnitType = IfcUnitEnum.LENGTHUNIT
        'myUnit.Name = IfcSIUnitName.METRE
        'myUnit.Dimensions = IIfcDimensionalExponents 'myModel.Instances.[New](Of IfcDimensionalExponents)(1, 0, 0, 0, 0, 0, 0)
        'myProp.Unit = myUnit
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addSingleValueLengthProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcLengthMeasure()
        Dim myUnit As IfcSIUnit = myModel.Instances.[New](Of IfcSIUnit)()
        myUnit.UnitType = IfcUnitEnum.LENGTHUNIT
        myUnit.Name = IfcSIUnitName.METRE
        Select Case uom
            Case "mm"
                myUnit.Prefix = IfcSIPrefix.MILLI
            Case "cm"
                myUnit.Prefix = IfcSIPrefix.CENTI
            Case "km"
                myUnit.Prefix = IfcSIPrefix.KILO
        End Select
        myProp.Unit = myUnit
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addSingleValueIntegerProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcInteger() 'IfcPositiveInteger()
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addSingleValueFrequencyProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcFrequencyMeasure()
        Dim myUnit As IfcSIUnit = myModel.Instances.[New](Of IfcSIUnit)()
        myUnit.UnitType = IfcUnitEnum.FREQUENCYUNIT
        myUnit.Name = IfcSIUnitName.HERTZ
        Select Case uom
            Case "kHz"
                myUnit.Prefix = IfcSIPrefix.KILO
        End Select
        myProp.Unit = myUnit
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addSingleValueForceProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcForceMeasure()
        Dim myUnit As IfcSIUnit = myModel.Instances.[New](Of IfcSIUnit)()
        myUnit.UnitType = IfcUnitEnum.FORCEUNIT
        myUnit.Name = IfcSIUnitName.NEWTON
        Select Case uom
            Case "kN"
                myUnit.Prefix = IfcSIPrefix.KILO
        End Select
        myProp.Unit = myUnit
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addSingleValueElVoltageProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcElectricVoltageMeasure()
        Dim myUnit As IfcSIUnit = myModel.Instances.[New](Of IfcSIUnit)()
        myUnit.UnitType = IfcUnitEnum.ELECTRICVOLTAGEUNIT
        myUnit.Name = IfcSIUnitName.VOLT
        Select Case uom
            Case "kV"
                myUnit.Prefix = IfcSIPrefix.KILO
        End Select
        myProp.Unit = myUnit
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addSingleValueElResitProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcElectricResistanceMeasure()
        Dim myUnit As IfcSIUnit = myModel.Instances.[New](Of IfcSIUnit)()
        myUnit.UnitType = IfcUnitEnum.ELECTRICRESISTANCEUNIT
        myUnit.Name = IfcSIUnitName.OHM
        Select Case uom
            Case "mohm"
                myUnit.Prefix = IfcSIPrefix.MILLI
        End Select
        myProp.Unit = myUnit
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addSingleValueElCurrentProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcElectricCurrentMeasure()
        'Dim myUnit As IfcSIUnit = myModel.Instances.[New](Of IfcSIUnit)()
        'myUnit.UnitType = IfcUnitEnum.ELECTRICCURRENTUNIT
        'myUnit.Name = IfcSIUnitName.AMPERE
        'Select Case uom
        '    Case "mAh"
        '        myUnit.Prefix = IfcSIPrefix.MILLI
        'End Select
        'myProp.Unit = myUnit
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addMultipleValueElChargProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValues As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        '
        myTransaction.Commit()
    End Sub
    Public Sub addSingleValueElChargProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcElectricChargeMeasure()
        'Dim myUnit As IfcSIUnit = myModel.Instances.[New](Of IfcSIUnit)()
        'myUnit.UnitType = IfcUnitEnum.ELECTRICCHARGEUNIT
        'myUnit.Name = IfcSIUnitName.AMPERE
        'Select Case uom
        '    Case "mAh"
        '        'myUnit.Prefix = IfcSIPrefix.MILLI
        'End Select
        'myProp.Unit = myUnit
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addSingleValueDateProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        'Dim mycald As IfcCalendarDate = myModel.Instances.[New](Of IfcCalendarDate)()
        'mycald.DayComponent = Date.Now.Day
        'mycald.MonthComponent = Date.Now.Month
        'mycald.YearComponent = Date.Now.Year
        'Dim myrefp As IfcPropertyReferenceValue = myModel.Instances.[New](Of IfcPropertyReferenceValue)()
        'myrefp.PropertyReference = mycald
        'myrefp.Name = "Date"
        'Dim myProp As IfcComplexProperty = myModel.Instances.[New](Of IfcComplexProperty)()
        'myProp.HasProperties.Add(myrefp)
        'myProp.Name = propertyName
        'myProp.UsageName = Date.Now.ToString
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcText("-")  'IfcDate()
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub



    Public Sub addSingleValueBooleanProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        Dim myBoolean As New IfcBoolean()
        myProp.Name = propertyName
        myProp.NominalValue = myBoolean
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addMultipleValueAreaProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValues As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        '
        Dim myAreaUnit As IfcSIUnit = myModel.Instances.[New](Of IfcSIUnit)()
        myAreaUnit.UnitType = IfcUnitEnum.AREAUNIT
        myAreaUnit.Name = IfcSIUnitName.SQUARE_METRE
        Select Case uom
            Case "m2"
            Case "cm2"
                myAreaUnit.Prefix = IfcSIPrefix.CENTI
            Case "mm2"
                myAreaUnit.Prefix = IfcSIPrefix.MILLI
            Case "km2"
                myAreaUnit.Prefix = IfcSIPrefix.KILO
            Case "hm2"
        End Select
        '
        Dim areaList As New List(Of IfcAreaMeasure)
        Dim campi() As String = propertyValues.Split(",")
        For Each campo In campi
            areaList.Add(New IfcAreaMeasure(campo))
        Next
        Dim myListOfProp As New List(Of IfcProperty)
        Dim myListOfPropVal As IfcPropertyListValue = myModel.Instances.[New](Of IfcPropertyListValue)()
        With myListOfPropVal
            .Name = "Valori ammissibili"
            .ListValues.AddRange(areaList.Cast(Of IfcValue)().ToList())
            .Unit = myAreaUnit
        End With
        myListOfProp.Add(myListOfPropVal)
        Dim myProp As IfcComplexProperty = myModel.Instances.[New](Of IfcComplexProperty)()
        myProp.HasProperties.AddRange(myListOfProp)
        myProp.Name = propertyName
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addSingleValueAreaProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String, uom As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        'Dim myPropColl As List(Of IfcPropertySingleValue) = New List(Of IfcPropertySingleValue)
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcAreaMeasure()
        Dim myAreaUnit As IfcSIUnit = myModel.Instances.[New](Of IfcSIUnit)()
        myAreaUnit.UnitType = IfcUnitEnum.AREAUNIT
        myAreaUnit.Name = IfcSIUnitName.SQUARE_METRE

        Select Case uom
            Case "m2"
            Case "cm2"
                myAreaUnit.Prefix = IfcSIPrefix.CENTI
            Case "mm2"
                myAreaUnit.Prefix = IfcSIPrefix.MILLI
            Case "km2"
                myAreaUnit.Prefix = IfcSIPrefix.KILO
            Case "hm2"
        End Select

        myProp.Unit = myAreaUnit
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
    End Sub
    Public Sub addSingleValueProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValue As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        '--
        'Dim myPropColl As List(Of IfcPropertySingleValue) = New List(Of IfcPropertySingleValue)
        Dim myProp As IfcPropertySingleValue = myModel.Instances.[New](Of IfcPropertySingleValue)()
        myProp.Name = propertyName
        myProp.NominalValue = New IfcText("-") 'IfcText(propertyValue)
        'myPropColl.Add(myProp)
        myPset.HasProperties.Add(myProp)
        myTransaction.Commit()
        'myPropColl = Nothing
    End Sub
    Public Sub addMultipleValueProperty(myModel As IfcStore, myPset As IfcPropertySet, propertyName As String, propertyValues As String)
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        '
        Dim labelList As New List(Of IfcLabel)
        Dim campi() As String = propertyValues.Split(",")
        For Each campo In campi
            labelList.Add(New IfcLabel(campo))
        Next
        'Dim myListOfProp As New List(Of IfcProperty)
        Dim myListOfPropVal As IfcPropertyListValue = myModel.Instances.[New](Of IfcPropertyListValue)()
        With myListOfPropVal
            .Name = "Valori ammissibili"
            .ListValues.AddRange(labelList.Cast(Of IfcValue)().ToList())
        End With
        'myListOfProp.Add(myListOfPropVal)
        'Dim myProp As IfcComplexProperty = myModel.Instances.[New](Of IfcComplexProperty)()
        'myProp.HasProperties.AddRange(myListOfProp)
        'myProp.Name = propertyName
        Dim myProp As IfcPropertyListValue = myModel.Instances.[New](Of IfcPropertyListValue)()
        myProp.Name = propertyName
        myProp.ListValues.AddRange(labelList.Cast(Of IfcValue)().ToList())
        myPset.HasProperties.Add(myProp)
        '
        myTransaction.Commit()
    End Sub

    Public Function AddPropertySet(myModel As IfcStore, myIfcObj As IfcObject, psetName As String) As IfcPropertySet
        Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add Pset")
        '
        Dim rel As IfcRelDefinesByProperties = myModel.Instances.FirstOrDefault(Of IfcRelDefinesByProperties)()
        Dim myPset As IfcPropertySet = myModel.Instances.[New](Of IfcPropertySet)()
        myPset.Name = psetName
        'rel.RelatingPropertyDefinition = myPset
        'myPset.DefinesType.
        '
        myIfcObj.AddPropertySet(myPset)
        '
        myTransaction.Commit()
        Return myPset
    End Function

    Public Function SetupIfcObj(myObj As IfcObject, myModel As IfcStore, myIfcObjType As String, myIfcObjTypeEnum As String, myIfcObjName As String) As IfcObject
        'Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add IfcWall")
        '
        myObj.Name = myIfcObjName
        myObj.ObjectType = myIfcObjType
        Dim myProduct As IfcProduct = CType(myObj, IfcProduct)
        Dim myIfcStorey As IfcBuildingStorey = myModel.Instances.OfType(Of IfcBuildingStorey).FirstOrDefault()
        '
        Dim rel0 As IfcRelAggregates = myModel.Instances.[New](Of IfcRelAggregates)()
        rel0.RelatingObject = myIfcStorey
        rel0.RelatedObjects.Add(myProduct)
        '
        'Dim rel As IfcRelDefinesByProperties = myModel.Instances.[New](Of IfcRelDefinesByProperties)()
        'rel.RelatedObjects.Add(myProduct)
        '
        If (myIfcObjType.Length() > 0) Then
            '
            createIfcObjType(myObj, myModel, myIfcObjType, myIfcObjTypeEnum)
            '
        End If
        'myTransaction.Commit()
        Return myObj
    End Function
    Public Sub createIfcObjType(myObj As IfcObject, myModel As IfcStore, myIfcObjType As String, myIfcObjTypeEnum As String)
        Select Case myIfcObjType
            Case "IfcTransformerType"
                Dim myTransfType As IfcTransformerType = myModel.Instances.[New](Of IfcTransformerType)()
                myTransfType.Name = myIfcObjType
                myTransfType.PredefinedType = IfcTransformerTypeEnum.NOTDEFINED
                Dim rel_a As IfcRelDefinesByType = myModel.Instances.[New](Of IfcRelDefinesByType)()
                rel_a.RelatedObjects.Add(myObj)
                rel_a.RelatingType = myTransfType
            Case "IfcValveType"
                Dim myValveType As IfcValveType = myModel.Instances.[New](Of IfcValveType)()
                myValveType.Name = myIfcObjType
                myValveType.PredefinedType = IfcValveTypeEnum.NOTDEFINED
                Dim rel_a As IfcRelDefinesByType = myModel.Instances.[New](Of IfcRelDefinesByType)()
                rel_a.RelatedObjects.Add(myObj)
                rel_a.RelatingType = myValveType
            Case "IfcPipeFittingType"
                Dim myFittingType As IfcPipeFittingType = myModel.Instances.[New](Of IfcPipeFittingType)()
                myFittingType.Name = myIfcObjType
                myFittingType.PredefinedType = IfcPipeFittingTypeEnum.NOTDEFINED
                Select Case myIfcObjTypeEnum
                    Case "JUNCTION"
                        myFittingType.PredefinedType = IfcPipeFittingTypeEnum.JUNCTION
                        Dim myPort1 As IfcDistributionPort = myModel.Instances.[New](Of IfcDistributionPort)()
                        Dim rel_port1 As IfcRelConnectsPortToElement = myModel.Instances.[New](Of IfcRelConnectsPortToElement)()
                        myPort1.Name = "1"
                        rel_port1.RelatedElement = CType(myObj, IfcDistributionElement)
                        rel_port1.RelatingPort = myPort1
                        Dim myPort2 As IfcDistributionPort = myModel.Instances.[New](Of IfcDistributionPort)()
                        Dim rel_port2 As IfcRelConnectsPortToElement = myModel.Instances.[New](Of IfcRelConnectsPortToElement)()
                        myPort2.Name = "2"
                        rel_port2.RelatedElement = CType(myObj, IfcDistributionElement)
                        rel_port2.RelatingPort = myPort2
                        Dim myPort3 As IfcDistributionPort = myModel.Instances.[New](Of IfcDistributionPort)()
                        Dim rel_port3 As IfcRelConnectsPortToElement = myModel.Instances.[New](Of IfcRelConnectsPortToElement)()
                        myPort3.Name = "3"
                        rel_port3.RelatedElement = CType(myObj, IfcDistributionElement)
                        rel_port3.RelatingPort = myPort3
                    Case "CONNECTOR"
                        myFittingType.PredefinedType = IfcPipeFittingTypeEnum.CONNECTOR
                    Case "OBSTRUCTION"
                        myFittingType.PredefinedType = IfcPipeFittingTypeEnum.OBSTRUCTION
                        Dim myPort As IfcDistributionPort = myModel.Instances.[New](Of IfcDistributionPort)()
                        myPort.Name = "1"
                        Dim rel_port As IfcRelConnectsPortToElement = myModel.Instances.[New](Of IfcRelConnectsPortToElement)()
                        rel_port.RelatedElement = CType(myObj, IfcDistributionElement)
                        rel_port.RelatingPort = myPort
                    Case "BEND"
                        myFittingType.PredefinedType = IfcPipeFittingTypeEnum.BEND
                End Select
                Dim rel_a As IfcRelDefinesByType = myModel.Instances.[New](Of IfcRelDefinesByType)()
                rel_a.RelatedObjects.Add(myObj)
                rel_a.RelatingType = myFittingType
            Case "IfcPumpType"
                Dim myPumpType As IfcPumpType = myModel.Instances.[New](Of IfcPumpType)()
                myPumpType.Name = myIfcObjType
                myPumpType.PredefinedType = IfcPumpTypeEnum.NOTDEFINED
                Dim rel_a As IfcRelDefinesByType = myModel.Instances.[New](Of IfcRelDefinesByType)()
                rel_a.RelatedObjects.Add(myObj)
                rel_a.RelatingType = myPumpType
            Case "IfcPipeSegmentType"
                Dim mySegmType As IfcPipeSegmentType = myModel.Instances.[New](Of IfcPipeSegmentType)()
                mySegmType.Name = myIfcObjType
                mySegmType.PredefinedType = IfcPipeSegmentTypeEnum.NOTDEFINED
                Select Case myIfcObjTypeEnum
                    Case "FLEXIBLESEGMENT"
                        mySegmType.PredefinedType = IfcPipeSegmentTypeEnum.FLEXIBLESEGMENT
                    Case "RIGIDSEGMENT"
                        mySegmType.PredefinedType = IfcPipeSegmentTypeEnum.RIGIDSEGMENT
                    Case "SPOOL"
                        mySegmType.PredefinedType = IfcPipeSegmentTypeEnum.SPOOL
                End Select
                Dim rel_a As IfcRelDefinesByType = myModel.Instances.[New](Of IfcRelDefinesByType)()
                rel_a.RelatedObjects.Add(myObj)
                rel_a.RelatingType = mySegmType
            Case "IfcTankType"
                Dim myTankType As IfcTankType = myModel.Instances.[New](Of IfcTankType)()
                myTankType.Name = myIfcObjType
                myTankType.PredefinedType = IfcTankTypeEnum.NOTDEFINED
                Dim rel_a As IfcRelDefinesByType = myModel.Instances.[New](Of IfcRelDefinesByType)()
                rel_a.RelatedObjects.Add(myObj)
                rel_a.RelatingType = myTankType
        End Select
    End Sub

    Public Function CreateIfcStorey(myModel As IfcStore, myIfcObjType As String, myIfcObjName As String) As IfcObject
        'Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add IfcWall")
        '
        Dim myObj As IfcObject = myModel.Instances.[New](Of IfcBuildingStorey)()
        myObj.Name = myIfcObjName
        myObj.ObjectType = myIfcObjType
        Dim myBuilding As IfcBuilding = myModel.Instances.OfType(Of IfcBuilding).FirstOrDefault()
        Dim myStorey As IfcBuildingStorey = CType(myObj, IfcBuildingStorey)
        myStorey.CompositionType = IfcElementCompositionEnum.ELEMENT
        Dim rel0 As IfcRelAggregates = myModel.Instances.[New](Of IfcRelAggregates)()
        rel0.RelatingObject = myBuilding
        rel0.RelatedObjects.Add(myStorey)
        '
        'Dim rel As IfcRelDefinesByProperties = myModel.Instances.[New](Of IfcRelDefinesByProperties)()
        'rel.RelatedObjects.Add(myStorey)
        '
        'myTransaction.Commit()
        Return myObj
    End Function
    Public Function CreateIfcWall(myModel As IfcStore, myIfcObjType As String, myIfcObjName As String) As IfcObject
        'Dim myTransaction As Xbim.Common.ITransaction = myModel.BeginTransaction("Add IfcWall")
        '
        Dim myObj As IfcObject = myModel.Instances.[New](Of IfcWall)()
        myObj.Name = myIfcObjName
        myObj.ObjectType = myIfcObjType
        Dim myProduct As IfcProduct = CType(myObj, IfcProduct)
        Dim myIfcStorey As IfcBuildingStorey = myModel.Instances.OfType(Of IfcBuildingStorey).FirstOrDefault()
        '
        Dim rel0 As IfcRelAggregates = myModel.Instances.[New](Of IfcRelAggregates)()
        rel0.RelatingObject = myIfcStorey
        rel0.RelatedObjects.Add(myProduct)
        '
        'Dim rel As IfcRelDefinesByProperties = myModel.Instances.[New](Of IfcRelDefinesByProperties)()
        'rel.RelatedObjects.Add(myProduct)
        '
        'myTransaction.Commit()
        Return myObj
    End Function
    Public Function CreateSite(model As IfcStore, siteName As String) As IfcSite
        Dim myTransaction As Xbim.Common.ITransaction = model.BeginTransaction("Initialize buildig")
        '
        Dim mySite As IfcSite = model.Instances.[New](Of IfcSite)()
        mySite.Name = siteName
        mySite.CompositionType = IfcElementCompositionEnum.ELEMENT
        Dim localPlacement As IfcLocalPlacement = model.Instances.[New](Of IfcLocalPlacement)()
        mySite.ObjectPlacement = localPlacement
        Dim placement As IfcPlacement = model.Instances.[New](Of IfcAxis2Placement3D)()
        localPlacement.RelativePlacement = CType(placement, IfcAxis2Placement)
        placement.Location = model.Instances.[New](Of IfcCartesianPoint)()
        placement.Location.SetXYZ(0, 0, 0)
        Dim myPrj As IfcProject
        myPrj = model.Instances.OfType(Of IfcProject).FirstOrDefault()
        Dim rel0 As IfcRelAggregates = model.Instances.[New](Of IfcRelAggregates)()
        rel0.RelatingObject = myPrj
        rel0.RelatedObjects.Add(mySite)

        'myPrj.AddBuilding(myBuilding)
        '
        myTransaction.Commit()
        Return mySite
    End Function
    Public Function CreateBuilding(model As IfcStore, buildingName As String) As IfcBuilding
        Dim myTransaction As Xbim.Common.ITransaction = model.BeginTransaction("Initialize buildig")
        '
        Dim myBuilding As IfcBuilding = model.Instances.[New](Of IfcBuilding)()
        myBuilding.Name = buildingName
        myBuilding.CompositionType = IfcElementCompositionEnum.ELEMENT
        Dim localPlacement As IfcLocalPlacement = model.Instances.[New](Of IfcLocalPlacement)()
        myBuilding.ObjectPlacement = localPlacement
        Dim placement As IfcPlacement = model.Instances.[New](Of IfcAxis2Placement3D)()
        localPlacement.RelativePlacement = CType(placement, IfcAxis2Placement)
        placement.Location = model.Instances.[New](Of IfcCartesianPoint)()
        placement.Location.SetXYZ(0, 0, 0)
        Dim mySite As IfcSite
        mySite = model.Instances.OfType(Of IfcSite).FirstOrDefault()
        mySite.AddBuilding(myBuilding)
        '
        myTransaction.Commit()
        Return myBuilding
    End Function

    'Public Sub writeIfc(myModel As IfcStore)
    '    Dim DataModelData As Dictionary(Of String, List(Of String))
    '    DataModelData = ReadDataModel()

    '    With myModel
    '        Const V As String = "myWall"
    '        Dim wall As IfcWall = .Instances.[New](Of IfcWall)()
    '        wall.Name = V
    '        Dim rel As IIfcRelDefinesByProperties = .Instances.[New](Of IfcRelDefinesByProperties)()
    '        rel.RelatedObjects.Add(wall)
    '        Dim myPset As IfcPropertySet = .Instances.[New](Of IfcPropertySet)()
    '        myPset.Name = "PSet_primo"


    '        Dim myPropColl As List(Of IfcPropertySingleValue) = New List(Of IfcPropertySingleValue)


    '        Dim myProp As IfcPropertySingleValue
    '        myProp = .Instances.[New](Of IfcPropertySingleValue)()
    '        myProp.Name = "prop_001"
    '        myProp.NominalValue = New IfcText("N/A")
    '        myPropColl.Add(myProp)

    '        'myPset.HasProperties.Add(myProp)
    '        myProp = .Instances.[New](Of IfcPropertySingleValue)()
    '        myProp.Name = "prop_002"
    '        myProp.NominalValue = New IfcText("N/A")
    '        myPropColl.Add(myProp)
    '        myPset.HasProperties.AddRange(myPropColl)
    '        'myPset.HasProperties.Add(myProp)
    '        rel.RelatingPropertyDefinition = myPset
    '        wall.AddPropertySet(myPset)
    '    End With

    '    'myTransaction.Commit()
    '    myModel.SaveAs("C:\Temp\SNM_Rete\IfcMapping\DummyIfcForDataModelExtension.ifc")

    'End Sub
End Class


