import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { GridDataResult, PageChangeEvent, DataStateChangeEvent, DataBindingDirective, GridComponent } from '@progress/kendo-angular-grid';
import { SortDescriptor, process } from '@progress/kendo-data-query';
import { positions } from '../models/position';
import { titles } from '../models/title';
import { EmployeeService } from '../service/employee.service';
import { Employee } from '../models/employee';
import { FilterExpandSettings } from '@progress/kendo-angular-treeview';
// import * as $ from "jquery";
import { NgbActiveModal, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { App } from '../app';



declare var $: any;


@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css'],
  encapsulation: ViewEncapsulation.None,
  providers: []
})

export class EmployeeComponent implements OnInit {



  public departmentsTreeNode: any
  public selectedKeys!: any[]

  public filterExpandSettings: FilterExpandSettings = {
    maxAutoExpandResults: 4,
    // expandMatches: true,
  };

  // public expandedOnClear = "none";
  public filterSettingsDropdowntree: FilterExpandSettings = {
    expandedOnClear: "none"
  }





  public treeNodes: any[] = [];
  public treeNodesDepartment: any[] = [];

  public searchBy: any

  public employees!: any[];

  public defaultDepartment = { text: "Khối nhân sự", value: 3 };

  public positions = positions;
  public searchPosition = positions;
  public defaultPosition = { text: "Nhân viên", value: "Nhân viên" };

  public titles = titles;
  public searchTitles = titles;
  public defaultTitle = { text: "Nhân viên văn phòng", value: "Nhân viên văn phòng" };

  public dropDownDepartmentItems: any = [];
  public dataSearchDepartment: any = [];
  public defaultItem = { text: "Tổng tất cả các phòng ban", value: null };

  public state: any = {
    skip: 0,
    take: 5,
    filter: {
      logic: 'and',
      filters: []
    }
  };

  public gridItems!: GridDataResult

  public pageSize: number = 5;
  public skip: number = 0;
  public sortDescriptor: SortDescriptor[] = [];
  public filterDepartmentID: number | null = null;
  public filterTerm = "";

  public departmentIdChange: number = 3;
  public departmentIdChangeEdit: number = 3;
  public value = 3;
  public dataItem: Object = { id: 3, name: "Khối nhân sự", parentId: 0 };


  public formCreateEmployee = this.formBuildData.group({
    AvatarPath: [''],
    FirstName: [''],
    LastName: [''],
    Position: [this.defaultPosition.value,],
    Title: [this.defaultTitle.value],
    DepartmentId: []
  })
  // public formCreateEmployee: FormGroup = new FormGroup({
  //   AvatarPath: new FormControl('',Validators.required),
  //   FirstName: new FormControl(''),
  //   LastName: new FormControl(''),
  //   Position: new FormControl(''),
  //   Title: new FormControl(''),
  //   //  DepartmentId: new FormControl(''),
  // });

  public formEditEmployee = this.formBuildData.group({
    IdEmployee: [],
    AvatarPath: [''],
    FirstName: [''],
    LastName: [''],
    Position: [this.defaultPosition.value],
    Title: [this.defaultTitle.value],
    DepartmentId: ['']
  })





    ;

  constructor(private httpServer: EmployeeService, private formBuildData: FormBuilder, private modalService: NgbModal) {

  }

  public ngOnInit(): void {
    this.loadDataChange(this.filterDepartmentID)
    this.showTreeDepartment();
    this.getAllDepartment();
    this.dataSearchDepartment = this.dropDownDepartmentItems;

  }

  public showFormCreate(): void {

    $("#outputImage").attr("src", "https://png.pngtree.com/png-clipart/20210608/ourlarge/pngtree-dark-gray-simple-avatar-png-image_3418404.jpg")

    this.value = 3;
    this.dataItem = { id: 3, name: "Khối nhân sự", parentId: 0 };
    this.departmentIdChange = 3;

    this.formCreateEmployee = this.formBuildData.group({
      FirstName: [''],
      LastName: [''],
      Position: [this.defaultPosition.value],
      Title: [this.defaultTitle.value],
      AvatarPath: [''],

      //DepartmentId: [this.defaultDepartment.value]
      DepartmentId: [this.departmentIdChange]
    })
  console.log(typeof (this.formCreateEmployee.value.DepartmentId))
  }


