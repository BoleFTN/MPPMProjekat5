using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
    /// <summary>
    /// Projekat5Importer - import CIM entiteta za projekat 5
    /// 
    /// HIJERARHIJA ENTITETA:
    /// =====================
    /// 
    /// IdentifiedObject
    ///   ├── Curve
    ///   ├── CurveData
    ///   ├── BasicIntervalSchedule
    ///   │     ├── RegularIntervalSchedule
    ///   │     ├── IrregularIntervalSchedule
    ///   │     └── OutageSchedule
    ///   ├── RegularTimePoint
    ///   ├── IrregularTimePoint
    ///   ├── SwitchingOperation
    ///   └── PowerSystemResource
    ///         └── Equipment
    ///               └── ConductingEquipment
    ///                     └── Switch
    /// 
    /// RELACIONE VEZE:
    /// ===============
    /// Curve (1) ←──── (0..*) CurveData
    /// RegularIntervalSchedule (1) ←──── (1..*) RegularTimePoint
    /// IrregularIntervalSchedule (1) ←──── (1..*) IrregularTimePoint
    /// Switch (0..*) ←──── (0..1) SwitchingOperation
    /// 
    /// REDOSLED IMPORTA (respektuje zavisnosti):
    /// ==========================================
    /// 1. Curve (nema zavisnosti)
    /// 2. CurveData (zavisi od Curve)
    /// 3. BasicIntervalSchedule, RegularIntervalSchedule, IrregularIntervalSchedule, OutageSchedule
    /// 4. RegularTimePoint (zavisi od RegularIntervalSchedule)
    /// 5. IrregularTimePoint (zavisi od IrregularIntervalSchedule)
    /// 6. Switch (zavisi od ConductingEquipment hijerarhije)
    /// 7. SwitchingOperation (zavisi od Switch)
    /// </summary>
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

        /// <summary>
        /// Konvertuje CIM model u NMS Delta objekt
        /// Poštuje redosled zavisnosti izmedju entiteta
        /// </summary>
        private void ConvertModelAndPopulateDelta()
        {
            LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

            // FAZA 1: Entiteti bez zavisnosti (nezavisni)
            ImportCurves();

            // FAZA 2: Entiteti koji zavise od Curve
            ImportCurveDatas();

            // FAZA 3: Schedule entiteti (nasledjuju BasicIntervalSchedule)
            ImportBasicIntervalSchedules();
            ImportRegularIntervalSchedules();
            ImportIrregularIntervalSchedules();
            ImportOutageSchedules();

            // FAZA 4: TimePoint entiteti (zavise od Schedule-a)
            ImportRegularTimePoints();
            ImportIrregularTimePoints();

            // FAZA 5: ConductingEquipment hijerarhija
            ImportSwitches();

            // FAZA 6: Entiteti koji zavise od Switch
            ImportSwitchingOperations();

            LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
        }

        #region Import Methods

        /// <summary>
        /// Import Curve entiteta (FTN.Curve)
        /// Hijerarhija: IdentifiedObject → Curve
        /// </summary>
        private void ImportCurves()
        {
            SortedDictionary<string, object> cimCurves = concreteModel.GetAllObjectsOfType("FTN.Curve");
            if (cimCurves != null)
            {
                foreach (var pair in cimCurves)
                {
                    FTN.Curve cimObj = pair.Value as FTN.Curve;
                    ResourceDescription rd = ImportCurveDatas(cimObj);
                    AddInsertOperation(rd, cimObj, "Curve");
                }
                report.Report.AppendLine();
            }
        }

        /// <summary>
        /// Import CurveData entiteta (FTN.CurveData)
        /// Hijerarhija: IdentifiedObject → CurveData
        /// Zavisi od: Curve (preko reference)
        /// </summary>
        private void ImportCurveDatas()
        {
            SortedDictionary<string, object> cimCurveDatas = concreteModel.GetAllObjectsOfType("FTN.CurveData");
            if (cimCurveDatas != null)
            {
                foreach (var pair in cimCurveDatas)
                {
                    // TODO: Implementirati CreateCurveDataResourceDescription
                }
                report.Report.AppendLine();
            }
        }

        /// <summary>
        /// Import BasicIntervalSchedule entiteta (FTN.BasicIntervalSchedule)
        /// Hijerarhija: IdentifiedObject → BasicIntervalSchedule
        /// Napomena: Ovo je apstraktna klasa u CIM-u, ali možda ima konkretne instance
        /// </summary>
        private void ImportBasicIntervalSchedules()
        {
            SortedDictionary<string, object> cimSchedules = concreteModel.GetAllObjectsOfType("FTN.BasicIntervalSchedule");
            if (cimSchedules != null)
            {
                foreach (var pair in cimSchedules)
                {
                    // TODO: Ako BasicIntervalSchedule nije apstraktan u vašem modelu
                }
                report.Report.AppendLine();
            }
        }

        /// <summary>
        /// Import RegularIntervalSchedule entiteta (FTN.RegularIntervalSchedule)
        /// Hijerarhija: IdentifiedObject → BasicIntervalSchedule → RegularIntervalSchedule
        /// </summary>
        private void ImportRegularIntervalSchedules()
        {
            SortedDictionary<string, object> cimSchedules = concreteModel.GetAllObjectsOfType("FTN.RegularIntervalSchedule");
            if (cimSchedules != null)
            {
                foreach (var pair in cimSchedules)
                {
                    // TODO: Implementirati CreateRegularIntervalScheduleResourceDescription
                }
                report.Report.AppendLine();
            }
        }

        /// <summary>
        /// Import IrregularIntervalSchedule entiteta (FTN.IrregularIntervalSchedule)
        /// Hijerarhija: IdentifiedObject → BasicIntervalSchedule → IrregularIntervalSchedule
        /// </summary>
        private void ImportIrregularIntervalSchedules()
        {
            SortedDictionary<string, object> cimSchedules = concreteModel.GetAllObjectsOfType("FTN.IrregularIntervalSchedule");
            if (cimSchedules != null)
            {
                foreach (var pair in cimSchedules)
                {
                    // TODO: Implementirati CreateIrregularIntervalScheduleResourceDescription
                }
                report.Report.AppendLine();
            }
        }

        /// <summary>
        /// Import OutageSchedule entiteta (FTN.OutageSchedule)
        /// Hijerarhija: IdentifiedObject → BasicIntervalSchedule → OutageSchedule
        /// </summary>
        private void ImportOutageSchedules()
        {
            SortedDictionary<string, object> cimSchedules = concreteModel.GetAllObjectsOfType("FTN.OutageSchedule");
            if (cimSchedules != null)
            {
                foreach (var pair in cimSchedules)
                {
                    // TODO: Implementirati CreateOutageScheduleResourceDescription
                }
                report.Report.AppendLine();
            }
        }

        /// <summary>
        /// Import RegularTimePoint entiteta (FTN.RegularTimePoint)
        /// Hijerarhija: IdentifiedObject → RegularTimePoint
        /// Zavisi od: RegularIntervalSchedule (preko reference)
        /// </summary>
        private void ImportRegularTimePoints()
        {
            SortedDictionary<string, object> cimTimePoints = concreteModel.GetAllObjectsOfType("FTN.RegularTimePoint");
            if (cimTimePoints != null)
            {
                foreach (var pair in cimTimePoints)
                {
                    // TODO: Implementirati CreateRegularTimePointResourceDescription
                }
                report.Report.AppendLine();
            }
        }

        /// <summary>
        /// Import IrregularTimePoint entiteta (FTN.IrregularTimePoint)
        /// Hijerarhija: IdentifiedObject → IrregularTimePoint
        /// Zavisi od: IrregularIntervalSchedule (preko reference)
        /// </summary>
        private void ImportIrregularTimePoints()
        {
            SortedDictionary<string, object> cimTimePoints = concreteModel.GetAllObjectsOfType("FTN.IrregularTimePoint");
            if (cimTimePoints != null)
            {
                foreach (var pair in cimTimePoints)
                {
                    // TODO: Implementirati CreateIrregularTimePointResourceDescription
                }
                report.Report.AppendLine();
            }
        }

        /// <summary>
        /// Import Switch entiteta (FTN.Switch)
        /// Hijerarhija: IdentifiedObject → PowerSystemResource → Equipment → ConductingEquipment → Switch
        /// </summary>
        private void ImportSwitches()
        {
            SortedDictionary<string, object> cimSwitches = concreteModel.GetAllObjectsOfType("FTN.Switch");
            if (cimSwitches != null)
            {
                foreach (var pair in cimSwitches)
                {
                    // TODO: Implementirati CreateSwitchResourceDescription
                }
                report.Report.AppendLine();
            }
        }

        /// <summary>
        /// Import SwitchingOperation entiteta (FTN.SwitchingOperation)
        /// Hijerarhija: IdentifiedObject → SwitchingOperation
        /// Zavisi od: Switch (preko reference)
        /// </summary>
        private void ImportSwitchingOperations()
        {
            SortedDictionary<string, object> cimOperations = concreteModel.GetAllObjectsOfType("FTN.SwitchingOperation");
            if (cimOperations != null)
            {
                foreach (var pair in cimOperations)
                {
                    // TODO: Implementirati CreateSwitchingOperationResourceDescription
                }
                report.Report.AppendLine();
            }
        }


        private void AddInsertOperation(ResourceDescription rd, FTN.IDClass cimObj, string label)
        {
            if (rd != null)
            {
                delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                report.Report.Append(label).Append(" ID = ").Append(cimObj != null ? cimObj.ID : string.Empty).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
            }
            else
            {
                report.Report.Append(label).Append(" ID = ").Append(cimObj != null ? cimObj.ID : string.Empty).AppendLine(" FAILED to be converted");
            }
        }
        #endregion Import Methods

        #region Helper Methods - TODO: Implementirati u sledećem koraku

        // TODO: Sledeci korak je kreiranje converter metoda kao što su:
        // - CreateCurveResourceDescription(FTN.Curve cimCurve)
        // - CreateCurveDataResourceDescription(FTN.CurveData cimCurveData)
        // - CreateRegularIntervalScheduleResourceDescription(...)
        // - CreateIrregularIntervalScheduleResourceDescription(...)
        // - CreateOutageScheduleResourceDescription(...)
        // - CreateRegularTimePointResourceDescription(...)
        // - CreateIrregularTimePointResourceDescription(...)
        // - CreateSwitchResourceDescription(...)
        // - CreateSwitchingOperationResourceDescription(...)

        #endregion Helper Methods
    }
}
