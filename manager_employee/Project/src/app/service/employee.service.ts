import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DataResult, orderBy, process, SortDescriptor } from '@progress/kendo-data-query';
import { Observable, of } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Employee } from '../models/employee';



const API_URL = `${environment.apiUrl}`;

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': 'application/x-www-form-urlencoded',
    // Authorization: 'my-auth-token'
  })}

  constructor(private http: HttpClient) {
   
  }

  public getAll(): Observable<any[]> {
    return this.http.get<any[]>(API_URL + '/Employee');
  }

  public saveEmployee(employee: any): Observable<any> {
    return this.http.post<any>(API_URL + '/Employee', employee);
  }

  public findById(id: number): Observable<any> {
    return this.http.get<any>(`${API_URL}/Employee/${id}`);
  }

  public updateEmployee(id: number, employee: any): Observable<any> {
    return this.http.put<any>(`${API_URL}/Employee/${id}`, employee);
  }

  public deleteEmployee(id: number): Observable<any> {
    return this.http.delete<any>(`${API_URL}/Employee/${id}`);
  }

  public getAllEmployeesByDepartmnetId(id: number): Observable<any[]> {
    return this.http.get<any[]>(API_URL + `/EmployeeByDepartmentId/${id}`);
  }
  public getAllEmployeesTreeByDepartmnetId(id: number): Observable<any[]> {
    return this.http.get<any[]>(API_URL + `/EmployeeOfTreeByDepartmentId/${id}`);
  }

  public getAllEmployeesSearchBy(searchBy : any[]): Observable<any[]> {
    return this.http.post<any[]>(API_URL + '/searchBy', searchBy)
  }


  public getAllDepartment(): Observable<any[]> {
    return this.http.get<any[]>(API_URL + '/Department');
  }
  public getAllDepartmentTree(): Observable<any[]> {
    return this.http.get<any[]>(API_URL + '/showTree');
  }

  public getAllDepartmentTreeToSearch(keyword: any): Observable<any[]> {
    return this.http.post<any[]>(API_URL + '/showTree/searchDepartmentBy',"keywords="+keyword,this.httpOptions);
  }

  public getPageChange(pageChange : any): Observable<any>  {
    return this.http.post<any[]>(API_URL + "/pageChange",pageChange) ;
  }


}

