import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root',
})
export class DepartmentService {
  readonly APIURL = 'http://localhost:5021/api';
  readonly PHOTOURL = 'http://localhost:5021/Photos';

  /**
   * This is a constructor function that takes in an instance of the HttpClient class as a parameter and
   * assigns it to a private property.
   * @param {HttpClient} http - The "http" parameter is an instance of the HttpClient class, which is
   * used to make HTTP requests to a server. It is typically injected into a component or service using
   * Angular's dependency injection system. The HttpClient class provides methods for making GET, POST,
   * PUT, DELETE, and other types of HTTP
   */
  constructor(private http: HttpClient) {}

  /**
   * This function returns an observable of an array of departments obtained through an HTTP GET request
   * to a specified API URL.
   * @returns An Observable of an array of objects of type `any` representing the departments fetched
   * from the API endpoint `/department`.
   */
  getDepartments(): Observable<any[]> {
    return this.http.get<any[]>(this.APIURL + '/Department');
  }

  /**
   * The function adds a department by sending a POST request to a specified API URL.
   * @param {any} department - The parameter `department` is an object that represents a department. It
   * could contain properties such as `name`, `description`, `manager`, `location`, etc. The
   * `AddDepartment` function sends a POST request to the API endpoint `/department` with the
   * `department` object as the request body
   * @returns The `AddDepartment` function is returning an HTTP POST request to the API endpoint
   * `/department` with the `department` object as the request body. The response from the API is not
   * specified in this function, so it is not clear what is being returned to the caller.
   */
  AddDepartment(department: any) {
    return this.http.post(this.APIURL + '/department', department);
  }

  /**
   * This function updates a department using an HTTP PUT request.
   * @param {any} department - The parameter `department` is an object that represents a department. It
   * is being passed as an argument to the `updateDepartment` function. The function uses the `http`
   * service to send a PUT request to the API endpoint `/department` with the `department` object as
   * the request body. This
   * @returns The `updateDepartment` function is returning an HTTP PUT request to the APIURL endpoint
   * `/department` with the `department` object as the request body. The response from the API is not
   * specified, so it is not clear what is being returned to the caller of the `updateDepartment`
   * function.
   */
  updateDepartment(department: any) {
    return this.http.put(this.APIURL + '/department', department);
  }

  /**
   * This function sends a DELETE request to the APIURL endpoint for deleting a department with the
   * given value.
   * @param {any} value - The parameter "value" is the data that needs to be sent along with the HTTP
   * DELETE request to the API endpoint. It could be an object or a value that represents the department
   * that needs to be deleted.
   * @returns The `deleteDepartment` method is returning an HTTP DELETE request to the APIURL endpoint
   * `/department` with the `value` parameter as the request body.
   */
  deleteDepartment(value: any) {
    return this.http.delete(this.APIURL + '/department', value);
  }
}
