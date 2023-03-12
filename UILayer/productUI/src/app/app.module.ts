import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/Auth/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { AppRoutingModule } from './app-routing.module';
import { HomeTabComponent } from './components/home-tab/home-tab.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatTabsModule} from '@angular/material/tabs';
import { TabBodyComponent } from './components/tab-body/tab-body.component';
import {MatFormFieldModule} from '@angular/material/form-field';
import { ControlsComponent } from './components/controls/controls.component';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import { MatInputModule} from '@angular/material/input';
import { MatSelectModule} from '@angular/material/select';
import {MatButtonModule} from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import { TableComponent } from './components/table/table.component';
import {MatTableModule} from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { NewEditObjectComponent } from './components/new-edit-object/new-edit-object.component';
import { HomeDashboardComponent } from './components/home-dashboard/home-dashboard.component';
import { AdminConfigComponent } from './components/admin-config/admin-config.component';
import { NotImplementedComponent } from './components/not-implemented/not-implemented.component';
import {MatSidenavModule} from '@angular/material/sidenav';
import { AdminDashboardShortcutsComponent } from './components/admin-dashboard-shortcuts/admin-dashboard-shortcuts.component';
import { ManageDropdownsComponent } from './components/manage-dropdowns/manage-dropdowns.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    HomeTabComponent,
    TabBodyComponent,
    ControlsComponent,
    TableComponent,
    NewEditObjectComponent,
    HomeDashboardComponent,
    AdminConfigComponent,
    NotImplementedComponent,
    AdminDashboardShortcutsComponent,
    ManageDropdownsComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatTabsModule,
    MatFormFieldModule,
    FormsModule,
    ReactiveFormsModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatIconModule,
    MatTableModule,
    MatPaginatorModule,
    MatSidenavModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
