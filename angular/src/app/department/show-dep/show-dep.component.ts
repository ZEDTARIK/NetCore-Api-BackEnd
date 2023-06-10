import { Component, OnInit } from '@angular/core';
import { DepartmentService } from 'src/app/service/department.service';

@Component({
  selector: 'app-show-dep',
  templateUrl: './show-dep.component.html',
  styleUrls: ['./show-dep.component.css']
})
export class ShowDepComponent implements OnInit {

  /* `departmentLists: any =[];` is declaring a class property called `departmentLists` and
  initializing it as an empty array. The `any` type allows the property to hold any type of value.
  This property is used to store the list of departments retrieved from the `DepartmentService` and
  display it in the template. */
  departmentLists: any =[];

 /**
  * This is a constructor function that takes in a DepartmentService as a parameter and assigns it to a
  * private property.
  * @param {DepartmentService} departmentService - The "departmentService" parameter is a dependency
  * injection of the "DepartmentService" class. It allows the current class to access the methods and
  * properties of the "DepartmentService" class, which can be used to perform operations related to
  * departments. This is a common practice in Angular applications, where services are
  */
  constructor(private departmentService: DepartmentService) {}

  /**
   * The ngOnInit function calls the ListDepartments function.
   */
  ngOnInit(): void {
    this.ListDepartments();
  }

 /**
  * This function retrieves a list of departments from a service and assigns it to a component
  * variable.
  */
  ListDepartments() {
    this.departmentService.getDepartments().subscribe((departments) => {
      this.departmentLists = departments;
    })
  }
}
