using System.Windows;
using DevExpress.XtraScheduler;
using System.Data;
using System.Data.SqlClient;
using System;

namespace SchedulerCarsSQLServerWpf {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        private CarsDBDataSet dataSet = new CarsDBDataSet();
        private CarsDBDataSetTableAdapters.CarSchedulingTableAdapter tableAdapterAppointments = new CarsDBDataSetTableAdapters.CarSchedulingTableAdapter();
        private CarsDBDataSetTableAdapters.CarsTableAdapter tableAdapterResources = new CarsDBDataSetTableAdapters.CarsTableAdapter();
        
        public MainWindow() {
            InitializeComponent();

            schedulerControl1.Storage.Appointments.ResourceSharing = true;
            schedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Week;

            tableAdapterResources.Fill(dataSet.Cars);
            tableAdapterAppointments.Fill(dataSet.CarScheduling);

            schedulerControl1.Storage.Resources.DataSource = dataSet.Cars;
            schedulerControl1.Storage.Appointments.DataSource = dataSet.CarScheduling;

            schedulerControl1.Start = schedulerControl1.Storage.Appointments[0].Start;

            schedulerControl1.Storage.AppointmentsInserted +=
                new PersistentObjectsEventHandler(Storage_AppointmentsModified);
            schedulerControl1.Storage.AppointmentsChanged +=
                new PersistentObjectsEventHandler(Storage_AppointmentsModified);
            schedulerControl1.Storage.AppointmentsDeleted +=
                new PersistentObjectsEventHandler(Storage_AppointmentsModified);

            tableAdapterAppointments.Adapter.RowUpdated += 
                new System.Data.SqlClient.SqlRowUpdatedEventHandler(Adapter_RowUpdated);
        }

        void Storage_AppointmentsModified(object sender, PersistentObjectsEventArgs e) {
            this.tableAdapterAppointments.Adapter.Update(this.dataSet);
            this.dataSet.AcceptChanges();
        }

        void Adapter_RowUpdated(object sender, System.Data.SqlClient.SqlRowUpdatedEventArgs e) {
            if (e.Status == UpdateStatus.Continue && e.StatementType == StatementType.Insert) {
                int id = 0;
                using (SqlCommand cmd = new SqlCommand("SELECT IDENT_CURRENT('CarScheduling')", tableAdapterAppointments.Connection)) {
                    id = Convert.ToInt32(cmd.ExecuteScalar());
                }
                e.Row["ID"] = id;
            }
        }

    }
}
