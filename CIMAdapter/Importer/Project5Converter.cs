using FTN.Common;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
    public static class Project5Converter
    {
        #region Populate ResourceDescription

        public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimObj, ResourceDescription rd)
        {
            if ((cimObj != null) && (rd != null))
            {
                if (cimObj.MRIDHasValue)
                    rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimObj.MRID));
                if (cimObj.NameHasValue)
                    rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimObj.Name));
                if (cimObj.AliasNameHasValue)
                    rd.AddProperty(new Property(ModelCode.IDOBJ_ALIASNAME, cimObj.AliasName));
            }
        }

        public static void PopulateCurveProperties(FTN.Curve cimCurve, ResourceDescription rd)
        {
            if ((cimCurve != null) && (rd != null))
            {
                PopulateIdentifiedObjectProperties(cimCurve, rd);
                if (cimCurve.CurveStyleHasValue)
                    rd.AddProperty(new Property(ModelCode.CURVE_CURVESTYLE, (short)GetDMSCurveStyle(cimCurve.CurveStyle)));
                if (cimCurve.XMultiplierHasValue)
                    rd.AddProperty(new Property(ModelCode.CURVE_XMULTIPLIER, (short)GetDMSUnitMultiplier(cimCurve.XMultiplier)));
                if (cimCurve.XUnitHasValue)
                    rd.AddProperty(new Property(ModelCode.CURVE_XUNIT, (short)GetDMSUnitSymbol(cimCurve.XUnit)));
                if (cimCurve.Y1MultiplierHasValue)
                    rd.AddProperty(new Property(ModelCode.CURVE_Y1MULTIPLIER, (short)GetDMSUnitMultiplier(cimCurve.Y1Multiplier)));
                if (cimCurve.Y1UnitHasValue)
                    rd.AddProperty(new Property(ModelCode.CURVE_Y1UNIT, (short)GetDMSUnitSymbol(cimCurve.Y1Unit)));
                if (cimCurve.Y2MultiplierHasValue)
                    rd.AddProperty(new Property(ModelCode.CURVE_Y2MULTIPLIER, (short)GetDMSUnitMultiplier(cimCurve.Y2Multiplier)));
                if (cimCurve.Y2UnitHasValue)
                    rd.AddProperty(new Property(ModelCode.CURVE_Y2UNIT, (short)GetDMSUnitSymbol(cimCurve.Y2Unit)));
                if (cimCurve.Y3MultiplierHasValue)
                    rd.AddProperty(new Property(ModelCode.CURVE_Y3MULTIPLIER, (short)GetDMSUnitMultiplier(cimCurve.Y3Multiplier)));
                if (cimCurve.Y3UnitHasValue)
                    rd.AddProperty(new Property(ModelCode.CURVE_Y3UNIT, (short)GetDMSUnitSymbol(cimCurve.Y3Unit)));
            }
        }

        public static void PopulateCurveDataProperties(FTN.CurveData cimCurveData, ResourceDescription rd)
        {
            if ((cimCurveData != null) && (rd != null))
            {
                PopulateIdentifiedObjectProperties(cimCurveData, rd);
                if (cimCurveData.XvalueHasValue)
                    rd.AddProperty(new Property(ModelCode.CURVEDATA_XVALUE, cimCurveData.Xvalue));
                if (cimCurveData.Y1valueHasValue)
                    rd.AddProperty(new Property(ModelCode.CURVEDATA_Y1VALUE, cimCurveData.Y1value));
                if (cimCurveData.Y2valueHasValue)
                    rd.AddProperty(new Property(ModelCode.CURVEDATA_Y2VALUE, cimCurveData.Y2value));
                if (cimCurveData.Y3valueHasValue)
                    rd.AddProperty(new Property(ModelCode.CURVEDATA_Y3VALUE, cimCurveData.Y3value));
            }
        }

        public static void PopulateBasicIntervalScheduleProperties(FTN.BasicIntervalSchedule cimSchedule, ResourceDescription rd)
        {
            if ((cimSchedule != null) && (rd != null))
            {
                PopulateIdentifiedObjectProperties(cimSchedule, rd);
                if (cimSchedule.StartTimeHasValue)
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_STARTTIME, cimSchedule.StartTime));
                if (cimSchedule.Value1MultiplierHasValue)
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VALUE1MULTI, (short)GetDMSUnitMultiplier(cimSchedule.Value1Multiplier)));
                if (cimSchedule.Value1UnitHasValue)
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VALUE1UNIT, (short)GetDMSUnitSymbol(cimSchedule.Value1Unit)));
                if (cimSchedule.Value2MultiplierHasValue)
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VALUE2MULTI, (short)GetDMSUnitMultiplier(cimSchedule.Value2Multiplier)));
                if (cimSchedule.Value2UnitHasValue)
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VALUE2UNIT, (short)GetDMSUnitSymbol(cimSchedule.Value2Unit)));
            }
        }

        public static void PopulateRegularIntervalScheduleProperties(FTN.RegularIntervalSchedule cimSchedule, ResourceDescription rd)
        {
            if ((cimSchedule != null) && (rd != null))
            {
                PopulateBasicIntervalScheduleProperties(cimSchedule, rd);
                if (cimSchedule.EndTimeHasValue)
                    rd.AddProperty(new Property(ModelCode.REGULARINTERVALSCHEDULE_ENDTIME, cimSchedule.EndTime));
                if (cimSchedule.TimeStepHasValue)
                    rd.AddProperty(new Property(ModelCode.REGULARINTERVALSCHEDULE_TIMESTEP, cimSchedule.TimeStep));
            }
        }

        public static void PopulateIrregularIntervalScheduleProperties(FTN.IrregularIntervalSchedule cimSchedule, ResourceDescription rd)
        {
            if ((cimSchedule != null) && (rd != null))
                PopulateBasicIntervalScheduleProperties(cimSchedule, rd);
        }

        public static void PopulateOutageScheduleProperties(FTN.OutageSchedule cimSchedule, ResourceDescription rd)
        {
            if ((cimSchedule != null) && (rd != null))
                PopulateIrregularIntervalScheduleProperties(cimSchedule, rd);
        }

        public static void PopulateRegularTimePointProperties(FTN.RegularTimePoint cimTimePoint, ResourceDescription rd)
        {
            if ((cimTimePoint != null) && (rd != null))
            {
                PopulateIdentifiedObjectProperties(cimTimePoint, rd);
                if (cimTimePoint.SequenceNumberHasValue)
                    rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_SEQUENCENUMBER, cimTimePoint.SequenceNumber));
                if (cimTimePoint.Value1HasValue)
                    rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_VALUE1, cimTimePoint.Value1));
                if (cimTimePoint.Value2HasValue)
                    rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_VALUE2, cimTimePoint.Value2));
            }
        }

        public static void PopulateIrregularTimePointProperties(FTN.IrregularTimePoint cimTimePoint, ResourceDescription rd)
        {
            if ((cimTimePoint != null) && (rd != null))
            {
                PopulateIdentifiedObjectProperties(cimTimePoint, rd);
                if (cimTimePoint.TimeHasValue)
                    rd.AddProperty(new Property(ModelCode.IRREGULARTIMEPOINT_TIME, cimTimePoint.Time));
                if (cimTimePoint.Value1HasValue)
                    rd.AddProperty(new Property(ModelCode.IRREGULARTIMEPOINT_VALUE1, cimTimePoint.Value1));
                if (cimTimePoint.Value2HasValue)
                    rd.AddProperty(new Property(ModelCode.IRREGULARTIMEPOINT_VALUE2, cimTimePoint.Value2));
            }
        }

        public static void PopulateSwitchProperties(FTN.Switch cimSwitch, ResourceDescription rd)
        {
            if ((cimSwitch != null) && (rd != null))
                PopulateIdentifiedObjectProperties(cimSwitch, rd);
        }

        public static void PopulateSwitchingOperationProperties(FTN.SwitchingOperation cimOperation, ResourceDescription rd)
        {
            if ((cimOperation != null) && (rd != null))
            {
                PopulateIdentifiedObjectProperties(cimOperation, rd);
                if (cimOperation.NewStateHasValue)
                    rd.AddProperty(new Property(ModelCode.SWITCHINGOPERATION_NEWSTATE, (short)GetDMSSwitchState(cimOperation.NewState)));
                if (cimOperation.OperationTimeHasValue)
                    rd.AddProperty(new Property(ModelCode.SWITCHINGOPERATION_OPERATIONTIME, cimOperation.OperationTime));
            }
        }

        #endregion Populate ResourceDescription

        #region Enum Conversions

        public static FTN.Common.CurveStyle GetDMSCurveStyle(FTN.CurveStyle curveStyle)
        {
            switch (curveStyle)
            {
                case FTN.CurveStyle.constantYValue: return FTN.Common.CurveStyle.ConstantYValue;
                case FTN.CurveStyle.formula: return FTN.Common.CurveStyle.Formula;
                case FTN.CurveStyle.rampYValue: return FTN.Common.CurveStyle.RampYValue;
                case FTN.CurveStyle.straightLineYValues: return FTN.Common.CurveStyle.StraightLineYValues;
                default: return FTN.Common.CurveStyle.ConstantYValue;
            }
        }

        public static FTN.Common.UnitMultiplier GetDMSUnitMultiplier(FTN.UnitMultiplier multiplier)
        {
            switch (multiplier)
            {
                case FTN.UnitMultiplier.c: return FTN.Common.UnitMultiplier.Centi;
                case FTN.UnitMultiplier.d: return FTN.Common.UnitMultiplier.Deci;
                case FTN.UnitMultiplier.G: return FTN.Common.UnitMultiplier.Giga;
                case FTN.UnitMultiplier.k: return FTN.Common.UnitMultiplier.Kilo;
                case FTN.UnitMultiplier.m: return FTN.Common.UnitMultiplier.Milli;
                case FTN.UnitMultiplier.M: return FTN.Common.UnitMultiplier.Mega;
                case FTN.UnitMultiplier.micro: return FTN.Common.UnitMultiplier.Micro;
                case FTN.UnitMultiplier.n: return FTN.Common.UnitMultiplier.Nano;
                case FTN.UnitMultiplier.p: return FTN.Common.UnitMultiplier.Pico;
                case FTN.UnitMultiplier.T: return FTN.Common.UnitMultiplier.Tera;
                default: return FTN.Common.UnitMultiplier.None;
            }
        }

        public static FTN.Common.UnitSymbol GetDMSUnitSymbol(FTN.UnitSymbol symbol)
        {
            switch (symbol)
            {
                case FTN.UnitSymbol.A: return FTN.Common.UnitSymbol.A;
                case FTN.UnitSymbol.deg: return FTN.Common.UnitSymbol.Deg;
                case FTN.UnitSymbol.degC: return FTN.Common.UnitSymbol.DegC;
                case FTN.UnitSymbol.F: return FTN.Common.UnitSymbol.F;
                case FTN.UnitSymbol.H: return FTN.Common.UnitSymbol.H;
                case FTN.UnitSymbol.Hz: return FTN.Common.UnitSymbol.Hz;
                case FTN.UnitSymbol.m: return FTN.Common.UnitSymbol.M;
                case FTN.UnitSymbol.m2: return FTN.Common.UnitSymbol.M2;
                case FTN.UnitSymbol.m3: return FTN.Common.UnitSymbol.M3;
                case FTN.UnitSymbol.min: return FTN.Common.UnitSymbol.Min;
                case FTN.UnitSymbol.N: return FTN.Common.UnitSymbol.N;
                case FTN.UnitSymbol.ohm: return FTN.Common.UnitSymbol.Ohm;
                case FTN.UnitSymbol.Pa: return FTN.Common.UnitSymbol.Pa;
                case FTN.UnitSymbol.s: return FTN.Common.UnitSymbol.Sec;
                case FTN.UnitSymbol.VA: return FTN.Common.UnitSymbol.VA;
                case FTN.UnitSymbol.VAh: return FTN.Common.UnitSymbol.VAh;
                case FTN.UnitSymbol.VAr: return FTN.Common.UnitSymbol.VAr;
                case FTN.UnitSymbol.VArh: return FTN.Common.UnitSymbol.VArh;
                case FTN.UnitSymbol.V: return FTN.Common.UnitSymbol.V;
                case FTN.UnitSymbol.W: return FTN.Common.UnitSymbol.W;
                case FTN.UnitSymbol.Wh: return FTN.Common.UnitSymbol.Wh;
                default: return FTN.Common.UnitSymbol.None;
            }
        }

        public static FTN.Common.SwitchState GetDMSSwitchState(FTN.SwitchState switchState)
        {
            switch (switchState)
            {
                case FTN.SwitchState.close: return FTN.Common.SwitchState.Close;
                case FTN.SwitchState.open: return FTN.Common.SwitchState.Open;
                default: return FTN.Common.SwitchState.Open;
            }
        }

        #endregion Enum Conversions
    }
}
