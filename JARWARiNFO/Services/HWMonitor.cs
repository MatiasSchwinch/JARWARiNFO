using JARWARiNFO.Models;
using LibreHardwareMonitor.Hardware;
using System.Collections.ObjectModel;

namespace JARWARiNFO.Services
{
    public class HWMonitor
    {
        private readonly Computer _computer;
        private readonly UpdateVisitor _updateVisitor = new();
        public IList<IHardware> Hardware => _computer.Hardware;


        public HWMonitor()
        {
            _computer = new()
            {
                IsCpuEnabled = true,
                IsGpuEnabled = true,
                IsMemoryEnabled = true,
                IsMotherboardEnabled = true,
                IsStorageEnabled = true
            };
            _computer.Open();
        }

        public IEnumerable<HWDataModel> GetHardwareData()
        {
            try
            {
                _computer.Accept(_updateVisitor);

                List<HWDataModel> _hardwareList = new();

                foreach (IHardware hardware in _computer.Hardware)
                {
                    HWDataModel _currentHardware = new()
                    {
                        Type = hardware.HardwareType.ToString(),
                        Identifier = hardware.Identifier.ToString(),
                        Name = hardware.Name,
                        Sensors = new()
                    };
                    List<Sensor> _sensorList = new();
                    _currentHardware.Sensors = _sensorList;

                    var _sensorsTypeGroup = hardware.Sensors.GroupBy(grb => grb.SensorType);

                    foreach (var sensor in _sensorsTypeGroup)
                    {
                        Sensor _newSensor = new() { Type = sensor.Key.ToString(), Values = new List<Models.SensorValue>() };

                        foreach (var subinfo in sensor)
                        {
                            _newSensor.Values.Add(new Models.SensorValue { Name = subinfo.Name, Value = float.IsNaN((float)subinfo.Value) ? null : subinfo.Value });
                        }

                        _sensorList.Add(_newSensor);
                    }

                    _hardwareList.Add(_currentHardware);
                }

                return _hardwareList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IEnumerable<HWDataModel> GetDataByHardwareType(string pHardwareType)
        {
            var info = GetHardwareData().Where(x => x.Type == pHardwareType);
            return (!info.Any() || info is null) ? null : info;
        }

        public void Close()
        {
            _computer.Close();
        }

    }

    public class UpdateVisitor : IVisitor
    {
        public void VisitComputer(IComputer computer)
        {
            computer.Traverse(this);
        }
        public void VisitHardware(IHardware hardware)
        {
            hardware.Update();
            foreach (IHardware subHardware in hardware.SubHardware) subHardware.Accept(this);
        }
        public void VisitSensor(ISensor sensor) { }
        public void VisitParameter(IParameter parameter) { }
    }
}
