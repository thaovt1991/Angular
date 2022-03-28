import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {HttpClientModule , HTTP_INTERCEPTORS } from '@angular/common/http';
import { DropDownsModule } from '@progress/kendo-angular-dropdowns';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { GridModule,PDFModule,ExcelModule   } from '@progress/kendo-angular-grid';
import { InputsModule } from "@progress/kendo-angular-inputs";

import { EmployeeComponent } from './employee/employee.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { ButtonsModule } from '@progress/kendo-angular-buttons';
import { ExcelExportModule } from '@progress/kendo-angular-excel-export';
import { PDFExportModule } from '@progress/kendo-angular-pdf-export';
import { TreeViewModule } from '@progress/kendo-angular-treeview';
import { FilterModule } from '@progress/kendo-angular-filter';
import { LabelModule } from '@progress/kendo-angular-label';
import { AfterValueChangedDirective } from './employee/after-value-changed.directive';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


// import { LoginComponent } from './login/login.component';


@NgModule({
  declarations: [
    AppComponent,
    EmployeeComponent,
    PageNotFoundComponent,
    AfterValueChangedDirective
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    DropDownsModule,
    BrowserAnimationsModule,
    GridModule, 
    ButtonsModule,
    ReactiveFormsModule,
    FormsModule,
    ExcelExportModule,
    PDFExportModule,
    TreeViewModule,
    PDFModule,
    ExcelModule,
    InputsModule,
    FilterModule,
    LabelModule,
    NgbModule 
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
