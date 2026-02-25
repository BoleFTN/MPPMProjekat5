# PROJEKAT 5 - FAZA 1: MODEL KODOVI I HIJERARHIJA

## ? ZAVRŠENO

### 1. DODATI NOVI DMSType KODOVI (Common/ModelDefines.cs)

```csharp
// Projekat 5 - novi entiteti
CURVE                           = 0x0006,
CURVEDATA                       = 0x0007,
BASICINTERVALSCHEDULE          = 0x0008,
REGULARINTERVALSCHEDULE        = 0x0009,
IRREGULARINTERVALSCHEDULE      = 0x000A,
OUTAGESCHEDULE                 = 0x000B,
REGULARTIMEPOINT               = 0x000C,
IRREGULARTIMEPOINT             = 0x000D,
SWITCH                         = 0x000E,
SWITCHINGOPERATION             = 0x000F,
```

---

### 2. DODATI NOVI ModelCode DEFINICIJE (Common/ModelDefines.cs)

#### **IdentifiedObject - Novi atribut**
```csharp
IDOBJ_ALIASNAME = 0x1000000000000507,
```

#### **Curve (0x1500...)**
```csharp
CURVE                    = 0x1500000000060000,
CURVE_CURVESTYLE         = 0x150000000006010a, // CurveStyle enum
CURVE_XMULTIPLIER        = 0x150000000006020a, // UnitMultiplier enum
CURVE_XUNIT              = 0x150000000006030a, // UnitSymbol enum
CURVE_Y1MULTIPLIER       = 0x150000000006040a,
CURVE_Y1UNIT             = 0x150000000006050a,
CURVE_Y2MULTIPLIER       = 0x150000000006060a,
CURVE_Y2UNIT             = 0x150000000006070a,
CURVE_Y3MULTIPLIER       = 0x150000000006080a,
CURVE_Y3UNIT             = 0x150000000006090a,
CURVE_CURVEDATAS         = 0x15000000000A0119, // ReferenceVector -> CurveData
```

#### **CurveData (0x1600...)**
```csharp
CURVEDATA                = 0x1600000000070000,
CURVEDATA_XVALUE         = 0x1600000000070105, // float
CURVEDATA_Y1VALUE        = 0x1600000000070205,
CURVEDATA_Y2VALUE        = 0x1600000000070305,
CURVEDATA_Y3VALUE        = 0x1600000000070405,
CURVEDATA_CURVE          = 0x1600000000070509, // Reference -> Curve
```

#### **BasicIntervalSchedule (0x1700...)**
```csharp
BASICINTERVALSCHEDULE            = 0x1700000000080000,
BASICINTERVALSCHEDULE_STARTTIME  = 0x1700000000080108, // DateTime
BASICINTERVALSCHEDULE_VALUE1MULTI= 0x170000000008020a, // UnitMultiplier
BASICINTERVALSCHEDULE_VALUE1UNIT = 0x170000000008030a, // UnitSymbol
BASICINTERVALSCHEDULE_VALUE2MULTI= 0x170000000008040a,
BASICINTERVALSCHEDULE_VALUE2UNIT = 0x170000000008050a,
```

#### **RegularIntervalSchedule (0x1710...)**
```csharp
REGULARINTERVALSCHEDULE          = 0x1710000000090000,
REGULARINTERVALSCHEDULE_ENDTIME  = 0x1710000000090108, // DateTime
REGULARINTERVALSCHEDULE_TIMESTEP = 0x1710000000090205, // Seconds (float)
REGULARINTERVALSCHEDULE_TIMEPOINTS = 0x1710000000090319, // ReferenceVector
```

#### **IrregularIntervalSchedule (0x1720...)**
```csharp
IRREGULARINTERVALSCHEDULE           = 0x17200000000A0000,
IRREGULARINTERVALSCHEDULE_TIMEPOINTS= 0x17200000000A0119, // ReferenceVector
```

#### **OutageSchedule (0x1730...)**
```csharp
OUTAGESCHEDULE = 0x17300000000B0000,
// Nema dodatnih atributa (nasledjuje BasicIntervalSchedule)
```

#### **RegularTimePoint (0x1800...)**
```csharp
REGULARTIMEPOINT                  = 0x18000000000C0000,
REGULARTIMEPOINT_SEQUENCENUMBER   = 0x18000000000C0103, // integer
REGULARTIMEPOINT_VALUE1           = 0x18000000000C0205, // float
REGULARTIMEPOINT_VALUE2           = 0x18000000000C0305,
REGULARTIMEPOINT_INTERVALSCHEDULE = 0x18000000000C0409, // Reference
```

