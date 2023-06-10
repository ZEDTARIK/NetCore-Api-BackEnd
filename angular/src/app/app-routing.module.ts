import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { DepartmentComponent } from './department/department.component';
import { EmployeeComponent } from './employee/employee.component';

const appRoutes: Routes = [
  { path: '', component: DepartmentComponent},
  {path: 'department', redirectTo: '', component: EmployeeComponent},
  { path: 'employees', component:EmployeeComponent}
];

@NgModule({
  declarations: [],
  imports: [
    RouterModule.forRoot(appRoutes)
  ],
  exports: [
    RouterModule
  ]
})
export class AppRoutingModule { }
