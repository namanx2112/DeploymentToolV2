import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
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
import {MatMenuModule} from '@angular/material/menu';
import { SonicHomePageComponent } from './components/Sonic/sonic-home-page/sonic-home-page.component';
import { SonicDashboardComponent } from './components/Sonic/sonic-dashboard/sonic-dashboard.component';
import { NewProjectComponent } from './components/Sonic/new-project/new-project.component';
import { StoreViewComponent } from './components/Sonic/store-view/store-view.component'
import {MatDialogModule} from '@angular/material/dialog';
import { DialogControlsComponent } from './components/dialog-controls/dialog-controls.component';
import { ProjectTableComponent } from './components/Sonic/project-table/project-table.component';
import { NotesListComponent } from './components/Sonic/notes-list/notes-list.component';
import { ProjectPortfolioComponent } from './components/Sonic/project-portfolio/project-portfolio.component';
import { ImportProjectsComponent } from './components/Sonic/import-projects/import-projects.component';
import { DropzoneDirective } from './directives/dropzone.directive';
import { PosInstallationComponent } from './components/Sonic/pos-installation/pos-installation.component';
import { AudioInstallationComponent } from './components/Sonic/audio-installation/audio-installation.component';
import { MenuInstallationComponent } from './components/Sonic/menu-installation/menu-installation.component';
import { PaymentTerminalInstallationComponent } from './components/Sonic/payment-terminal-installation/payment-terminal-installation.component';
import { WorkflowsComponent } from './components/Sonic/workflows/workflows.component';
import { QuoteRequestWorkflowTemplateComponent } from './components/quote-request-workflow-template/quote-request-workflow-template.component';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { QuoteRequestTemplateListComponent } from './components/quote-request-template-list/quote-request-template-list.component';
import { FileStoreSelectionComponent } from './components/Sonic/file-store-selection/file-store-selection.component';
import {MatChipsModule} from '@angular/material/chips';
import { LoadingComponent } from './components/loading/loading.component';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { LoadingInterceptorService } from './services/loading-interceptor.service';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import { NewStoreComponent } from './components/Sonic/new-store/new-store.component';
import { SearchStoreComponent } from './components/Sonic/search-store/search-store.component';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { ProjectTemplateListComponent } from './components/Sonic/project-template-list/project-template-list.component';
import { RenderQuoteRequestComponent } from './components/Sonic/render-quote-request/render-quote-request.component';
import { PurchaseOrderTemplateListComponent } from './components/purchase-order-template-list/purchase-order-template-list.component';
import { PurchaseOrderWorkflowTemplateComponent } from './components/purchase-order-workflow-template/purchase-order-workflow-template.component';
import { POConfigQuantityFieldsComponent } from './components/poconfig-quantity-fields/poconfig-quantity-fields.component';
import { RenderPurchaseOrderComponent } from './components/Sonic/render-purchase-order/render-purchase-order.component';
import { ForFilterPipe } from './pipes/for-filter.pipe';
import { SupportPageComponent } from './components/support-page/support-page.component';
import { CKEditorModule } from 'ckeditor4-angular';
import { TableExpendableComponent } from './components/table-expendable/table-expendable.component';
import { StoreTechComponentsComponent } from './components/Sonic/store-tech-components/store-tech-components.component';

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
    ManageDropdownsComponent,
    SonicHomePageComponent,
    SonicDashboardComponent,
    NewProjectComponent,
    StoreViewComponent,
    DialogControlsComponent,
    ProjectTableComponent,
    NotesListComponent,
    ProjectPortfolioComponent,
    ImportProjectsComponent,
    DropzoneDirective,
    PosInstallationComponent,
    AudioInstallationComponent,
    MenuInstallationComponent,
    PaymentTerminalInstallationComponent,
    WorkflowsComponent,
    QuoteRequestWorkflowTemplateComponent,
    QuoteRequestTemplateListComponent,
    FileStoreSelectionComponent,
    LoadingComponent,
    NewStoreComponent,
    SearchStoreComponent,
    ProjectTemplateListComponent,
    RenderQuoteRequestComponent,
    PurchaseOrderTemplateListComponent,
    PurchaseOrderWorkflowTemplateComponent,
    POConfigQuantityFieldsComponent,
    RenderPurchaseOrderComponent,
    ForFilterPipe,
    SupportPageComponent,
    TableExpendableComponent,
    StoreTechComponentsComponent
  ],
  imports: [
    CKEditorModule,
    BrowserModule,
    HttpClientModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MatAutocompleteModule,
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
    MatSidenavModule,
    MatMenuModule,
    MatDialogModule,
    MatCheckboxModule,
    MatChipsModule,
    MatProgressSpinnerModule,
    MatDatepickerModule
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass:LoadingInterceptorService,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