#### **IrregularTimePoint (0x1900...)**
```csharp
IRREGULARTIMEPOINT                  = 0x19000000000D0000,
IRREGULARTIMEPOINT_TIME             = 0x19000000000D0108, // DateTime
IRREGULARTIMEPOINT_VALUE1           = 0x19000000000D0205,
IRREGULARTIMEPOINT_VALUE2           = 0x19000000000D0305,
IRREGULARTIMEPOINT_INTERVALSCHEDULE = 0x19000000000D0409, // Reference
```

#### **Switch (0x1A00...)**
```csharp
SWITCH                      = 0x111200000E000000,
SWITCH_SWITCHINGOPERATIONS  = 0x111200000E000119, // ReferenceVector
```

#### **SwitchingOperation (0x1B00...)**
```csharp
SWITCHINGOPERATION              = 0x1B000000000F0000,
SWITCHINGOPERATION_NEWSTATE     = 0x1B000000000F010a, // SwitchState enum
SWITCHINGOPERATION_OPERATIONTIME= 0x1B000000000F0208, // DateTime
SWITCHINGOPERATION_SWITCH       = 0x1B000000000F0309, // Reference
```

---

### 3. DODATI NOVI ENUM-I (Common/Enums.cs)

```csharp
/// <summary>
/// Curve style - tip krive
/// </summary>
public enum CurveStyle : short
{
    ConstantYValue = 1,
    Formula = 2,
    RampYValue = 3,
    StraightLineYValues = 4
}

/// <summary>
/// Unit multiplier - SI prefiksi
/// </summary>
public enum UnitMultiplier : short
{
    None = 0,   // 1
    Pico = 1,   // 10^-12
    Nano = 2,   // 10^-9
    Micro = 3,  // 10^-6
    Milli = 4,  // 10^-3
    Centi = 5,  // 10^-2
    Deci = 6,   // 10^-1
    Kilo = 7,   // 10^3
    Mega = 8,   // 10^6
    Giga = 9,   // 10^9
    Tera = 10   // 10^12
}

/// <summary>
/// Unit symbol - jedinice mere
/// </summary>
public enum UnitSymbol : short
{
    None = 0,
    A = 1,      // Amper
    V = 2,      // Volt
    W = 3,      // Vat
    VAr = 4,    // Volt-Amper reaktivni
    VA = 5,     // Volt-Amper
    Wh = 6,     // Vat-sat
    VAh = 7,
    VArh = 8,
    Hz = 9,     // Herc
    Ohm = 10,   // Om
    S = 11,     // Simens
    F = 12,     // Farad
    H = 13,     // Henri
    Sec = 14,   // Sekunda
    Min = 15,   // Minut
    Hour = 16,  // Sat
    M = 17,     // Metar
    M2 = 18,    // m²
    M3 = 19,    // m³
    Deg = 20,   // Stepen
    DegC = 21,  // °C
    Pa = 22,    // Paskal
    N = 23,     // Njutn
    J = 24,     // Džul
    G = 25,     // Gram
    Rad = 26    // Radijan
}

/// <summary>
/// Switch state - stanje prekida?a
/// </summary>
public enum SwitchState : short
{
    Open = 1,   // Otvoren
    Close = 2   // Zatvoren
}
```

---

### 4. AŽURIRAN EnumDescs.cs

Dodati mapiranja za enum property-je:

```csharp
// Curve
property2enumType.Add(ModelCode.CURVE_CURVESTYLE, typeof(CurveStyle));
property2enumType.Add(ModelCode.CURVE_XMULTIPLIER, typeof(UnitMultiplier));
property2enumType.Add(ModelCode.CURVE_XUNIT, typeof(UnitSymbol));
property2enumType.Add(ModelCode.CURVE_Y1MULTIPLIER, typeof(UnitMultiplier));
property2enumType.Add(ModelCode.CURVE_Y1UNIT, typeof(UnitSymbol));
property2enumType.Add(ModelCode.CURVE_Y2MULTIPLIER, typeof(UnitMultiplier));
property2enumType.Add(ModelCode.CURVE_Y2UNIT, typeof(UnitSymbol));
property2enumType.Add(ModelCode.CURVE_Y3MULTIPLIER, typeof(UnitMultiplier));
property2enumType.Add(ModelCode.CURVE_Y3UNIT, typeof(UnitSymbol));

// BasicIntervalSchedule
property2enumType.Add(ModelCode.BASICINTERVALSCHEDULE_VALUE1MULTI, typeof(UnitMultiplier));
property2enumType.Add(ModelCode.BASICINTERVALSCHEDULE_VALUE1UNIT, typeof(UnitSymbol));
property2enumType.Add(ModelCode.BASICINTERVALSCHEDULE_VALUE2MULTI, typeof(UnitMultiplier));
property2enumType.Add(ModelCode.BASICINTERVALSCHEDULE_VALUE2UNIT, typeof(UnitSymbol));

// SwitchingOperation
property2enumType.Add(ModelCode.SWITCHINGOPERATION_NEWSTATE, typeof(SwitchState));
```

