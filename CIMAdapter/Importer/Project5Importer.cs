using System;
using System.Collections.Generic;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
    public class Projekat5Importer
    {
        private static Projekat5Importer instance = null;
        private static readonly object singletonLock = new object();

        private ConcreteModel concreteModel;
        private Delta delta;
        private ImportHelper importHelper;
        private TransformAndLoadReport report;

        public static Projekat5Importer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (singletonLock)
                    {
                        if (instance == null)
                        {
                            instance = new Projekat5Importer();
                            instance.Reset();
                        }
                    }
                }
                return instance;
            }
        }

        public Delta NMSDelta
        {
            get { return delta; }
        }

        public void Reset()
        {
            concreteModel = null;
            delta = new Delta();
            importHelper = new ImportHelper();
            report = null;
        }

        public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
        {
            LogManager.Log("Importing Projekat5 elements...", LogLevel.Info);
            report = new TransformAndLoadReport();
            concreteModel = cimConcreteModel;
            delta.ClearDeltaOperations();

            if ((concreteModel != null) && (concreteModel.ModelMap != null))
            {
                try
                {
                    ConvertModelAndPopulateDelta();
                }
                catch (Exception ex)
                {
                    string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
                    LogManager.Log(message);
                    report.Report.AppendLine(ex.Message);
                    report.Success = false;
                }
            }
            LogManager.Log("Importing Projekat5 elements - END.", LogLevel.Info);
            return report;
        }

        private void ConvertModelAndPopulateDelta()
        {
            LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

            // FAZA 1: Nezavisni entiteti
            ImportCurves();

            // FAZA 2: Entiteti koji zavise od Curve
            ImportCurveDatas();

            // FAZA 3: Schedule entiteti
            ImportBasicIntervalSchedules();
            ImportRegularIntervalSchedules();
            ImportIrregularIntervalSchedules();
            ImportOutageSchedules();

            // FAZA 4: TimePoint entiteti
            ImportRegularTimePoints();
            ImportIrregularTimePoints();

            // FAZA 5: Switch
            ImportSwitches();

            // FAZA 6: SwitchingOperation
            ImportSwitchingOperations();

            LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
        }

        #region Import Methods

        private void ImportCurves()
        {
            SortedDictionary<string, object> cimObjects = concreteModel.GetAllObjectsOfType("FTN.Curve");
            if (cimObjects != null)
            {
                foreach (KeyValuePair<string, object> pair in cimObjects)
                {
                    FTN.Curve cimObj = pair.Value as FTN.Curve;
                    ResourceDescription rd = CreateCurveResourceDescription(cimObj);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Curve ID = ").Append(cimObj.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Curve ID = ").Append(cimObj.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private void ImportCurveDatas()
        {
            SortedDictionary<string, object> cimObjects = concreteModel.GetAllObjectsOfType("FTN.CurveData");
            if (cimObjects != null)
            {
                foreach (KeyValuePair<string, object> pair in cimObjects)
                {
                    FTN.CurveData cimObj = pair.Value as FTN.CurveData;
                    ResourceDescription rd = CreateCurveDataResourceDescription(cimObj);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("CurveData ID = ").Append(cimObj.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("CurveData ID = ").Append(cimObj.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private void ImportBasicIntervalSchedules()
        {
            SortedDictionary<string, object> cimObjects = concreteModel.GetAllObjectsOfType("FTN.BasicIntervalSchedule");
            if (cimObjects != null)
            {
                foreach (KeyValuePair<string, object> pair in cimObjects)
                {
                    FTN.BasicIntervalSchedule cimObj = pair.Value as FTN.BasicIntervalSchedule;
                    ResourceDescription rd = CreateBasicIntervalScheduleResourceDescription(cimObj);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("BasicIntervalSchedule ID = ").Append(cimObj.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("BasicIntervalSchedule ID = ").Append(cimObj.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private void ImportRegularIntervalSchedules()
        {
            SortedDictionary<string, object> cimObjects = concreteModel.GetAllObjectsOfType("FTN.RegularIntervalSchedule");
            if (cimObjects != null)
            {
                foreach (KeyValuePair<string, object> pair in cimObjects)
                {
                    FTN.RegularIntervalSchedule cimObj = pair.Value as FTN.RegularIntervalSchedule;
                    ResourceDescription rd = CreateRegularIntervalScheduleResourceDescription(cimObj);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("RegularIntervalSchedule ID = ").Append(cimObj.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("RegularIntervalSchedule ID = ").Append(cimObj.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private void ImportIrregularIntervalSchedules()
        {
            SortedDictionary<string, object> cimObjects = concreteModel.GetAllObjectsOfType("FTN.IrregularIntervalSchedule");
            if (cimObjects != null)
            {
                foreach (KeyValuePair<string, object> pair in cimObjects)
                {
                    FTN.IrregularIntervalSchedule cimObj = pair.Value as FTN.IrregularIntervalSchedule;
                    ResourceDescription rd = CreateIrregularIntervalScheduleResourceDescription(cimObj);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("IrregularIntervalSchedule ID = ").Append(cimObj.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("IrregularIntervalSchedule ID = ").Append(cimObj.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private void ImportOutageSchedules()
        {
            SortedDictionary<string, object> cimObjects = concreteModel.GetAllObjectsOfType("FTN.OutageSchedule");
            if (cimObjects != null)
            {
                foreach (KeyValuePair<string, object> pair in cimObjects)
                {
                    FTN.OutageSchedule cimObj = pair.Value as FTN.OutageSchedule;
                    ResourceDescription rd = CreateOutageScheduleResourceDescription(cimObj);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("OutageSchedule ID = ").Append(cimObj.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("OutageSchedule ID = ").Append(cimObj.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private void ImportRegularTimePoints()
        {
            SortedDictionary<string, object> cimObjects = concreteModel.GetAllObjectsOfType("FTN.RegularTimePoint");
            if (cimObjects != null)
            {
                foreach (KeyValuePair<string, object> pair in cimObjects)
                {
                    FTN.RegularTimePoint cimObj = pair.Value as FTN.RegularTimePoint;
                    ResourceDescription rd = CreateRegularTimePointResourceDescription(cimObj);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("RegularTimePoint ID = ").Append(cimObj.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("RegularTimePoint ID = ").Append(cimObj.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private void ImportIrregularTimePoints()
        {
            SortedDictionary<string, object> cimObjects = concreteModel.GetAllObjectsOfType("FTN.IrregularTimePoint");
            if (cimObjects != null)
            {
                foreach (KeyValuePair<string, object> pair in cimObjects)
                {
                    FTN.IrregularTimePoint cimObj = pair.Value as FTN.IrregularTimePoint;
                    ResourceDescription rd = CreateIrregularTimePointResourceDescription(cimObj);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("IrregularTimePoint ID = ").Append(cimObj.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("IrregularTimePoint ID = ").Append(cimObj.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private void ImportSwitches()
        {
            SortedDictionary<string, object> cimObjects = concreteModel.GetAllObjectsOfType("FTN.Switch");
            if (cimObjects != null)
            {
                foreach (KeyValuePair<string, object> pair in cimObjects)
                {
                    FTN.Switch cimObj = pair.Value as FTN.Switch;
                    ResourceDescription rd = CreateSwitchResourceDescription(cimObj);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Switch ID = ").Append(cimObj.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Switch ID = ").Append(cimObj.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        private void ImportSwitchingOperations()
        {
            SortedDictionary<string, object> cimObjects = concreteModel.GetAllObjectsOfType("FTN.SwitchingOperation");
            if (cimObjects != null)
            {
                foreach (KeyValuePair<string, object> pair in cimObjects)
                {
                    FTN.SwitchingOperation cimObj = pair.Value as FTN.SwitchingOperation;
                    ResourceDescription rd = CreateSwitchingOperationResourceDescription(cimObj);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("SwitchingOperation ID = ").Append(cimObj.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("SwitchingOperation ID = ").Append(cimObj.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        #endregion Import Methods

        #region Create ResourceDescription Methods

        private ResourceDescription CreateCurveResourceDescription(FTN.Curve cimCurve)
        {
            ResourceDescription rd = null;
            if (cimCurve != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.CURVE, importHelper.CheckOutIndexForDMSType(DMSType.CURVE));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimCurve.ID, gid);
                Project5Converter.PopulateCurveProperties(cimCurve, rd);
            }
            return rd;
        }

        private ResourceDescription CreateCurveDataResourceDescription(FTN.CurveData cimCurveData)
        {
            ResourceDescription rd = null;
            if (cimCurveData != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.CURVEDATA, importHelper.CheckOutIndexForDMSType(DMSType.CURVEDATA));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimCurveData.ID, gid);
                Project5Converter.PopulateCurveDataProperties(cimCurveData, rd);
            }
            return rd;
        }

        private ResourceDescription CreateBasicIntervalScheduleResourceDescription(FTN.BasicIntervalSchedule cimSchedule)
        {
            ResourceDescription rd = null;
            if (cimSchedule != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.BASICINTERVALSCHEDULE, importHelper.CheckOutIndexForDMSType(DMSType.BASICINTERVALSCHEDULE));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimSchedule.ID, gid);
                Project5Converter.PopulateBasicIntervalScheduleProperties(cimSchedule, rd);
            }
            return rd;
        }

        private ResourceDescription CreateRegularIntervalScheduleResourceDescription(FTN.RegularIntervalSchedule cimSchedule)
        {
            ResourceDescription rd = null;
            if (cimSchedule != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.REGULARINTERVALSCHEDULE, importHelper.CheckOutIndexForDMSType(DMSType.REGULARINTERVALSCHEDULE));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimSchedule.ID, gid);
                Project5Converter.PopulateRegularIntervalScheduleProperties(cimSchedule, rd);
            }
            return rd;
        }

        private ResourceDescription CreateIrregularIntervalScheduleResourceDescription(FTN.IrregularIntervalSchedule cimSchedule)
        {
            ResourceDescription rd = null;
            if (cimSchedule != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.IRREGULARINTERVALSCHEDULE, importHelper.CheckOutIndexForDMSType(DMSType.IRREGULARINTERVALSCHEDULE));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimSchedule.ID, gid);
                Project5Converter.PopulateIrregularIntervalScheduleProperties(cimSchedule, rd);
            }
            return rd;
        }

        private ResourceDescription CreateOutageScheduleResourceDescription(FTN.OutageSchedule cimSchedule)
        {
            ResourceDescription rd = null;
            if (cimSchedule != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.OUTAGESCHEDULE, importHelper.CheckOutIndexForDMSType(DMSType.OUTAGESCHEDULE));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimSchedule.ID, gid);
                Project5Converter.PopulateOutageScheduleProperties(cimSchedule, rd);
            }
            return rd;
        }

        private ResourceDescription CreateRegularTimePointResourceDescription(FTN.RegularTimePoint cimTimePoint)
        {
            ResourceDescription rd = null;
            if (cimTimePoint != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.REGULARTIMEPOINT, importHelper.CheckOutIndexForDMSType(DMSType.REGULARTIMEPOINT));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimTimePoint.ID, gid);
                Project5Converter.PopulateRegularTimePointProperties(cimTimePoint, rd);
            }
            return rd;
        }

        private ResourceDescription CreateIrregularTimePointResourceDescription(FTN.IrregularTimePoint cimTimePoint)
        {
            ResourceDescription rd = null;
            if (cimTimePoint != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.IRREGULARTIMEPOINT, importHelper.CheckOutIndexForDMSType(DMSType.IRREGULARTIMEPOINT));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimTimePoint.ID, gid);
                Project5Converter.PopulateIrregularTimePointProperties(cimTimePoint, rd);
            }
            return rd;
        }

        private ResourceDescription CreateSwitchResourceDescription(FTN.Switch cimSwitch)
        {
            ResourceDescription rd = null;
            if (cimSwitch != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.SWITCH, importHelper.CheckOutIndexForDMSType(DMSType.SWITCH));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimSwitch.ID, gid);
                Project5Converter.PopulateSwitchProperties(cimSwitch, rd);
            }
            return rd;
        }

        private ResourceDescription CreateSwitchingOperationResourceDescription(FTN.SwitchingOperation cimOperation)
        {
            ResourceDescription rd = null;
            if (cimOperation != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.SWITCHINGOPERATION, importHelper.CheckOutIndexForDMSType(DMSType.SWITCHINGOPERATION));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimOperation.ID, gid);
                Project5Converter.PopulateSwitchingOperationProperties(cimOperation, rd);
            }
            return rd;
        }

        #endregion Create ResourceDescription Methods
    }
}
