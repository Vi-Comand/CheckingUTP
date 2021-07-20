using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace CheckingUTP.Models.ModelsUTP
{


    public class TableModel
    {
        public List<RowTableModel> Row { get; set; } = new List<RowTableModel>();
        public void Add(RowTableModel row)
        {
            if (row != null)
            {
                Row.Add(row);
            }
        }
    }

    public class RowTableModel
    {
        public int ID { get; set; }
        public string NameTypeTrainingLoad { get; set; }
        public float VoulmeHoursPerListener { get; set; }
        /*public float NumberHours { get; set; }*/
        public float NumberGroups { get; set; }
        public float NumberSubgroups { get; set; }
        public float NumberControlForms { get; set; }
        public float NumberListeners { get; set; }
        public float VolumeHours { get; set; }
        public string Status { get; set; }
        public int Type { get; set; }
    }
}
