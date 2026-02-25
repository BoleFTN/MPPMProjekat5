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
            importBasicIntervalScheldue();

            importSwitchingOperation();

            importCurveData();
            importCurve();

            importIrregularTimePoint();
            importRegularTimePoint();
            importRegularIntervalScheldue();

            importIrregularIntervalScheldue();
            imporrtOutageScheldue();

            importSwitch();
            importConductingEquipment();
            importEquipment();
            importPowerSystemResource();


            LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
        }


        private void importBasicIntervalScheldue()
        {
            SortedDictionary<string, object> cimObjects = concreteModel.GetAllObjectsOfType("FTN.AssetModel");
            if (cimObjects != null)
            {
                foreach (var pair in cimObjects)
                {
                    FTN.AssetModel cimObj = pair.Value as FTN.AssetModel;
                    ResourceDescription rd = CreateAssetModelResourceDescription(cimObj);
                    AddInsertOperation(rd, cimObj, "AssetModel");
                }
                report.Report.AppendLine();
            }
        }


        private void importSwitchingOperation()
        {
        }

        private void importCurveData()
        {
        }

        private void importCurve()
        {
        }

        private void importIrregularTimePoint()
        {
        }

        private void importRegularTimePoint()
        {
        }

        private void importRegularIntervalScheldue()
        {
        }

        private void importIrregularIntervalScheldue()
        {
        }

        private void imporrtOutageScheldue()
        {
        }

        private void importSwitch()
        {
        }

        private void importConductingEquipment()
        {
        }

        private void importEquipment()
        {
        }

        private void importPowerSystemResource()
        {
        }

    }
    }
