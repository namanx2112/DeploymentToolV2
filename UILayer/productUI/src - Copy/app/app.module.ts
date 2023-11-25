import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { LoginComponent } from './components/Auth/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { AppRoutingModule } from './app-routing.module';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {DragDropModule} from '@angular/cdk/drag-drop';
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
import {MatSortModule} from '@angular/material/sort';
import { MatPaginatorModule } from '@angular/material/paginator';
import { NewEditObjectComponent } from './components/new-edit-object/new-edit-object.component';
import { HomeDashboardComponent } from './components/home-dashboard/home-dashboard.component';
import { AdminConfigComponent } from './components/admin-config/admin-config.component';
import { NotImplementedComponent } from './components/not-implemented/not-implemented.component';
import {MatButtonToggleModule} from '@angular/material/button-toggle';
import {MatSidenavModule} from '@angular/material/sidenav';
import { AdminDashboardShortcutsComponent } from './components/admin-dashboard-shortcuts/admin-dashboard-shortcuts.component';
import { ManageDropdownsComponent } from './components/manage-dropdowns/manage-dropdowns.component';
import {MatMenuModule} from '@angular/material/menu';
import { BrandHomePageComponent } from './components/Brand/brand-home-page/brand-home-page.component';
import { BrandDashboardComponent } from './components/Brand/brand-dashboard/brand-dashboard.component';
import { NewProjectComponent } from './components/Brand/new-project/new-project.component';
import { StoreViewComponent } from './components/Brand/store-view/store-view.component'
import {MatDialogModule} from '@angular/material/dialog';
import { DialogControlsComponent } from './components/dialog-controls/dialog-controls.component';
import { ProjectTableComponent } from './components/Brand/project-table/project-table.component';
import {MatTooltipModule} from '@angular/material/tooltip';
import { NotesListComponent } from './components/Brand/notes-list/notes-list.component';
import { ProjectPortfolioComponent } from './components/Brand/project-portfolio/project-portfolio.component';
import { ImportProjectsComponent } from './components/Brand/import-projects/import-projects.component';
import { DropzoneDirective } from './directives/dropzone.directive';
import { PosInstallationComponent } from './components/Brand/pos-installation/pos-installation.component';
import { AudioInstallationComponent } from './components/Brand/audio-installation/audio-installation.component';
import { MenuInstallationComponent } from './components/Brand/menu-installation/menu-installation.component';
import { PaymentTerminalInstallationComponent } from './components/Brand/payment-terminal-installation/payment-terminal-installation.component';
import { QuoteRequestWorkflowTemplateComponent } from './components/quote-request-workflow-template/quote-request-workflow-template.component';
import {MatCheckboxModule} from '@angular/material/checkbox';
import { QuoteRequestTemplateListComponent } from './components/quote-request-template-list/quote-request-template-list.component';
import { FileStoreSelectionComponent } from './components/Brand/file-store-selection/file-store-selection.component';
import {MatChipsModule} from '@angular/material/chips';
import { LoadingComponent } from './components/loading/loading.component';
import {MatProgressSpinnerModule} from '@angular/material/progress-spinner';
import { LoadingInterceptorService } from './services/loading-interceptor.service';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import { NewStoreComponent } from './components/Brand/new-store/new-store.component';
import { SearchStoreComponent } from './components/Brand/search-store/search-store.component';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { ProjectTemplateListComponent } from './components/Brand/project-template-list/project-template-list.component';
import { RenderQuoteRequestComponent } from './components/Brand/render-quote-request/render-quote-request.component';
import { PurchaseOrderTemplateListComponent } from './components/purchase-order-template-list/purchase-order-template-list.component';
import { PurchaseOrderWorkflowTemplateComponent } from './components/purchase-order-workflow-template/purchase-order-workflow-template.component';
import { POConfigQuantityFieldsComponent } from './components/poconfig-quantity-fields/poconfig-quantity-fields.component';
import { RenderPurchaseOrderComponent } from './components/Brand/render-purchase-order/render-purchase-order.component';
import { ForFilterPipe } from './pipes/for-filter.pipe';
import { SupportPageComponent } from './components/support-page/support-page.component';
import { CKEditorModule } from 'ckeditor4-angular';
import { TableExpendableComponent } from './components/table-expendable/table-expendable.component';
import { StoreTechComponentsComponent } from './components/Brand/store-tech-components/store-tech-components.component';
import { ChangeGoliveDateComponent } from './components/Brand/change-golive-date/change-golive-date.component';
import { ChangePasswordComponent } from './components/change-password/change-password.component';
import { TicketViewComponent } from './components/ticket-view/ticket-view.component';
import { DeliveryStatusComponent } from './components/Brand/delivery-status/delivery-status.component';
import { DocumentsTabComponent } from './components/Brand/documents-tab/documents-tab.component';
import { RenderDateChangeTemplateComponent } from './components/Brand/render-date-change-template/render-date-change-template.component';
import { DateChangeRevisedPOComponent } from './components/Brand/date-change-revised-po/date-change-revised-po.component';
import { ForgetPasswordComponent } from './components/forget-password/forget-password.component';
import { ProjectPortfolioCellComponent } from './components/Brand/project-portfolio-cell/project-portfolio-cell.component';
import { ReportTableComponent } from './components/report-table/report-table.component';
import { NotificationComponent } from './components/notification/notification.component';
import { DashboardComponent } from './components/Brand/dashboard/dashboard.component';
import { DashboardChartComponent } from './components/Brand/dashboard-chart/dashboard-chart.component';
import { DashboardPaneComponent } from './components/Brand/dashboard-pane/dashboard-pane.component';
import {MatNativeDateModule} from '@angular/material/core';
import { NoItemsComponent } from './components/no-items/no-items.component';
import { AdvancedSearchComponent } from './components/Brand/advanced-search/advanced-search.component';
import { AdvancedSearchResultComponent } from './components/Brand/advanced-search-result/advanced-search-result.component';
import {MatExpansionModule} from '@angular/material/expansion';
import { SavedReportsComponent } from './components/Brand/saved-reports/saved-reports.component';
import { AuditViewComponent } from './components/Brand/audit-view/audit-view.component';
import { ProjectRolloutEditorComponent } from './components/Brand/project-rollout-editor/project-rollout-editor.component';
import { ProjectRolloutImportComponent } from './components/Brand/project-rollout-import/project-rollout-import.component';
import { RolloutProjectTableComponent } from './components/Brand/rollout-project-table/rollout-project-table.component';
import { ProjectGlimpseComponent } from './components/Brand/project-glimpse/project-glimpse.component';
import { RolloutOwnProjectsComponent } from './components/Brand/rollout-own-projects/rollout-own-projects.component';
import { SearchFieldsComponent } from './components/Brand/search-fields/search-fields.component';
import { AdvancedSearchResultTryComponent } from './components/Brand/advanced-search-result-try/advanced-search-result-try.component';
import { ExButtonComponent } from './components/ex-button/ex-button.component';
import {TableVirtualScrollModule} from 'ng-table-virtual-scroll';

