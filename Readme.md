<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128656644/11.1.6%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/E2895)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [MainWindow.xaml](./CS/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/MainWindow.xaml.vb))
<!-- default file list end -->
# How to bind SchedulerControl to SQL Server database


<p>This example demonstrates how to bind the SchedulerControl to a database located in MS SQL Server 2005.</p><p>The basic steps to create this example are equivalent to the steps described in <a href="http://documentation.devexpress.com/#WPF/CustomDocument8653"><u>Lesson 1 - Bind a Scheduler to Data</u></a>. However, several additional steps are added to demonstrate more SchedulerControl capabilites:</p><p>- Resources for appointments<br />
Besides the appointment mappings, resource mappings are defined in the SchedulerStorage. A separate TableAdapter is used to retrieve resources from the database. </p><p>- Resource sharing<br />
The <a href="http://documentation.devexpress.com/#CoreLibraries/DevExpressXtraSchedulerAppointmentStorageBase_ResourceSharingtopic"><u>AppointmentStorageBase.ResourceSharing Property</u></a> value is set to true (note the <a href="https://www.devexpress.com/Support/Center/p/B182164">It is impossible to set ResourceSharing property to true via XAML</a> bug report).</p><p>To test this example locally, you should setup our CarsXtraScheduling sample database in your SQL Server instance (you can download the corresponding *.sql scripts from the <a href="https://www.devexpress.com/Support/Center/p/E215">How to bind ASPxScheduler to MS SQL Server database</a> example).</p>

<br/>


