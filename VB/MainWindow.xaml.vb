Imports Microsoft.VisualBasic
Imports System.Windows
Imports DevExpress.XtraScheduler
Imports System.Data
Imports System.Data.SqlClient
Imports System

Namespace SchedulerCarsSQLServerWpf
	''' <summary>
	''' Interaction logic for MainWindow.xaml
	''' </summary>
	Partial Public Class MainWindow
		Inherits Window
		Private dataSet As New CarsDBDataSet()
		Private tableAdapterAppointments As New CarsDBDataSetTableAdapters.CarSchedulingTableAdapter()
		Private tableAdapterResources As New CarsDBDataSetTableAdapters.CarsTableAdapter()

		Public Sub New()
			InitializeComponent()

			schedulerControl1.Storage.AppointmentStorage.ResourceSharing = True
			schedulerControl1.ActiveViewType = DevExpress.XtraScheduler.SchedulerViewType.Week

			tableAdapterResources.Fill(dataSet.Cars)
			tableAdapterAppointments.Fill(dataSet.CarScheduling)

			schedulerControl1.Storage.ResourceStorage.DataSource = dataSet.Cars
			schedulerControl1.Storage.AppointmentStorage.DataSource = dataSet.CarScheduling

			schedulerControl1.Start = schedulerControl1.Storage.AppointmentStorage(0).Start

			AddHandler schedulerControl1.Storage.AppointmentsInserted, AddressOf Storage_AppointmentsModified
			AddHandler schedulerControl1.Storage.AppointmentsChanged, AddressOf Storage_AppointmentsModified
			AddHandler schedulerControl1.Storage.AppointmentsDeleted, AddressOf Storage_AppointmentsModified

			AddHandler tableAdapterAppointments.Adapter.RowUpdated, AddressOf Adapter_RowUpdated
		End Sub

		Private Sub Storage_AppointmentsModified(ByVal sender As Object, ByVal e As PersistentObjectsEventArgs)
			Me.tableAdapterAppointments.Adapter.Update(Me.dataSet)
			Me.dataSet.AcceptChanges()
		End Sub

		Private Sub Adapter_RowUpdated(ByVal sender As Object, ByVal e As System.Data.SqlClient.SqlRowUpdatedEventArgs)
			If e.Status = UpdateStatus.Continue AndAlso e.StatementType = StatementType.Insert Then
				Dim id As Integer = 0
				Using cmd As New SqlCommand("SELECT IDENT_CURRENT('CarScheduling')", tableAdapterAppointments.Connection)
					id = Convert.ToInt32(cmd.ExecuteScalar())
				End Using
				e.Row("ID") = id
			End If
		End Sub
	End Class
End Namespace