@NgModule({
    declarations: [
        AppComponent,
        LoginComponent,
        HomeComponent,
        TabBodyComponent,
        ControlsComponent,
        TableComponent,
        NewEditObjectComponent,
        HomeDashboardComponent,
        AdminConfigComponent,
        NotImplementedComponent,
        AdminDashboardShortcutsComponent,
        ManageDropdownsComponent,
        BrandHomePageComponent,
        BrandDashboardComponent,
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
        StoreTechComponentsComponent,
        ChangeGoliveDateComponent,
        ChangePasswordComponent,
        TicketViewComponent,
        DeliveryStatusComponent,
        DocumentsTabComponent,
        RenderDateChangeTemplateComponent,
        DateChangeRevisedPOComponent,
        ForgetPasswordComponent,
        ProjectPortfolioCellComponent,
        ReportTableComponent,
        NotificationComponent,
        DashboardComponent,
        DashboardChartComponent,
        DashboardPaneComponent,
        NoItemsComponent,
        AdvancedSearchComponent,
        AdvancedSearchResultComponent,
        SavedReportsComponent,
        AuditViewComponent,
        ProjectRolloutEditorComponent,
        ProjectRolloutImportComponent,
        RolloutProjectTableComponent,
        ProjectGlimpseComponent,
        RolloutOwnProjectsComponent,
        SearchFieldsComponent,
        AdvancedSearchResultTryComponent,
        ExButtonComponent,
    ],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: LoadingInterceptorService,
            multi: true
        }
    ],
    bootstrap: [AppComponent],
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
        MatSortModule,
        MatPaginatorModule,
        MatSidenavModule,
        MatMenuModule,
        MatDialogModule,
        MatCheckboxModule,
        MatChipsModule,
        MatProgressSpinnerModule,
        MatDatepickerModule,
        DragDropModule,
        MatTooltipModule,
        MatButtonToggleModule,
        MatNativeDateModule,
        MatExpansionModule,
        TableVirtualScrollModule
    ]
})
export class AppModule { }