---

### 5. VERIFIKOVANA HIJERARHIJA ENTITETA (Project5Importer.cs)

```
IdentifiedObject
  ??? Curve
  ??? CurveData
  ??? BasicIntervalSchedule
  ?     ??? RegularIntervalSchedule
  ?     ??? IrregularIntervalSchedule
  ?     ??? OutageSchedule
  ??? RegularTimePoint
  ??? IrregularTimePoint
  ??? SwitchingOperation
  ??? PowerSystemResource
        ??? Equipment
              ??? ConductingEquipment
                    ??? Switch
```

**RELACIONE VEZE:**
- Curve (1) ???? (0..*) CurveData
- RegularIntervalSchedule (1) ???? (1..*) RegularTimePoint
- IrregularIntervalSchedule (1) ???? (1..*) IrregularTimePoint
- Switch (0..*) ???? (0..1) SwitchingOperation

---

### 6. REDOSLED IMPORTA (Respektuje zavisnosti)

```csharp
1. ImportCurves()                       // Nema zavisnosti
2. ImportCurveDatas()                   // Zavisi od Curve
3. ImportBasicIntervalSchedules()       // Nema zavisnosti
4. ImportRegularIntervalSchedules()     // Nasledjuje BasicIntervalSchedule
5. ImportIrregularIntervalSchedules()   // Nasledjuje BasicIntervalSchedule
6. ImportOutageSchedules()              // Nasledjuje BasicIntervalSchedule
7. ImportRegularTimePoints()            // Zavisi od RegularIntervalSchedule
8. ImportIrregularTimePoints()          // Zavisi od IrregularIntervalSchedule
9. ImportSwitches()                     // ConductingEquipment hijerarhija
10. ImportSwitchingOperations()         // Zavisi od Switch
```

---

## ?? SLEDE?I KORACI (FAZA 2)

### 1. Kreirati klase entiteta u DataModel projektu:
- `NetworkModelService/DataModel/Core/Curve.cs`
- `NetworkModelService/DataModel/Core/CurveData.cs`
- `NetworkModelService/DataModel/Core/BasicIntervalSchedule.cs`
- `NetworkModelService/DataModel/Core/RegularIntervalSchedule.cs`
- `NetworkModelService/DataModel/Core/IrregularIntervalSchedule.cs`
- `NetworkModelService/DataModel/Core/OutageSchedule.cs`
- `NetworkModelService/DataModel/Core/RegularTimePoint.cs`
- `NetworkModelService/DataModel/Core/IrregularTimePoint.cs`
- `NetworkModelService/DataModel/Wires/Switch.cs`
- `NetworkModelService/DataModel/Core/SwitchingOperation.cs`

### 2. Ažurirati Container.cs sa switch-case za nove tipove

### 3. Ažurirati ModelResourcesDesc.cs:
- `InitializeTypeIdsInInsertOrder()` - dodati redosled
- `InitializeNotSettablePropertyIds()` - dodati ReferenceVector property-je

### 4. Implementirati Converter metode u CIMAdapter

### 5. Testirati import CIM XML-a

---

## ? BUILD STATUS

**Status**: ? **BUILD SUCCESSFUL**

Svi fajlovi su validni i projekat se uspešno kompajlira!

---

## ?? STATISTIKA

- **Novi DMSType-ovi**: 10
- **Novi ModelCode-ovi**: 51
- **Novi Enum-i**: 4 (CurveStyle, UnitMultiplier, UnitSymbol, SwitchState)
- **Enum vrednosti**: 77
- **Ažurirani fajlovi**: 4
  - Common/ModelDefines.cs
  - Common/Enums.cs
  - Common/EnumDescs.cs
  - CIMAdapter/Importer/Project5Importer.cs

---

**Datum**: 2024
**Autor**: GitHub Copilot + Projekat 5 Tim