  public createEmployee(): void {
    this.formCreateEmployee.markAllAsTouched();
    if (this.formCreateEmployee.valid) {
      const emp: Employee = {
        FirstName: this.formCreateEmployee.value.FirstName,
        LastName: this.formCreateEmployee.value.LastName,
        Position: this.formCreateEmployee.value.Position,
        Title: this.formCreateEmployee.value.Title,
        AvatarPath: $("#base64-create").val(),
        DepartmentId: this.departmentIdChange,
      }
      this.httpServer.saveEmployee(emp).subscribe((data) => {
        console.log(data)
    
          App.showSuccessAlert("Tạo nhân viên thành công !");
          this.employees=[data,...this.employees] ;
          this.gridItems = process(this.employees, this.state);

          // this.loadDataChange(this.filterDepartmentID)
          $("#base64-create").val("")
          $("#file-create").val("")
          $("#outputImage").attr("src", "")
      
      });

      // $('#createModal').modal('hide');     
      this.modalService.dismissAll();
    }
  }

  public getEmployee(event: any): void {

    this.httpServer.findById(event.dataItem.employeeId).subscribe((data) => {
      this.formEditEmployee = this.formBuildData.group({
        IdEmployee: [data.id],
        FirstName: [data.firstName],
        LastName: [data.lastName],
        Position: [data.position],
        Title: [data.title],
        AvatarPath: [data.avatarPath,],
       
        DepartmentId: [this.departmentIdChangeEdit]
      })
      this.value = data.department.id;
      this.dataItem = data.department;
      this.departmentIdChangeEdit = this.value ;

      if (data.avatarPath) {
        $("#outputImageEdit").attr("src", data.avatarPath)
      }
    });
  }

  public editEmployee(): void {
    this.formEditEmployee.markAllAsTouched();
    if (this.formEditEmployee.valid) {
      const empEdit: Employee = {
        EmployeeId: this.formEditEmployee.value.IdEmployee,
        FirstName: this.formEditEmployee.value.FirstName,
        LastName: this.formEditEmployee.value.LastName,
        Position: this.formEditEmployee.value.Position,
        Title: this.formEditEmployee.value.Title,
        AvatarPath: $("#base64-edit").val(),
        // DepartmentId: this.formEditEmployee.value.DepartmentId
        DepartmentId: this.departmentIdChangeEdit,
      }

      const id = this.formEditEmployee.value.IdEmployee

      this.httpServer.updateEmployee(id, empEdit).subscribe((data) => {
        console.log(data)
        // $('#editModal').modal('hide');
        this.loadDataChange(this.filterDepartmentID)
        // this.formEditEmployee.reset();
        $("#base64-edit").val("")
        $("#file-edit").val("")
        App.showSuccessAlert("Sửa thông tin nhân viên thành công !");
        this.modalService.dismissAll();
        // alert("Sửa thông tin nhân viên thành công !")
      });
    }
  }

  public removeHandler(event: any) {
   
      var fullname = event.dataItem.firstName + " " + event.dataItem.lastName 
    App.showDeleteConfirmDialog(fullname).then((result) => {
      if (result.isConfirmed) {

        this.httpServer.deleteEmployee(event.dataItem.employeeId).subscribe((data) => {
       
          App.showSuccessAlert("Xoá nhân viên thành công !");
          //cài lai page
          if ((this.employees.length - 1) % this.pageSize == 0) {
            this.skip = this.skip - this.pageSize;
          }
          this.state.skip = this.skip;
          this.loadDataChange(this.filterDepartmentID);
        })
    }
    })
  }





  public pageChange(event: PageChangeEvent): void {
    this.skip = event.skip;
    this.pageSize = event.take
    this.state.skip = event.skip;
    this.state.take = event.take
    this.gridItems = process(this.employees, this.state);

    //đổ data change ra được
    var pageChange = {
      DepartmentId : this.filterDepartmentID?this.filterDepartmentID:0,
      Skip : event.skip ,
      Take : event.take
    }
    console.log(pageChange) ;
   this.httpServer.getPageChange(pageChange).subscribe((data)=>{
     console.log(data) ;
    // this.gridItems = process(data, this.state);
   })
  }

  public handleSortChange(descriptor: SortDescriptor[]): void {
    this.sortDescriptor = descriptor;
    this.gridItems = process(this.employees, this.state);
   
  }

  public handleFilterChange(item: {
    text: string;
    value: number | null;
  }): void {
    this.filterDepartmentID = item.value;
    this.skip = 0;
    this.loadDataChange(this.filterDepartmentID)
  }

  public handleClickNode(event: any): void {
    this.filterDepartmentID = event.dataItem.id
    this.skip = 0;
    this.loadDataChange(this.filterDepartmentID)
  }

  public loadDataChange(id: number | null = null) {
    console.log("dang chayj")
    if (id != null) {
      this.loadDataByDepartment(id)
    }
    else {
      this.loadData()

    }
  }

  public loadData(): void {
    this.httpServer.getAll().subscribe((data) => {
      this.gridItems = process(data, this.state);
      this.employees = data;
      // this.gridItems = data;
    })
  }

  public dataStateChange(state: DataStateChangeEvent): void {
    this.state = state;
    console.log(state.filter?.filters[0])
  
    if (this.filterDepartmentID) {
      this.searchBy = { departmentId: this.filterDepartmentID, ...state.filter?.filters[0] }
    }
    else {
      this.searchBy = { departmentId: 0, ...state.filter?.filters[0] }
    }

    if (this.searchBy.value) {
      this.httpServer.getAllEmployeesSearchBy(this.searchBy).subscribe(data => {
        this.gridItems = process(data, this.state);
        // this.gridItems = data ;
      })
   } 
    else {
      this.gridItems = process(this.employees, this.state);
    //   this.loadDataChange(this.searchBy.departmentId)
    }
  }

  public loadDataByDepartment(id: number): void {
    this.httpServer.getAllEmployeesTreeByDepartmnetId(id).subscribe((data) => {
      this.gridItems = process(data, this.state);
      this.employees = data
      // this.gridItems = data;
      console.log(data)
    })
  }

  public showTreeDepartment(): void {
    this.httpServer.getAllDepartmentTree().subscribe((data) => {
      this.treeNodes = [{
        name: "Tổng công ty nhân sự Việt Nam",
        children: data
      }];
      this.treeNodesDepartment = data;
    })
  }

  public getAllDepartment() {
    this.httpServer.getAllDepartment().subscribe((data) => {
      this.dropDownDepartmentItems = data;
      this.dataSearchDepartment = data;
    })
  }

  public handleFilter(value: any) {
    console.log(value)
    this.dataSearchDepartment = this.dropDownDepartmentItems.filter(
      (s: any) => s.name.toLowerCase().indexOf(value.toLowerCase()) !== -1
    );
  }

  public handleFilterPosition(value: any) {
    console.log(value)
    this.searchPosition = this.positions.filter(
      (s: any) => s.text.toLowerCase().indexOf(value.toLowerCase()) !== -1
    );
  }

  public handleFilterTitles(value: any) {
    console.log(value)
    this.searchTitles = this.titles.filter(
      (s: any) => s.text.toLowerCase().indexOf(value.toLowerCase()) !== -1
    );
  }




  public findDepartmnetBy(event: any) {
    this.httpServer.getAllDepartmentTreeToSearch(event).subscribe(data => {
      console.log(data)
      console.log(this.filterExpandSettings)
      this.treeNodes = [{
        name: "Tổng công ty nhân sự Việt Nam",
        children: data
      }]
      //  this.filterTerm = event ;
      //  console.log(this.filterTerm) 
    })
  }



  //modal 
  //bo sung vao constro private modalService: NgbModal
  public openFormCreate(content: any) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' }).result.then((result) => {

    }, () => {
      this.showFormCreate()
    });
  }

  public openFormEdit(content: any) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title-edit' }).result
  }

  public valueChangeDepartment(event: any) {
    console.log(event)
    this.departmentIdChange = event;
  }

  public valueChangeDepartmentEdit(event: any) {
    this.departmentIdChangeEdit = event;
  }

  public refreshPage() {
    window.location.reload()
  }

}





